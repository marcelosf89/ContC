﻿using ContC.domain.entities.Models;
using ContC.domain.services.Contracts;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.domain.services.Implementations
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository()
        {

        }


        public void teste()
        {
           Produto pro =  this.Find(1);
        }

    }
}