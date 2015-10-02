using ContC.domain.entities.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Contracts
{
    public interface IEmpresaService : IService<Empresa>
    {

        void Insert(Empresa em);

        IList<Empresa> GetAllEmpresaByGrupo(int grupoId);

        IList<Empresa> GetByEmpresa(int empresaId);

        IList<Empresa> GetAllEmpresaByUser(string p);

        void SendCommunicationReceita(int empresaId);
    }
}
