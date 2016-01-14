using ContC.crosscutting.Authentication.Interface;
using Microsoft.Practices.Unity;
using System.Web;
using System.Web.Mvc;

namespace ContC.presentation.mvc.Filters
{
    public class ContCAuthorizeAttribute : AuthorizeAttribute 
    {
        public ContCAuthorizeAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return gerenciadorAutenticacao.Get().Autenticado;
        }

        [Dependency]
        public IGerenciadorAutenticacao gerenciadorAutenticacao { get; set; }

    }
}