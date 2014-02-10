using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MelonReader.Startup))]
namespace MelonReader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
