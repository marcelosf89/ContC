using System;
using System.Web;
using NHibernate;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace ContC.Repositories.Mapping.Configuration
{
    public class NHibernateSessionPerRequest : IHttpModule, IDispatchMessageInspector, IServiceBehavior
    {

        public void Init(HttpApplication context)
        {

            context.BeginRequest += BeginRequest;

            context.EndRequest += EndRequest;

        }

        public static ISession GetCurrentSession()
        {

            return NHibernateWebSessionFactory.GetInstancia().GetSession();

        }

        public void Dispose() { }

        private static void BeginRequest(object sender, EventArgs e)
        {

            ISession session = NHibernateWebSessionFactory.GetInstancia().GetSessionFactory().OpenSession();

            NHibernateWebSessionFactory.GetInstancia().BindSession(session);

        }

        private static void EndRequest(object sender, EventArgs e)
        {

            NHibernateWebSessionFactory.GetInstancia().UnBindSession();

        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            IniciarRequisicao();
            return null;
        }

        public void IniciarRequisicao()
        {
            BeginRequest(null, null);
        }

        public void FinalizarRequisicao()
        {
            EndRequest(null, null);
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            FinalizarRequisicao();
        }


        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher ed in cd.Endpoints)
                {
                    ed.DispatchRuntime.MessageInspectors.Add(this);
                }
            }

        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {

        }
    }

}
