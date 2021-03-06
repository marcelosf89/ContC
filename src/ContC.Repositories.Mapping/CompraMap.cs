﻿using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class CompraMap : ClassMap<Compra>
    {

        public CompraMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.OrdenDeServico);
            Map(p => p.ValorTotal);
            Map(p => p.Data);
            Map(p => p.ValorTotalProdutos);
            Map(p => p.NotaFiscal);
            Map(p => p.TipoPagamento);
            
            Map(p => p.ValorFrete);
            Map(p => p.ValorDespesaAdministrativa);
            Map(p => p.ValorSeguro);
            Map(p => p.Desconto);

            Map(p => p.ValorIPINota);
            Map(p => p.ValorICMSNota);
            Map(p => p.ValorTotalNota);
            
            References(p => p.Usuario).Column(Constantes.ID_USUARIO);
            References(p => p.Categoria).Column(Constantes.ID_CATEGORIA);
            References(p => p.Empresa).Column(Constantes.ID_EMPRESA);
            References(p => p.Fornecedor).Column(Constantes.ID_FORNECEDOR);
        }
    }
}
