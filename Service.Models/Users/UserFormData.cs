using System.Collections.Generic;
using TopTal.JoggingApp.Security.Principals;

namespace TopTal.JoggingApp.Service.Models.Users
{
    /// <summary>
    /// DO NOT not expose entities from the BusinessEntities project to the clients!
    /// </summary>
    public sealed class UserFormData : Helpers.ServiceResultWithToken 
    {
        public User Entity;

        public KeyValuePair<Group, string>[] RoleTitles;
    }
}