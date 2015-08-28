using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models
{
    public class EmpresaSelectionViewModel
    {
        [Display(Name = "Empresa")]
        public string EmpresaId { get; set; }
        [Display(Name = "Empresa")]
        public IEnumerable<SelectListItem> Empresas { get; set; }
    }
}