using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models
{
    public class CompetenciaSelectionViewModel
    {
        [Display(Name = "Competência")]
        public string CompetenciaId { get; set; }
        [Display(Name = "Competência")]
        public IEnumerable<SelectListItem> Competencias { get; set; }
    }
}