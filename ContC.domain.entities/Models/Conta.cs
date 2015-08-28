using Entity.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class Conta : Entidade
    {
        public virtual int Id {get;set;}
        public virtual Banco Banco {get;set;}
        public virtual String  Agencia {get;set;}
        public virtual String  NumeroConta {get;set;}
        public virtual String  Extensao {get;set;}
        public virtual Funcionario Funcionario { get; set; }
    }
}
