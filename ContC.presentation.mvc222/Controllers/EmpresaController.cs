using System;
using System.Collections.Generic;
using System.Data.Entity.Core.ObjeContC;
using System.Linq;
using System.Web.Mvc;
using ContC.domain.entities.Context;
using ContC.domain.entities.Models;
using ContC.domain.services;
using DevExpress.Web.Mvc;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace ContC.presentation.mvc.Controllers
{
    public class EmpresaController : Controller
    {
        [Authorize(Roles = "CONFIG, ADMIN")]
        // GET: Empresa
        public ActionResult Index()
        {
            return View(ListProvider.GetEmpresas());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial()
        {
            return PartialView("EmpresaGridPartial", ListProvider.GetEmpresas());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<Empresa, int> updateValues)
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
            return PartialView("EmpresaGridPartial", ListProvider.GetEmpresas());
        }
        
        private void Validar(Empresa entity)
        {
            if (string.IsNullOrEmpty(entity.RazaoSocial))
                throw new Exception("Razão Social não pode ser vazio.");
        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<Empresa, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Empresa> repository = new Repository<Empresa>(context, unitOfWork);
                var service = new EmpresaService(repository);
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

        private void Update(Empresa entity, MVCxGridViewBatchUpdateValues<Empresa, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Empresa> repository = new Repository<Empresa>(context, unitOfWork);
                var service = new EmpresaService(repository);
                Empresa toUpdate = service.Find(entity.Id); ;
                toUpdate.RazaoSocial = entity.RazaoSocial;
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

        private void Insert(Empresa entity, MVCxGridViewBatchUpdateValues<Empresa, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Empresa> repository = new Repository<Empresa>(context, unitOfWork);
                var service = new EmpresaService(repository);
                var toInsert = new Empresa
                {
                    RazaoSocial = entity.RazaoSocial,
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