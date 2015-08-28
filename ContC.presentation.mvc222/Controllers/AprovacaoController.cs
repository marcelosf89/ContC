using ContC.domain.entities.Context;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using ContC.domain.services;
using ContC.presentation.mvc.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class AprovacaoController : Controller
    {
        // GET: Aprovacao
        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult AprovadoGridPartial(string profissionalId, string empresaId)
        {
            return PartialView(ObterAprovados(Convert.ToInt32(profissionalId), Convert.ToInt32(empresaId)));
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult AprovacaoPartial()
        {
            AprovadoViewModel avm = new AprovadoViewModel();
            avm.Profissionais = new List<SelectListItem>();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();

                try
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    IList<Usuario> profissionais = apService.ObterProfissionaisApontamentosPorLiderAprovacao(lider);

                    IEnumerable<SelectListItem> usuarios = profissionais.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)usuarios).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    avm.Profissionais = usuarios;
                    profissionais.Select(p => p);
                    return PartialView(avm);
                }
                catch (Exception ex)
                {
                    return PartialView(avm);
                }
            }
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult DesaprovadoPartial()
        {
            AprovadoViewModel avm = new AprovadoViewModel();
            avm.Profissionais = new List<SelectListItem>();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();

                try
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    IList<Usuario> profissionais = apService.ObterProfissionaisApontamentosPorLiderDesaprovado(lider);

                    IEnumerable<SelectListItem> usuarios = profissionais.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)usuarios).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    avm.Profissionais = usuarios;
                    profissionais.Select(p => p);
                    return PartialView(avm);
                }
                catch (Exception ex)
                {
                    return PartialView(avm);
                }
            }
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult AprovadoPartial()
        {
            AprovadoViewModel avm = new AprovadoViewModel();
            avm.Profissionais = new List<SelectListItem>();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();

                try
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    IList<Usuario> profissionais = apService.ObterProfissionaisApontamentosPorLider(lider);

                    IEnumerable<SelectListItem> usuarios = profissionais.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)usuarios).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    avm.Profissionais = usuarios;
                    profissionais.Select(p => p);
                    return PartialView(avm);
                }
                catch (Exception ex)
                {
                    return PartialView(avm);
                }
            }
        }

        public ActionResult EmpresaAprovadoPartial(string profissionalId)
        {
            return PartialView(GetEmpresas(profissionalId));
        }
        public ActionResult EmpresaDesaprovadoPartial(string profissionalId)
        {
            return PartialView(GetEmpresasDesaprovado(profissionalId));
        }
        public ActionResult EmpresaAprovacaoPartial(string profissionalId)
        {
            return PartialView(GetEmpresasAprovacao(profissionalId));
        }

        private EmpresasProfissionalViewModel GetEmpresasAprovacao(string profissionalId)
        {
            EmpresasProfissionalViewModel avm = new EmpresasProfissionalViewModel();
            avm.Empresas = new List<SelectListItem>();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                int pID = Convert.ToInt32(profissionalId);
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();
                Usuario profissional = uService.Query(c => c.Id == pID).Select().FirstOrDefault();

                try
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    IList<Empresa> listEmpresas = apService.ObterEmpresasPorProfissionaisApontamentosPorLiderAprovacao(lider, profissional);

                    IEnumerable<SelectListItem> empresas = listEmpresas.Select(x => new SelectListItem() { Text = x.RazaoSocial, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)empresas).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    avm.Empresas = empresas;
                    return avm;
                }
                catch (Exception ex)
                {
                    return avm;
                }
            }
        }

        private EmpresasProfissionalViewModel GetEmpresasDesaprovado(string profissionalId)
        {
            EmpresasProfissionalViewModel avm = new EmpresasProfissionalViewModel();
            avm.Empresas = new List<SelectListItem>();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                int pID = Convert.ToInt32(profissionalId);
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();
                Usuario profissional = uService.Query(c => c.Id == pID).Select().FirstOrDefault();

                try
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    IList<Empresa> listEmpresas = apService.ObterEmpresasPorProfissionaisApontamentosPorLiderDesaprovado(lider, profissional);

                    IEnumerable<SelectListItem> empresas = listEmpresas.Select(x => new SelectListItem() { Text = x.RazaoSocial, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)empresas).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    avm.Empresas = empresas;
                    return avm;
                }
                catch (Exception ex)
                {
                    return avm;
                }
            }
        }

        private EmpresasProfissionalViewModel GetEmpresas(string profissionalId)
        {
            EmpresasProfissionalViewModel avm = new EmpresasProfissionalViewModel();
            avm.Empresas = new List<SelectListItem>();

            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                int pID = Convert.ToInt32(profissionalId);
                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();
                Usuario profissional = uService.Query(c => c.Id == pID).Select().FirstOrDefault();

                try
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    IList<Empresa> listEmpresas = apService.ObterEmpresasPorProfissionaisApontamentosPorLider(lider, profissional);

                    IEnumerable<SelectListItem> empresas = listEmpresas.Select(x => new SelectListItem() { Text = x.RazaoSocial, Value = x.Id.ToString() }).ToList();
                    ((List<SelectListItem>)empresas).Insert(0, new SelectListItem { Text = "<< Selecione >>", Value = "0" });

                    avm.Empresas = empresas;
                    return avm;
                }
                catch (Exception ex)
                {
                    return avm;
                }
            }
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult AprovacaoGridPartial(string profissionalId, string empresaId)
        {

            return PartialView(ObterAprovacao(Convert.ToInt32(profissionalId), Convert.ToInt32(empresaId)));
        }


        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult DesaprovadoGridPartial(string profissionalId, string empresaId)
        {
            return PartialView(ObterDesaprovados(Convert.ToInt32(profissionalId), Convert.ToInt32(empresaId)));
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult GetAprovacao(string profissionalId, string empresaId)
        {
            return PartialView("AprovacaoGridPartial", ObterAprovacao(Convert.ToInt32(profissionalId), Convert.ToInt32(empresaId)));
        }

        [Authorize(Roles = "APROP, APROV, CONFIG, ADMIN")]
        public ActionResult GetDesaprovados(string profissionalId, string empresaId)
        {
            return PartialView("DesaprovadoGridPartial", ObterDesaprovados(Convert.ToInt32(profissionalId), Convert.ToInt32(empresaId)));
        }

        private IList<AprovacaoDTO> ObterAprovados(int profissionalId, int empresaId)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);
                Empresa empresa = eService.Query(c => c.Id == empresaId).Select().FirstOrDefault();

                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();
                Usuario profissional = uService.Query(c => c.Id == profissionalId).Select().FirstOrDefault();

                try
                {

                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    var apontamentos = apService.ObterApontamentosAprovadosPorLider(lider, profissional, empresa);
                    return apontamentos.OrderBy(o => o.DataApropriacao).ToList();

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        private IList<AprovacaoDTO> ObterAprovacao(int profissionalId, int empresaId)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);
                Empresa empresa = eService.Query(c => c.Id == empresaId).Select().FirstOrDefault();

                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();
                Usuario profissional = uService.Query(c => c.Id == profissionalId).Select().FirstOrDefault();

                try
                {

                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    var apontamentos = apService.ObterApontamentosAprovacaoPorLider(lider, profissional, empresa);
                    return apontamentos.OrderBy(o => o.DataApropriacao).ToList();

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        private IList<AprovacaoDTO> ObterDesaprovados(int profissionalId , int empresaId)
        {
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                //int eID = Convert.ToInt32(empresaId);

                //IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                //UsuarioService uService = new UsuarioService(uRepository);
                //Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();

                IRepositoryAsync<Usuario> uRepository = new Repository<Usuario>(context, unitOfWork);
                UsuarioService uService = new UsuarioService(uRepository);
                Usuario lider = uService.Query(c => c.Email == User.Identity.Name).Select().FirstOrDefault();
                Usuario profissional = uService.Query(c => c.Id == profissionalId).Select().FirstOrDefault();

                IRepositoryAsync<Empresa> eRepository = new Repository<Empresa>(context, unitOfWork);
                EmpresaService eService = new EmpresaService(eRepository);
                Empresa empresa = eService.Query(c => c.Id == empresaId).Select().FirstOrDefault();

                try
                {
                    IRepositoryAsync<Apontamento> apRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService apService = new ApontamentoService(apRepository);
                    IList<AprovacaoDTO> apontamentos = apService.ObterApontamentosDesaprovadoPorLider(lider, empresa, profissional);
                    return apontamentos.OrderBy(o => o.DataApropriacao).ToList();

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public String Aprovar(string id)
        {
            String mensagem = "";
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    IRepositoryAsync<Apontamento> uRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService aService = new ApontamentoService(uRepository);
                    aService.AprovarApontamento(Convert.ToInt32(id));
                    unitOfWork.SaveChanges();
                    unitOfWork.Commit();
                    mensagem = "Hh aprovado com sucesso!";
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                    unitOfWork.Rollback();
                    //return PartialView();
                }
            }
            return mensagem;//PartialView("AprovacaoGridPartial", ObterAprovados());
        }

        public String Desaprovar(string id)
        {
            String mensagem;
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    IRepositoryAsync<Apontamento> uRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService aService = new ApontamentoService(uRepository);
                    aService.DesaprovarApontamento(Convert.ToInt32(id));
                    unitOfWork.SaveChanges();
                    unitOfWork.Commit();
                    mensagem = "Hh desaprovado com sucesso!";
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                    unitOfWork.Rollback();
                    //return PartialView();
                }
            }
            return mensagem;

        }

        public string AprovarDesaprovado(string id)
        {
            string mensagem = "";
            using (IDataContextAsync context = new DbContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                try
                {
                    unitOfWork.BeginTransaction();
                    IRepositoryAsync<Apontamento> uRepository = new Repository<Apontamento>(context, unitOfWork);
                    ApontamentoService aService = new ApontamentoService(uRepository);
                    aService.AprovarApontamento(Convert.ToInt32(id));
                    unitOfWork.SaveChanges();
                    unitOfWork.Commit();
                    mensagem = "Hh Aprovado com sucesso!";
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    //return PartialView();
                    mensagem = ex.Message;
                }
            }

            return mensagem;
        }
    }
}