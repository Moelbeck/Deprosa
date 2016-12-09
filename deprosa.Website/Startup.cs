using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(deprosa.Website.Startup))]
namespace deprosa.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
