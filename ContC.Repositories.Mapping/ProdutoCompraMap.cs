using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class ProdutoCompraMap : ClassMap<ProdutoCompra>
    {

        public ProdutoCompraMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Validade);
            Map(p => p.Valor);
            References(p => p.Produto).Column(Constantes.ID_PRODUTO);
            References(p => p.Compra).Column(Constantes.ID_COMPRA);
        }
    }
}
