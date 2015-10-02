using ContC.Extension.EA.domain.entities.Models;
using ContC.Extension.EA.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.domain.services.Implementations
{
    public class ReceitasRepository : Repository<Receita>, IReceitasRepository
    {
        public ReceitasRepository()
        {

        }
        public IEnumerable<Receita> GetAll()
        {
            return (from a in this.SessaoAtual.Query<Receita>()
                    select a);
        }
        public IEnumerable<entities.ReceitaDTO> GetReceita(DateTime dateTime, DateTime now)
        {
            String sql = @"select fp.idformapgto, sum(fp.valor) from lancamento l 
                join formapgtodocecf fp on fp.idformapgtodocecf = l.idformapgtodocecf
                where fp.idusuariocancelou is null
                    and TO_TIMESTAMP(dthrlancamento) between 
                        CAST('" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + @"' as timestamp) and
                        CAST('" + now.ToString("yyyy-MM-dd HH:mm:ss") + @"' as timestamp)
                group by fp.idformapgto";

            System.Collections.IList o = this.SessaoAtual.CreateSQLQuery(sql).List();

            IList<entities.ReceitaDTO> lr = new List<entities.ReceitaDTO>();
            foreach (object[] item in o)
            {
                lr.Add(new entities.ReceitaDTO()
                {
                    TipoReceita = Convert.ToInt32(item[0]),
                    Valor = Convert.ToDecimal(item[1])
                });
            }
            return lr;
        }
    }
}
