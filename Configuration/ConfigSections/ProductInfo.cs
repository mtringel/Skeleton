using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public sealed class ProductInfo : ConfigSection
    {
        internal ProductInfo(IConfiguration configuration)
            : base(configuration.GetSection("ProductInfo"))
        {
        }

        public string Title { get; set; }

        public string Product { get; set; }

        public string Copyright { get; set; }

        public string Company { get; set; }

        public string Version { get; set; }
    }
}
