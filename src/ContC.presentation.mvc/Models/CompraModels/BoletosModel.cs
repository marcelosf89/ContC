using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.CompraModels
{
    public class BoletosModel
    {
        public int Id { get; set; }
        public Guid IdTemp { get; set; }
        public string Numero { get; set; }
        public string Vencimento { get; set; }
        public decimal Valor { get; set; }
    }
}
