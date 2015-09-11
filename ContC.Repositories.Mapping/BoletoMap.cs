using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{

    public class PagamentoMap : ClassMap<Pagamento>
    {
        public PagamentoMap()
        {
            Table("boletos");
            DiscriminateSubClassesOnColumn("discriminator");

            Id(p => p.Id).Column("id_boleto").GeneratedBy.Identity();


            Map(p => p.Data);
            Map(p => p.DataPagamento);
            Map(p => p.Valor);
            References(p => p.Empresa).Column(Constantes.ID_EMPRESA);

            Where(Constantes.ID_USUARIO_CANCELAMENTO + " is null");
        }
    }

    public class PagamentoDiretoMap : SubclassMap<PagamentoDireto>
    {
        public PagamentoDiretoMap()
        {
            DiscriminatorValue("PGD");
            References(p => p.Compra).Column(Constantes.ID_COMPRA);

        }
    }

    public class BoletoMap : SubclassMap<Boleto>
    {

        public BoletoMap()
        {
            DiscriminatorValue("BOL");
            Map(p => p.Numero);
            Map(p => p.DataVencimento);
            Map(p => p.MotivoCancelamento);
            Map(p => p.DataCancelamento);
            References(p => p.Fornecedor).Column(Constantes.ID_FORNECEDOR);
            References(p => p.Compra).Column(Constantes.ID_COMPRA);
            References(p => p.UsuarioCancelamento).Column(Constantes.ID_USUARIO_CANCELAMENTO);
        }
    }
}
