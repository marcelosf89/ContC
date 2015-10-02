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
    public class FornecedorService : Service<Fornecedor>, IFornecedorService
    {
        public FornecedorService(IFornecedorRepository repository)
        {
            base._repository = repository;
        }



        public IList<Fornecedor> GetAllByGrupo(int grupoId)
        {
            return ((IFornecedorRepository)base._repository).GetAllByGrupo(grupoId);
        }


        public IList<Fornecedor> GetAllByEmpresaCategoria(string startsWith, int empresaId, int categoriaId, int maxRows)
        {
            return ((IFornecedorRepository)base._repository).GetAllByEmpresaCategoria( startsWith,  empresaId,  categoriaId,  maxRows);
        }
    }
}
