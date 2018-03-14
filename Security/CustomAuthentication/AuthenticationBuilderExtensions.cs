#if DEBUG

using System;
using Microsoft.AspNetCore.Authentication;

namespace TopTal.JoggingApp.Security.CustomAuthentication
{
    /// <summary>
    /// CustomAuthenticationNetCore20
    /// https://github.com/ignas-sakalauskas/CustomAuthenticationNetCore20
    /// </summary>
    public static class AuthenticationBuilderExtensions
    {
        // Custom authentication extension method
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<CustomAuthOptions> configureOptions)
        {
            // Add custom authentication scheme with custom options and custom handler
            return builder.AddScheme<CustomAuthOptions, CustomAuthHandler>(CustomAuthOptions.DefaultScheme, configureOptions);
        }
    }
}

#endif