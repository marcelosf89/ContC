using ContC.Extension.EA.CorssCutting.Exceptions;
using ContC.Extension.EA.presentation.mvc.Models.ExceptionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContC.Extension.EA.presentation.mvc.Extension
{
    public class ControllerException : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception == null) return;

            Type exceptionType = filterContext.Exception.GetType();

            if (exceptionType == typeof(StatusException) || exceptionType == typeof(ExceptionMessage))
            {

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.StatusDescription = filterContext.Exception.Message;
            }
        }
    }
}
