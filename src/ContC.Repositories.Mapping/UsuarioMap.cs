using ContC.domain.entities.Models;
using FluentNHibernate.Mapping;

namespace ContC.Repositories.Mapping
{
    public class UsuarioMap : ClassMap<Usuario>
    {

        public UsuarioMap()
        {
            Table("usuarios");

            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Sigla);
            Map(p => p.Situacao);
            Map(p => p.Senha);
            HasOne(p => p.Funcionario);
        }
    }
}
