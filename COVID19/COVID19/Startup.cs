using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COVID19.Startup))]
namespace COVID19
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
