using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models
{
    public class TipoRelacaoViewModel
    {
        [Required]
        [Display(Name = "Tipo da Relação")]
        public string TipoRelacaoId { get; set; }
        [Display(Name = "Tipo da Relação")]
        public IEnumerable<SelectListItem> TiposRelacao { get; set; }
        public IList<RelacaoViewModel> Relacoes { get; set; }
    }
}