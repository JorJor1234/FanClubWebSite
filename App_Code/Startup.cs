using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FanClubWebSite.Startup))]
namespace FanClubWebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
