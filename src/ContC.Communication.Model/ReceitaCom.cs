using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Communication.Model
{
    [DataContract]
    public class ReceitaCom
    {
        [DataMember]
        public virtual string Descricao { get; set; }
        [DataMember]
        public virtual decimal Valor { get; set; }
        [DataMember]
        public virtual DateTime DataCadastro { get; set; }
        [DataMember]
        public virtual decimal EmpresaId { get; set; }
        [DataMember]
        public string TipoReceita { get; set; }
        [DataMember]
        public string CommunicationId { get; set; }
    }
}
