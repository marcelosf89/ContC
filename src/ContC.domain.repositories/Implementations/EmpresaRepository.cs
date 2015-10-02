using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository()
        {

        }


        public IList<Empresa> GetAllEmpresaByGrupo(int grupoId)
        {
            return (from a in this.SessaoAtual.Query<Empresa>()
                       where a.Grupo.Id == grupoId
                        select a).ToList();
        }

        public IList<Empresa> GetByEmpresa(int empresaId)
        {
            return (from a in this.SessaoAtual.Query<Empresa>()
                    where a.Id == empresaId
                    select a).Distinct().ToList();
        }


        public IList<Empresa> GetAllEmpresaByUser(string email)
        {
            return (from a in this.SessaoAtual.Query<FuncionarioEndereco>()
                    where a.Funcionario.Email.ToUpper().Equals(email.ToUpper())
                    select a.Empresa).Distinct().ToList();
        }
    }
}
