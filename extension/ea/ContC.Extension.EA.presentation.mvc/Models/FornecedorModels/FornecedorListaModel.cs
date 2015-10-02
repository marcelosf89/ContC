using ContC.Extension.EA.domain.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.presentation.mvc.Models.FornecedorModels
{
    public class FornecedorListaModel
    {
        public int GrupoId { get; set; }
        public IList<Fornecedor> Fornecedores { get; set; }
    }
}
