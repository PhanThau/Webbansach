using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nhom7_WebsiteClothes.Startup))]
namespace Nhom7_WebsiteClothes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
