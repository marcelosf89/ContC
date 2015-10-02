using ContC.domain.entities.Models;
using ContC.Repositories.Mapping;
using ContC.Repositories.Mapping.Conventions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeSessionFactory();
        }

        private static void InitializeSessionFactory()
        {
            try
            {
                var config = Fluently.Configure()
                               .Database(PostgreSQLConfiguration.Standard.ConnectionString(
                                   "Server=localhost;database=contc;user id=postgres;password=admin;Preload Reader = true")
                                    .ShowSql()

                               )

                               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProdutoMap>()
                               .Conventions.Add<ClasseComumConvencao>())                                    
                               .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                               .BuildConfiguration();

                var factory = config.BuildSessionFactory();

                _sessionFactory = factory;


                using (var s = _sessionFactory.OpenSession())
                {
                    using (var transaction = s.BeginTransaction())
                    {
                        Produto pro = new Produto();
                        pro.Descricao = "Teste do Goo.Search";
                        //pro.Codigo = "GOO";

                        s.Save(pro);
                        transaction.Commit();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        private static ISessionFactory _sessionFactory;
    }
}
