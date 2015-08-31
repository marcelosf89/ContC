using System;
using System.Collections.Generic;
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
        public int CategoriaId { get; set; }
        public int QuntidadeVezes { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }

        public IEnumerable<SelectListItem> TipoPagamentos { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }
        public IList<ProdutosModel> Pordutos { get; set; }
    }
}
