using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class BoletoMap : ClassMap<Boleto>
    {

        public BoletoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Numero);
            Map(p => p.DataVencimento);
            Map(p => p.Data);
            Map(p => p.DataPagamento);
            Map(p => p.Valor);
            Map(p => p.MotivoCancelamento);
            Map(p => p.DataCancelamento);
            References(p => p.Compra).Column(Constantes.ID_COMPRA);
            References(p => p.Empresa).Column(Constantes.ID_EMPRESA);
            References(p => p.Fornecedor).Column(Constantes.ID_FORNECEDOR);
            References(p => p.UsuarioCancelamento).Column(Constantes.ID_USUARIO_CANCELAMENTO);

            Where(Constantes.ID_USUARIO_CANCELAMENTO + " is null");
        }
    }
}
