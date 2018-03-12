using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class AzureAdOptions : ConfigSection
    {
        public AzureAdOptions()
            : base(null)
        {
        }

        internal AzureAdOptions(IConfiguration configuration)
            : base(configuration.GetSection("AzureAd"))
        {
        }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Instance { get; set; }

        //public string Domain { get; set; }

        //public string TenantId { get; set; }

        public string CallbackPath { get; set; }

        public string Scopes { get; set; }

        public string GraphResourceId { get; set; }

        public string GraphScopes { get; set; }
    }
}