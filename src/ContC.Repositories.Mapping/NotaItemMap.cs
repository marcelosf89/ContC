using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class NotaItemMap : ClassMap<NotaItem>
    {

        public NotaItemMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Titulo);
            Map(p => p.Cadastro);
            Map(p => p.Concluido);
            References(p => p.ConcluidoPor).Column(Constantes.ID_CONCLUIDO_POR);
            References(p => p.CadastradoPor).Column(Constantes.ID_CADASTRADO_POR);
            References(p => p.Lista).Column(Constantes.ID_NOTA);
        }
    }
}
