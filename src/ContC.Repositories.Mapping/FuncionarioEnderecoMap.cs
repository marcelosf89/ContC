using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping
{
    public class FuncionarioEnderecoMap : ClassMap<FuncionarioEndereco>
    {

        public FuncionarioEnderecoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            References(p => p.Funcionario).Column(Constantes.ID_FUNCIONARIO);
            References(p => p.Empresa).Column(Constantes.ID_EMPRESA);

        }
    }
}
