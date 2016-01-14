using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Usuario : Entidade
    {
        public Usuario()
        {
        }

        public virtual int Id { get; set; }

        public virtual string Sigla { get; set; }

        public virtual bool Situacao { get; set; }        

        public virtual string Senha { get; set; }

        public virtual Funcionario Funcionario { get; set; }
    }
}
