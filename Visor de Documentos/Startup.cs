using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VisorDeDocumentos.Startup))]
namespace VisorDeDocumentos
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
