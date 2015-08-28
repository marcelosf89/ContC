using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContC.presentation.mvc.Startup))]
namespace ContC.presentation.mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
