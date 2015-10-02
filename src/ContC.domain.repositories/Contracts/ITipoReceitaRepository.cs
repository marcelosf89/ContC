using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface ITipoReceitaRepository : IRepository<TipoReceita>
    {
        TipoReceita GetByDescricao(string descricao);
    }
}
