using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChanLap.Web.Startup))]
namespace ChanLap.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
