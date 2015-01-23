using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViShop.PhoneShop.Startup))]
namespace ViShop.PhoneShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
