#if DEBUG

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;

namespace TopTal.JoggingApp.Security.CustomAuthentication
{
    /// <summary>
    /// CustomAuthenticationNetCore20
    /// https://github.com/ignas-sakalauskas/CustomAuthenticationNetCore20
    /// </summary>
    public sealed class CustomAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "custom auth";

        public string Scheme => DefaultScheme;

        public StringValues AuthKey { get; set; }
    }
}

#endif