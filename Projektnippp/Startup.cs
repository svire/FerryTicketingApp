using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projektnippp.Startup))]
namespace Projektnippp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
