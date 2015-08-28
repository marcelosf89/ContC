using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models
{
    public class UserRoleViewModel
    {
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }
        [Display(Name = "Usuário")]
        public IEnumerable<SelectListItem> ListaDeUsuarios { get; set; }
        public IList<string> ListaDePapeisDoUsuario { get; set; }
        public List<string> TodosOsPapeis { get; set; }
    }
}