using ContC.crosscutting.DataContracts;

namespace ContC.domain.services.Contracts
{
    public interface IAutenticacaoService
    {
        UsuarioSessao Autenticar(string email, string senha);
    }
}
