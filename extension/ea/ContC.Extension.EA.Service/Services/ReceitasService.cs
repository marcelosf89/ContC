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
    public class ReceitasService
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        IReceitaService _receitaService;
        ContCExtension config = (ContCExtension)System.Configuration.ConfigurationManager.GetSection("ContCExtension");

        double timers = 30000;
        public ReceitasService()
        {
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
                // ExecuteProperty.Instance.SetCloseCommunication();
                Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                // ExecuteProperty.Instance.SetCloseCommunication();
                timer.Start();
            }
        }
        private void Execute()
        {
            // if (!ExecuteProperty.Instance.GetStartCommunication)
            //    ExecuteProperty.Instance.SetStartCommunication();
            //else
            //    return;

            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            FileInfo fi = new FileInfo(config.FileConfigJson);//@"C:\Desenvolvimento\MaSF\ContC\extension\ea\ContC.Extension.EA.Service\config.json");
            ConfigModel configModel = null;
            try
            {                
                String s = File.ReadAllText(fi.FullName);                
                configModel = json_serializer.Deserialize<ConfigModel>(s);


                DateTime dtm = Convert.ToDateTime(configModel.LastImportDate);
                DateTime dtnow = dtm.AddHours(2) > DateTime.Now ? DateTime.Now : dtm.AddHours(2);

                AjustTimer(dtnow);


                IEnumerable<ReceitaDTO> er = this._receitaService.GetReceita(Convert.ToDateTime(configModel.LastImportDate), dtnow);
                Thread.Sleep(10);

                foreach (ReceitaDTO item in er)
                {
                    String cid = Guid.NewGuid().ToString();
                    FileInfo fiR = new FileInfo(Path.Combine(config.PathReceitaBag, cid + ".json"));
                    if (!fiR.Directory.Exists) fiR.Directory.Create();

                    ContC.Communication.Model.ReceitaCom rc = new Communication.Model.ReceitaCom();
                    rc.Descricao = "Communicação Automatica do EasyAssist";
                    rc.EmpresaId = config.EmpresaId;
                    rc.Valor = item.Valor;
                    rc.DataCadastro = dtnow;
                    rc.CommunicationId = cid;

                    switch (item.TipoReceita)
                    {
                        case 1:
                            rc.TipoReceita = "DINHEIRO";
                            break;
                        case 4:
                            rc.TipoReceita = "CARTAO";
                            break;
                    }
                    String sO = json_serializer.Serialize(rc);
                    File.WriteAllText(fiR.FullName, sO);

                    configModel.LastImportDate = dtnow.ToString("dd/MM/yyyy HH:mm:ss");
                }


                configModel.LastImportDate = dtnow.ToString("dd/MM/yyyy HH:mm:ss");

            }
            catch (Exception ex)
            {
                Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                if (configModel != null)
                {
                    String s = json_serializer.Serialize(configModel);

                    File.Delete(fi.FullName);
                    File.WriteAllText(fi.FullName, s);
                }
            }

            Thread.Sleep(100);
            //  ExecuteProperty.Instance.SetCloseCommunication();
        }

        private void AjustTimer(DateTime dtnow)
        {
            if ((dtnow - DateTime.Now) < new TimeSpan(1, 0, 0, 0))
            {
                timers = 30000;
            }
            else
            {
                timers = 1000;
            }
        }

        internal void Stop()
        {
            timer.Stop();
        }

        internal void StartActivityMonitoring(domain.services.Contracts.IReceitaService receitaService)
        {

            _receitaService = receitaService;
            timer.Interval = timers;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }
    }
}
