using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IisSiteViewerWebApp.Startup))]
namespace IisSiteViewerWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
