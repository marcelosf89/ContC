using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Communication.Model.ServerToClient
{
    public class GetValuesModel
    {
        public GetValuesEnum GetValuesEnum { get; set; }
    }

    public enum GetValuesEnum
    {
        GetReceitas
    }
}
