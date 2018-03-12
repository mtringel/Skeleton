using System.Collections.Generic;

namespace TopTal.JoggingApp.Security.Principals
{
    /// <summary>
    /// This entity is SENT to the client!
    /// Application specific user entity
    /// Contains all security information needed for authentication / authorization.
    /// Should not contain information from SQL database, only what we can get from ClaimsIdentity from Azure (to avoid SQL roundtrips at each request).
    /// </summary>
    public sealed class AppUser : Azure.Principals.ClaimsPrincipal
    {
        public Group Group { get; private set; }

        /// <summary>
        /// Returns null for Admin, since she is authorized for everything
        /// </summary>
        public HashSet<Permission> Permissions { get { return Group.Permissions(); } }

        public AppUser(System.Security.Claims.ClaimsPrincipal principal, Group group)
            : base(principal)
        {
            this.Group = group;
        }  

    }
}