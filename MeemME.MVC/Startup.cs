using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MeemME.MVC.Startup))]
namespace MeemME.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
