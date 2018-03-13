using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TopTal.JoggingApp.Exceptions;

namespace TopTal.JoggingApp.Web.UI
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
            services.AddMvc(setupAction =>
            {
                setupAction.Filters.Add(typeof(ExceptionFilter));
            });

            // This sample uses an in-memory cache for tokens and subscriptions. Production apps will typically use some method of persistent storage.

            #region Configure services

            // CallContext
            TopTal.JoggingApp.CallContext.Startup.ConfigureServices(services);

            // Configuration
            TopTal.JoggingApp.Configuration.Startup.ConfigureServices(services);
            var config = services.BuildServiceProvider().GetService<TopTal.JoggingApp.Configuration.AppConfig>();

            // Azure - needs AppConfig
            TopTal.JoggingApp.Azure.Startup.ConfigureServices(services, config);

            // Security - needs AppConfig, must be after Azure
            TopTal.JoggingApp.Security.Startup.ConfigureServices(services, config);

            // DataAccess
            TopTal.JoggingApp.DataAccess.Startup.ConfigureServices(services);

            // DatabaseInitializers
            TopTal.JoggingApp.DatabaseInitializers.Startup.ConfigureServices(services);

            // BusinessLogic
            TopTal.JoggingApp.BusinessLogic.Startup.ConfigureServices(services);

            // Services
            TopTal.JoggingApp.Service.Api.Startup.ConfigureServices(services);

            #endregion

            #region Anti-forgery token validation

            services.AddAntiforgery(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(config.ServiceApi.AntiForgeryTokenCookieExpiresAfterMinutes);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            #endregion  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}