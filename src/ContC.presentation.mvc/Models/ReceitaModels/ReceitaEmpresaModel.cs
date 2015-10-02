using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.ReceitaModels
{
   public class ReceitaEmpresaModel
    {
        public int EmpresaId { get; set; }
        public bool AtulizarReceitas { get; set; }
        public string RabbitMqQueue { get; set; }
    }
}
