using ContC.domain.services.Contracts;
using ContC.domain.services.Implementations;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services
{
    public static class BootstraperService
    {

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IProdutoRepository, ProdutoRepository>()
                     .RegisterType<IEmpresaRepository, EmpresaRepository>()
                     .RegisterType<IFuncionarioRepository, FuncionarioRepository>()
                     .RegisterType<IGrupoRepository, GrupoRepository>()
                     .RegisterType<IUsuarioRepository, UsuarioRepository>()
                     .RegisterType<IFornecedorRepository, FornecedorRepository>()
                     .RegisterType<IBoletoRepository, BoletoRepository>()
                     .RegisterType<IBancoRepository, BancoRepository>()
                     .RegisterType<IContaRepository, ContaRepository>()
                     .RegisterType<ICategoriaRepository, CategoriaRepository>()
                     .RegisterType<ICompraRepository, CompraRepository>()
                     .RegisterType<IProdutoCompraRepository, ProdutoCompraRepository>()
                     .RegisterType<IReceitaRepository, ReceitaRepository>()
                     .RegisterType<ITipoReceitaRepository, TipoReceitaRepository>()
                     .RegisterType<IPagamentoDiretoRepository, PagamentoDiretoRepository>()
                     .RegisterType<INotaRepository, NotaRepository>()
                     .RegisterType<INotaItemRepository, NotaItemRepository>()
                     .RegisterType<INotaUsuarioRepository, NotaUsuarioRepository>()
                     .RegisterType<IFuncionarioBuilder, FuncionarioBuilder>()
                     .RegisterType<IAutenticacaoService, AutenticacaoService>();
            
        }


    }
}
