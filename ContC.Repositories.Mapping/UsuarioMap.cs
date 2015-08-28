using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class UsuarioMap : SubclassMap<Usuario>
    {

        public UsuarioMap()
        {
            Table("usuarios");
            

            Map(p => p.Sigla);
            Map(p => p.Situacao);
        }
    }
}
