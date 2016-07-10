using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(wreq.Startup))]
namespace wreq
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
