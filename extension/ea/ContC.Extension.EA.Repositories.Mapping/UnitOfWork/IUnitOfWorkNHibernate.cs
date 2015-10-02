using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Repositories.Mapping.UnitOfWork
{
    public interface IUnitOfWorkNHibernate : IUnitOfWork
    {
        ISession SessaoAtual { get; }
    }
}
