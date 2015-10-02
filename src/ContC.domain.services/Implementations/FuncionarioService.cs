using ContC.domain.entities.DTO;
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
    public class FuncionarioService : Service<Funcionario>, IFuncionarioService
    {
        private IFuncionarioEnderecoRepository _funcionarioEnderecoRepository;
        private IContaService _iContaService;

        public FuncionarioService(IFuncionarioRepository repository, IFuncionarioEnderecoRepository funcionarioEnderecoRepository
            , IContaService iContaService)
        {
            base._repository = repository;
            _funcionarioEnderecoRepository = funcionarioEnderecoRepository;
            _iContaService = iContaService;
        }

        public IList<Funcionario> GetByEmpresa(int empresaId)
        {
            return ((IFuncionarioRepository)base._repository).GetByEmpresa(empresaId);
        }


        public FuncionariosDTO GetByEmpresaTipoPagamentoLider(int empresaId)
        {
            return ((IFuncionarioRepository)base._repository).GetByEmpresaTipoPagamentoLider(empresaId);
        }

        public IList<Funcionario> GetByEmpresa(int empresaId, int tipoPagamento, string liderId)
        {
            return ((IFuncionarioRepository)base._repository).GetByEmpresa(empresaId, tipoPagamento, liderId);
        }


        public void Insert(Funcionario func,Conta conta,  int empresaId)
        {
            base._repository.Update(func);

            conta.Funcionario = func;
            _iContaService.Update(conta);

            FuncionarioEndereco fe = _funcionarioEnderecoRepository.GetByEmpresaFuncionario(func.Id, empresaId);
            if (fe == null)
            {
                fe = new FuncionarioEndereco();
                fe.Funcionario = func;
                fe.Empresa = new Empresa() { Id = empresaId };
                _funcionarioEnderecoRepository.Insert(fe);
            }
            

        }
    }
}
