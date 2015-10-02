using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface ICompraService : IService<Compra>
    {
        void Insert(Compra compra, IList<ProdutoCompra> lprods, IList<Boleto> lbs);

        IList<entities.DTO.CompraDTO> GetCompraDTOByEmpresaFOrnecedorData(int empresaId, string dataInicio, string dataFinal, string fornecedor);

        IList<ProdutoCompra> GetProdutos(int compraId);

        IList<Boleto> GetBoletos(int compraId);
    }
}
