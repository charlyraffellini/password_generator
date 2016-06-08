using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PasswordGenerator.Startup))]
namespace PasswordGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
