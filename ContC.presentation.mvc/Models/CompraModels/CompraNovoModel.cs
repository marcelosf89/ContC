using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models.CompraModels
{
    public class CompraNovoModel
    {
        public int EmpresaId { get; set; }
        public int Id { get; set; }
        
        public int FornecedorId { get; set; }

        [Required(ErrorMessage = "A Categoria tem que ser preenchido")]
        public int CategoriaId { get; set; }

        private int _quantidadeVezes;
        public int QuantidadeVezes
        {
            get { if (this._quantidadeVezes == 0) return 1; else return this._quantidadeVezes; }
            set { this._quantidadeVezes = value; }
        }
        public decimal Valor { get; set; }

        public IEnumerable<SelectListItem> TipoPagamentos { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }

        public DateTime? Entrega { get; set; }

        [Required(ErrorMessage = "O Fornecedor tem que ser preenchido")]
        public String Fornecedor { get; set; }

        [Required(ErrorMessage = "O Tipo de Pagamento tem que ser preenchido")]
        public int TipoPagamentoId { get; set; }

        [Required(ErrorMessage = "O Numero da Nota nao pode ficar em branco")]
        public string NumeroNota { get; set; }

        public IList<ProdutosModel> Produtos { get; set; }

        public IList<BoletosModel> Boletos { get; set; }


        public decimal ValorFrete { get; set; }

        public decimal ValorDespesaAdministrativa { get; set; }

        public decimal ValorSeguro { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Desconto { get; set; }

        public decimal ValorICMSNota { get; set; }

        public decimal ValorIPINota { get; set; }

        public decimal ValorTotalNota { get; set; }
    }
}
