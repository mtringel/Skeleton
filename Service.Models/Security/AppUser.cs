using System;
using System.Linq;
using TopTal.JoggingApp.Security.Principals;
using TopTal.JoggingApp.Service.Models.Helpers;

namespace TopTal.JoggingApp.Service.Models.Security
{
    /// <summary>
    /// Application specific user entity. AuthProvider.CurrentUser returns it.
    /// Contains all security information needed for authentication / authorization.
    /// DO NOT not expose entities from the BusinessEntities project to the clients!
    /// /// Should not contain information from SQL database, only what we can get from ClaimsIdentity from Azure (to avoid SQL roundtrips at each request).
    /// </summary>
    public sealed class AppUser : ServiceResult 
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        /// <summary>
        /// Azure User Id
        /// </summary>
        public string UserId { get; set; }

        public string TenantId { get; set; }

        public Group Group { get; set; }

        public string GroupName { get { return Group.Name(); } }

        public string GroupTitle { get { return Group.Title(); } }

        public Permission[] Permissions { get; set; }

        public AppUser()
        {
        }

        public AppUser(TopTal.JoggingApp.Security.Principals.AppUser user)
        {
            this.UserId = user.UserId;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Group = user.Group;
            this.TenantId = user.TenantId;
            this.FullName = user.FullName;

            if (user.Permissions != null)
                this.Permissions = user.Permissions.ToArray();
        }



    }
}