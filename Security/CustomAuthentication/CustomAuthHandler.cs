#if DEBUG

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace TopTal.JoggingApp.Security.CustomAuthentication
{
    /// <summary>
    /// CustomAuthenticationNetCore20
    /// https://github.com/ignas-sakalauskas/CustomAuthenticationNetCore20
    /// </summary>
    public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // [mtringel] we don't want to authenticate in development mode, just return the admin user

            //// Get Authorization header value
            //if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
            //{
            //    return Task.FromResult(AuthenticateResult.Fail("Cannot read authorization header."));
            //}

            //// The auth key from Authorization header check against the configured ones
            //if (authorization.Any(key => Options.AuthKey.All(ak => ak != key)))
            //{
            //    return Task.FromResult(AuthenticateResult.Fail("Invalid auth key."));
            //}

            // Create authenticated user
            // var identities = new[] { new ClaimsIdentity("custom auth type") };

            // Skips Azure authentication and impersonate the Admin user silently (for development purposes only)
            // See appsettings.development.json / Security / ImpersonateAdminUserInDebugMode for configuration settings
            // See TopTal.JoggingApp.AzureHelper.Principals.ClaimsPrincipal.AdminUser() for impersonation details
            var ticket = new AuthenticationTicket(AzureHelper.Principals.ClaimsPrincipal.AdminUser(), Options.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}

#endif