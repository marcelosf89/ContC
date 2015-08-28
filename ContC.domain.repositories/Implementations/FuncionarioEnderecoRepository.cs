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

namespace ContC.domain.services.Implementations
{
    public class FuncionarioEnderecoRepository : Repository<FuncionarioEndereco>, IFuncionarioEnderecoRepository
    {
        public FuncionarioEnderecoRepository()
        {

        }



        public FuncionarioEndereco GetByEmpresaFuncionario(int p, int empresaId)
        {
            return (from a in this.SessaoAtual.Query<FuncionarioEndereco>()
                    where a.Funcionario.Id == p && a.Empresa.Id == empresaId
                    select a).SingleOrDefault();
        }
    }
}
