
using ContC.Extension.EA.domain.entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.presentation.mvc.Models.EmpresaModels
{
    public class EmpresaPartialModel
    {
        public int GrupoId { get; set; }
        public IList<Empresa> Empresas { get; set; }

    }
}
