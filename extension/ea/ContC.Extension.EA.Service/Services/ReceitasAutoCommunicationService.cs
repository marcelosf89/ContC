using ContC.Extension.EA.crosscutting.utilities.Config;
using ContC.Extension.EA.domain.entities;
using ContC.Extension.EA.domain.services.Contracts;
using ContC.Extension.EA.Service.Config;
using ContC.Extension.EA.Service.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;

namespace ContC.Extension.EA.Service.Services
{
    public class ReceitasAutoCommunicationService
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        public ReceitasAutoCommunicationService()
        {
            timer.Interval = 600000;
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                ExecuteProperty.Instance.SetCloseCommunication();
                Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                ExecuteProperty.Instance.SetCloseCommunication();
                timer.Start();
            }
        }
        private void Execute()
        {
            //Singleton.ExecuteProperty.Instance.EventLog.WriteEntry("Testando Liberacao do Receita Auto");
            if (!ExecuteProperty.Instance.GetStartCommunication)
                ExecuteProperty.Instance.SetStartCommunication();
            else
                return;

            //Singleton.ExecuteProperty.Instance.EventLog.WriteEntry("Receita Auto Liberado");
            try
            {
                //Singleton.ExecuteProperty.Instance.EventLog.WriteEntry("Iniciando a comunicação automatica");
                new ReceitaCommunicationService();
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                ExecuteProperty.Instance.SetCloseCommunication();
                //Singleton.ExecuteProperty.Instance.EventLog.WriteEntry("Receita Auto Finalizado");
            }
            Thread.Sleep(100);
        }

        internal void Stop()
        {
            timer.Stop();
        }

        internal void StartActivityMonitoring()
        {
            timer.Start();
        }
    }
}
