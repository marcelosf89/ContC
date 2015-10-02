using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Boleto : Pagamento
    {
        //TODO Solucao para problema de conversao datetime/datetime2 do Entity: DateTime?
        //http://stackoverflow.com/questions/18795762/entity-framework-cannot-save-conversion-of-datetime2-to-datetime-failing

        public virtual string Numero { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Compra Compra { get; set; }

        public virtual String MotivoCancelamento { get; set; }
        public virtual DateTime DataCancelamento { get; set; }
        public virtual Usuario UsuarioCancelamento { get; set; }


        public override int GetHashCode()
        {
            return (Id.ToString() + Numero + DataVencimento + Valor).GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return GetHashCode().Equals(obj.GetHashCode());
        }
    }
}
