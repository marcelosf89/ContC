using System;
using System.Collections.Generic;
using System.Data.Entity.Core.ObjeContC;
using System.Linq;
using System.Web.Mvc;
using ContC.domain.entities.Context;
using ContC.domain.entities.Models;
using ContC.domain.services;
using DevExpress.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace ContC.presentation.mvc.Controllers
{
    public class CompetenciaController : Controller
    {
        // GET: Competencia
        [Authorize(Roles = "CONFIG, ADMIN")]
        public ActionResult Index()
        {
            return View(ListProvider.GetCompetencias());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial()
        {
            return PartialView("CompetenciaGridPartial", ListProvider.GetCompetencias());
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<Competencia, int> updateValues)
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

            return PartialView("CompetenciaGridPartial", ListProvider.GetCompetencias());
        }
        
        private void ValidarCompetencia(Competencia entity)
        {
            if (string.IsNullOrEmpty(entity.MesAno))
                throw new Exception("Mês/Ano da competência não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Descricao))
                throw new Exception("Descrição da competência não pode ser vazio.");

            if (entity.DataInicial == null)
                throw new Exception("Data Inicial da competência não pode ser vazia.");

            if (entity.DataFinal == null)
                throw new Exception("Data Final da competência não pode ser vazia.");

            if (entity.DataInicial > entity.DataFinal)
                throw new Exception("Data Inicial não pode ser maior que a Data Final.");

        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<Competencia, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Competencia> competenciaRepository = new Repository<Competencia>(context, unitOfWork);
                var competenciaService = new CompetenciaService(competenciaRepository);
                try
                {
                    unitOfWork.BeginTransaction();
                    competenciaService.Delete(id);
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

        private void Update(Competencia entity, MVCxGridViewBatchUpdateValues<Competencia, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                ((DbContext) context).Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                IRepositoryAsync<Competencia> competenciaRepository = new Repository<Competencia>(context, unitOfWork);
                var competenciaService = new CompetenciaService(competenciaRepository);

                Competencia competencia = competenciaService.Find(entity.Id); ;

                competencia.MesAno = entity.MesAno;
                competencia.Descricao = entity.Descricao;
                competencia.DataInicial = entity.DataInicial;
                competencia.DataFinal = entity.DataFinal;
                competencia.Status = entity.Status;
                competencia.ObjectState = ObjectState.Modified;
                try
                {                    
                    unitOfWork.BeginTransaction();
                    ValidarCompetencia(competencia);
                    competenciaService.Update(competencia);
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

        private void Insert(Competencia entity, MVCxGridViewBatchUpdateValues<Competencia, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Competencia> competenciaRepository = new Repository<Competencia>(context, unitOfWork);
                var competenciaService = new CompetenciaService(competenciaRepository);
                var competencia = new Competencia
                {
                    MesAno = entity.MesAno,
                    Descricao = entity.Descricao,
                    DataInicial = entity.DataInicial,
                    DataFinal = entity.DataFinal,
                    Status = entity.Status,
                    ObjectState = ObjectState.Added                    
                };
                try
                {
                    unitOfWork.BeginTransaction();
                    ValidarCompetencia(competencia);
                    competenciaService.Insert(competencia);
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