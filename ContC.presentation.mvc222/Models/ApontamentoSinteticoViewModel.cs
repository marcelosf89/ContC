using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContC.presentation.mvc.CustomValidators;

namespace ContC.presentation.mvc.Models
{
    public class ApontamentoSinteticoViewModel
    {
        public ApontamentoSinteticoViewModel()
        {
            Empresas = new EmpresaSelectionViewModel();
            Competencias = new CompetenciaSelectionViewModel();
            Usuarios = new UsuarioSelectionViewModel();
        }

        public string EmpresaId { get; set; }
        public EmpresaSelectionViewModel Empresas { get; set; }
        public string UsuarioId { get; set; }
        public UsuarioSelectionViewModel Usuarios { get; set; }
        public string CompetenciaId { get; set; }
        public CompetenciaSelectionViewModel Competencias { get; set; }

    }
}