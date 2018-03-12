
using TopTal.JoggingApp.Service.Models.Helpers;

namespace TopTal.JoggingApp.Service.Models.Users
{
    /// <summary>
    /// DO NOT not expose entities from the BusinessEntities project to the clients!
    /// </summary>
    public sealed class UserListData : ServiceResult
    {
        /// <summary>
        /// Top 100 items only
        /// </summary>
        public User[] List;

        /// <summary>
        /// If there are more than 100 returned items
        /// </summary>
        public bool TooMuchData;
    }
}