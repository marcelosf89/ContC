using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class EmpresaMap : ClassMap<Empresa>
    {

        public EmpresaMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.RazaoSocial);
            Map(p => p.NomeFantasia);
            References(p => p.Grupo).Column(Constantes.ID_GRUPO);


            Map(p => p.CNPJ);
            Map(p => p.Email);
            Map(p => p.Rua);
            Map(p => p.Complemento);
            Map(p => p.Bairro);
            Map(p => p.Cidade);
            Map(p => p.CodigoPostal);
            Map(p => p.Estado);
            Map(p => p.Pais);
            Map(p => p.IsMatriz);
            Map(p => p.Numero);
        }
    }
}
