using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCShort.Startup))]
namespace MVCShort
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
