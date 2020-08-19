using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment5_SanghyukLee.Startup))]
namespace Assignment5_SanghyukLee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
