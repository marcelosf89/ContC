using ContC.presentation.mvc.Extension.ModelBinder;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq;


namespace ContC.presentation.mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            
            AreaRegistration.RegisterAllAreas();
            IUnityContainer container = new UnityContainer();
            Bootstrapper.RegisterTypes(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));


            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
        }
    }
}
