using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Models.FornecedorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class FornecedoresController : Controller
    {
        private IEmpresaService _empresaServico;
        private IFornecedorService _fornecedorService;
        private IGrupoService _grupoService;

        public FornecedoresController(IEmpresaService empresaServico, IFornecedorService fornecedorService, IGrupoService grupoService)
        {
            _empresaServico = empresaServico;
            _fornecedorService = fornecedorService;
            _grupoService = grupoService;
        }
        // GET: Fornecedores
        public ActionResult Index(int grupoId)
        {
            IList<Fornecedor> fornecedores = _fornecedorService.GetAllByGrupo(grupoId);
            FornecedorListaModel flm = new FornecedorListaModel();
            flm.GrupoId = grupoId;
            flm.Fornecedores = fornecedores;
            return View(flm);
        }


        public ActionResult Editar(int fornecedorId)
        {
            Fornecedor fornecedor = _fornecedorService.Find(fornecedorId);
            FornecedorManterModel fmm = new FornecedorManterModel();

            fmm.Vendedor = fornecedor.Vendedor;
            fmm.RazaoSocial = fornecedor.RazaoSocial;
            fmm.CNPJ = fornecedor.CNPJ;
            fmm.InscricaoEstadual = fornecedor.InscricaoEstadual;
            fmm.EmailVendador = fornecedor.EmailVendador;
            fmm.TelefoneVendedor = fornecedor.TelefoneVendedor;
            fmm.Observacao = fornecedor.Observacao;
            fmm.Id = fornecedor.Id;
            fmm.GrupoId = fornecedor.Grupo.Id;

            return View("Manter",fmm);
        }

        public ActionResult Novo(int grupoId)
        {
            FornecedorManterModel fmm = new FornecedorManterModel();
            fmm.GrupoId = grupoId;

            return View("Manter",fmm);
        }

        public ActionResult Salvar(FornecedorManterModel fmm)
        {
            Fornecedor fornecedor = new Fornecedor();
            if (fmm.Id > 0)
            {
                fornecedor = _fornecedorService.Find(fmm.Id);
            }
            else
            {
                fornecedor.Grupo = _grupoService.Find(fmm.GrupoId);
            }

            fornecedor.Vendedor = fmm.Vendedor;
            fornecedor.RazaoSocial = fmm.RazaoSocial;
            fornecedor.CNPJ = fmm.CNPJ;
            fornecedor.InscricaoEstadual = fmm.InscricaoEstadual;
            fornecedor.EmailVendador = fmm.EmailVendador;
            fornecedor.TelefoneVendedor = fmm.TelefoneVendedor;
            fornecedor.Observacao= fmm.Observacao;

            _fornecedorService.Update(fornecedor);

            IList<Fornecedor> fornecedores = _fornecedorService.GetAllByGrupo(fmm.GrupoId);
            FornecedorListaModel flm = new FornecedorListaModel();
            flm.GrupoId = fmm.GrupoId;
            flm.Fornecedores = fornecedores;
            return View("Index", flm);
        }
    }
}