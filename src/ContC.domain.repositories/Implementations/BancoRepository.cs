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
    public class BancoRepository : Repository<Banco>, IBancoRepository
    {
        public BancoRepository()
        {

        }


        public IEnumerable<Banco> GetAll()
        {
            return (from a in this.SessaoAtual.Query<Banco>()
                        select a);
        }
    }
}
