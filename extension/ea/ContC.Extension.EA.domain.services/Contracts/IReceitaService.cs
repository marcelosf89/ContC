using ContC.Extension.EA.domain.entities;
using ContC.Extension.EA.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.domain.services.Contracts
{
    public interface IReceitaService : IService<Receita>
    {
        IEnumerable<ReceitaDTO> GetReceita(DateTime dateTime, DateTime now);
    }
}
