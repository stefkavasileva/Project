using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Landmarks.Web.Areas.Identity.IdentityHostingStartup))]
namespace Landmarks.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}