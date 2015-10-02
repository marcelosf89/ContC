using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class GrupoMap : ClassMap<Grupo>
    {

        public GrupoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome);
            Map(p => p.Email);
            Map(p => p.Sigla);
            Map(p => p.Situacao);
            Map(p => p.DataInicio);
            Map(p => p.DataTermino);
            References(p => p.Responsavel).Column(Constantes.ID_RESPONSAVEL).LazyLoad();

        }
    }
}
