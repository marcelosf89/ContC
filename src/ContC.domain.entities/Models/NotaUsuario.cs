using Entity.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class NotaUsuario : Entidade
    {
        public virtual int Id { get; set; }
        //public virtual Guid Id { get; set; }
        public virtual Nota Lista { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
