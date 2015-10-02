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
    public class FuncionarioEndereco : Entidade
    {
        public FuncionarioEndereco()
        {
        }

        public virtual int Id { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
