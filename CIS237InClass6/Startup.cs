using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CIS237InClass6.Startup))]
namespace CIS237InClass6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
