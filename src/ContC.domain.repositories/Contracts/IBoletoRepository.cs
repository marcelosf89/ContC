using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface IBoletoRepository : IRepository<Boleto>
    {

        IList<ContasDTO> GetContasByDashboard(int empresaId, int quantidadesDia);

        IList<ContasDTO> GetContasByDashboard(int empresaId, DateTime inicio, DateTime termino);
    }
}
