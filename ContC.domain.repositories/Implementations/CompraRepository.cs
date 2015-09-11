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
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public IList<entities.DTO.CompraDTO> GetCompraDTOByEmpresaFOrnecedorData(int empresaId, string dataInicio, string dataFinal, string fornecedor)
        {
            fornecedor = fornecedor.ToUpper();

            if (!String.IsNullOrEmpty(dataInicio) && !String.IsNullOrEmpty(dataFinal))
            {
                return GetCompraDTOByEmpresaFornecedorDataInicioFim(empresaId, dataInicio, dataFinal, fornecedor);
            }
            else if (!String.IsNullOrEmpty(dataInicio))
            {
                return GetCompraDTOByEmpresaFornecedorDataInicio(empresaId, dataInicio, fornecedor);
            }
            else
            {
                return GetCompraDTOByEmpresaFornecedor(empresaId, fornecedor);
            }
        }

        private IList<CompraDTO> GetCompraDTOByEmpresaFornecedor(int empresaId, string fornecedor)
        {
            return (from a in this.SessaoAtual.Query<Compra>()
                    where a.Empresa.Id == empresaId && a.Fornecedor.RazaoSocial.ToUpper().StartsWith(fornecedor)
                    select
                    new CompraDTO()
                    {
                        Id = a.Id,
                        Valor = a.ValorTotal,
                        NumeroNota = a.NotaFiscal,
                        EmpresaId = empresaId,
                        Entrega = a.Data,
                        Fornecedor = a.Fornecedor.RazaoSocial
                    }
                    ).ToList();
        }

        private IList<CompraDTO> GetCompraDTOByEmpresaFornecedorDataInicio(int empresaId, string dataInicio, string fornecedor)
        {
            DateTime dtI = Convert.ToDateTime(dataInicio);

            return (from a in this.SessaoAtual.Query<Compra>()
                    where a.Empresa.Id == empresaId && a.Data.Date > dtI.Date && a.Fornecedor.RazaoSocial.ToUpper().StartsWith(fornecedor)
                    select
                    new CompraDTO()
                    {
                        Id = a.Id,
                        Valor = a.ValorTotal,
                        NumeroNota = a.NotaFiscal,
                        EmpresaId = empresaId,
                        Entrega = a.Data,
                        Fornecedor = a.Fornecedor.RazaoSocial
                    }
                    ).ToList();
        }

        private IList<CompraDTO> GetCompraDTOByEmpresaFornecedorDataInicioFim(int empresaId, string dataInicio, string dataFinal, string fornecedor)
        {
            DateTime dtI = Convert.ToDateTime(dataInicio);
            DateTime dtF = Convert.ToDateTime(dataFinal);
            return (from a in this.SessaoAtual.Query<Compra>()
                    where a.Empresa.Id == empresaId && a.Data.Date > dtI.Date && a.Data.Date < dtF.Date && a.Fornecedor.RazaoSocial.ToUpper().StartsWith(fornecedor)
                    select
                    new CompraDTO()
                    {
                        Id = a.Id,
                        Valor = a.ValorTotal,
                        NumeroNota = a.NotaFiscal,
                        EmpresaId = empresaId,
                        Entrega = a.Data,
                        Fornecedor = a.Fornecedor.RazaoSocial
                    }
             ).ToList();
        }


        public IList<Boleto> GetBoletos(int compraId)
        {
            return (from a in this.SessaoAtual.Query<Boleto>()
                    where a.Compra != null && a.Compra.Id == compraId
                    select a).ToList();
        }

        public IList<ProdutoCompra> GetProdutos(int compraId)
        {
            return (from a in this.SessaoAtual.Query<ProdutoCompra>()
                    where a.Compra != null && a.Compra.Id == compraId
                    select a).ToList();
        }
    }
}
