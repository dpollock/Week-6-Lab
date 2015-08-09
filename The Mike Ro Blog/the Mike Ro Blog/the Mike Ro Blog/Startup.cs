using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(the_Mike_Ro_Blog.Startup))]
namespace the_Mike_Ro_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
