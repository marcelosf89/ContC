using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.NotasModels
{
    public class ListaUsuarioNotasModel
    {
        public int NotaSelecionadaId { get; set; }
        public int EmpresaId { get; set; }
        public IList<domain.entities.DTO.UsuarioDTO> Usuarios { get; set; }
        public String NotaNome { get; set; }
    }
}
