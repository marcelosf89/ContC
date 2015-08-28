using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models
{
    public class UsuarioSelectionViewModel
    {
        [Display(Name = "Usuário")]
        public string UsuarioId { get; set; }
        [Display(Name = "Usuário")]
        public IEnumerable<SelectListItem> Usuarios { get; set; }
    }
}