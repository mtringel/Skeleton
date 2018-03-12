
using Microsoft.AspNetCore.Antiforgery;
using System.Security.Claims;

namespace TopTal.JoggingApp.CallContext
{
    /// <summary>
    /// Lifetime: Scoped (current request)
    /// Encapsulates all caller context related tasks.
    /// Default implementation for Web applications is just a wrapper around HttpContext.Current, but can be modified for different hosting options.
    /// HttpContext should ONLY be used here in the solution (Request, Response, Cookies, User etc.)
    /// </summary>
    public interface ICallContext
    {
        string ResourceUri { get; }

        ClaimsPrincipal Identity { get; }

        /// <summary>
        /// Generates anti-forgery token.
        /// Should (or not) set the response cookie.
        /// Returns token to be returned to the client (cookie is in response header).
        /// </summary>
        AntiforgeryTokenSet AntiforgeryTokenGenerate();

        /// <summary>
        /// Validates generated anti-forgery token at post back, if cookie + token were supplied successfully
        /// </summary>
        bool AntiforgeryTokenValidate(bool throwAntiforgeryTokenValidationException);
    }
}
