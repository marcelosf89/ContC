using ContC.domain.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.DTO
{
    public class UsuarioDTO
    {
        public Usuario Usuario { get; set; }
        public bool ECriador { get; set; }

    }
}
