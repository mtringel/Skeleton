using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public abstract class ConfigSection
    {
        protected internal ConfigSection(IConfigurationSection section)
        {
            if (section != null)
                section.Bind(this);
        }
    }
}