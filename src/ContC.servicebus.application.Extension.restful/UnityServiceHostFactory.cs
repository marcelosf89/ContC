using ContC.domain.services.Contracts;
using ContC.domain.services.Implementations;
using Infrastructure.Unityprovider;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace ContC.servicebus.application.Extension.restful
{
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        private readonly IUnityContainer container;

        public UnityServiceHostFactory()
        {
            try
            {
                container = new UnityContainer();
                container.RegisterType<IReceitaService, ReceitaService>();
             //   ContC.domain.services.BootstraperService.RegisterTypes(container);
            }
            catch (Exception ex)
            {

            }
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(this.container, serviceType, baseAddresses);
        }
    }

}