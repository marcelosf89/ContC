using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;
using System.Collections.Generic;

namespace ContC.domain.services.Contracts
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {

        IList<Funcionario> GetByEmpresa(int empresaId);

        FuncionariosDTO GetByEmpresaTipoPagamentoLider(int empresaId);

        IList<Funcionario> GetByEmpresa(int empresaId, int tipoPagamento, string liderId);

        TipoRegimeFuncionario GetRegime(int id);

        TipoPagamento GetTipoPagamento(int id);
    }
}
