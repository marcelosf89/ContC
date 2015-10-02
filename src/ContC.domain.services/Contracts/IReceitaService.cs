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
    public interface IReceitaService : IService<Receita>
    {
        IList<ReceitasDTO> GetReceitasByEmpresaPeriodo(int empresaId, DateTime inicio, DateTime final);
    }
}
