using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;

namespace ContC.presentation.mvc.Models
{
    public class ApropriacaoViewModel
    {

        [Display(Name = "EmpresaId")]
        public string EmpresaId { get; set; }

        [Display(Name = "Competência")]
        public string CompetenciaId { get; set; }

        [Display(Name = "Competência")]
        public IEnumerable<SelectListItem> Competencias { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data inicial")]
        public DateTime? DataInicial { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data final")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Centro de custo")]
        public string CentroDeCustoId { get; set; }

        [Display(Name = "Centro de custo")]
        public IEnumerable<SelectListItem> CentroDeCustos { get; set; }

        [Display(Name = "Categoria")]
        public string CategoriaId { get; set; }

        [Display(Name = "Categoria")]
        public IEnumerable<SelectListItem> Categorias { get; set; }

        [Required]
        [Display(Name = "Localidade")]
        public string LocalidadeId { get; set; }

        [Display(Name = "Localidade")]
        public IEnumerable<SelectListItem> Localidades { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Hh diário")]
        public decimal HhDiario { get; set; }

        [Display(Name = "Inclui Finais de Semana?")]
        public bool IncluirFinaisDeSemana { get; set; }

        [Display(Name = "Inclui Feriados?")]
        public bool IncluirFeriados { get; set; }

        [Display(Name = "Aprovação Automática?")]
        public bool AprovarAutomaticamente { get; set; }
        
        public bool HabilitaAprovarAutomaticamente { get; set; }

        public IList<ApontamentoDTO> Apontamentos { get; set; } 

    }
}