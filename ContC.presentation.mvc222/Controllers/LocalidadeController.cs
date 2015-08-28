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
    public class LocalidadeController : Controller
    {
        [Authorize(Roles = "CONFIG, ADMIN")]
        // GET: Localidade
        public ActionResult Index()
        {
            return View(ListProvider.GetLocalidades());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial()
        {
            return PartialView("LocalidadeGridPartial", ListProvider.GetLocalidades());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<Localidade, int> updateValues)
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
            return PartialView("LocalidadeGridPartial", ListProvider.GetLocalidades());
        }
        
        private void Validar(Localidade entity)
        {
            if (string.IsNullOrEmpty(entity.Sigla))
                throw new Exception("Sigla não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Descricao))
                throw new Exception("Descrição não pode ser vazio.");
        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<Localidade, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Localidade> repository = new Repository<Localidade>(context, unitOfWork);
                var service = new LocalidadeService(repository);
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

        private void Update(Localidade entity, MVCxGridViewBatchUpdateValues<Localidade, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Localidade> repository = new Repository<Localidade>(context, unitOfWork);
                var service = new LocalidadeService(repository);
                Localidade toUpdate = service.Find(entity.Id); ;
                toUpdate.Sigla = entity.Sigla;
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

        private void Insert(Localidade entity, MVCxGridViewBatchUpdateValues<Localidade, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Localidade> repository = new Repository<Localidade>(context, unitOfWork);
                var service = new LocalidadeService(repository);
                var toInsert = new Localidade
                {
                    Sigla = entity.Sigla,
                    Descricao = entity.Descricao,
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