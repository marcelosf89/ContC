using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface ICompraRepository : IRepository<Compra>
    {
        IList<entities.DTO.CompraDTO> GetCompraDTOByEmpresaFOrnecedorData(int empresaId, string dataInicio, string dataFinal, string fornecedor);

        IList<Boleto> GetBoletos(int compraId);

        IList<ProdutoCompra> GetProdutos(int compraId);
    }
}
