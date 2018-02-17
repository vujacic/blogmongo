using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(blogmongo.Startup))]
namespace blogmongo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
