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
    public class TipoReceitaRepository : Repository<TipoReceita>, ITipoReceitaRepository
    {
        public TipoReceitaRepository()
        {

        }

        public TipoReceita GetByDescricao(string descricao)
        {
            descricao = descricao.ToUpper();
            return (from a in this.SessaoAtual.Query<TipoReceita>()
                    where a.Descricao.ToUpper().Equals(descricao)
                    select a).SingleOrDefault();
        }
    }
}
