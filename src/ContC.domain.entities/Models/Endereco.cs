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
    public class Endereco : Entidade
    {
        public Endereco()
        {
        }

        public virtual int Id { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string Email { get; set; }
        public virtual string Rua { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string CodigoPostal { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual string Estado { get; set; }
        public virtual string Pais { get; set; }
        public virtual bool IsMatriz { get; set; }

        public virtual string Numero { get; set; }
    }
}
