using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopTal.JoggingApp.Service.Api
{
    /// <summary>
    /// Register provided services here (services are almost always transient)
    /// </summary>
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Security
            services.AddTransient(typeof(Security.AuthService));

            // Users
            services.AddTransient(typeof(Users.UserService));
        }
    }
}
