using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.CorssCutting.Exceptions
{
    public class ExceptionMessage : Exception
    {
        public ExceptionMessage(string msg) : base(msg) { }
    }
}