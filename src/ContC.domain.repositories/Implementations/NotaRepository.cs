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
using ContC.domain.entities.DTO;

namespace ContC.domain.services.Implementations
{
    public class NotaRepository : Repository<Nota>, INotaRepository
    {
        public IList<Nota> GetNotasByEmpresaUsuario(int empresaId, string email)
        {
            email = email.ToUpper();

            return (from a in this.SessaoAtual.Query<Nota>()
                    where a.Empresa.Id == empresaId && a.Cadastrado.Email.ToUpper().Equals(email)
                    && a.Concluido == null
                    select a).ToList();
        }
    }
}
