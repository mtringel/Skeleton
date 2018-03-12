using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class WebApplication : ConfigSection
    {
        internal WebApplication(IConfiguration configuration)
            : base(configuration.GetSection("WebApplication"))
        {
        }

        public bool CookieConsent { get; set; }

        public string BasePath { get; set; }

        public string BaseUrl { get; set; }

        public int GridPageSize { get; set; }

        public int GridMaxRows { get; set; }
    }
}