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
    public class NotaUsuarioRepository : Repository<NotaUsuario>, INotaUsuarioRepository
    {

        public NotaUsuario GetNotaUsuario(int notaId, int usuarioId)
        {
            return (from a in this.SessaoAtual.Query<NotaUsuario>()
                    where a.Lista.Id == notaId && a.Usuario.Id == usuarioId
                    select a).SingleOrDefault();
        }
    }
}
