using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Models.NotasModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class NotasController : Controller
    {
        private INotaService _iNotasServices;
        public NotasController(INotaService iNotasServices)
        {
            _iNotasServices = iNotasServices;

        }

        // GET: Listas
        public ActionResult Index(int empresaId)
        {
            return View(empresaId);
        }

        public ActionResult NotasAtivas(int empresaId)
        {
            IList<Nota> notas = _iNotasServices.GetNotasByEmpresaUsuario(empresaId, User.Identity.Name);
            return View(notas);
        }

        public ActionResult ItensNota(int notaId)
        {
            IList<NotaItem> itens = _iNotasServices.GetItensByNotas(notaId);
            return View(itens);
        }

        public ActionResult Novo()
        {
            NotasNovoModel lm = new NotasNovoModel();
            return View(lm);
        }
    }
}