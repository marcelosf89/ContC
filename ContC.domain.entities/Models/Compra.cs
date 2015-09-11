using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Compra : Entidade
    {
        //TODO Solucao para problema de conversao datetime/datetime2 do Entity: DateTime?
        //http://stackoverflow.com/questions/18795762/entity-framework-cannot-save-conversion-of-datetime2-to-datetime-failing

        public virtual int Id { get; set; }
        public virtual string OrdenDeServico { get; set; }
        public virtual decimal ValorTotal { get; set; }

        public virtual decimal ValorTotalProdutos { get; set; }
        public virtual string NotaFiscal { get; set; }

        public virtual DateTime Data { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual int TipoPagamento { get; set; }

        public virtual decimal ValorFrete { get; set; }
        public virtual decimal ValorDespesaAdministrativa { get; set; }
        public virtual decimal ValorSeguro { get; set; }
        public virtual decimal Desconto { get; set; }


        public virtual decimal ValorIPINota { get; set; }
        public virtual decimal ValorICMSNota { get; set; }
        public virtual decimal ValorTotalNota { get; set; }
    }
}
