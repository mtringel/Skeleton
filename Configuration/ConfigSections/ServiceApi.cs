using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class ServiceApi : ConfigSection
    {
        internal ServiceApi(IConfiguration configuration)
            : base (configuration.GetSection("ServiceApi"))
        {
        }

        public bool UseAntiForgeryToken { get; set; }

        public int AntiForgeryTokenCookieExpiresAfterMinutes { get; set; }

        public bool ShowDetailedError { get; set; }

        public int MaximumReturnedRows { get; set; }
    }
}