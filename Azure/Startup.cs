using Microsoft.Extensions.DependencyInjection;
using TopTal.JoggingApp.Configuration;

namespace TopTal.JoggingApp.Azure 
{
    /// <summary>
    /// Register provided services here (services are almost always transient)
    /// </summary>
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, AppConfig appConfig)
        {
            services.AddTransient(typeof(GraphClient));
        }
    }
}
