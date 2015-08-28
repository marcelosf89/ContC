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
    public class RelacaoController : Controller
    {
        // GET: Relacao
        [Authorize(Roles = "CONFIG, ADMIN")]
        public ActionResult Index()
        {
            var model = PreencherTipoRelacaoViewModel();
            return View(model);
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial(int tipoRelacaoId)
        {
            return PartialView("RelacaoGridPartial", PreencherTipoRelacaoViewModel(tipoRelacaoId));
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<RelacaoViewModel, int> updateValues, int tipoRelacaoId)
        {            
            foreach (var entity in updateValues.Insert)
            {
                entity.TipoRelacaoId = tipoRelacaoId;
                if (updateValues.IsValid(entity))
                    Insert(entity, updateValues);
            }
            foreach (var entity in updateValues.Update)
            {
                entity.TipoRelacaoId = tipoRelacaoId;
                if (updateValues.IsValid(entity))
                    Update(entity, updateValues);
            }
            foreach (var id in updateValues.DeleteKeys)
            {
                Delete(id, updateValues);
            }
            return PartialView("RelacaoGridPartial", PreencherTipoRelacaoViewModel(tipoRelacaoId));
        }

        private void Validar(Relacao entity)
        {
            if (entity.Empresa == null || entity.Empresa.Id == 0)
                throw new Exception("Empresa não pode ser vazio.");

            if (entity.Usuario == null || entity.Usuario.Id == 0)
                throw new Exception("Usuario não pode ser vazio.");

            if (entity.TipoRelacao == null || entity.TipoRelacao.Id == 0)
                throw new Exception("Tipo de Relação não pode ser vazio.");
            
            if (entity.QtdeMaximaHorasDiarias <= 0)
                throw new Exception("Quantidade de horas diárias deve ser maior do que ZERO.");

            if (entity.QtdeMaximaHorasDiarias > 24)
                throw new Exception("Quantidade máxima de horas diárias excedeu o limite de 24 horas.");

            var decimalPart = entity.QtdeMaximaHorasDiarias - Math.Truncate(entity.QtdeMaximaHorasDiarias);
            if (decimalPart != (decimal) 0.50 && decimalPart != (decimal) 0.00)
            {
                throw new Exception("A parte decimal da quantidade máxima de horas diárias da relação deve ser .00(hora inteira), ou .50(meia-hora).");
            }

            if (ListProvider.IsSupplyCategory(entity.TipoRelacao.Id) && (string.IsNullOrEmpty(entity.RazaoSocial) || string.IsNullOrWhiteSpace(entity.RazaoSocial)) )
            {
                throw new Exception("A Relação entre um Usuário e um Fornecedor, exige que a razão social da empresa do fornecedor esteja obrigatoriamente preenchida.");
            }
            
        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<RelacaoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Relacao> repository = new Repository<Relacao>(context, unitOfWork);
                var service = new RelacaoService(repository);
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

        private void Update(RelacaoViewModel entity, MVCxGridViewBatchUpdateValues<RelacaoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Relacao> repository = new Repository<Relacao>(context, unitOfWork);
                var service = new RelacaoService(repository);
                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                var eService = new EmpresaService(eRepository);
                IRepositoryAsync<TipoRelacao> trRepository = new Repository<TipoRelacao>(context, unitOfWork);
                var trService = new TipoRelacaoService(trRepository);
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                var uService = new UsuarioService(uRepository);

                Relacao toUpdate = service.Find(entity.Id);
                toUpdate.Empresa = eService.Find(entity.EmpresaId);
                toUpdate.Usuario = uService.Find(entity.UsuarioId);
                toUpdate.TipoRelacao = trService.Find(entity.TipoRelacaoId);
                toUpdate.QtdeMaximaHorasDiarias = entity.QtdeMaximaHorasDiarias;
                toUpdate.RazaoSocial = entity.RazaoSocialFornecedor;
                toUpdate.ObjectState = ObjectState.Modified;
                try
                {                    
                    unitOfWork.BeginTransaction();
                    Validar(toUpdate);
                    service.Update(toUpdate);
                    unitOfWork.SaveChanges();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    updateValues.SetErrorText(entity, e.Message);
                }
            }
        }

        private void Insert(RelacaoViewModel entity, MVCxGridViewBatchUpdateValues<RelacaoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Relacao> repository = new Repository<Relacao>(context, unitOfWork);
                var service = new RelacaoService(repository);
                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                var eService = new EmpresaService(eRepository);
                IRepositoryAsync<TipoRelacao> trRepository = new Repository<TipoRelacao>(context, unitOfWork);
                var trService = new TipoRelacaoService(trRepository);
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                var uService = new UsuarioService(uRepository);
                var toInsert = new Relacao
                {
                    TipoRelacao = trService.Find(entity.TipoRelacaoId),
                    Empresa = eService.Find(entity.EmpresaId),
                    Usuario = uService.Find(entity.UsuarioId),
                    QtdeMaximaHorasDiarias = entity.QtdeMaximaHorasDiarias,
                    RazaoSocial = entity.RazaoSocialFornecedor,
                    ObjectState = ObjectState.Added                    
                };
                try
                {
                    unitOfWork.BeginTransaction();
                    Validar(toInsert);
                    service.Insert(toInsert);
                    unitOfWork.SaveChanges();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    updateValues.SetErrorText(entity, e.Message);
                }
            }
        }
        
        private static TipoRelacaoViewModel PreencherTipoRelacaoViewModel()
        {
            var model = new TipoRelacaoViewModel();
            IEnumerable<SelectListItem> tiposRelacao =
                ListProvider.GetTiposRelacao()
                    .Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() })
                    .ToList();
            ((List<SelectListItem>)tiposRelacao).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
            model.TiposRelacao = tiposRelacao;
            model.Relacoes = new List<RelacaoViewModel>();
            return model;
        }

        private static TipoRelacaoViewModel PreencherTipoRelacaoViewModel(int tipoRelacaoId)
        {
            var model = new TipoRelacaoViewModel { TipoRelacaoId = tipoRelacaoId.ToString() };
            IEnumerable<SelectListItem> tiposRelacao =
                ListProvider.GetTiposRelacao()
                    .Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() })
                    .ToList();
            ((List<SelectListItem>)tiposRelacao).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
            model.TiposRelacao = tiposRelacao;
            model.Relacoes = ListProvider.GetRelacoesViewModelPorTipo(tipoRelacaoId);
            return model;
        }


    }
}