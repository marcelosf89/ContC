using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class EmpresaService : Service<Empresa>, IEmpresaService
    {
        public EmpresaService(IEmpresaRepository repository)
        {
            base._repository = repository;
        }



        public IList<Empresa> GetAllEmpresaByGrupo(int grupoId)
        {
            return ((IEmpresaRepository)_repository).GetAllEmpresaByGrupo(grupoId);
        }


        public IList<Empresa> GetByEmpresa(int empresaId)
        {
            return ((IEmpresaRepository)_repository).GetByEmpresa(empresaId);
        }


        public IList<Empresa> GetAllEmpresaByUser(string email)
        {
            return ((IEmpresaRepository)_repository).GetAllEmpresaByUser(email);
        }

    }
}
