using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class ReceitaMap : ClassMap<Receita>
    {

        public ReceitaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Descricao);
            Map(p => p.Valor);
            Map(p => p.DataCadastro);
            Map(p => p.DataRecebimento);
            References(p => p.Endereco).Column(Constantes.ID_EMPRESA);
            References(p => p.TipoReceita).Column(Constantes.ID_TIPORECEITA);
            References(p => p.Usuario).Column(Constantes.ID_USUARIO);
        }
    }
}
