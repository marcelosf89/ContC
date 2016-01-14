namespace ContC.crosscutting.DataContracts
{
    public class UsuarioSessao
    {
        public UsuarioSessao()
        {
        }

        public UsuarioSessao(string login, long idGrupo)
        {
            Login = login;
            IdGrupo = idGrupo;
        }

        public string Login { get; set; }

        public long IdGrupo { get; set; }

        public bool Autenticado
        {
            get
            {
                return Login != null && Login.Trim().Length > 0;
            }
        }

    }
}