using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PyoyectoTest.Startup))]
namespace PyoyectoTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
