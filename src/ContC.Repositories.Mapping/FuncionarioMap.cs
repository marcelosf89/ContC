using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class FuncionarioMap : ClassMap<Funcionario>
    {

        public FuncionarioMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome);
            Map(p => p.Email);

            Map(p => p.DataInicio);
            Map(p => p.DataTermino);
            Map(p => p.Telefone);
            References(p => p.Lider).Column(Constantes.ID_LIDER);

            References(p => p.TipoPagamento);
            References(p => p.TipoRegimeFuncionario);
            Map(p => p.Valor);

            Map(p => p.Identificacao2);
            Map(p => p.Identificacao1);
            Map(p => p.Nascimento);
        }
    }
}
