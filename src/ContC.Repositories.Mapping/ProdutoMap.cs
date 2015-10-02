using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class ProdutoMap : ClassMap<Produto>
    {

        public ProdutoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome);
            Map(p => p.Numero);
            Map(p => p.Descricao);
            Map(p => p.ValorMedio);
            References(p => p.Grupo).Column(Constantes.ID_GRUPO);
        }
    }
}
