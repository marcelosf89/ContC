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
    public class ReceitaRepository : Repository<Receita>, IReceitaRepository
    {
        public ReceitaRepository()
        {

        }


        public IEnumerable<Banco> GetAll()
        {
            return (from a in this.SessaoAtual.Query<Banco>()
                    select a);
        }


        public bool HasByCommunicationId(string communicationId)
        {
            communicationId = communicationId.ToLower();
            return (from a in this.SessaoAtual.Query<Receita>()
                    where a.CommunicationId != null && a.CommunicationId.ToLower().Equals(communicationId)
                    select a.Id).Any();
        }


        public IList<entities.DTO.ReceitasDTO> GetReceitasByEmpresaPeriodo(int empresaId, DateTime inicio, DateTime final)
        {
            return (from a in this.SessaoAtual.Query<Receita>()
                    where a.Endereco.Id == empresaId && a.DataCadastro > inicio && a.DataCadastro < final
                    group a by new { a.TipoReceita.Descricao } into g
                    select new ReceitasDTO()
                    {
                        Tipo = g.Key.Descricao,
                        Valor = g.Sum(x => x.Valor)
                    }).ToList();
        }
    }
}
