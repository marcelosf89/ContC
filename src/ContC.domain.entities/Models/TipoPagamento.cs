using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class TipoPagamento
    {

        public virtual int Id { get; set; }
        public virtual String Nome { get; set; }
        //Diaria = 0,
        //Semanal = 1,
        //Mensal = 2
    }
}
