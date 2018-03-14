using System.Linq;

namespace TopTal.JoggingApp.AzureHelper.Principals
{
    /// <summary>
    /// We use v1.0 endpoints
    /// https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-v2-limitations
    /// 
    /// If you must support personal Microsoft accounts in your application, use the v2.0 endpoint. But before you do, be sure that you understand the limitations that we discuss in this article.
    /// If your application only needs to support Microsoft work and school accounts, don't use the v2.0 endpoint. Instead, refer to our Azure AD developer guide.
    /// </summary>
    public class ClaimsPrincipal
    {
        #region Azure v1.0 endpoint

        public const string ClaimType_EmailAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public const string ClaimType_SurName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        public const string ClaimType_GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        public const string ClaimType_TenantId = "http://schemas.microsoft.com/identity/claims/tenantid";
        public const string ClaimType_UserName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        public const string ClaimType_FullName = "name";
        public const string ClaimType_ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        #endregion

        public ClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal)
        {
            UserId = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_UserName)?.Value;
            TenantId = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_TenantId)?.Value;
            FirstName = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_GivenName)?.Value;
            LastName = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_SurName)?.Value;
#if DEBUG
            FullName = $"{FirstName} {LastName}";
#else
            FullName = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_FullName)?.Value;
#endif
            Email = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_EmailAddress)?.Value;
            ObjectId = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_ObjectIdentifier)?.Value;
        }

#if DEBUG
        /// <summary>
        /// Skips Azure authentication and impersonate the Admin user silently (for development purposes only)
        /// See appsettings.development.json / Security / ImpersonateAdminUserInDebugMode for configuration settings
        /// See TopTal.JoggingApp.AzureHelper.Principals.ClaimsPrincipal.AdminUser() for impersonation details
        /// </summary>
        public static System.Security.Claims.ClaimsPrincipal AdminUser()
        {
            return new System.Security.Claims.ClaimsPrincipal(new[]
            {
                new System.Security.Claims.ClaimsIdentity(
                    new System.Security.Principal.GenericIdentity("live.com#nekosoft.bt@gmail.com", "custom auth type"),
                    new[]{
                        new System.Security.Claims.Claim(ClaimType_UserName, "live.com#nekosoft.bt@gmail.com"),
                        new System.Security.Claims.Claim(ClaimType_SurName, "Admin"),
                        new System.Security.Claims.Claim(ClaimType_GivenName, "Nekosoft"),
                        //new System.Security.Claims.Claim(ClaimType_FullName, "Nekosoft Admin"), don't set it, names will mix
                        new System.Security.Claims.Claim(ClaimType_EmailAddress, "nekosoft.bt@gmail.com"),
                        new System.Security.Claims.Claim(ClaimType_TenantId, "3a9d8c99-f7d8-4418-a7de-1f864008974a"),
                        new System.Security.Claims.Claim(ClaimType_ObjectIdentifier, "db7da612-5d5a-41eb-8a40-976af2caf7a9")
                    })
            });
        }
#endif

        public string UserId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string FullName { get; private set; }

        public string Email { get; private set; }

        public string TenantId { get; private set; }

        public string ObjectId { get; private set; }
    }
}