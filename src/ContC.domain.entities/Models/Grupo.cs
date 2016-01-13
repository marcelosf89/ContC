using System;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Grupo : Entidade
    {
        public Grupo()
        {
        }

        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Sigla { get; set; }
        public virtual bool Situacao { get; set; }
        public virtual DateTime? DataInicio { get; set; }
        public virtual DateTime? DataTermino { get; set; }
        public  virtual Funcionario Responsavel { get; set; }
    }
}
