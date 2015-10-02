using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class FeriadoMap : ClassMap<Feriado>
    {

        public FeriadoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Data);
            Map(p => p.Descricao);
        }
    }
}
