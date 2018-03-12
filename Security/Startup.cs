using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using TopTal.JoggingApp.Azure.Extensions;
using TopTal.JoggingApp.Configuration;

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

            // Authentication
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddAzureAd() // see Azure.Extensions.AzureAdAuthenticationBuilderExtensions
            .AddCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(appConfig.Security.AuthCookieExpirationIntervalMinutes);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(appConfig.Security.AuthCookieExpirationIntervalMinutes);
            });
        }
    }
}
