using ContC.Extension.EA.domain.services.Contracts;
using ContC.Extension.EA.domain.services.Implementations;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.domain.services
{
    public static class BootstraperService
    {

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IReceitasRepository, ReceitasRepository>();
            
        }


    }
}
