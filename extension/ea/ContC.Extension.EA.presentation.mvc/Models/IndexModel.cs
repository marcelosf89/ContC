using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.presentation.mvc.Models
{
    public class IndexModel
    {
        public int SelectedGroupId { get; set; }

        public IList<domain.entities.Models.Grupo> Grupos { get; set; }
    }
}
