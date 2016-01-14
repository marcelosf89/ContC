using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using System.Collections.Generic;
using NHibernate.Linq;
using System.Linq;
using System;

namespace ContC.domain.services.Implementations
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository()
        {

        }
        
        public Usuario Get(string userName)
        {
            return this.SessaoAtual.Query<Usuario>()
                .Where(p => p.Funcionario.Email.ToUpper().Equals(userName.ToUpper()))
                .SingleOrDefault();
        }


        // TODO: Testar repositório
        public IList<Funcionario> GetAllByUsuarios(string startsWith, int empresaId, int maxRows)
        {
            return (from f in SessaoAtual.Query<Funcionario>().Take(maxRows)
                    from e in f.Empresas
                   where f.Email.ToUpper().Contains(startsWith.ToUpper()) && e.Id == empresaId
                  select f).ToList();
        }

        public Usuario GetFetchingFuncionario(string userName)
        {
            return this.SessaoAtual.Query<Usuario>().Fetch(p => p.Funcionario)
                .Where(p => p.Funcionario.Email.ToUpper().Equals(userName.ToUpper()))
                .SingleOrDefault();
        }

        public IList<int> GetGruposIds(string email)
        {

            string consulta = 
                @"SELECT DISTINCT e.id_grupo
                    FROM usuarios u
                    JOIN funcionarios f
                      ON u.funcionario_id = f.id_funcionario
                    JOIN funcionarioenderecos fe
                      ON u.funcionario_id = fe.id_funcionario
                    JOIN empresas e 
                      ON fe.id_empresa = e.id_empresa
                   WHERE f.email = lower(:email)";

            return
                   SessaoAtual.CreateSQLQuery(consulta)
                              .SetParameter("email", email.ToLower())
                              .List<int>();
                   
        }
    }
}
