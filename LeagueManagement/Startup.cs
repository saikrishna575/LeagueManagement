using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeagueManagement.Startup))]
namespace LeagueManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
