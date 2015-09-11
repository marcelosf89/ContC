using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface IProdutoService : IService<Produto>
    {
        void teste();

        IList<Produto> GetAllByEmpresaCategoria(string startsWith, int empresaId, int categoriaId, int maxRows);

        Produto GetByName(string produto, int empresaId);

        void Insert(Produto produto, int empresaId);
    }
}
