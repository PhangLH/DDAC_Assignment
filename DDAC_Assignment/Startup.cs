using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DDAC_Assignment.Startup))]
namespace DDAC_Assignment
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
