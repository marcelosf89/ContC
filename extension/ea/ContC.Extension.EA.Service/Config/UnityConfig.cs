using ContC.Extension.EA.domain.services.Contracts;
using ContC.Extension.EA.domain.services.Implementations;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Service.Config
{
    public class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<IReceitaService, ReceitaService>();
        }
    }
}
