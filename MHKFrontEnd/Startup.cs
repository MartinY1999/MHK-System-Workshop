using MHKFrontEnd.Services;
using MHKFrontEnd.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MHKFrontEnd
{
    //TODO:Move indicators main pages to Indicators folder
    //TODO:Move indicator sub pages to IndicatorPages folder
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IAPIClient, APIClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["serviceUrl"]);
            });
            services.AddSingleton<IAdminService, AdminService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAuthenticatedUser()
                          .RequireIsAdminClaim();
                });
            });
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin", "Admin");
                options.Conventions.AuthorizePage("/Search", "Admin");
                options.Conventions.AuthorizePage("/Finance", "Admin");
                options.Conventions.AuthorizePage("/Project", "Admin");
                options.Conventions.AuthorizePage("/Sale", "Admin");
                options.Conventions.AuthorizePage("/Support", "Admin");
                options.Conventions.AuthorizePage("/FinancePage", "Admin");
                options.Conventions.AuthorizePage("/ProjectPage", "Admin");
                options.Conventions.AuthorizePage("/SalePage", "Admin");
                options.Conventions.AuthorizePage("/SupportPage", "Admin");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
