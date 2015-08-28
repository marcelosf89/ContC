using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class GrupoRepository : Repository<Grupo>, IGrupoRepository
    {
        public GrupoRepository()
        {

        }


        public Grupo GetByCode(string code)
        {
            return (from a in this.SessaoAtual.Query<Grupo>()
                    where a.Nome.ToUpper().Equals(code.ToUpper())
                    select a).SingleOrDefault();
        }


        public IList<Grupo> GetAllGrupo(string nomeResponsavel)
        {
            return (from a in this.SessaoAtual.Query<Grupo>()
                    where a.Responsavel.Email.ToUpper().Equals(nomeResponsavel.ToUpper())
                    select a).ToList();
        }
    }
}
