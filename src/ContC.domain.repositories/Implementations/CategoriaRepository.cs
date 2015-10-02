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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {

        public IList<entities.DTO.ContasDTO> GetContasByDashboard(int empresaId, int quantidadesDia)
        {
            DateTime dt = DateTime.Now.AddDays(quantidadesDia);
            return (from a in SessaoAtual.Query<Boleto>()
                    where a.Empresa.Id == empresaId && a.DataPagamento == null && a.DataVencimento < dt
                    select new ContasDTO()
                    {
                        Id = a.Id,
                        EmpresaId = empresaId,
                        Empresa = a.Empresa.NomeFantasia,
                        Fornecedor = a.Fornecedor.RazaoSocial,
                        FornecedorId = a.Fornecedor.Id,
                        Numero = a.Numero,
                        Valor = a.Valor,
                        DataPagamento = a.DataPagamento,
                        DataVencimento = a.DataVencimento
                    }).ToList();

        }

        public IEnumerable<Categoria> GetByEmpresa(int empresaId)
        {
            Empresa emp = (from a in this.SessaoAtual.Query<Empresa>()
                               where a.Id == empresaId
                               select a).SingleOrDefault();

            return (from a in this.SessaoAtual.Query<Categoria>()
                    where a.Grupo.Id == emp.Grupo.Id
                    select a);
        }
    }
}
