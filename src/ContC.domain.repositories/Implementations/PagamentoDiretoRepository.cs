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
    public class PagamentoDiretoRepository : Repository<PagamentoDireto>, IPagamentoDiretoRepository
    {

        public PagamentoDireto GetByCompra(int p)
        {
            return (from a in this.SessaoAtual.Query<PagamentoDireto>()
                    where a.Compra.Id == p
                    select a).SingleOrDefault();
        }
    }
}
