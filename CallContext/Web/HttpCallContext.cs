using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopTal.JoggingApp.Configuration;

namespace TopTal.JoggingApp.CallContext.Web
{
    /// <summary>
    /// Encapsulates all caller context related tasks.
    /// Wrapper around HttpContext.Current, but can be modified for different hosting options.
    /// HttpContext should ONLY be used here in the solution (Request, Response, Cookies, User etc.)
    /// </summary>
    public sealed class HttpCallContext : ICallContext
    {
        public HttpCallContext(
            IHttpContextAccessor httpContextAccessor,
            AppConfig appConfig,
            IAntiforgery antiforgery
            )
        {
            this.HttpContext = httpContextAccessor.HttpContext;
            this.Antiforgery = antiforgery;
            this.AppConfig = appConfig;
        }

        #region Services

        private HttpContext HttpContext;

        private IAntiforgery Antiforgery;

        private AppConfig AppConfig;

        #endregion        

        public ClaimsPrincipal Identity { get { return HttpContext.User; } }

        public string ResourceUri { get { return HttpContext.Request.Path; } } 

        #region Antiforgery Token

        /// <summary>
        /// Generates anti-forgery token.
        /// Should (or not) set the response cookie.
        /// Return cookie to be stored in hidden-field
        /// </summary>
        public AntiforgeryTokenSet AntiforgeryTokenGenerate()
        {
            return Antiforgery.GetAndStoreTokens(this.HttpContext);
        }

        /// <summary>
        /// Validates generated anti-forgery token at post back, if cookie + hidden field were supplied successfully
        /// </summary>
        public bool AntiforgeryTokenValidate(bool throwAntiforgeryValidationException)
        {
            try
            {
                Antiforgery.ValidateRequestAsync(HttpContext).Wait();
                return true;
            }
            catch (Exception ex)
            {
                if (!throwAntiforgeryValidationException)
                    return false;
                else if (ex is Microsoft.AspNetCore.Antiforgery.AntiforgeryValidationException ||
                    ex.InnerException is Microsoft.AspNetCore.Antiforgery.AntiforgeryValidationException)
                {
                    var newEx = new TopTal.JoggingApp.Exceptions.Validation.AntiforgeryTokenValidationException();

                    if (AppConfig.ServiceApi.ShowDetailedError)
                        newEx.DetailerErrorMessage = ex is Microsoft.AspNetCore.Antiforgery.AntiforgeryValidationException ? ex.ToString() : ex.InnerException.ToString();

                    throw newEx;
                }
                else
                    throw;
            }
        }

        #endregion
    }
}
