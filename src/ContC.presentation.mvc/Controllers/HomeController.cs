using ContC.crosscutting.Authentication.Interface;
using ContC.crosscutting.DataContracts;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IUsuarioService us, IGrupoService gs, IEmpresaService es, IGerenciadorAutenticacao gerenciadorAutenticacao)
        {
            _usuarioService = us;
            _grupoService = gs;
            _empresaService = es;
            _gerenciadorAutenticacao = gerenciadorAutenticacao;
        }

        [ContCAuthorize]
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProjectsPartial()
        {
            UsuarioSessao usuario = _gerenciadorAutenticacao.Get();
            IList<Empresa> emps = _empresaService.GetAllEmpresaByUser(usuario.Login);

            return View(emps);
        }

        public ActionResult Dashboard(int empresaId)
        {
            return View(empresaId);
        }

        private IUsuarioService _usuarioService;
        private IGrupoService _grupoService;
        private IEmpresaService _empresaService;
        private IGerenciadorAutenticacao _gerenciadorAutenticacao;
    }
}