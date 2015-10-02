using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.presentation.mvc.Models.FornecedorModels
{
    public class FornecedorManterModel
    {
        public int GrupoId { get; set; }
        public  int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Vendedor { get; set; }
        public string EmailVendador { get; set; }
        public string TelefoneVendedor { get; set; }
        public string Observacao { get; set; }
    }
}
