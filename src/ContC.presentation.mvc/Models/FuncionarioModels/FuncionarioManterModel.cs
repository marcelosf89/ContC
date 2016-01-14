using ContC.crosscutting.DataContracts;
using ContC.domain.entities.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models.FuncionarioModels
{
    public class FuncionarioManterModel
    {
        public FuncionarioManterModel()
        {
            FuncionarioContaContract = new FuncionarioContaContract();
        }

        public FuncionarioContaContract FuncionarioContaContract { get; set; }

        public IEnumerable<SelectListItem> Lideres { get; set; }

        public IEnumerable<SelectListItem> TipoPagamentos { get; set; }

        public IEnumerable<SelectListItem> TipoRegimeFuncionarios { get; set; }

        public IEnumerable<SelectListItem> Bancos { get; set; }

        public string Erro { get; set; }

        public void FillFuncionarioContaContractBasedOn(Funcionario funcionario, Conta conta)
        {
            FuncionarioContaContract contract = FuncionarioContaContract;
            if (contract == null)
            {
                contract = new FuncionarioContaContract();
            }

            if (funcionario != null)
            {
                contract.Id = funcionario.Id;
                contract.Nome = funcionario.Nome;
                contract.Email = funcionario.Email;
                contract.Telefone = funcionario.Telefone;
                contract.Nascimento = funcionario.Nascimento;
                contract.Identificacao1 = funcionario.Identificacao1;
                contract.Identificacao2 = funcionario.Identificacao2;
                contract.LiderId = funcionario.Lider == null ? 0 : funcionario.Lider.Id;
                contract.TipoPagamentoId = funcionario.TipoPagamento.Id;
                contract.TipoRegimeFuncionarioId = funcionario.TipoRegimeFuncionario.Id;
                contract.Valor = funcionario.Valor;
            }
            
            if (conta != null)
            {
                contract.ContaId = conta.Id;
                contract.Agencia = conta.Agencia;
                contract.Conta = conta.NumeroConta;
                contract.Digito = conta.Extensao;
                contract.BancoId = conta.Banco.Id;
            }

        }
    }
}
