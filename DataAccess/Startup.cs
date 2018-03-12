using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TopTal.JoggingApp.DataAccess.Helpers;

namespace TopTal.JoggingApp.DataAccess
{
    /// <summary>
    /// Register provided services here (services are almost always transient)
    /// </summary>
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);
            services.AddScoped(typeof(ITransactionManager), t => t.GetService(typeof(AppDbContext))); // use the very same dbcontext instance

            // Users
            services.AddTransient(typeof(Users.UserDataProvider));
        }
    }
}
