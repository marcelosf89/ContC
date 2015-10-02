using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class FornecedorMap : ClassMap<Fornecedor>
    {

        public FornecedorMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.RazaoSocial);
            Map(p => p.CNPJ);

            Map(p => p.InscricaoEstadual);
            Map(p => p.Vendedor);
            Map(p => p.EmailVendador);
            Map(p => p.TelefoneVendedor);
            Map(p => p.Observacao);
            References(p => p.Grupo).Column(Constantes.ID_GRUPO);

        }
    }
}
