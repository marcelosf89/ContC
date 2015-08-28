using System;

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
    public class CentroDeCustoController : Controller
    {
        // GET: CentroDeCusto
        [Authorize(Roles = "CONFIG, ADMIN")]
        public ActionResult Index()
        {
            return View(ListProvider.GetCentroDeCustosViewModel());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial()
        {
            return PartialView("CentroDeCustoGridPartial", ListProvider.GetCentroDeCustosViewModel());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<CentroDeCustoViewModel, int> updateValues)
        {
            foreach (var entity in updateValues.Insert)
            {
                if (updateValues.IsValid(entity))
                    Insert(entity, updateValues);
            }
            foreach (var entity in updateValues.Update)
            {
                if (updateValues.IsValid(entity))
                    Update(entity, updateValues);
            }
            foreach (var id in updateValues.DeleteKeys)
            {
                Delete(id, updateValues);
            }
            return PartialView("CentroDeCustoGridPartial", ListProvider.GetCentroDeCustosViewModel());
        }

        private void Validar(CentroDeCusto entity)
        {
            if (entity.Empresa == null || entity.Empresa.Id == 0)
                throw new Exception("Empresa não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Sigla))
                throw new Exception("Sigla não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Descricao))
                throw new Exception("Descrição não pode ser vazio.");
            
            if (string.IsNullOrEmpty(entity.Tipo))
                throw new Exception("Tipo não pode ser vazio.");
        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<CentroDeCustoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<CentroDeCusto> repository = new Repository<CentroDeCusto>(context, unitOfWork);
                var service = new CentroDeCustoService(repository);
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

        private void Update(CentroDeCustoViewModel entity, MVCxGridViewBatchUpdateValues<CentroDeCustoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<CentroDeCusto> repository = new Repository<CentroDeCusto>(context, unitOfWork);
                var service = new CentroDeCustoService(repository);

                IRepositoryAsync<Empresa> tpRepository = new Repository<Empresa>(context, unitOfWork);
                var tpService = new EmpresaService(tpRepository);

                CentroDeCusto toUpdate = service.Find(entity.Id); ;
                toUpdate.Empresa = tpService.Find(entity.EmpresaId);
                toUpdate.Sigla = entity.Sigla;
                toUpdate.Tipo = entity.Tipo;
                toUpdate.Situacao = entity.Situacao;
                toUpdate.Descricao = entity.Descricao;
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

        private void Insert(CentroDeCustoViewModel entity, MVCxGridViewBatchUpdateValues<CentroDeCustoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<CentroDeCusto> repository = new Repository<CentroDeCusto>(context, unitOfWork);
                var service = new CentroDeCustoService(repository);

                IRepositoryAsync<Empresa> tpRepository = new Repository<Empresa>(context, unitOfWork);
                var tpService = new EmpresaService(tpRepository);

                var toInsert = new CentroDeCusto
                {
                    Empresa = tpService.Find(entity.EmpresaId),
                    Sigla = entity.Sigla,
                    Descricao = entity.Descricao,
                    Tipo = entity.Tipo,
                    Situacao = entity.Situacao,
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

    }
}