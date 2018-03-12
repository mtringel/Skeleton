using Microsoft.Extensions.DependencyInjection;
using System;

namespace TopTal.JoggingApp.DatabaseInitializers
{
    /// <summary>
    /// Register provided services here (services are almost always transient)
    /// </summary>
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(Initializer));
        }
    }
}
