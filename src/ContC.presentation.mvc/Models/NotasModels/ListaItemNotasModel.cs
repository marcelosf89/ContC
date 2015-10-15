using ContC.domain.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.NotasModels
{
    public class ListaItemNotasModel
    {
        public int NotaSelecionadaId { get; set; }
        public IList<Nota> Notas { get; set; }
    }
}
