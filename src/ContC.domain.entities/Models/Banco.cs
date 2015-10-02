using Entity.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class Banco : Entidade
    {
        public virtual int Id {get;set;}
        public virtual String Codigo {get;set;}
        public virtual String  Nome {get;set;}

        public virtual String DescricaoCompleta { get { return String.Format("{0} - {1}", this.Codigo, this.Nome); } }
    }
}
