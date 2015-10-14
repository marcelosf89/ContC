using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class NotaUsuarioMap : ClassMap<NotaUsuario>
    {

        public NotaUsuarioMap()
        {
            Id(p => p.Id).GeneratedBy.Native();
            References(p => p.Lista).Column(Constantes.ID_NOTA);
            References(p => p.Usuario).Column(Constantes.ID_USUARIO);
        }
    }
}
