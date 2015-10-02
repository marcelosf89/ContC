using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class CategoriaMap : ClassMap<Categoria>
    {

        public CategoriaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Descricao);
            References(p => p.Grupo).Column(Constantes.ID_GRUPO);

        }
    }
}
