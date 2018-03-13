using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
                 // [mtringel] Environment specific appsettings file
                 .ConfigureAppConfiguration((builderContext, config) =>
                 {
                     //IHostingEnvironment env = builderContext.HostingEnvironment;
#if DEBUG
                     config.AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true);
#else
                     config.AddJsonFile("appsettings.production.json", optional: false, reloadOnChange: true);
#endif
                 })
                .UseStartup<Startup>()
                .Build();
    }
}
