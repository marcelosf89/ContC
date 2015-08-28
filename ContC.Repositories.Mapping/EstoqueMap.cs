using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class EstoqueMap : ClassMap<Estoque>
    {

        public EstoqueMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Numero);
            Map(p => p.Nome);
            References(p => p.Empresa).Column(Constantes.ID_EMPRESA);

        }
    }
}
