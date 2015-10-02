using ContC.Communication.Model.ServerToClient;
using ContC.Extension.EA.crosscutting.utilities.Config;
using ContC.Extension.EA.crosscutting.utilities.Rabbitmq;
using ContC.Extension.EA.Service.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ContC.Extension.EA.Service.Services
{
    public class RabbitmqService
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        RabbitmqConfig _rabbitmqConfig;
        public void StartActivityMonitoring(RabbitmqConfig rabbitmqConfig)
        {
            _rabbitmqConfig = rabbitmqConfig;

            timer.Interval = 30000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
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
                timer.Start();
            }
        }

        private void Execute()
        {
            if (ExecuteProperty.Instance.GetStartCommunication)
                return;

            try
            {
                using (RabbitMQConsumer c = new RabbitMQConsumer(this._rabbitmqConfig.Server, this._rabbitmqConfig.User, this._rabbitmqConfig.Pass, this._rabbitmqConfig.Queue, this._rabbitmqConfig.Port))
                {

                    try
                    {
                        GetValuesModel wc = GetMessage(c);
                        if (wc == null) return;

                        if (!ExecuteProperty.Instance.GetStartCommunication)
                            ExecuteProperty.Instance.SetStartCommunication();
                        else
                            return;

                        switch (wc.GetValuesEnum)
                        {
                            case GetValuesEnum.GetReceitas:
                                new ReceitaCommunicationService();
                                break;
                            default:
                                throw new Exception("O Tipo não está configurado");
                        }

                        c.AcknowledgeMsg(true);
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        c.AcknowledgeMsg(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                ExecuteProperty.Instance.SetCloseCommunication();
            }
            Thread.Sleep(100);
        }

        private GetValuesModel GetMessage(RabbitMQConsumer c)
        {
            byte[] bodyBytes = c.Pop();
            if (bodyBytes == null) return null;

            using (MemoryStream ms = new MemoryStream(bodyBytes))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(GetValuesModel));
                return (GetValuesModel)deserializer.ReadObject(ms);
            }
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
