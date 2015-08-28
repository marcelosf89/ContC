using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContC.presentation.mvc.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Sigla { get; set; }
        public bool Situacao { get; set; }
        [Required]
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        [Required]
        public int LiderId { get; set; }
        public bool LoginAssociado { get; set; }
    }
}