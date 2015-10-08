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
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository()
        {

        }


        public void teste()
        {
           Produto pro =  this.Find(1);
        }



        public IList<Produto> GetAllByEmpresaCategoria(string startsWith, int empresaId, int categoriaId, int maxRows)
        {
            Empresa emp = this.SessaoAtual.QueryOver<Empresa>().Where(p => p.Id == empresaId).SingleOrDefault();
            startsWith = startsWith.ToUpper();

            IEnumerable<Produto> prod = (from a in this.SessaoAtual.Query<Produto>()
                                         where a.Descricao.ToUpper().Contains(startsWith) && a.Grupo.Id == emp.Grupo.Id
                                         select a).Take(maxRows);

            return prod.ToList();
    
        }


        public Produto GetByName(string produto, int empresaId)
        {
            Empresa emp = this.SessaoAtual.QueryOver<Empresa>().Where(p => p.Id == empresaId).SingleOrDefault();
            produto = produto.ToUpper();

            Produto prod = (from a in this.SessaoAtual.Query<Produto>()
                                         where a.Descricao.ToUpper().Equals(produto) && a.Grupo.Id == emp.Grupo.Id
                                         select a).SingleOrDefault();
            return prod;
        }
    }
}
