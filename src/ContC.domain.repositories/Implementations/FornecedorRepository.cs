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
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository()
        {

        }


        public IList<Fornecedor> GetAllByGrupo(int grupoId)
        {
            return (from a in SessaoAtual.Query<Fornecedor>()
                    where a.Grupo.Id == grupoId
                    select a).ToList();
        }


        public IList<Fornecedor> GetAllByEmpresaCategoria(string startsWith, int empresaId, int categoriaId, int maxRows)
        {
            int grupoId = (from a in this.SessaoAtual.Query<Empresa>()
                           where a.Id == empresaId
                           select a.Grupo.Id).SingleOrDefault();

            return (from a in SessaoAtual.Query<Fornecedor>()
                    where a.Grupo.Id == grupoId && a.RazaoSocial.ToUpper().StartsWith(startsWith.ToUpper())
                    select a).ToList();
        }
    }
}
