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
    public class BoletoRepository : Repository<Boleto>, IBoletoRepository
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


        public IList<ContasDTO> GetContasByDashboard(int empresaId, DateTime inicio, DateTime termino)
        {
            return (from a in SessaoAtual.Query<Boleto>()
                    where 
                    a.Empresa.Id == empresaId && 
                    a.DataVencimento.Date >= inicio && 
                    a.DataVencimento.Date <= termino
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
    }
}
