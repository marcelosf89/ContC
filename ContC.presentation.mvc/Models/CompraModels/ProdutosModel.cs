using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.CompraModels
{
    public class ProdutosModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string CodigoFornecedor { get; set; }
        public int Codigo { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
