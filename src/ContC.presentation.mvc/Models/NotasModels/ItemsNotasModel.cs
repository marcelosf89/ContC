using ContC.domain.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.NotasModels
{
    public class ItemsNotasModel
    {
        public int NotaId { get; set; }
        public IList<NotaItem> Itens { get; set; }
    }
}
