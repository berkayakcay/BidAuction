using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BidAuction.Startup))]
namespace BidAuction
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            

            ConfigureAuth(app);
            app.MapSignalR();

        }
    }
}
