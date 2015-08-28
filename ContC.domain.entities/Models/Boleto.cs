using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Pattern;

namespace ContC.domain.entities.Models
{
    public class Boleto : Entidade
    {
        //TODO Solucao para problema de conversao datetime/datetime2 do Entity: DateTime?
        //http://stackoverflow.com/questions/18795762/entity-framework-cannot-save-conversion-of-datetime2-to-datetime-failing

        public virtual int Id { get; set; }
        public virtual string Numero { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime? DataPagamento { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }

        public virtual string StatusAprovacao
        {
            get
            {
                if (DataPagamento.HasValue)
                {
                    return "PAGO";
                }
                else if (!DataPagamento.HasValue && DataPagamento.Value < DateTime.Now)
                {
                    return "AGENDADO";
                }
                else if (!DataPagamento.HasValue && DataPagamento.Value == DateTime.Now)
                {
                    return "AGENDADO ( HOJE )";
                }
                else if (!DataPagamento.HasValue && DataPagamento.Value > DateTime.Now)
                {
                    return "ATRASADO";
                }
                return "UNDEFINED";
            }
        }

        public virtual String MotivoCancelamento { get; set; }
        public virtual DateTime DataCancelamento { get; set; }
        public virtual Usuario UsuarioCancelamento { get; set; }
    }
}
