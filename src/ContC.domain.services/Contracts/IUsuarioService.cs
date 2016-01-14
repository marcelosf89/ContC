using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;

namespace ContC.domain.services.Contracts
{
    public interface IUsuarioService : IService<Usuario>
    {
        Usuario GetUsuario(String userName);

        IList<Funcionario> GetAllByUsuarios(string startsWith, int empresaId, int maxRows);

        Usuario GetUsuarioFetchFuncionario(string userName);
    }
}
