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
    public interface IBoletoService : IService<Boleto>
    {
        IList<ContasDTO> GetContasByDashboard(int empresaId, int quantidadesDia);

        void Insert(Boleto b, System.IO.FileInfo fileInfo);

        IList<ContasDTO> GetContasByEmpresaPeriodo(int empresaId, DateTime dateTime1, DateTime dateTime2);

        void Remove(Boleto boleto);
    }
}
