using ContC.crosscutting.DataContracts;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Models.FuncionarioModels
{
    public class FuncionarioManterModel
    {
        public FuncionarioManterModel()
        {
            FuncionarioContaContract = new FuncionarioContaContract();
        }

        public FuncionarioContaContract FuncionarioContaContract { get; set; }

        public IEnumerable<SelectListItem> Lideres { get; set; }

        public IEnumerable<SelectListItem> TipoPagamentos { get; set; }

        public IEnumerable<SelectListItem> TipoRegimeFuncionarios { get; set; }

        public IEnumerable<SelectListItem> Bancos { get; set; }

    }
}
