using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        private IEmpresaService _empreseService;
        public ProdutoService(IProdutoRepository repository, IEmpresaService empreseService)
        {
            base._repository = repository;
            _empreseService = empreseService;
        }


        public void teste()
        {
           Produto pro =  _repository.Find(1);
        }



        public IList<Produto> GetAllByEmpresaCategoria(string startsWith, int empresaId, int categoriaId, int maxRows)
        {
            return ((IProdutoRepository)_repository).GetAllByEmpresaCategoria(startsWith, empresaId, categoriaId, maxRows);
        }


        public Produto GetByName(string produto, int empresaId)
        {
            return ((IProdutoRepository)_repository).GetByName(produto, empresaId);

        }

        public void Insert(Produto produto, int empresaId)
        {
            Empresa emp = _empreseService.Find(empresaId);
            produto.Grupo = emp.Grupo;
            base.Insert(produto);
        }
    }
}
