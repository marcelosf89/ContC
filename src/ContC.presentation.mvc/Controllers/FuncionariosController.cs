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

            model.EmpresaId = empresaId;
            Funcionario func = _ifuncionarioService.Find(funcionarioId);

            model.Id = func.Id;
            model.Nome = func.Nome;
            model.Email = func.Email;
            model.Telefone = func.Telefone;
            model.Nascimento = func.Nascimento;
            model.Identificacao1 = func.Identificacao1;
            model.Identificacao2 = func.Identificacao2;
            model.LiderId = func.Lider == null ? 0 : func.Lider.Id;
            model.TipoPagamentoId = func.TipoPagamento.Id;
            model.TipoRegimeFuncionarioId = func.TipoRegimeFuncionario.Id;
            model.Valor = func.Valor;

            Conta conta = _iContaService.GetByFuncionario(funcionarioId);
            if (conta != null)
            {
                model.ContaId = conta.Id;
                model.Agencia = conta.Agencia;
                model.Conta = conta.NumeroConta;
                model.Digito = conta.Extensao;
                model.BancoId = conta.Banco.Id;
            }

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

            Funcionario func = new Funcionario() { Id = model.Id, DataInicio = DateTime.Now.Date };
            if (model.Id > 0) { func = _ifuncionarioService.Find(model.Id); }

            func.Nome = model.Nome;
            func.Email = model.Email;
            func.Telefone = model.Telefone;
            func.Nascimento = model.Nascimento.Value;
            func.Identificacao1 = model.Identificacao1;
            func.Identificacao2 = model.Identificacao2;
            if (model.LiderId > 0)
            {
                func.Lider = new Funcionario() { Id = model.LiderId };
            }
            func.TipoPagamento = new TipoPagamento() { Id = model.TipoPagamentoId };
            func.TipoRegimeFuncionario = new TipoRegimeFuncionario() { Id = model.TipoRegimeFuncionarioId };
            func.Valor = model.Valor;


            Conta conta = new Conta() { Id = model.ContaId };
            conta.Agencia = model.Agencia;
            conta.NumeroConta = model.Conta;
            conta.Extensao = model.Digito;
            conta.Banco = new Banco() { Id = model.BancoId };

            try
            {
                _ifuncionarioService.Insert(func, conta, model.EmpresaId);
            }
            catch (Exception ex)
            {
                model.Erro = ex.Message;
                GetSelects(model.EmpresaId, model);
                return PartialView("Novo", model);
            }

            FuncionariosDTO fdto = _ifuncionarioService.GetByEmpresaTipoPagamentoLider(model.EmpresaId);
            return View("Index", fdto);
        }
    }
}