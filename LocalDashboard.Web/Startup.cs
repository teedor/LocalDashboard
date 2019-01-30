using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalDashboard.Web.Startup))]
namespace LocalDashboard.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
