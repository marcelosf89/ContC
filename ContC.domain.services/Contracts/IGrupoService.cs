using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface IGrupoService : IService<Grupo>
    {
        void Insert(Grupo g);
        Grupo GetByCode(string code);

        IList<Grupo> GetAllGrupo(Funcionario usuario);

        IList<Grupo> GetAllGrupo(string p);
    }
}
