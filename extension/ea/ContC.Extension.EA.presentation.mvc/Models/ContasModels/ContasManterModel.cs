using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContC.Extension.EA.presentation.mvc.Models.ContasModels
{
    public class ContasManterModel
    {

        public int EmpresaId { get; set; }
        public int Id { get; set; }
        public Guid TempId { get; set; }
        public bool UploadFile { get; set; }

        [Required(ErrorMessage="O Codigo de barra tem que ser preenchido")]
        public String NumeroConta { get; set; }

        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A Data de Vencimento é obrigatório")]
        public DateTime? Validade { get; set; }

        [Required(ErrorMessage = "O Fornecedor é obrigatório")]
        public int FornecedorId { get; set; }
        [Required(ErrorMessage = "O Fornecedor é obrigatório")]
        public String Fornecedor { get; set; }

        [Required(ErrorMessage = "A Categoria é obrigatória")]
        public int CategoriaId { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }


        public string Erro { get; set; }
    }
}
