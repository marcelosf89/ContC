using ContC.domain.entities.DTO;
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
    public class CategoriaService : Service<Categoria>, ICategoriaService
    {
        public CategoriaService(ICategoriaRepository repository)
        {
            base._repository = repository;
        }

        public IEnumerable<Categoria> GetByEmpresa(int empresaId)
        {
            return ((ICategoriaRepository)_repository).GetByEmpresa(empresaId);
        }
    }
}
