using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository()
        {

        }


        public Conta GetByFuncionario(int funcionarioId)
        {
            return (from a in this.SessaoAtual.Query<Conta>()
                    where a.Funcionario.Id == funcionarioId
                    select a).SingleOrDefault();
        }
    }
}
