using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using Configuration = NHibernate.Cfg.Configuration;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
using ContC.Extension.EA.Repositories.Mapping.Conventions;
namespace ContC.Extension.EA.Repositories.Mapping.Configuration
{
    /// <summary>
    /// Classe para gerência da sessão do NHibernate
    /// </summary>
    public sealed class NHibernateWebSessionFactory : INHibernateFactory
    {

        #region Singleton
        private static NHibernateWebSessionFactory _instance;
        private string _user;

        private NHibernateWebSessionFactory()
        {
        }

        private NHibernateWebSessionFactory(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        /// <summary>
        /// Instância do objeto singleton
        /// </summary>
        public static NHibernateWebSessionFactory GetInstancia()
        {
            return _instance ?? (_instance = new NHibernateWebSessionFactory());
        }

        public static NHibernateWebSessionFactory GetInstancia(ISessionFactory sessionFactory)
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("ErrosEstrutura.CriacaoNHSessionFactory");
            }
            _instance = new NHibernateWebSessionFactory(sessionFactory);
            return _instance;
        }

        #endregion

        #region Métodos Privados
        public ISessionFactory GetSessionFactory()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ContC.Extension.EA"].ToString();
            if (this._sessionFactory == null)
            {
                FluentConfiguration config = Fluently.Configure();

                switch (_type)
                {
                    case "POSTGRESQL":
                    default:
                        config.CurrentSessionContext<NHibernate.Context.ThreadLocalSessionContext>()
                              .Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString)
                              .ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<ReceitaMap>()
                              .Conventions.Add<ClasseComumConvencao>())
                              .BuildConfiguration();
                        break;
                }

                config.ExposeConfiguration(cfg =>
                { //new SchemaExport(cfg).Execute(true, true, false);
                });
                this._sessionFactory = config.BuildSessionFactory();
            }

            return _sessionFactory;
        }
        #endregion

        #region Implementação de INHibernateFactory

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        public ISession GetSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        #endregion

        #region Atributos Privados
        private ISessionFactory _sessionFactory;
        private string _type = ConfigurationManager.AppSettings["dbtype"];
        #endregion
    }
}
