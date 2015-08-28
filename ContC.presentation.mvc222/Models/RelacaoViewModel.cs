using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContC.presentation.mvc.Models
{
    public class RelacaoViewModel
    {
        public int Id { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        [Required]
        public int TipoRelacaoId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public decimal QtdeMaximaHorasDiarias { get; set; }
        public string RazaoSocialFornecedor { get; set; }
    }
}