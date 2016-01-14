using ContC.crosscutting.DataContracts;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Service.Pattern;
using System.Collections.Generic;

namespace ContC.domain.services.Implementations
{
    public class FuncionarioService : Service<Funcionario>, IFuncionarioService
    {
        public FuncionarioService(IFuncionarioRepository repository, IContaService iContaService, IFuncionarioBuilder funcionarioBuilder)
        {
            base._repository = repository;
            _iContaService = iContaService;
            _funcionarioBuilder = funcionarioBuilder;
        }

        public IList<Funcionario> GetByEmpresa(int empresaId)
        {
            return _funcionarioRepository.GetByEmpresa(empresaId);
        }


        public FuncionariosDTO GetByEmpresaTipoPagamentoLider(int empresaId)
        {
            return _funcionarioRepository.GetByEmpresaTipoPagamentoLider(empresaId);
        }

        public IList<Funcionario> GetByEmpresa(int empresaId, int tipoPagamento, string liderId)
        {
            return _funcionarioRepository.GetByEmpresa(empresaId, tipoPagamento, liderId);
        }


        public void Save(FuncionarioContaContract contrato)
        {
            Funcionario funcionario = _funcionarioBuilder.BuildBasedOn(contrato);
            Conta conta = _iContaService.BuildBasedOn(contrato);
            conta.Funcionario = funcionario;

            _repository.Update(funcionario);
            _iContaService.Update(conta);

        }
        
        private IContaService _iContaService;

        private IFuncionarioRepository _funcionarioRepository
        {
            get
            {
                return (IFuncionarioRepository)_repository;
            }
        }

        private IFuncionarioBuilder _funcionarioBuilder;
    }
}
