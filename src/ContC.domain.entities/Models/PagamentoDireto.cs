using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class PagamentoDireto: Pagamento
    {
        public virtual Compra Compra { get; set; }
    }
}
