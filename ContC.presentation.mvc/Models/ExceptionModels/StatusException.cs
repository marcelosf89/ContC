using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.presentation.mvc.Models.ExceptionModels
{
    public class StatusException : Exception
    {
        public StatusException(string msg) : base(msg) { }


    }
}
