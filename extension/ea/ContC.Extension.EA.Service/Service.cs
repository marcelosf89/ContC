using ContC.Extension.EA.crosscutting.utilities.Config;
using ContC.Extension.EA.domain.services.Contracts;
using ContC.Extension.EA.domain.services.Implementations;
using ContC.Extension.EA.Service.ThreadDefs;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Service
{
    public partial class Service : ServiceBase
    {
        IReceitaService _receitaServices;

        RabbitmqConfig config;

        public Service()
        {
            try
            {
                _receitaServices = new ReceitaService(new ReceitasRepository());
                config = (RabbitmqConfig)System.Configuration.ConfigurationManager.GetSection("RabbitmqConfig");

                InitializeComponent();
                eventLog1.WriteEntry("Iniciando o monitoramento.");
                Singleton.ExecuteProperty.Instance.EventLog = eventLog1;
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                eventLog1.WriteEntry("Iniciando o monitoramento das Receitas");
                ReceitasThread t = new ReceitasThread(_receitaServices);
                System.Threading.Thread fwt = new System.Threading.Thread(t.Start);
                fwt.Start();

                eventLog1.WriteEntry("Iniciando o monitoramento do Rabbit");
                RabbitmqThread tr = new RabbitmqThread(config);
                System.Threading.Thread fwt2 = new System.Threading.Thread(tr.Start);
                fwt2.Start();

                eventLog1.WriteEntry("Iniciando o Envio automático das Receitas");
                ReceitasAutoThread rat = new ReceitasAutoThread(_receitaServices);
                System.Threading.Thread fwt3 = new System.Threading.Thread(rat.Start);
                fwt3.Start();
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Parando o Servico");
        }
    }
}
