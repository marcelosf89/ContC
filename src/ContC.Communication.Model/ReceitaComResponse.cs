using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Communication.Model
{
    [DataContract]
    public class ReceitaComResponse
    {
        [DataMember]
        public virtual bool FoiProcessado { get; set; }
        [DataMember]
        public virtual String Json { get; set; }
    }
}
