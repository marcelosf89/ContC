using Entity.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class Pagamento : Entidade
    {
        public virtual int Id { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual DateTime? DataPagamento { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual Empresa Empresa { get; set; }

        public virtual string StatusAprovacao
        {
            get
            {
                if (DataPagamento.HasValue)
                {
                    return "PAGO";
                }
                else if (!DataPagamento.HasValue && DataPagamento.Value.Date < DateTime.Now.Date)
                {
                    return "AGENDADO";
                }
                else if (!DataPagamento.HasValue && DataPagamento.Value.Date == DateTime.Now.Date)
                {
                    return "AGENDADO ( HOJE )";
                }
                else if (!DataPagamento.HasValue && DataPagamento.Value.Date > DateTime.Now.Date)
                {
                    return "ATRASADO";
                }
                return "UNDEFINED";
            }
        }
    }
}
