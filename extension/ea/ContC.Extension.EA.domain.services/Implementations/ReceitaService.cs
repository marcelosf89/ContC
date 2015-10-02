using ContC.Extension.EA.domain.entities;
using ContC.Extension.EA.domain.entities.Models;
using ContC.Extension.EA.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.domain.services.Implementations
{
    public class ReceitaService : Service<Receita>, IReceitaService
    {
        public ReceitaService(IReceitasRepository repository)
        {
            base._repository = repository;
        }



        public IEnumerable<Receita> GetAll()
        {
            return null;
        }


        public IEnumerable<ReceitaDTO> GetReceita(DateTime dateTime , DateTime now)
        {
            return ((IReceitasRepository)base._repository).GetReceita(dateTime, now);
        }
    }
}
