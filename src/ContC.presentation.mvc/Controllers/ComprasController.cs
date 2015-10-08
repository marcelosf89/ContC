using ContC.CorssCutting.Exceptions;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using ContC.presentation.mvc.Extension;
using ContC.presentation.mvc.Models.CompraModels;
using ContC.presentation.mvc.Models.ExceptionModels;
using ContC.Repositories.Mapping.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ContC.presentation.mvc.Controllers
{
    public class ComprasController : ControllerException
    {
        private ICategoriaService _icategoriaServicos;
        private IProdutoService _iprodutosService;
        private IUsuarioService _iUsuarioService;
        private ICompraService _compraService;

        public ComprasController(ICategoriaService icategoriaServicos, IProdutoService iprodutosService, IUsuarioService iUsuarioService, ICompraService compraService)
        {
            _icategoriaServicos = icategoriaServicos;
            _iprodutosService = iprodutosService;
            _iUsuarioService = iUsuarioService;
            _compraService = compraService;

        }
        // GET: Compras
        public ActionResult Index(int empresaId)
        {
            return View(empresaId);
        }

        public ActionResult Novo(int empresaId)
        {
            CompraNovoModel cnm = new CompraNovoModel();
            cnm.EmpresaId = empresaId;
            cnm.TipoPagamentos = new List<SelectListItem>();

            GetCategoriasETipoPagamento(empresaId, cnm);

            return View(cnm);
        }


        public ActionResult Editar(int compraId)
        {
            Compra cp = _compraService.Find(compraId);

            CompraNovoModel cnm = new CompraNovoModel();
            cnm.EmpresaId = cp.Empresa.Id;
            cnm.Entrega = cp.Data;
            cnm.CategoriaId = cp.Categoria.Id;
            cnm.FornecedorId = cp.Fornecedor.Id;
            cnm.Fornecedor = cp.Fornecedor.RazaoSocial;
            cnm.Id = cp.Id;
            cnm.TipoPagamentoId = cp.TipoPagamento;
            cnm.NumeroNota = cp.NotaFiscal;
            cnm.Valor = cp.ValorTotal;
            cnm.ValorDespesaAdministrativa = cp.ValorDespesaAdministrativa;
            cnm.ValorFrete = cp.ValorFrete;
            cnm.ValorSeguro = cp.ValorSeguro;
            cnm.Desconto = cp.Desconto;
            cnm.ValorICMSNota = cp.ValorICMSNota;
            cnm.ValorIPINota = cp.ValorIPINota;
            cnm.ValorTotalNota = cp.ValorTotalNota;

            IList<ProdutoCompra> pros = _compraService.GetProdutos(compraId);

            cnm.Produtos = new List<ProdutosModel>();
            foreach (ProdutoCompra pro in pros)
            {
                ProdutosModel pm = new ProdutosModel();
                pm.Id = pro.Id;
                pm.ProdutoId = pro.Produto.Id;
                pm.Quantidade = Convert.ToInt32(pro.Quantidade);
                pm.Valor = pro.Valor;
                pm.TipoQuantidade = pro.TipoQuantidade;
                pm.QuantidadeCaixa = pro.QuantidadeCaixa;
                pm.Descricao = pro.Produto.Nome;
                pm.ValorTotal = pro.Valor * pro.Quantidade;
                pm.ValorICMS = pm.ValorTotal * (pro.ICMS / 100);
                pm.ValorIPI = pm.ValorTotal * (pro.IPI / 100);
                pm.ValorTotalComImposto = pm.ValorTotal + pm.ValorICMS + pm.ValorIPI;
                pm.IPI = pro.IPI;
                pm.ICMS = pro.ICMS;
                ((IList<ProdutosModel>)cnm.Produtos).Add(pm);
            }

            IList<Boleto> bols = _compraService.GetBoletos(compraId);

            cnm.Boletos = new List<BoletosModel>();
            foreach (Boleto bol in bols)
            {
                BoletosModel b = new BoletosModel();
                b.Id = bol.Id;
                b.Numero = bol.Numero;
                b.Valor = bol.Valor;
                b.Vencimento = bol.DataVencimento.ToShortDateString();
                ((IList<BoletosModel>)cnm.Boletos).Add(b);
            }

            cnm.TipoPagamentos = new List<SelectListItem>();
            GetCategoriasETipoPagamento(cnm.EmpresaId, cnm);

            return View("Novo", cnm);
        }

        private void GetCategoriasETipoPagamento(int empresaId, CompraNovoModel cnm)
        {
            //TODO:Isso é temporario ... Somente para a entrega ( Criar um objeto para isso )
            cnm.TipoPagamentos = new List<SelectListItem>();
            ((List<SelectListItem>)cnm.TipoPagamentos).Add(new SelectListItem() { Value = "2", Text = "A Vista", Selected = true });
            ((List<SelectListItem>)cnm.TipoPagamentos).Add(new SelectListItem() { Value = "3", Text = "Cartão" });
            ((List<SelectListItem>)cnm.TipoPagamentos).Add(new SelectListItem() { Value = "1", Text = "Boleto" });

            ((IList<SelectListItem>)cnm.TipoPagamentos).Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Selecione --"
            });

            cnm.Categorias = _icategoriaServicos.GetByEmpresa(empresaId).Select(p => new SelectListItem() { Text = p.Descricao, Value = p.Id.ToString() });
        }

        public JsonResult GetProdutos(int maxRows, string startsWith, int empresaId, int categoriaId)
        {
            IList<Produto> lfor = _iprodutosService.GetAllByEmpresaCategoria(startsWith, empresaId, categoriaId, maxRows);
            return Json(lfor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduto(string produto, decimal qtd, decimal valor, int empresaId, string tpqtd, decimal icms, int qtdCaixa, decimal ipi)
        {
            ValidarAddProduto(produto, qtd, valor);

            Produto pro = _iprodutosService.GetByName(produto, empresaId);

            if (pro == null)
            {
                pro = new Produto();
                pro.Descricao = produto.ToUpper();
                pro.Nome = produto.ToUpper();
                pro.ValorMedio =  valor;
                _iprodutosService.Insert(pro, empresaId);
            }

            ProdutosModel pm = new ProdutosModel();
            pm.IdTemp = Guid.NewGuid();
            pm.ProdutoId = pro.Id;
            pm.Descricao = pro.Descricao;
            pm.Quantidade = qtd;
            pm.TipoQuantidade = tpqtd;
            pm.QuantidadeCaixa = qtdCaixa;
            pm.Valor = valor;
            pm.ValorTotal = qtd * valor;
            pm.ValorICMS = pm.ValorTotal * (icms / 100);
            pm.ValorIPI = pm.ValorTotal * (ipi / 100);            
            pm.ValorTotalComImposto = pm.ValorTotal + pm.ValorICMS + pm.ValorIPI;

            pm.ICMS = icms;
            pm.IPI = ipi;



            return Json(pm, JsonRequestBehavior.AllowGet);
        }

        private static void ValidarAddProduto(string produto, decimal qtd, decimal valor)
        {
            if (String.IsNullOrEmpty(produto))
            {
                throw new ExceptionMessage("Para adicionar um produto tem que ter um nome");
            }
            if (valor < 0)
            {
                throw new ExceptionMessage("O Valor do produto tem que ser maior ou igual a 0 (zero)");
            }
            if (qtd <= 0)
            {
                throw new ExceptionMessage("Para adicionar um produto a quantidade tem que ser maior que 1 (um)");
            }
        }

        public ActionResult Incluir(CompraNovoModel model, string produtosJson, string boletosJson)
        {
            IList<ProdutosModel> prods = new JavaScriptSerializer().Deserialize<IList<ProdutosModel>>(produtosJson);
            model.Produtos = prods;
            IList<BoletosModel> bols = new JavaScriptSerializer().Deserialize<IList<BoletosModel>>(boletosJson);
            model.Boletos = bols;

            if (!ModelState.IsValid)
            {
                model.ValorIPINota = prods.Sum(p => ((p.Valor * p.Quantidade) * (p.IPI / 100)));
                model.ValorICMSNota = prods.Sum(p => ((p.Valor * p.Quantidade) * (p.ICMS / 100)));
                model.ValorTotalNota = (model.Valor + model.ValorIPINota + model.ValorICMSNota + model.ValorFrete + model.ValorSeguro) - model.Desconto;


                GetCategoriasETipoPagamento(model.EmpresaId, model);
                return View("Novo", model);
            }

            model.Valor = model.Produtos.Sum(p => p.ValorTotal);

            Compra cp = new Compra() { Id = model.Id };
            cp.Empresa = new Empresa() { Id = model.EmpresaId };
            cp.Fornecedor = new Fornecedor() { Id = model.FornecedorId, RazaoSocial = model.Fornecedor };
            cp.ValorTotal = model.Valor;
            cp.Data = model.Entrega.Value;
            cp.Usuario = _iUsuarioService.GetUsuario(User.Identity.Name);
            cp.Categoria = new Categoria() { Id = model.CategoriaId };
            cp.NotaFiscal = model.NumeroNota;
            cp.TipoPagamento = model.TipoPagamentoId;
            cp.ValorDespesaAdministrativa = model.ValorDespesaAdministrativa;
            cp.ValorFrete = model.ValorFrete;
            cp.ValorSeguro = model.ValorSeguro;
            cp.Desconto = model.Desconto;

            IList<ProdutoCompra> lpc = CapturarProdutoCompra(model, cp);

            
            cp.ValorIPINota = lpc.Sum(p => ((p.Valor * p.Quantidade) * (p.IPI / 100)));
            cp.ValorICMSNota = lpc.Sum(p => ((p.Valor * p.Quantidade) * (p.ICMS / 100)));
            cp.ValorTotalNota = (cp.ValorTotal + cp.ValorIPINota + cp.ValorICMSNota + cp.ValorFrete + cp.ValorSeguro) - cp.Desconto;

            IList<Boleto> lb = CapturarBoletos(model, cp);

            InserirCompra(cp, lpc, lb);
            return View("Index", model.EmpresaId);
        }

        private void InserirCompra(Compra cp, IList<ProdutoCompra> lpc, IList<Boleto> lb)
        {
            try
            {
                UnitOfWorkNHibernate.GetInstancia().IniciarTransacao();
                _compraService.Insert(cp, lpc, lb);
                UnitOfWorkNHibernate.GetInstancia().ConfirmarTransacao();
            }
            catch (ExceptionMessage em)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw em;
            }
            catch (Exception ex)
            {
                UnitOfWorkNHibernate.GetInstancia().DesfazerTransacao();
                throw new StatusException("Erro interno . Favor informe ao administrador.");
            }
        }

        private static IList<ProdutoCompra> CapturarProdutoCompra(CompraNovoModel model, Compra cp)
        {
            IList<ProdutoCompra> lpc = new List<ProdutoCompra>();
            foreach (ProdutosModel item in model.Produtos)
            {
                ProdutoCompra pc = new ProdutoCompra() { Id = item.Id };
                pc.Produto = new Produto() { Id = item.ProdutoId };
                pc.Valor = item.Valor;
                pc.Quantidade = item.Quantidade;
                pc.TipoQuantidade = item.TipoQuantidade;
                pc.QuantidadeCaixa = item.QuantidadeCaixa;
                pc.Compra = cp;
                pc.Id = item.Id;
                pc.IPI = item.IPI;
                pc.ICMS = item.ICMS;

                lpc.Add(pc);
            }
            return lpc;
        }

        private static IList<Boleto> CapturarBoletos(CompraNovoModel model, Compra cp)
        {
            IList<Boleto> lb = new List<Boleto>();
            foreach (BoletosModel item in model.Boletos)
            {
                Boleto b = new Boleto();
                b.Compra = cp;
                b.DataVencimento = Convert.ToDateTime(item.Vencimento);
                b.Empresa = new Empresa() { Id = model.EmpresaId };
                b.Fornecedor = cp.Fornecedor;
                b.Numero = item.Numero;
                b.Valor = model.Valor;
                b.Id = item.Id;

                lb.Add(b);
            }
            return lb;
        }

        public String GetDateBoleto(string numeroDias)
        {
            try
            {
                DateTime dt = Convert.ToDateTime("07/10/1997", new CultureInfo("pt-BR"));
                dt = dt.AddDays(Convert.ToInt32(numeroDias));
                return dt.ToShortDateString();
            }
            catch (Exception) { return ""; }

        }

        public JsonResult AddBoleto(string numero, decimal valor, string vencimento)
        {
            BoletosModel bm = new BoletosModel();
            bm.Numero = numero;
            bm.Valor = valor;
            bm.IdTemp = Guid.NewGuid();
            bm.Vencimento = vencimento;

            return Json(bm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FiltrarConsulta(int empresaId, string dataInicio, string dataFinal, string fornecedor)
        {
            IList<CompraDTO> contas = _compraService.GetCompraDTOByEmpresaFOrnecedorData(empresaId, dataInicio, dataFinal, fornecedor);
            return View(contas);
        }
    }
}