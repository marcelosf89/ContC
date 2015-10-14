using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Text;
using System.Threading.Tasks;
using ContC.domain.entities.DTO;

namespace ContC.domain.services.Implementations
{
    public class NotaItemRepository : Repository<NotaItem>, INotaItemRepository
    {

        public IList<NotaItem> GetItensByNotas(int notaId)
        {
            return (from a in this.SessaoAtual.Query<NotaItem>()
                    where a.Lista.Id == notaId
                    select a).ToList();
        }
    }
}
