using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Feriado : Entidade
    {
        public virtual int Id { get; set; }
        public virtual DateTime? Data { get; set; }
        public virtual string Descricao { get; set; }
        //public virtual Competencia Competencia { get; set; }
        //public virtual Localidade Localidade { get; set; }
    }
}
