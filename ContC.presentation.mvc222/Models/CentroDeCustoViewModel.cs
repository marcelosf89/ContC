using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace ContC.presentation.mvc.Models
{
    public class CentroDeCustoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Sigla { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public bool Situacao { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public  int EmpresaId { get; set; }
    }
}
