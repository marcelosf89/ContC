using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Produto : Entidade
    {
        //TODO Solucao para problema de conversao datetime/datetime2 do Entity: DateTime?
        //http://stackoverflow.com/questions/18795762/entity-framework-cannot-save-conversion-of-datetime2-to-datetime-failing

        public virtual int Id { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double ValorMedio { get; set; }
        public virtual Grupo Grupo { get; set; }

    }
}
