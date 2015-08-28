using ContC.domain.entities.DTO;
using ContC.domain.entities.Models;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {

        IList<Funcionario> GetByEmpresa(int empresaId);

        FuncionariosDTO GetByEmpresaTipoPagamentoLider(int empresaId);

        IList<Funcionario> GetByEmpresa(int empresaId, int tipoPagamento, string liderId);
    }
}
