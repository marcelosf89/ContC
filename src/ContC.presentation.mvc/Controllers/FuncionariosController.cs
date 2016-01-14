using ContC.crosscutting.DataContracts;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Models.FuncionarioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Controllers
{
    public class FuncionariosController : Controller
    {
        private IFuncionarioService _ifuncionarioService;
        private IBancoService _iBancoService;
        private IContaService _iContaService;

        public FuncionariosController(IFuncionarioService ifuncionarioService, IBancoService iBancoService, IContaService iContaService)
        {
            _ifuncionarioService = ifuncionarioService;
            _iBancoService = iBancoService;
            _iContaService = iContaService;
        }

        public ActionResult Index(int empresaId)
        {
            FuncionariosDTO fdto = _ifuncionarioService.GetByEmpresaTipoPagamentoLider(empresaId);
            return View(fdto);
        }

        [HttpPost]
        public ActionResult CarregarGridFuncionarios(int empresaId, int tipoPagamento, string liderId)
        {
            if (liderId.Equals("-")) liderId = null;

            CarregarGridFuncionarioModel cgfm = new CarregarGridFuncionarioModel();
            cgfm.EmpresaId = empresaId;
            cgfm.Funcionarios = _ifuncionarioService.GetByEmpresa(empresaId, tipoPagamento, liderId);

            return View(cgfm);
        }

        public ActionResult Editar(int funcionarioId, int empresaId)
        {
            FuncionarioManterModel model = new FuncionarioManterModel();
            GetSelects(empresaId, model);

            model.FuncionarioContaContract.EmpresaId = empresaId;
            Funcionario funcionario = _ifuncionarioService.Find(funcionarioId);
            Conta conta = _iContaService.GetByFuncionario(funcionarioId);

            model.FillFuncionarioContaContractBasedOn(funcionario, conta);
            
            return View("Novo", model);
        }

        public ActionResult Novo(int empresaId)
        {
            FuncionarioManterModel model = new FuncionarioManterModel();

            model.FuncionarioContaContract.EmpresaId = empresaId;

            GetSelects(empresaId, model);

            return View(model);
        }

        private void GetSelects(int empresaId, FuncionarioManterModel fmm)
        {
            FuncionariosDTO funcionariosDTO = _ifuncionarioService.GetByEmpresaTipoPagamentoLider(empresaId);
            fmm.Lideres = funcionariosDTO.Lideres.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nome }).ToList();
            ((IList<SelectListItem>)fmm.Lideres).Add(new SelectListItem() { Value = "0", Text = " ----- ", Selected = true });

            fmm.TipoPagamentos = funcionariosDTO.TipoPagamentos.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nome });
            fmm.TipoRegimeFuncionarios = funcionariosDTO.TipoRegimeFuncionarios.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Nome });
            fmm.Bancos = _iBancoService.GetAll().Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.DescricaoCompleta });
        }


        public ActionResult Salvar(FuncionarioManterModel model)
        {
            if (!ModelState.IsValid)
            {
                GetSelects(model.FuncionarioContaContract.EmpresaId, model);
                return PartialView("Novo", model);
            }
            
            try
            {
                _ifuncionarioService.Save(model.FuncionarioContaContract);
            }
            catch (Exception ex)
            {
                model.Erro = ex.Message;
                GetSelects(model.FuncionarioContaContract.EmpresaId, model);
                return PartialView("Novo", model);
            }

            FuncionariosDTO fdto = _ifuncionarioService.GetByEmpresaTipoPagamentoLider(model.FuncionarioContaContract.EmpresaId);
            return View("Index", fdto);
        }
    }
}