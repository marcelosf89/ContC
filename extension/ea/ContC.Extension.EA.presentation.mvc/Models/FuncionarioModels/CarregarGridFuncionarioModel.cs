using ContC.Extension.EA.domain.entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContC.Extension.EA.presentation.mvc.Models.FuncionarioModels
{
    public class CarregarGridFuncionarioModel
    {
        public int EmpresaId { get; set; }
        public virtual IList<Funcionario> Funcionarios { get; set; }
    }
}
