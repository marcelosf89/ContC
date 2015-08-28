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
    public class FeriadoController : Controller
    {
        // GET: Relacao
        [Authorize(Roles = "CONFIG, ADMIN")]
        public ActionResult Index()
        {
            var model = PreencherLocalidadeViewModel();
            return View(model);
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial(int localidadeId)
        {
            return PartialView("FeriadoGridPartial", PreencherLocalidadeViewModel(localidadeId));
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<FeriadoViewModel, int> updateValues, int localidadeId)
        {            
            foreach (var entity in updateValues.Insert)
            {
                entity.LocalidadeId = localidadeId;
                if (updateValues.IsValid(entity))
                    Insert(entity, updateValues);
            }
            foreach (var entity in updateValues.Update)
            {
                entity.LocalidadeId = localidadeId;
                if (updateValues.IsValid(entity))
                    Update(entity, updateValues);
            }
            foreach (var id in updateValues.DeleteKeys)
            {
                Delete(id, updateValues);
            }
            return PartialView("FeriadoGridPartial", PreencherLocalidadeViewModel(localidadeId));
        }

        private void Validar(Feriado entity)
        {
            if (entity.Competencia == null || entity.Competencia.Id == 0)
                throw new Exception("Competência não pode ser vazio.");

            if (entity.Localidade == null || entity.Localidade.Id == 0)
                throw new Exception("Localidade não pode ser vazio.");

            if (entity.Data == null)
                throw new Exception("Data do feriado pode ser vazio.");
            
            if (string.IsNullOrEmpty(entity.Descricao))
                throw new Exception("Descrição do feriado não pode ser vazio.");

        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<FeriadoViewModel, int> updateValues)
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

        private void Update(FeriadoViewModel entity, MVCxGridViewBatchUpdateValues<FeriadoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Feriado> repository = new Repository<Feriado>(context, unitOfWork);
                var service = new FeriadoService(repository);
                IRepositoryAsync<Localidade> lRepository = new Repository<Localidade>(context, unitOfWork);
                var lService = new LocalidadeService(lRepository);
                IRepositoryAsync<Competencia> cRepository = new Repository<Competencia>(context, unitOfWork);
                var cService = new CompetenciaService(cRepository);

                Feriado toUpdate = service.Find(entity.Id);
                toUpdate.Competencia = cService.Find(entity.CompetenciaId);
                toUpdate.Localidade = lService.Find(entity.LocalidadeId);
                toUpdate.Data = entity.Data;
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

        private void Insert(FeriadoViewModel entity, MVCxGridViewBatchUpdateValues<FeriadoViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Feriado> repository = new Repository<Feriado>(context, unitOfWork);
                var service = new FeriadoService(repository);
                IRepositoryAsync<Localidade> lRepository = new Repository<Localidade>(context, unitOfWork);
                var lService = new LocalidadeService(lRepository);
                IRepositoryAsync<Competencia> cRepository = new Repository<Competencia>(context, unitOfWork);
                var cService = new CompetenciaService(cRepository);
                var toInsert = new Feriado
                {
                    Competencia = cService.Find(entity.CompetenciaId),
                    Localidade = lService.Find(entity.LocalidadeId),
                    Data = entity.Data,
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
        
        private static LocalidadeViewModel PreencherLocalidadeViewModel()
        {
            var model = new LocalidadeViewModel();
            IEnumerable<SelectListItem> localidades =
                ListProvider.GetLocalidades()
                    .Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() })
                    .ToList();
            ((List<SelectListItem>)localidades).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
            model.Localidades = localidades;
            model.Feriados = new List<FeriadoViewModel>();
            return model;
        }

        private static LocalidadeViewModel PreencherLocalidadeViewModel(int localidadeId)
        {
            var model = new LocalidadeViewModel();
            IEnumerable<SelectListItem> localidades =
                ListProvider.GetLocalidades()
                    .Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() })
                    .ToList();
            ((List<SelectListItem>)localidades).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
            model.LocalidadeId = localidadeId.ToString();
            model.Localidades = localidades;
            model.Feriados = ListProvider.GetFeriadosViewModelPorLocalidade(localidadeId);
            return model;
        }


    }
}