using AsyncControllers;

using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace AsyncControllers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
