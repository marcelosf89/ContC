using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContC.presentation.mvc.Models
{
    public class FeriadoViewModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime? Data { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int CompetenciaId { get; set; }
        [Required]
        public int LocalidadeId { get; set; }
    }
}