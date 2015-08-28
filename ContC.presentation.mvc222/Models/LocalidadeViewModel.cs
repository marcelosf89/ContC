using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models
{
    public class LocalidadeViewModel
    {
        [Required]
        [Display(Name = "Localidade")]
        public string LocalidadeId { get; set; }
        [Display(Name = "Localidade")]
        public IEnumerable<SelectListItem> Localidades { get; set; }
        public IList<FeriadoViewModel> Feriados { get; set; }
    }
}