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
        public Guid IdTemp { get; set; }
        public string Descricao { get; set; }
        public string CodigoFornecedor { get; set; }
        public int Codigo { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
        //public DateTime Validade  { get; set; }
        public String TipoQuantidade { get; set; }
        public decimal ValorTotal { get; set; }

        public decimal IPI { get; set; }
        public decimal ICMS { get; set; }

        public int ProdutoId { get; set; }

        public decimal ValorICMS { get; set; }

        public decimal ValorIPI { get; set; }

        public decimal ValorTotalComImposto { get; set; }

        public int QuantidadeCaixa { get; set; }
    }
}
