using ContC.presentation.mvc.Models.CompraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class ComprasController : Controller
    {
        // GET: Compras
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Novo(int empresaId)
        {
            CompraNovoModel cnm = new CompraNovoModel();
            cnm.EmpresaId = empresaId;

            return View(cnm);
        }

    }
}