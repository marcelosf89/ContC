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
            container.RegisterType<IProdutoRepository, ProdutoRepository>();
            container.RegisterType<IEmpresaRepository, EmpresaRepository>();
            container.RegisterType<IFuncionarioRepository, FuncionarioRepository>();
            container.RegisterType<IGrupoRepository, GrupoRepository>();
            container.RegisterType<IUsuarioRepository, UsuarioRepository>();
            container.RegisterType<IFornecedorRepository, FornecedorRepository>();
            container.RegisterType<IBoletoRepository, BoletoRepository>();
            container.RegisterType<IFuncionarioEnderecoRepository, FuncionarioEnderecoRepository>();
            container.RegisterType<IBancoRepository, BancoRepository>();
            container.RegisterType<IContaRepository, ContaRepository>();
            container.RegisterType<ICategoriaRepository, CategoriaRepository>();
            
        }


    }
}
