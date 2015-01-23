using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViShop.WebCrm.Startup))]
namespace ViShop.WebCrm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
