using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;

namespace ContC.presentation.mvc.Models
{
    public class AprovadoViewModel
    {
        [Display(Name = "Profissionais")]
        public IEnumerable<SelectListItem> Profissionais { get; set; }

        [Display(Name = "Profissional")]
        public string ProfissionalId { get; set; }

    }
    public class EmpresasProfissionalViewModel
    {
        [Display(Name = "Profissional")]
        public string EmpresaId { get; set; }

        [Display(Name = "Empresas")]
        public IEnumerable<SelectListItem> Empresas { get; set; }
    }
}