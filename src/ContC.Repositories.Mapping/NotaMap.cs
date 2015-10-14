using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class NotaMap : ClassMap<Nota>
    {

        public NotaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Titulo);
            Map(p => p.Cadastro);
            Map(p => p.Concluido);
            References(p => p.Empresa).Column(Constantes.ID_EMPRESA);
            References(p => p.Cadastrado).Column(Constantes.ID_CADASTRADO);
        }
    }
}
