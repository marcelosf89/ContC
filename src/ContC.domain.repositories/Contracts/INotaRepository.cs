using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface INotaRepository : IRepository<Nota>
    {
        IList<Nota> GetNotasByEmpresaUsuario(int empresaId, string email);
    }
}
