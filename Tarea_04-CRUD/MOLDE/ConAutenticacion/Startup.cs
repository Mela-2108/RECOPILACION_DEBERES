using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConAutenticacion.Startup))]
namespace ConAutenticacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
