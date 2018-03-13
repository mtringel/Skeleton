using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace TopTal.JoggingApp.Configuration.ConfigSections
{
    public abstract class ConfigSection
    {
        protected internal ConfigSection(IConfiguration configuration, IConfigurationSection section)
        {
            if (section != null)
            {
                // bind appsettings.json
                section.Bind(this);

                // overwrite with environment variables (loaded into configuration in Program.cs by calling config.AddEnvironmentVariables())
                // Azure Application Settings / Connection strings goes to GetSection("ConnectionStrings")["..."], overwriting values coming from appsettings.json
                // Azure Application Settings / Application settings goes to root config (configuration["..."]) and therefore expected to be specified as sectionName.propertyName
                var properties = this.GetType().GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);

                foreach (var property in properties)
                {
                    var key = $"{section.Key}.{property.Name}";

                    try
                    {
                        property.SetValue(this, configuration.GetValue(property.PropertyType, key, property.GetValue(this)));
                    }
                    catch
                    {
                        throw new InvalidCastException($"Value of environment variable '{key}' cannot be set into {property.PropertyType}.");
                    }
                }
            }
        }
    }
}