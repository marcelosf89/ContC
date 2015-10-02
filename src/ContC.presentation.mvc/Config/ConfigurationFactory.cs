using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Config
{
   public class ConfigurationFactory
    {
       private static readonly ConfigurationFactory _instance = new ConfigurationFactory();

       public static ConfigurationFactory Instance { get { return _instance; } }
       private ConfigurationFactory() { }

       public String PastaComprovante
       {
           get
           {
               String s = ConfigurationManager.AppSettings["pastaComprovante"];
               if (!Directory.Exists(s)) { Directory.CreateDirectory(s); }

               return ConfigurationManager.AppSettings["pastaComprovante"];
           }

       }

       public String PastaTemp
       {
           get
           {
               String s = ConfigurationManager.AppSettings["pastaTemp"];
               if (!Directory.Exists(s)) { Directory.CreateDirectory(s); }

               return ConfigurationManager.AppSettings["pastaTemp"];
           }
       }

       public String PastaBoleto {
           get
           {
               String s = ConfigurationManager.AppSettings["pastaBoleto"];
               if (!Directory.Exists(s)) { Directory.CreateDirectory(s); }

               return ConfigurationManager.AppSettings["pastaBoleto"];
           }
       }
    }
}
