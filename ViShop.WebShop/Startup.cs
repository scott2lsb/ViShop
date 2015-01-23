using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViShop.WebShop.Startup))]
namespace ViShop.WebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
