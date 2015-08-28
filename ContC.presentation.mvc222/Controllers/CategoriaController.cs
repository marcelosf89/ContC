using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ContC.domain.entities.Context;
using ContC.domain.entities.Models;
using ContC.domain.services;
using ContC.presentation.mvc.Models;
using DevExpress.Web.Mvc;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace ContC.presentation.mvc.Controllers
{
    public class CategoriaController : Controller
    {
        [Authorize(Roles = "CONFIG, ADMIN")]
        // GET: Categoria
        public ActionResult Index()
        {
            var model = PreencherModelo(0);
            return View(model);
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial(int empresaId)
        {
            return PartialView("CategoriaGridPartial", PreencherModelo(empresaId));
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<CategoriaViewModel, int> updateValues, int empresaId)
        {
            foreach (var entity in updateValues.Insert)
            {
                entity.EmpresaId = empresaId;
                if (updateValues.IsValid(entity))
                    Insert(entity, updateValues);
            }
            foreach (var entity in updateValues.Update)
            {
                entity.EmpresaId = empresaId;
                if (updateValues.IsValid(entity))
                    Update(entity, updateValues);
            }
            foreach (var id in updateValues.DeleteKeys)
            {
                Delete(id, updateValues);
            }
            return PartialView("CategoriaGridPartial", PreencherModelo(empresaId));
        }

        private void Validar(Categoria entity)
        {
            if (entity.TipoCategoria == null || entity.TipoCategoria.Id == 0)
                throw new Exception("Tipo de categoria não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Descricao))
                throw new Exception("Descrição não pode ser vazio.");
        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<CategoriaViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Categoria> repository = new Repository<Categoria>(context, unitOfWork);
                var service = new CategoriaService(repository);
                try
                {
                    unitOfWork.BeginTransaction();
                    service.Delete(id);
                    unitOfWork.SaveChanges();
                    unitOfWork.Commit();
                }            
                catch(Exception e)
                {
                    unitOfWork.Rollback();
                    updateValues.SetErrorText(id, e.Message);
                }
            }
        }

        private void Update(CategoriaViewModel entity, MVCxGridViewBatchUpdateValues<CategoriaViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Categoria> repository = new Repository<Categoria>(context, unitOfWork);
                var service = new CategoriaService(repository);
                IRepositoryAsync<TipoCategoria> tpRepository = new Repository<TipoCategoria>(context, unitOfWork);
                var tpService = new TipoCategoriaService(tpRepository);
                IRepositoryAsync<TipoRelacao> trRepository = new Repository<TipoRelacao>(context, unitOfWork);
                TipoRelacaoService trService = new TipoRelacaoService(trRepository);
                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);

                Categoria toUpdate = service.Find(entity.Id);
                toUpdate.Empresa = eService.Find(entity.EmpresaId);
                toUpdate.TipoCategoria = tpService.Find(entity.TipoCategoriaId);
                toUpdate.Descricao = entity.Descricao;
                toUpdate.TiposRelacao.Clear(); //Limpa a table N-N
                toUpdate.ObjectState = ObjectState.Modified;
                try
                {                    

                    unitOfWork.BeginTransaction();
                    Validar(toUpdate);
                    service.Update(toUpdate);
                    unitOfWork.SaveChanges(); //Salva a alteração sem a associação N-N com Tipo de Relação
                    
                    //Atualiza a relação N-N de acordo com o informado na tela
                    toUpdate.TiposRelacao = !string.IsNullOrEmpty(entity.TiposRelacaoIds) ? ListProvider.GetTiposRelacaoFromListaDeIds(entity.TiposRelacaoIds, trService.Query().Select().ToList()) : null;
                    service.Update(toUpdate);
                    unitOfWork.SaveChanges(); //Salva a alteração com a nova associação N-N com Tipo de Relação

                    unitOfWork.Commit();

                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    updateValues.SetErrorText(entity, e.Message);
                }
            }
        }

        private void Insert(CategoriaViewModel entity, MVCxGridViewBatchUpdateValues<CategoriaViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Categoria> repository = new Repository<Categoria>(context, unitOfWork);
                var service = new CategoriaService(repository);
                IRepositoryAsync<TipoCategoria> tpRepository = new Repository<TipoCategoria>(context, unitOfWork);
                var tpService = new TipoCategoriaService(tpRepository);
                IRepositoryAsync<TipoRelacao> trRepository = new Repository<TipoRelacao>(context, unitOfWork);
                TipoRelacaoService trService = new TipoRelacaoService(trRepository);
                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);
                
                var toInsert = new Categoria
                {
                    Empresa = eService.Find(entity.EmpresaId),
                    TipoCategoria = tpService.Find(entity.TipoCategoriaId),
                    Descricao = entity.Descricao,
                    ObjectState = ObjectState.Added                    
                };
                try
                {
                    unitOfWork.BeginTransaction();
                    Validar(toInsert);
                    service.Insert(toInsert);
                    unitOfWork.SaveChanges(); //Salva a inclusão sem a associação N-N com Tipo de Relação
                    
                    //Atualiza a relação N-N de acordo com o informado na tela
                    toInsert.TiposRelacao = !string.IsNullOrEmpty(entity.TiposRelacaoIds)
                        ? ListProvider.GetTiposRelacaoFromListaDeIds(entity.TiposRelacaoIds, trService.Query().Select().ToList())
                        : null;
                    service.Update(toInsert);
                    unitOfWork.SaveChanges(); //Salva a inclusão com a associação N-N com Tipo de Relação

                    unitOfWork.Commit();

                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    updateValues.SetErrorText(entity, e.Message);
                }
            }
        }

        private static EmpresaCategoriaViewModel PreencherModelo(int empresaId)
        {
            var model = new EmpresaCategoriaViewModel { EmpresaId = empresaId.ToString() };
            IEnumerable<SelectListItem> empresas =
                ListProvider.GetEmpresas()
                    .Select(x => new SelectListItem() { Text = x.RazaoSocial, Value = x.Id.ToString() })
                    .ToList();
            ((List<SelectListItem>)empresas).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
            model.Empresas = new SelectList(empresas, "Value", "Text", "0");
            model.Categorias = ListProvider.GetCategoriasViewModelPorEmpresa(empresaId);
            return model;
        }

    }
}