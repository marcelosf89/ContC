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
    public class FuncionarioEnderecoService : Service<FuncionarioEndereco>, IFuncionarioEnderecoService
    {
        public FuncionarioEnderecoService(IFuncionarioEnderecoRepository repository)
        {
            base._repository = repository;
        }



        public IList<Fornecedor> GetAllByGrupo(int grupoId)
        {
            return ((IFornecedorRepository)base._repository).GetAllByGrupo(grupoId);
        }
    }
}
