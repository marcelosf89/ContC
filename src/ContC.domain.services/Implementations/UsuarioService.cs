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
    public class UsuarioService : Service<Usuario>, IUsuarioService
    {
        public UsuarioService(IUsuarioRepository repository)
        {
            base._repository = repository;
        }

        public Usuario GetUsuario(string userName)
        {
            return ((IUsuarioRepository)base._repository).GetUsuario(userName);
        }


        public IList<Funcionario> GetAllByUsuarios(string startsWith, int empresaId, int maxRows)
        {
            return ((IUsuarioRepository)base._repository).GetAllByUsuarios(startsWith, empresaId, maxRows);
        }
    }
}
