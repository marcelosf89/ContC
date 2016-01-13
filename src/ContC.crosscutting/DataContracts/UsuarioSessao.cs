namespace ContC.crosscutting.DataContracts
{
    public class UsuarioSessao
    {
        public string Login { get; set; }

        public long IdGrupo { get; set; }

        public bool Autenticado
        {
            get
            {
                return Login.Trim().Length > 0;
            }
        }

    }
}