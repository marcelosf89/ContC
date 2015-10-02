using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class TipoReceita : Entidade
    {
        public TipoReceita()
        {
        }

        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
    }
}
