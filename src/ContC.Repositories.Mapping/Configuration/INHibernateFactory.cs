using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Repositories.Mapping.Configuration
{
    public interface INHibernateFactory
    {
        ISession OpenSession();
        ISession GetSession();
        ISessionFactory GetSessionFactory();
    }
}
