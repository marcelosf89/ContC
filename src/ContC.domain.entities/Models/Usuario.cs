namespace ContC.domain.entities.Models
{
    public class Usuario : Funcionario
    {
        public Usuario()
        {
        }

        public virtual string Sigla { get; set; }
        public virtual bool Situacao { get; set; }        
    }
}
