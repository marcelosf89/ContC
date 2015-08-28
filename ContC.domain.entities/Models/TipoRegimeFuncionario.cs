using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class TipoRegimeFuncionario
    {
        public virtual int Id { get; set; }
        public virtual String Nome { get; set; }
        //CLT = 0,
        //Freelancer = 1,
        //PessoaJuridica = 2,
        //Socio = 99
    }
}
