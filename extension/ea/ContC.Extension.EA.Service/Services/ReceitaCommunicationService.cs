using ContC.Communication.Model;
using ContC.Extension.EA.crosscutting.utilities.Config;
using ContC.Extension.EA.domain.entities;
using ContC.Extension.EA.domain.services.Contracts;
using ContC.Extension.EA.Service.Config;
using ContC.Extension.EA.Service.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;

namespace ContC.Extension.EA.Service.Services
{
    public class ReceitaCommunicationService
    {
        ServerCommunicationConfig config = (ServerCommunicationConfig)System.Configuration.ConfigurationManager.GetSection("ServerCommunicationConfig");
        ContCExtension configExt = (ContCExtension)System.Configuration.ConfigurationManager.GetSection("ContCExtension");

        public ReceitaCommunicationService()
        {
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                ExecuteProperty.Instance.SetCloseCommunication();
                Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }

        }


        private void Execute()
        {
            //if (!ExecuteProperty.Instance.CheckConnection(config.Server + ":" + config.Port))
                //throw new Exception("A Conexão para o link '" + config.Server + ":" + config.Port + "' falhou !");

            IList<FileInfo> fis = new DirectoryInfo(configExt.PathReceitaBag).GetFiles().OrderBy(p => p.LastWriteTime).Take(15).ToList();
            foreach (FileInfo item in fis)
            {
                try
                {
                    String json = File.ReadAllText(item.FullName);
                    if (Communication(json))
                    {
                        item.Delete();
                    }
                }
                catch (Exception ex)
                {
                    Singleton.ExecuteProperty.Instance.EventLog.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                  //  if (!ExecuteProperty.Instance.CheckConnection(config.Server + ":" + config.Port))
                        //throw new Exception("A Conexão para o link '" + config.Server + ":" + config.Port + "' falhou !");
                }
            }
        }

        private bool Communication(String json)
        {
            
            using (WebClient client = new WebClient())
            {
                client.Headers["Content-type"] = "application/json";
                using (MemoryStream stream = new MemoryStream())
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    ReceitaCom rc = jss.Deserialize<ReceitaCom>(json);

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ReceitaCom));
                    serializer.WriteObject(stream, rc);

                    Uri uri = new Uri(config.Server + ":" + config.Port + "/Receitas.svc/Import");
                    byte[] response = client.UploadData(uri, "POST", stream.ToArray());

                    using (MemoryStream ms = new MemoryStream(response))
                    {
                        DataContractJsonSerializer djscr = new DataContractJsonSerializer(typeof(ReceitaComResponse));
                        ReceitaComResponse cresp = ((ReceitaComResponse)djscr.ReadObject(ms));
                        return cresp.FoiProcessado;
                    }
                }
            }
        }
    }
}
