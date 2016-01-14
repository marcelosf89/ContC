using ContC.crosscutting;
using ContC.crosscutting.DataContracts;
using ContC.domain.entities.Models;
using System;

namespace ContC.domain.services.Contracts
{
    public interface IFuncionarioBuilder
    {
        Funcionario BuildBasedOn(FuncionarioContaContract contract);

        IFuncionarioBuilder SetNome(string nome);

        IFuncionarioBuilder SetEmail(string email);

        IFuncionarioBuilder SetTelefone(string telefone);

        IFuncionarioBuilder SetNascimento(DateTime nascimento);

        IFuncionarioBuilder SetIdentificacao1(string identificacao1);

        IFuncionarioBuilder SetIdentificacao2(string identificacao2);

        IFuncionarioBuilder SetLider(int liderId);

        IFuncionarioBuilder SetTipoPagamento(int tipoPagamento);

        IFuncionarioBuilder SetTipoRegime(int tipoRegime);

        IFuncionarioBuilder SetValor(decimal valor);

        IFuncionarioBuilder SetEmpresa(int idEmpresa);

        OperacaoStatus Validar();

        Funcionario Build();
    }
}