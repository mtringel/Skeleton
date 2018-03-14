using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Web.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                 .ConfigureAppConfiguration((builderContext, config) =>
                 {
                     //IHostingEnvironment env = builderContext.HostingEnvironment;
                     config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#if DEBUG
                     config.AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true);
#else
                     config.AddJsonFile("appsettings.production.json", optional: false, reloadOnChange: true);
#endif
                     config.AddEnvironmentVariables();
                 })
                .UseStartup<Startup>()
                .Build();
    }
}
