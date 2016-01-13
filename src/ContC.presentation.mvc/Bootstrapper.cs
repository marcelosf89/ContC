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

            container.RegisterType<IProdutoService, ProdutoService>();
            container.RegisterType<IFuncionarioService, FuncionarioService>();
            container.RegisterType<IGrupoService, GrupoService>();
            container.RegisterType<IUsuarioService, UsuarioService>();
            container.RegisterType<IEmpresaService, EmpresaService>();
            container.RegisterType<IFornecedorService, FornecedorService>();
            container.RegisterType<IBoletoService, BoletoService>();
            container.RegisterType<IFuncionarioEnderecoService, FuncionarioEnderecoService>();
            container.RegisterType<IContaService, ContaService>();
            container.RegisterType<IBancoService, BancoService>();
            container.RegisterType<ICategoriaService, CategoriaService>();
            container.RegisterType<ICompraService, CompraService>();
            container.RegisterType<IReceitaService, ReceitaService>();
            container.RegisterType<INotaService, NotaService>();
            container.RegisterType<IGerenciadorAutenticacao, GerenciadorAutenticacaoCookie>();


            container.RegisterType<DbContext, ApplicationDbContext>(
    new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(
                new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>(
                new InjectionConstructor());

        }
    }
}