using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContC.Extension.EA.presentation.mvc.Startup))]
namespace ContC.Extension.EA.presentation.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
