using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VikramWebRole.Startup))]
namespace VikramWebRole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
