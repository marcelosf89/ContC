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
        public ProdutoService(IProdutoRepository repository)
        {
            base._repository = repository;
        }


        public void teste()
        {
           Produto pro =  _repository.Find(1);
        }

    }
}
