using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class GrupoController : Controller
    {
        private IGrupoService _grupoService;
        private IUsuarioService _usuarioService;

        public GrupoController(IGrupoService grupoService, IUsuarioService usuarioService)
        {
            _grupoService = grupoService;
            _usuarioService = usuarioService;

        }
        // GET: Grupo
        public ActionResult Index()
        {

            return View(_grupoService.GetAllGrupo(User.Identity.Name));
        }


        // GET: Grupo
        public ActionResult Novo()
        {
            return View(new Grupo());
        }


        public ActionResult Salvar(Grupo g)
        {
            g.Responsavel = _usuarioService.GetUsuarioFetchFuncionario(User.Identity.Name).Funcionario;
            _grupoService.Insert(g);

            return View("Index", _grupoService.GetAllGrupo(g.Responsavel));
        }
    }
}