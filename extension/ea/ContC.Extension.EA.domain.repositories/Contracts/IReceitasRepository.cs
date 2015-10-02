using ContC.Extension.EA.domain.entities.Models;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.domain.services.Contracts
{
    public interface IReceitasRepository : IRepository<Receita>
    {

        IEnumerable<Receita> GetAll();

        IEnumerable<entities.ReceitaDTO> GetReceita(DateTime dateTime, DateTime now);
    }
}
