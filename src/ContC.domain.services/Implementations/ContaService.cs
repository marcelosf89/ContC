using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Service.Pattern;
using System;
using ContC.crosscutting.DataContracts;

namespace ContC.domain.services.Implementations
{
    public class ContaService : Service<Conta>, IContaService
    {
        public ContaService(IContaRepository repository, IFuncionarioService funcionarioService)
        {
            base._repository = repository;
        }

        public Conta BuildBasedOn(FuncionarioContaContract contract)
        {
            throw new NotImplementedException();
        }

        public Conta GetByFuncionario(int funcionarioId)
        {
            return ((IContaRepository)base._repository).GetByFuncionario(funcionarioId);
        }

        public Conta BuildBasedOn(FuncionarioContaContract contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("Contract deve ser preenchido");
            }
        }
    }
}
