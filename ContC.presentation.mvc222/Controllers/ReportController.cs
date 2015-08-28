using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using ContC.crosscutting.utilities;
using ContC.domain.entities.Context;
using ContC.domain.entities.Models;
using ContC.domain.services;
using ContC.presentation.mvc.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace ContC.presentation.mvc.Controllers
{
    public class ReportController : Controller
    {
        // GET: ApontamentoAnalitico
        [Authorize(Roles = "REPORTS")]
        public ActionResult ApontamentoAnalitico()
        {
            return View("ReportApontamentoAnalitico", PreencheApontamentoAnaliticoViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "REPORTS")]
        public ActionResult ApontamentoAnalitico(ApontamentoAnaliticoViewModel model)
        {
            var arquivo = Server.MapPath("~/Content/Reports/Apontamentos/crApontamentoAnalitico.rpt");

            if (!ModelState.IsValid)
                return View("ReportApontamentoAnalitico", PreencheApontamentoAnaliticoViewModel());
            var rptH = new ReportClass
            {
                FileName = arquivo
            };
            rptH.Load();
            rptH.SetDataSource(DataBaseProvider.GetApontamentoAnalitico(Convert.ToInt32(model.EmpresaId), Convert.ToInt32(model.UsuarioId), Convert.ToInt32(model.CompetenciaId)));
            var stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", "RelatorioAnalitico.pdf");
        }
        [Authorize(Roles = "REPORTS")]
        public ActionResult CompetenciaPartial(int empresaId)
        {
            CompetenciaSelectionViewModel model = new CompetenciaSelectionViewModel();
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);

                IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                ApontamentoService apService = new ApontamentoService(apRepository);
                var listaDeCompetencias = apService.ObterCompetenciasApontadosPorEmpresa(eService.Find(empresaId));
                var q = listaDeCompetencias.OrderByDescending(o => o.DataFinal);

                IEnumerable<SelectListItem> competencias = q.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)competencias).Insert(0,
                    new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                model.Competencias = competencias;

            }

            return PartialView("CompetenciaPartial", model);

        }
        [Authorize(Roles = "REPORTS")]
        public ActionResult UsuarioPartial(int empresaId, int competenciaId)
        {
            UsuarioSelectionViewModel model = new UsuarioSelectionViewModel();
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);

                IRepositoryAsync<Competencia> compRepository = new Repository<Competencia>(context, unitOfWork);
                CompetenciaService compService = new CompetenciaService(compRepository);

                IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                ApontamentoService apService = new ApontamentoService(apRepository);
                var listaDeUsuarios = apService.ObterUsuariosApontadosPorEmpresaCompetencia(eService.Find(empresaId), compService.Find(competenciaId));
                var q = listaDeUsuarios.OrderBy(o => o.Nome);

                IEnumerable<SelectListItem> usuarios = q.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)usuarios).Insert(0,
                    new SelectListItem { Text = "<< Todos >>", Value = "0" });
                model.Usuarios = usuarios;

            }

            return PartialView("UsuarioPartial", model);

        }
        [Authorize(Roles = "REPORTS")]
        public ActionResult ApontamentoSintetico()
        {
            return View("ReportApontamentoSintetico", PreencheApontamentoSinteticoViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "REPORTS")]
        public ActionResult ApontamentoSintetico(ApontamentoSinteticoViewModel model)
        {
            var arquivo = Server.MapPath("~/Content/Reports/Apontamentos/crApontamentoSintetico.rpt");

            if (!ModelState.IsValid)
                return View("ReportApontamentoSintetico", PreencheApontamentoSinteticoViewModel());
            var rptH = new ReportClass
            {
                FileName = arquivo
            };
            rptH.Load();
            rptH.SetDataSource(DataBaseProvider.GetApontamentoSintetico(Convert.ToInt32(model.EmpresaId), Convert.ToInt32(model.UsuarioId), Convert.ToInt32(model.CompetenciaId)));
            var stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", "RelatorioSintetico.pdf");
        }

        private static ApontamentoAnaliticoViewModel PreencheApontamentoAnaliticoViewModel()
        {
            ApontamentoAnaliticoViewModel model = new ApontamentoAnaliticoViewModel();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);
                var e = eService.Query().Select().OrderBy(o => o.RazaoSocial);
                IEnumerable<SelectListItem> empresas = e.Select(x => new SelectListItem() { Text = x.RazaoSocial, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)empresas).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                model.Empresas.Empresas = empresas;

                var u = new List<Usuario>();
                IEnumerable<SelectListItem> usuarios = u.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)usuarios).Insert(0, new SelectListItem { Text = "<< Todos >>", Value = "0" });
                model.Usuarios.Usuarios = usuarios;

                var q = new List<Competencia>();
                IEnumerable<SelectListItem> competencias = q.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)competencias).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                model.Competencias.Competencias = competencias;

            }

            return model;
        }
        private static ApontamentoSinteticoViewModel PreencheApontamentoSinteticoViewModel()
        {
            ApontamentoSinteticoViewModel model = new ApontamentoSinteticoViewModel();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);
                var e = eService.Query().Select().OrderBy(o => o.RazaoSocial);
                IEnumerable<SelectListItem> empresas = e.Select(x => new SelectListItem() { Text = x.RazaoSocial, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)empresas).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                model.Empresas.Empresas = empresas;

                var u = new List<Usuario>();
                IEnumerable<SelectListItem> usuarios = u.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)usuarios).Insert(0, new SelectListItem { Text = "<< Todos >>", Value = "0" });
                model.Usuarios.Usuarios = usuarios;

                var q = new List<Competencia>();
                IEnumerable<SelectListItem> competencias = q.Select(x => new SelectListItem() { Text = x.Descricao, Value = x.Id.ToString() }).ToList();
                ((List<SelectListItem>)competencias).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });
                model.Competencias.Competencias = competencias;

            }

            return model;
        }

    }
}