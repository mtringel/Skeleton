using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class Security : ConfigSection
    {
        internal Security(IConfiguration configuration)
            : base(configuration.GetSection("Security"))
        {
        }
    
        public int AuthCookieExpirationIntervalMinutes { get; set; }
    }
}