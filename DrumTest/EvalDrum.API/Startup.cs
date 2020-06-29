using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EvalDrum.API.Startup))]

namespace EvalDrum.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }

    
    }
}
