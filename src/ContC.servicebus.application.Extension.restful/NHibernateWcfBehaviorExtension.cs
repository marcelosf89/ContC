using ContC.Repositories.Mapping.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ContC.servicebus.application.Extension.restful
{
    public class NHibernateWcfBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(NHibernateSessionPerRequest); }
        }

        protected override object CreateBehavior()
        {
            return new NHibernateSessionPerRequest();
        }
    }
}