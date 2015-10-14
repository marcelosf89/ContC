using Entity.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class Nota : Entidade
    {
        public virtual int Id { get; set; }
        public virtual String Titulo { get; set; }
        public virtual Usuario Cadastrado { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual DateTime Cadastro { get; set; }
        public virtual DateTime Concluido { get; set; }

    }
}
