using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KID.Startup))]
namespace KID
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
