using System.Linq;

namespace TopTal.JoggingApp.Azure.Principals
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
        public const string ClaimType_Name = "http://schemas.microsoft.com/identity/claims/name";
        public const string ClaimType_ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        #endregion

        #region Azure v2.0 endpoint (not used)

        //public const string ClaimType_PreferredUserName = "preferred_username";
        //public const string ClaimType_ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        //public const string ClaimType_EmailAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        //public const string ClaimType_TenantId = "http://schemas.microsoft.com/identity/claims/tenantid";

        #endregion

        public ClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal)
        {
            UserId = principal.Identity.Name;
            TenantId = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_TenantId)?.Value;
            FirstName = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_SurName)?.Value;
            LastName = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_GivenName)?.Value;
            FullName = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_Name)?.Value;
            Email = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_EmailAddress)?.Value;
            ObjectId = principal.Claims.FirstOrDefault(t => t.Type == ClaimType_ObjectIdentifier)?.Value;
        }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string TenantId { get; set; }

        public string ObjectId { get; set; }
    }
}
