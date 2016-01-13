using ContC.crosscutting.DataContracts;
using ContC.domain.entities.Models;
using Service.Pattern;

namespace ContC.domain.services.Contracts
{
    public interface IContaService : IService<Conta>
    {
        Conta GetByFuncionario(int funcionarioId);

        Conta BuildBasedOn(FuncionarioContaContract contract);
    }
}
