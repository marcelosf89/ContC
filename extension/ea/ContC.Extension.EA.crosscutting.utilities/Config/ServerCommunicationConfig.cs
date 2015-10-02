using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.crosscutting.utilities.Config
{
    public class ServerCommunicationConfig : ConfigurationSection
    {
        [ConfigurationProperty("Server", IsRequired = true)]
        public String Server
        {
            get
            {
                return this["Server"] as string;
            }
        }

        [ConfigurationProperty("Port", IsRequired = true)]
        public String Port
        {
            get
            {
                return this["Port"] as string;
            }
        }
    }
}
