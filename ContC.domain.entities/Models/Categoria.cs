using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Categoria : Entidade
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }        
        public virtual Grupo Grupo { get; set; }
    }
}
