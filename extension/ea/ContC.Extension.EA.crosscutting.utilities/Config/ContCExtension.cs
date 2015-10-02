using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.crosscutting.utilities.Config
{
    public class ContCExtension : ConfigurationSection
    {
        [ConfigurationProperty("EmpresaId", IsRequired = true)]
        public int EmpresaId
        {
            get
            {
                return (int)this["EmpresaId"];
            }
        }
        [ConfigurationProperty("FileConfigJson", IsRequired = true)]
        public String FileConfigJson
        {
            get
            {
                return (String)this["FileConfigJson"];
            }
        }

        [ConfigurationProperty("PathReceitaBag", IsRequired = true)]
        public String PathReceitaBag
        {
            get
            {
                //C:\Desenvolvimento\MaSF\ContC\extension\ea\ContC.Extension.EA.Service\Com\Receita
                return (String)this["PathReceitaBag"];
            }
        }


    }
}
