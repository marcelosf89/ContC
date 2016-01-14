using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Service.Pattern;
using System;
using ContC.crosscutting.DataContracts;
using ContC.crosscutting.Exceptions;

namespace ContC.domain.services.Implementations
{
    public class ContaService : Service<Conta>, IContaService
    {
        public ContaService(IContaRepository repository, IBancoRepository bancoRepository)
        {
            base._repository = repository;
            _bancoRepository = bancoRepository;
        }

        public Conta BuildBasedOn(FuncionarioContaContract contract)
        {
            Banco banco = _bancoRepository.Find(contract.BancoId);
            if (banco == null)
            {
                throw new EntidadeNaoEncontradaException(string.Format("Banco {0} não encontrado", contract.BancoId));
            }

            Conta conta = Find(contract.ContaId);
            if (conta == null)
            {
                conta = new Conta();
            }
            conta.Agencia = contract.Agencia;
            conta.NumeroConta = contract.Conta;
            conta.Extensao = contract.Digito;
            return conta;
        }

        public Conta GetByFuncionario(int funcionarioId)
        {
            return ((IContaRepository)base._repository).GetByFuncionario(funcionarioId);
        }

        private IBancoRepository _bancoRepository;

    }
}
