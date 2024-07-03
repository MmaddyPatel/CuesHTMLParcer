using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CuseHTMLParcer.Startup))]
namespace CuseHTMLParcer
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
