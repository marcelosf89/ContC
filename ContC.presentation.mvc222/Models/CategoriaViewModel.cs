using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContC.presentation.mvc.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        [Required]
        public int TipoCategoriaId { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string TiposRelacaoIds { get; set; }
        public string TiposRelacaoDescritivos { get; set; }
        public int EmpresaId { get; set; }
    }
}