using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class ConnectionStrings : ConfigSection
    {
        internal ConnectionStrings(IConfiguration configuration)
            : base(configuration, configuration.GetSection("ConnectionStrings"))
        {
        }

        public string AppDb { get; set; }
    }
}
