using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Service.Singleton
{
    public class ExecuteProperty
    {
        private static readonly ExecuteProperty _instance = new ExecuteProperty();
        public static ExecuteProperty Instance
        {
            get
            {
                return _instance;
            }
        }

        private ExecuteProperty() { }

        private bool _getStartCommunication;
        public bool GetStartCommunication { get { return _getStartCommunication; } set { _getStartCommunication = value; } }

        public void SetStartCommunication()
        {
            GetStartCommunication = true;
        }

        public void SetCloseCommunication()
        {
            GetStartCommunication = false;
        }

        public bool CheckConnection(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        public System.Diagnostics.EventLog EventLog { get; set; }
    }
}
