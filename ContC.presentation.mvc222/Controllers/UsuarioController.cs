using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContC.domain.entities.Context;
using ContC.domain.entities.Models;
using ContC.domain.services;
using ContC.presentation.mvc.Models;
using DevExpress.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace ContC.presentation.mvc.Controllers
{
    public class UsuarioController : Controller
    {
        public UsuarioController()
        {

        }

        public UsuarioController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Usuario
        [Authorize(Roles = "CONFIG, ADMIN")]
        public ActionResult Index()
        {
            return View(ListProvider.GetUsuariosViewModel(UserManager.Users.ToList()));
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingPartial()
        {
            return PartialView("UsuarioGridPartial", ListProvider.GetUsuariosViewModel(UserManager.Users.ToList()));
        }

        [Authorize(Roles = "CONFIG, ADMIN")]
        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<UsuarioViewModel, int> updateValues)
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
            return PartialView("UsuarioGridPartial", ListProvider.GetUsuariosViewModel(UserManager.Users.ToList()));
        }

        private void Validar(Usuario entity)
        {
            if (entity.Lider == null || entity.Lider.Id == 0)
                throw new Exception("Líder não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Nome))
                throw new Exception("Nome não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Sigla))
                throw new Exception("Sigla não pode ser vazio.");

            if (string.IsNullOrEmpty(entity.Email))
                throw new Exception("Email não pode ser vazio.");

            if (entity.DataInicio == null)
                throw new Exception("Data de início de atividades do usuário deve ser informado.");

            if (entity.DataTermino != null && entity.DataTermino < entity.DataInicio)
                throw new Exception("Data de término de atividades do usuário deve ser maior do que a Data de Início.");

        }

        private void Delete(int id, MVCxGridViewBatchUpdateValues<UsuarioViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Usuario> repository = new Repository<Usuario>(context, unitOfWork);
                var service = new UsuarioService(repository);
                try
                {
                    unitOfWork.BeginTransaction();
                    service.Delete(id);
                    unitOfWork.SaveChanges();
                    unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    updateValues.SetErrorText(id, e.Message);
                }
            }
        }

        private void Update(UsuarioViewModel entity, MVCxGridViewBatchUpdateValues<UsuarioViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Usuario> repository = new Repository<Usuario>(context, unitOfWork);
                var service = new UsuarioService(repository);

                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                var uService = new UsuarioService(uRepository);

                Usuario toUpdate = service.Find(entity.Id); ;
                toUpdate.Nome = entity.Nome;
                toUpdate.Sigla = entity.Sigla;
                toUpdate.Email = entity.Email;
                toUpdate.Situacao = entity.DataTermino == null;
                toUpdate.DataInicio = entity.DataInicio;
                toUpdate.DataTermino = entity.DataTermino;
                toUpdate.Lider = uService.Find(entity.LiderId);
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

        private void Insert(UsuarioViewModel entity, MVCxGridViewBatchUpdateValues<UsuarioViewModel, int> updateValues)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Usuario> repository = new Repository<Usuario>(context, unitOfWork);
                var service = new UsuarioService(repository);
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                var uService = new UsuarioService(uRepository);

                var toInsert = new Usuario
                {
                    Nome = entity.Nome,
                    Sigla = entity.Sigla,
                    Email = entity.Email,
                    Situacao = entity.DataTermino == null,
                    DataInicio = entity.DataInicio,
                    DataTermino = entity.DataTermino,
                    Lider = uService.Find(entity.LiderId),
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }

            base.Dispose(disposing);
        }
        

    }
}