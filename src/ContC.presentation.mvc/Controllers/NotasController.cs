using ContC.CorssCutting.Exceptions;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Extension;
using ContC.presentation.mvc.Models.ExceptionModels;
using ContC.presentation.mvc.Models.NotasModels;
using ContC.Repositories.Mapping.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class NotasController : ControllerException
    {
        private INotaService _iNotasServices;
        public NotasController(INotaService iNotasServices)
        {
            _iNotasServices = iNotasServices;

        }

        // GET: Listas
        public ActionResult Index(int empresaId)
        {
            ListaNotasModel lnm = new ListaNotasModel();
            lnm.EmpresaId = empresaId;

            return View(lnm);
        }

        public ActionResult NotasAtivas(int empresaId, int notaSelecionada)
        {
            IList<Nota> notas = _iNotasServices.GetNotasByEmpresaUsuario(empresaId, User.Identity.Name);
            ListaItemNotasModel lnm = new ListaItemNotasModel();
            lnm.Notas = notas;
            lnm.NotaSelecionadaId = notaSelecionada;

            return View(lnm);
        }

        public ActionResult ItensNota(int notaId)
        {
            IList<NotaItem> itens = _iNotasServices.GetItensByNotas(notaId);
            ItemsNotasModel inm = new ItemsNotasModel();
            inm.Itens = itens;
            inm.NotaId = notaId;
            return View(inm);
        }

        public ActionResult Novo(int empresaId)
        {
            NotasNovoModel lm = new NotasNovoModel();
            lm.EmpresaId = empresaId;
            return View(lm);
        }

        public ListaNotasModel Salvar(NotasNovoModel model)
        {
            Nota nota = new Nota();
            nota.Titulo = model.Titulo;
            nota.Empresa = new Empresa() { Id = model.EmpresaId };

            try
            {
                UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
                _iNotasServices.InsertNota(nota, User.Identity.Name);
                UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
            }
            catch (ExceptionMessage em)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw em;
            }
            catch (Exception ex)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw new StatusException("Erro interno . Favor informe ao administrador.");
            }

            ListaNotasModel lnm = new ListaNotasModel();
            lnm.EmpresaId = model.EmpresaId;
            lnm.NotaSelecionadaId = nota.Id;

            return lnm;
        }

        public ActionResult SalvarItem(int notaId, string title, int itemNotaId)
        {
            NotaItem ni = new NotaItem();
            ni.Id = itemNotaId;
            ni.Lista = new Nota() { Id = notaId };
            ni.Titulo = title;

            try
            {
                UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
                _iNotasServices.InsertItem(ni, User.Identity.Name);
                UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
            }
            catch (ExceptionMessage em)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw em;
            }
            catch (Exception ex)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw new StatusException("Erro interno . Favor informe ao administrador.");
            }
            return Json(ni);
        }

        public ActionResult CheckItem(int itemNotaId, bool ischecked)
        {

            NotaItem ni = _iNotasServices.CheckItem(itemNotaId, ischecked, User.Identity.Name);
            return Json(ischecked);
        }

        public void RemoverItem(int itemNotaId)
        {
            _iNotasServices.RemoveItem(itemNotaId,  User.Identity.Name);
        }

        public int Concluir(int notaId)
        {
            try
            {
                UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
                _iNotasServices.ConcluirNota(notaId, User.Identity.Name);
                UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
                return notaId;
            }
            catch (ExceptionMessage em)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw em;
            }
            catch (Exception ex)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw new StatusException("Erro interno . Favor informe ao administrador.");
            }
            
        }
    }
}