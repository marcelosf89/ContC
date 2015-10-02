using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

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
