using ContC.Repositories.Mapping;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.entities.Models
{
    public class ContaMap : ClassMap<Conta>
    {
        public ContaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Agencia);
            Map(p => p.NumeroConta);
            Map(p => p.Extensao);
            References(p => p.Funcionario).Column(Constantes.ID_FUNCIONARIO);
            References(p => p.Banco).Column(Constantes.ID_BANCO);

        }
    }
}
