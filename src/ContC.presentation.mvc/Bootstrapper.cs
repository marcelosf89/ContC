using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using ContC.domain.services;
using ContC.domain.services.Contracts;
using ContC.domain.services.Implementations;
using System.Data.Entity;
using ContC.presentation.mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ContC.presentation.mvc.Controllers;
using ContC.crosscutting.Authentication.Interface;
using ContC.crosscutting.Authentication;

namespace ContC.presentation.mvc
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            BootstraperService.RegisterTypes(container);

            container.RegisterType<IProdutoService, ProdutoService>()
                     .RegisterType<IFuncionarioService, FuncionarioService>()
                     .RegisterType<IGrupoService, GrupoService>()
                     .RegisterType<IUsuarioService, UsuarioService>()
                     .RegisterType<IEmpresaService, EmpresaService>()
                     .RegisterType<IFornecedorService, FornecedorService>()
                     .RegisterType<IBoletoService, BoletoService>()
                     .RegisterType<IContaService, ContaService>()
                     .RegisterType<IBancoService, BancoService>()
                     .RegisterType<ICategoriaService, CategoriaService>()
                     .RegisterType<ICompraService, CompraService>()
                     .RegisterType<IReceitaService, ReceitaService>()
                     .RegisterType<INotaService, NotaService>()
                     .RegisterType<IGerenciadorAutenticacao, GerenciadorAutenticacaoCookie>()
                     .RegisterType<IAutenticacaoService, AutenticacaoService>();


            container.RegisterType<DbContext, ApplicationDbContext>(
    new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager());


        }
    }
}