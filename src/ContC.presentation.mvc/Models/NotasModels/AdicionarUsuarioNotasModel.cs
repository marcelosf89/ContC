using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.NotasModels
{
    public class AdicionarUsuarioNotasModel
    {
        public int UsuarioId { get; set; }

        public string UsuarioNome { get; set; }

        public string UsuarioEmail { get; set; }

        public int NotaId { get; set; }
    }
}
