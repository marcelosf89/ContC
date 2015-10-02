
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.EmpresaModels
{
    public class EmpresaNovoModel
    {
        public String Codigo { get; set; }
        public String RazaoSocial { get; set; }
        public String NomeFantasia { get; set; }
        public String CNPJ { get; set; }
        public String Rua { get; set; }
        public String Bairro { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        private string _pais;
        public String Pais { get { return "Brasil"; } set { _pais = value; } }
        public String Complemento { get; set; }
        public String Numero { get; set; }
        public String CodigoPostal { get; set; }


        public int GroupId { get; set; }

        public int EmpresaId { get; set; }

        public int EnderecoId { get; set; }
    }
}
