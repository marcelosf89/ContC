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
    public class TipoCategoriaController : Controller
    {
        [Authorize(Roles = "CONFIG, ADMIN")]
        // GET: TipoCategoria
        public ActionResult Index()
        {
            return View(ListProvider.GetTiposCategoria());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial()
        {
            return PartialView("TipoCategoriaGridPartial", ListProvider.GetTiposCategoria());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<TipoCategoria, int> updateValues)
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
            return PartialView("TipoCategoriaGridPartial", ListProvider.GetTiposCategoria());
        }
        
        private void Validar(TipoCategoria entity)
        {
            if (string.IsNullOrEmpty(entity.Sigla))
                throw new Exception("Sigla não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Descricao))
                throw new Exception("Descrição não pode ser vazio.");
        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<TipoCategoria, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<TipoCategoria> repository = new Repository<TipoCategoria>(context, unitOfWork);
                var service = new TipoCategoriaService(repository);
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

        private void Update(TipoCategoria entity, MVCxGridViewBatchUpdateValues<TipoCategoria, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<TipoCategoria> repository = new Repository<TipoCategoria>(context, unitOfWork);
                var service = new TipoCategoriaService(repository);
                TipoCategoria toUpdate = service.Find(entity.Id); ;
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

        private void Insert(TipoCategoria entity, MVCxGridViewBatchUpdateValues<TipoCategoria, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<TipoCategoria> repository = new Repository<TipoCategoria>(context, unitOfWork);
                var service = new TipoCategoriaService(repository);
                var toInsert = new TipoCategoria
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