using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScalabelSimpleBlog.Startup))]
namespace ScalabelSimpleBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
