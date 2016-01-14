using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;
using System.Collections.Generic;

namespace ContC.domain.services.Contracts
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Get(string userName);

        IList<Funcionario> GetAllByUsuarios(string startsWith, int empresaId, int maxRows);

        IList<int> GetGruposIds(string email);

        Usuario GetFetchingFuncionario(string userName);
    }
}
