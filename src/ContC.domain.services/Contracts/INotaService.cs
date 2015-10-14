using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface INotaService : IService<Nota>
    {
        IList<Nota> GetNotasByEmpresaUsuario(int empresaId, string email);

        IList<NotaItem> GetItensByNotas(int notaId);
    }
}
