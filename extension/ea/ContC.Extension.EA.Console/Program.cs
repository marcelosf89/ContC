using ContC.Extension.EA.crosscutting.utilities.Config;
using ContC.Extension.EA.domain.entities.Models;
using ContC.Extension.EA.domain.services.Contracts;
using ContC.Extension.EA.domain.services.Implementations;
using ContC.Extension.EA.Repositories.Mapping;
using ContC.Extension.EA.Repositories.Mapping.Conventions;
using ContC.Extension.EA.Service.Config;
using ContC.Extension.EA.Service.ThreadDefs;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ContC.Extension.EA.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = new DateTime(2015, 09, 27, 18, 0, 0, 0,System.DateTimeKind.Utc);
            DateTime dt2 = new DateTime(1970, 1, 1);
            //long a = (dt2 - dt);


        }
        static void Mainaa(string[] args)
        {
            UnityContainer container = new UnityContainer();

            IReceitaService _receitaServices = new ReceitaService(new ReceitasRepository());

            RabbitmqConfig config = (RabbitmqConfig)System.Configuration.ConfigurationManager.GetSection("RabbitmqConfig");

            //ReceitasThread t = new ReceitasThread(_receitaServices);
            //System.Threading.Thread fwt = new System.Threading.Thread(t.Start);
            //fwt.Start();


            ReceitasAutoThread rat = new ReceitasAutoThread(_receitaServices);
            System.Threading.Thread fwt3 = new System.Threading.Thread(rat.Start);
            fwt3.Start();

            while (true) { }
        }

        static void Main2(string[] args)
        {
            FileInfo fi = new FileInfo(@"C:\Desenvolvimento\MaSF\ContC\extension\ea\ContC.Extension.EA.Service\config.json");
            String s = File.ReadAllText(fi.FullName);

            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            ConfigModel cm = new ConfigModel();
            s = json_serializer.Serialize(cm).ToString();
            ConfigModel configModel = json_serializer.Deserialize<ConfigModel>(s);
        }
    }
}
