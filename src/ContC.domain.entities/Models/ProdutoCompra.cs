using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class ProdutoCompra : Entidade
    {
        //TODO Solucao para problema de conversao datetime/datetime2 do Entity: DateTime?
        //http://stackoverflow.com/questions/18795762/entity-framework-cannot-save-conversion-of-datetime2-to-datetime-failing

        public virtual int Id { get; set; }
        public virtual DateTime Validade { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual string TipoQuantidade { get; set; }
        public virtual int QuantidadeCaixa { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Compra Compra { get; set; }

        public virtual decimal IPI { get; set; }
        public virtual decimal ICMS { get; set; }
        public virtual decimal BaseICMS { get; set; }

        public override int GetHashCode()
        {
            return (Id.ToString() + Validade + Valor + Quantidade + TipoQuantidade + Produto.Id).GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return GetHashCode().Equals(obj.GetHashCode());
        }




        
    }
}
