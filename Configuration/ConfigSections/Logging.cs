using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class Logging : ConfigSection
    {
        internal Logging(IConfiguration configuration)
            : base(configuration.GetSection("Logging"))
        {
        }

        public string EventSource { get; set; }
    }
}