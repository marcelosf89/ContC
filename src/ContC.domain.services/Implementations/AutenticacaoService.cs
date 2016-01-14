using ContC.domain.services.Contracts;
using ContC.domain.entities.Models;
using ContC.crosscutting.DataContracts;
using System.Collections.Generic;

namespace ContC.domain.services.Implementations
{
    public class AutenticacaoService : IAutenticacaoService
    {
        public AutenticacaoService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UsuarioSessao Autenticar(string email, string senha)
        {
            Usuario usuario = _usuarioRepository.Get(email);
            if (usuario == null || usuario.Senha != senha)
            {
                return new UsuarioSessao();
            }

            IList<int> grupos = _usuarioRepository.GetGruposIds(email);
            int grupoUsuario = 0;
            if (grupos != null && grupos.Count > 0)
            {
                grupoUsuario = grupos[0];
            }

            return new UsuarioSessao(email, grupoUsuario);
        }

        private IUsuarioRepository _usuarioRepository;
    }
}
