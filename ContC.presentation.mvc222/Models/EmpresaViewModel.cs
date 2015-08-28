using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContC.domain.entities.Models;

namespace ContC.presentation.mvc.Models
{
    public class EmpresaViewModel
    {

        [Required]
        [Display(Name = "Empresa")]
        public string EmpresaId { get; set; }
        
        [Display(Name = "Empresa")]
        public IEnumerable<SelectListItem> Empresas { get; set; }

        [Display(Name = "Profissional")]
        public string Profissional { get; set; }

        [Display(Name = "Líder")]
        public string Lider { get; set; }

    }
}