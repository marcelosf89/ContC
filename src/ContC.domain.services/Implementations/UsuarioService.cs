using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Service.Pattern;
using System.Collections.Generic;

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
            return _usuarioRepository.Get(userName);
        }

        public Usuario GetUsuarioFetchFuncionario(string userName)
        {
            return _usuarioRepository.GetFetchingFuncionario(userName);
        }

        public IList<Funcionario> GetAllByUsuarios(string startsWith, int empresaId, int maxRows)
        {
            return _usuarioRepository.GetAllByUsuarios(startsWith, empresaId, maxRows);
        }

        private IUsuarioRepository _usuarioRepository
        {
            get
            {
                return (IUsuarioRepository)_repository;
            }
        }

    }
}
