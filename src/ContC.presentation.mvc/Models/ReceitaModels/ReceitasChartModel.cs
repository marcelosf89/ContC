using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.ReceitaModels
{
   public class ReceitasChartModel
    {
        public int EmpresaId { get; set; }

        public String DataInicio { get; set; }
        public String HoraInicio { get; set; }
        public String HoraFinal { get; set; }
        public String DataFinal { get; set; }
    }
}
