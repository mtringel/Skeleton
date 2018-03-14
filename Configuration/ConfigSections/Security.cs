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
            : base(configuration, configuration.GetSection("Security"))
        {
        }

        public int AuthCookieExpirationIntervalMinutes { get; set; }

#if DEBUG
        /// <summary>
        /// Skips Azure authentication and impersonate the Admin user silently (for development purposes only)
        /// See appsettings.development.json / Security / ImpersonateAdminUserInDebugMode for configuration settings
        /// See TopTal.JoggingApp.AzureHelper.Principals.ClaimsPrincipal.AdminUser() for impersonation details
        /// </summary>
        public bool ImpersonateAdminUserInDebugMode { get; set; }
#endif
    }
}