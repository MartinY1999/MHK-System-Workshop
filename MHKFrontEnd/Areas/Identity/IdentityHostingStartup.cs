using MHKFrontEnd.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(MHKFrontEnd.Areas.Identity.IdentityHostingStartup))]
namespace MHKFrontEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityDbContextConnection")));

                services.AddDefaultIdentity<User>().AddEntityFrameworkStores<IdentityDbContext>()
                                                               .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();
                //services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                //.AddEntityFrameworkStores<IdentityDbContext>();

                services.Configure<IdentityOptions>(options =>
                {
                    //Password Settings
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 0;

                    //User Settings
                    options.User.RequireUniqueEmail = true;
                });
            });
        }
    }
}