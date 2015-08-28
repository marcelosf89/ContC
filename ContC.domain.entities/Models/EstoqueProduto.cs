using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class EstoqueProduto : Entidade
    {

        public virtual int Id { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual double ValorMedio { get; set; }
        public virtual double ValorVenda { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Estoque Estoque { get; set; }

    }
}
