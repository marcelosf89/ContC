using ContC.CorssCutting.Exceptions;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class CompraService : Service<Compra>, ICompraService
    {
        private IProdutoCompraRepository _iprodutoCompraRep;
        private IBoletoService _iboletoService;
        private IPagamentoDiretoRepository _iPagamentoDiretoRepository;
        private IFornecedorService _fornecedorService;

        public CompraService(ICompraRepository repository, IProdutoCompraRepository iprodutoCompraRep, IBoletoService iboletoService, IPagamentoDiretoRepository iPagamentoDiretoRepository, IFornecedorService fornecedorService)
        {
            base._repository = repository;
            _iprodutoCompraRep = iprodutoCompraRep;
            _iboletoService = iboletoService;
            _iPagamentoDiretoRepository = iPagamentoDiretoRepository;
            _fornecedorService = fornecedorService;
        }



        public void Insert(Compra compra, IList<ProdutoCompra> lprods, IList<Boleto> lbs)
        {
            try
            {
                ValidateInsert(compra, lprods, lbs);

                base.Update(compra);
                InserirProdutosNaCompra(compra, lprods);

                if (lbs != null && lbs.Any())
                {
                    InserirBoletosNaCompra(compra, lbs);
                }
                else
                {
                    InserirPagamentoDiretoNaCompra(compra);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateInsert(Compra compra, IList<ProdutoCompra> lprods, IList<Boleto> lbs)
        {
            if (!lprods.Any()) { throw new ExceptionMessage("A Nota tem que pelo menos 1 (um) produto."); }
            if (compra.TipoPagamento == 1 && !lbs.Any()) { throw new ExceptionMessage("Nao existem boletos para essa compra. Favor preencher o boleto ou mudar o Tipo de Pagamento"); }
            if (compra.TipoPagamento <= 0) { throw new ExceptionMessage("O Tipo de Pagamento tem que ser preenchido."); }
            if (String.IsNullOrEmpty(compra.NotaFiscal)) { throw new ExceptionMessage("O Numero da Nota é obrigatório."); }
            if (compra.Fornecedor == null || String.IsNullOrEmpty(compra.Fornecedor.RazaoSocial)) { throw new ExceptionMessage("O Fornecedor é obrigatório."); }

            compra.Fornecedor = _fornecedorService.Find(compra.Fornecedor.Id);
            if (compra.Fornecedor == null) { throw new ExceptionMessage("O Fornecedor não Existe. Favor incluir um fornecedor valido."); }
        }

        private void InserirPagamentoDiretoNaCompra(Compra compra)
        {
            IList<Boleto> bols = this.GetBoletos(compra.Id);
            foreach (Boleto bR in bols)
            {
                _iboletoService.Delete(bR);
            }

            PagamentoDireto pd = _iPagamentoDiretoRepository.GetByCompra(compra.Id);
            if (pd == null) { pd = new PagamentoDireto(); }

            pd.Compra = compra;
            pd.Data = DateTime.Now;
            pd.DataPagamento = compra.Data;
            pd.Empresa = compra.Empresa;
            pd.Valor = compra.ValorTotal;
            _iPagamentoDiretoRepository.Update(pd);
        }

        private void InserirBoletosNaCompra(Compra compra, IList<Boleto> lbs)
        {
            IList<Boleto> bols = this.GetBoletos(compra.Id);
            IList<Boleto> b = bols.Except(lbs).ToList();

            foreach (Boleto bR in b)
            {
                _iboletoService.Delete(bR);
            }
            if (lbs.Any(p => p.Id <= 0))
            {
                _iboletoService.InsertRange(lbs.Where(p => p.Id <= 0));
            }

            PagamentoDireto pd = _iPagamentoDiretoRepository.GetByCompra(compra.Id);
            if (pd != null)
            {
                _iPagamentoDiretoRepository.Delete(pd);
            }
        }

        private void InserirProdutosNaCompra(Compra compra, IList<ProdutoCompra> lprods)
        {

            IList<ProdutoCompra> pros = this.GetProdutos(compra.Id);
            IList<ProdutoCompra> a = pros.Except(lprods).ToList();

            foreach (ProdutoCompra proR in a)
            {
                _iprodutoCompraRep.Delete(proR);
            }
            if (lprods.Any(p => p.Id <= 0))
            {
                _iprodutoCompraRep.InsertRange(lprods.Where(p => p.Id <= 0));
            }
        }


        public IList<entities.DTO.CompraDTO> GetCompraDTOByEmpresaFOrnecedorData(int empresaId, string dataInicio, string dataFinal, string fornecedor)
        {
            return ((ICompraRepository)base._repository).GetCompraDTOByEmpresaFOrnecedorData(empresaId, dataInicio, dataFinal, fornecedor);
        }


        public IList<ProdutoCompra> GetProdutos(int compraId)
        {
            return ((ICompraRepository)base._repository).GetProdutos(compraId);
        }

        public IList<Boleto> GetBoletos(int compraId)
        {
            return ((ICompraRepository)base._repository).GetBoletos(compraId);
        }
    }
}
