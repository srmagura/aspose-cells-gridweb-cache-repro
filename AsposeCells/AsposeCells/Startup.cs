using Aspose.Cells.GridWeb;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AsposeCells
{
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
            services.AddRazorPages();
            services.AddDistributedMemoryCache();

            // For Aspose.Cells.GridWeb & DocuVieware
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHostedService, GridScheduedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                // For Aspose.Cells.GridWeb
                endpoints.MapControllerRoute(
                    "acw",
                    "acw/{type}/{id}",
                    new { controller = "Acw", action = "Operation" }
                );

                endpoints.MapDefaultControllerRoute();
            });
        }
        }
}
