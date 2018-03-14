using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using TopTal.JoggingApp.AzureHelper.Authentication;
using TopTal.JoggingApp.Configuration;
using TopTal.JoggingApp.Security.CustomAuthentication;

namespace TopTal.JoggingApp.Security 
{
    /// <summary>
    /// Register provided services here (services are almost always transient)
    /// </summary>
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, AppConfig appConfig)
        {
            // Services
            services.AddScoped(typeof(Managers.IAuthProvider), typeof(Managers.AuthProvider));

#if DEBUG
            // Skips Azure authentication and impersonate the Admin user silently (for development purposes only)
            // See appsettings.development.json / Security / ImpersonateAdminUserInDebugMode for configuration settings
            // See TopTal.JoggingApp.AzureHelper.Principals.ClaimsPrincipal.AdminUser() for impersonation details
            if (appConfig.Security.ImpersonateAdminUserInDebugMode)
            {
                // XXX
                // Add authentication 
                services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = CustomAuthOptions.DefaultScheme;
                        options.DefaultChallengeScheme = CustomAuthOptions.DefaultScheme;
                    })
                    // Call custom authentication extension method
                    .AddCustomAuth(options =>
                    {
                        // Configure password for authentication
                        options.AuthKey = "custom auth key";
                    });

            }
            else
            {
#endif
                // Authentication
                services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddAzureAd(appConfig)
                .AddCookie(options =>
                {
                    // Cookie settings
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Expiration = TimeSpan.FromMinutes(appConfig.Security.AuthCookieExpirationIntervalMinutes);
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(appConfig.Security.AuthCookieExpirationIntervalMinutes);
                });
#if DEBUG
            }
#endif
        }
    }
}
