using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FamilyReunion.Startup))]
namespace FamilyReunion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
