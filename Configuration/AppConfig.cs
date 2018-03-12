using TopTal.JoggingApp.Configuration.ConfigSections;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration
{
    /// <summary>
    /// Lifetime: Singleton
    /// </summary>
    public class AppConfig
    {
        public AppConfig(IConfiguration configuration)
        {
            ConnectionStrings = new ConnectionStrings(configuration);
            AppDb = new AppDb();
            WebApplication = new WebApplication(configuration);
            ProductInfo = new ProductInfo(configuration);
            Security = new Security(configuration);
            ServiceApi = new ServiceApi(configuration);
            Logging = new Logging(configuration);
            AzureAdOptions = new AzureAdOptions(configuration);
        }

        public ConnectionStrings ConnectionStrings { get; private set; }

        public ProductInfo ProductInfo { get; private set; }

        public Security Security { get; private set; }

        public ServiceApi ServiceApi { get; private set; }

        public WebApplication WebApplication { get; private set; }

        public AppDb AppDb { get; private set; }

        public Logging Logging { get; private set; }

        public AzureAdOptions AzureAdOptions { get; private set; }
    }


}
