using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class EstoqueProdutoMap : ClassMap<EstoqueProduto>
    {

        public EstoqueProdutoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Quantidade);
            Map(p => p.ValorMedio);
            Map(p => p.ValorVenda);
            References(p => p.Produto).Column(Constantes.ID_PRODUTO);
            References(p => p.Estoque).Column(Constantes.ID_ESTOQUE);


        }
    }
}
