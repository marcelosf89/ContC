using ContC.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ContC.servicebus.application.Extension.restful
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IReceitas
    {

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Import", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ReceitaComResponse Import(ReceitaCom value);

    }
}
