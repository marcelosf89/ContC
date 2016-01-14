using ContC.crosscutting.DataContracts;

namespace ContC.crosscutting.Authentication.Interface
{
    public interface IGerenciadorAutenticacao
    {
        void Registrar(UsuarioSessao usuario);
        UsuarioSessao Get();
        void Logoff();
    }
}
