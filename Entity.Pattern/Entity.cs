using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Pattern
{
    public class Entidade : IObjectState
    {
        
        public virtual ObjectState ObjectState { get; set; }
    }
}
