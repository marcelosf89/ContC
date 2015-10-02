using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class ContaService : Service<Conta>, IContaService
    {
        public ContaService(IContaRepository repository)
        {
            base._repository = repository;
        }



        public Conta GetByFuncionario(int funcionarioId)
        {
            return ((IContaRepository)base._repository).GetByFuncionario(funcionarioId);
        }
    }
}
