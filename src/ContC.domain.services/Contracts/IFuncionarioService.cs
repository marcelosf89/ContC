using ContC.crosscutting.DataContracts;
using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using Service.Pattern;
using System.Collections.Generic;

namespace ContC.domain.services.Contracts
{
    public interface IFuncionarioService : IService<Funcionario>
    {
        IList<Funcionario> GetByEmpresa(int empresaId);

        FuncionariosDTO GetByEmpresaTipoPagamentoLider(int empresaId);

        IList<Funcionario> GetByEmpresa(int empresaId, int tipoPagamento, string liderId);

        void Save(FuncionarioContaContract contract);


    }
}
