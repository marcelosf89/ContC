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
    public class BancoService : Service<Banco>, IBancoService
    {
        public BancoService(IBancoRepository repository)
        {
            base._repository = repository;
        }



        public IEnumerable<Banco> GetAll()
        {
            return ((IBancoRepository)base._repository).GetAll();
        }
    }
}
