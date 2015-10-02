using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface IFornecedorService : IService<Fornecedor>
    {

        IList<Fornecedor> GetAllByGrupo(int grupoId);

        IList<Fornecedor> GetAllByEmpresaCategoria(string startsWith, int empresaId, int categoriaId, int maxRows);
    }
}
