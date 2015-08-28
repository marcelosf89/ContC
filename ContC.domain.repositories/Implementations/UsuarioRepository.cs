﻿using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using NHibernate.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository()
        {

        }


        public Usuario GetUsuario(string userName)
        {
            return this.SessaoAtual.Query<Usuario>()
                .Where(p => p.Email.ToUpper().Equals(userName.ToUpper()))
                .SingleOrDefault();
        }
    }
}
