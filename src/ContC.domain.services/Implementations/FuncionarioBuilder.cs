using System;
using ContC.crosscutting.DataContracts;
using ContC.domain.services.Contracts;
using ContC.domain.entities.Models;
using ContC.crosscutting;
using ContC.crosscutting.Exceptions;

namespace ContC.domain.services.Implementations
{
    public class FuncionarioBuilder : IFuncionarioBuilder
    {
        public FuncionarioBuilder(IFuncionarioRepository funcionariorepository)
        {
            this._funcionariorepository = funcionariorepository;
        }

        public Funcionario BuildBasedOn(FuncionarioContaContract contract)
        {
            if (contract.Id > 0)
            {
                _funcionario = _funcionariorepository.Find(contract.Id);
                if (_funcionario == null)
                {
                    throw new ConstrucaoObjetoException(string.Format("Funcionário {0} não existe", contract.Id));
                }
            }
            else
            {
                _funcionario.DataInicio = DateTime.Now;
            }

           return
               this.SetNome(contract.Nome)
                   .SetValor(contract.Valor)
                   .SetEmail(contract.Email)
                   .SetIdentificacao1(contract.Identificacao1)
                   .SetIdentificacao2(contract.Identificacao2)
                   .SetLider(contract.LiderId)
                   .SetTipoPagamento(contract.TipoPagamentoId)
                   .SetTipoRegime(contract.TipoRegimeFuncionarioId)
                   .SetNascimento(contract.Nascimento.Value)
                   .Build();
        }

        public IFuncionarioBuilder SetNome(string nome)
        {
            _funcionario.Nome = nome;
            return this;
        }

        public IFuncionarioBuilder SetEmail(string email)
        {
            _funcionario.Email = email;
            return this;
        }

        public IFuncionarioBuilder SetTelefone(string telefone)
        {
            _funcionario.Telefone = telefone;
            return this;
        }

        public IFuncionarioBuilder SetNascimento(DateTime nascimento)
        {
            _funcionario.Nascimento = nascimento;
            return this;
        }

        public IFuncionarioBuilder SetIdentificacao1(string identificacao1)
        {
            _funcionario.Identificacao1 = identificacao1;
            return this;
        }

        public IFuncionarioBuilder SetIdentificacao2(string identificacao2)
        {
            _funcionario.Identificacao2 = identificacao2;
            return this;
        }

        public IFuncionarioBuilder SetLider(int liderId)
        {
            Funcionario lider = _funcionariorepository.Find(liderId);
            if (lider == null)
            {
                throw new ConstrucaoObjetoException(string.Format("Líder funcionario {0} não encontrado", liderId));
            }
            _funcionario.Lider = lider;
            return this;
        }

        public IFuncionarioBuilder SetTipoPagamento(int tipoPagamento)
        {
            TipoPagamento tipo = _funcionariorepository.GetTipoPagamento(tipoPagamento);
            if (tipo == null)
            {
                throw new ConstrucaoObjetoException(string.Format("Tipo de pagamento {0} não encontrado", tipoPagamento));
            }
            _funcionario.TipoPagamento = tipo;
            return this;
        }

        public IFuncionarioBuilder SetTipoRegime(int tipoRegime)
        {
            TipoRegimeFuncionario tipo = _funcionariorepository.GetRegime(tipoRegime);
            if (tipo == null)
            {
                throw new ConstrucaoObjetoException(string.Format("Tipo de regime {0} não encontrado", tipo));
            }
            _funcionario.TipoRegimeFuncionario = tipo;
            return this;
        }

        public IFuncionarioBuilder SetValor(decimal valor)
        {
            _funcionario.Valor = valor;
            return this;
        }

        public OperacaoStatus Validar()
        {
            throw new NotImplementedException();
        }

        public Funcionario Build()
        {
            return _funcionario;
        }

        private IFuncionarioRepository _funcionariorepository;

        private Funcionario _funcionario;

    }
}
