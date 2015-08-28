using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContC.presentation.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ContC.presentation.mvc.Controllers
{
    public class UserRoleController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UserRoleController()
        {
            
        }

        public UserRoleController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            var users = UserManager.Users.Select(s => new {s.UserName}).ToArray();
            var selectListUsers = users.Select(x => new SelectListItem() { Text = x.UserName, Value = x.UserName.ToString() }).ToList();
            selectListUsers.Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
            var todosPapeis = RoleManager.Roles.Select(r => r.Name).ToList();
            var model = new UserRoleViewModel
            {
                ListaDeUsuarios = selectListUsers,
                TodosOsPapeis = todosPapeis
            };
            return View(model);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]            
        public JsonResult SalvarPapeisDeUsuario(UserRoleViewModel model)
        {
            var mensagem = new MensagemViewModel();
            try
            {
                var usuario = UserManager.Users.FirstOrDefault(u => u.UserName == model.Usuario);
                if (usuario == null)
                    throw new Exception("Usuário não encontrado no banco de dados.");
                var todosPapeis = RoleManager.Roles.Select(r => r.Name).ToList();

                foreach (var role in todosPapeis)
                {
                    var checkedRole = bool.Parse(Request.Form[role].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)[0]);
                    if (checkedRole)
                        UserManager.AddToRole(usuario.Id, role);
                }
                mensagem = new MensagemViewModel { Tipo = "0", Texto = "Configuração de Papéis de usuário salvo com sucesso." };
                return Json(mensagem, "json");

            }
            catch (Exception e)
            {
                mensagem = new MensagemViewModel { Tipo = "1", Texto = e.Message };
                return Json(mensagem, "json");
            }
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult RolesPartial(string username)
        {
            var usuario = UserManager.Users.FirstOrDefault(u => u.UserName == username);
            var todosPapeis = new List<string>();
            var todosPapeisDoUsuario = new List<string>();
            if (usuario != null)
            {
                todosPapeis = RoleManager.Roles.Select(r => r.Name).ToList();
                todosPapeisDoUsuario = UserManager.GetRoles(usuario.Id).ToList();
            }
            var model = new UserRoleViewModel
            {
                Usuario = username,
                TodosOsPapeis = todosPapeis,
                ListaDePapeisDoUsuario = todosPapeisDoUsuario
            };

            return PartialView(model);
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

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                    _roleManager = null;
                }

            }

            base.Dispose(disposing);
        }

    }
}