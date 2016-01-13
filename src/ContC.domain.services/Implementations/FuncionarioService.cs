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


        public void Save(FuncionarioContaContract contrato)
        {
            
            base._repository.Insert(func);

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

        public Funcionario BuildBasedOn(FuncionarioContaContract contract)
        {
            Funcionario funcionario = new Funcionario();
            if (contract.Id > 0)
            {
                funcionario = Find(contract.Id);

                funcionario.Nome = contract.Nome;
                funcionario.Email = contract.Email;
                funcionario.Telefone = contract.Telefone;
                funcionario.Nascimento = contract.Nascimento.Value;
                funcionario.Identificacao1 = contract.Identificacao1;
                funcionario.Identificacao2 = contract.Identificacao2;

                if (contract.LiderId > 0)
                {
                    funcionario.Lider =  Find(contract.LiderId);
                }

                funcionario.TipoPagamento = _repository.ti
                func.TipoRegimeFuncionario = new TipoRegimeFuncionario() { Id = model.TipoRegimeFuncionarioId };
            }
            
            funcionario.DataInicio = DateTime.Now.Date;

            { Id = model.Id, DataInicio = DateTime.Now.Date };
            if (model.Id > 0) { func = _ifuncionarioService.Find(model.Id); }

            func.Nome = model.Nome;
            func.Email = model.Email;
            func.Telefone = model.Telefone;
            func.Nascimento = model.Nascimento.Value;
            func.Identificacao1 = model.Identificacao1;
            func.Identificacao2 = model.Identificacao2;
            if (model.LiderId > 0)
            {
                func.Lider = new Funcionario() { Id = model.LiderId };
            }
            func.TipoPagamento = new TipoPagamento() { Id = model.TipoPagamentoId };
            func.TipoRegimeFuncionario = new TipoRegimeFuncionario() { Id = model.TipoRegimeFuncionarioId };
            func.Valor = model.Valor;
        }

        private IFuncionarioEnderecoRepository _funcionarioEnderecoRepository;

        private IContaService _iContaService;

        private IFuncionarioRepository _funcionarioRepository
        {
            get
            {
                return (IFuncionarioRepository)_repository;
            }
        }

    }
}
