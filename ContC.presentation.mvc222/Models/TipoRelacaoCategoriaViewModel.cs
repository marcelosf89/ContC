using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContC.presentation.mvc.Models
{
    public class TipoRelacaoCategoriaViewModel
    {
        public string Id { get; set; }
        public int CategoriaId  { get; set; }
        public int TipoRelacaoId { get; set; }
        public string CategoriaDescricao { get; set; }
        public string TipoRelacaoDescricao { get; set; }
    }
}
