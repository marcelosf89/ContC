using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;
using System.Collections.ObjectModel;

namespace ContC.domain.entities.Models
{
    public class Fornecedor : Entidade
    {
        //TODO Solucao para problema de conversao datetime/datetime2 do Entity: DateTime?
        //http://stackoverflow.com/questions/18795762/entity-framework-cannot-save-conversion-of-datetime2-to-datetime-failing

        public Fornecedor()
        {

            
        }
        public virtual int Id { get; set; }
        public virtual string RazaoSocial { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual string Vendedor { get; set; }
        public virtual string EmailVendador { get; set; }
        public virtual string TelefoneVendedor { get; set; }
        public virtual string Observacao { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}
