using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;

namespace ContC.domain.services.Contracts
{
    public interface ITipoReceitaRepository : IRepository<TipoReceita>
    {
        TipoReceita GetByDescricao(string descricao);
    }
}
