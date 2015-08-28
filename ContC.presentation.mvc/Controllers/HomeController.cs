using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class HomeController : Controller
    {
        IUsuarioService _us;
        IGrupoService _gs;
        IEmpresaService _es;
        public HomeController(IUsuarioService us, IGrupoService gs, IEmpresaService es)
        {
            _us = us;
            _gs = gs;
            _es = es;
        }

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
            IList<Empresa> emps = _es.GetAllEmpresaByUser(User.Identity.Name);

            return View(emps);
        }
    }
}