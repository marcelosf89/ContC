using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping.UnitOfWork
{
    public interface IUnitOfWork
    {
        void IniciarTransacao();
        void ConfirmarTransacao();
        void DesfazerTransacao();
    }
}

