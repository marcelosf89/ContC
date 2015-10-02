using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.DTO
{
    public class CompraDTO
    {
        public string NumeroNota { get; set; }
        public String Fornecedor { get; set; }
        public DateTime? Entrega { get; set; }
        public decimal Valor { get; set; }
        public int EmpresaId { get; set; }
        public int Id { get; set; }
    }
}
