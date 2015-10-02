using ContC.Extension.EA.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Repositories.Mapping
{
    public class ReceitaMap : ClassMap<Receita>
    {

        public ReceitaMap()
        {
            Table("lancamento");
            Id(p => p.Id).Column("idlancamento");
        }
    }
}
