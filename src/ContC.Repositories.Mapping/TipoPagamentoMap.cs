using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class TipoPagamentoMap : ClassMap<TipoPagamento>
    {

        public TipoPagamentoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome);
        }
    }
}
