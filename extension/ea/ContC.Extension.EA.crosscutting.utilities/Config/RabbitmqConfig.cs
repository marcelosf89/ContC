using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.crosscutting.utilities.Config
{
    public class RabbitmqConfig : ConfigurationSection
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
        public int Port
        {
            get
            {
                return (int)this["Port"];
            }
        }
        [ConfigurationProperty("Queue", IsRequired = true)]
        public String Queue
        {
            get
            {
                return this["Queue"] as string;
            }
        }
        [ConfigurationProperty("User", IsRequired = true)]
        public string User
        {
            get
            {
                return this["User"] as string;
            }
        }
        [ConfigurationProperty("Pass", IsRequired = true)]
        public string Pass
        {
            get
            {
                return this["Pass"] as string;
            }
        }
    }
}
