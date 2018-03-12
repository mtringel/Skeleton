
using TopTal.JoggingApp.Service.Models.Helpers;

namespace TopTal.JoggingApp.Service.Models.Users
{
    /// <summary>
    /// DO NOT not expose entities from the BusinessEntities project to the clients!
    /// </summary>
    public sealed class UserSelectItem : Model 
    {
        /// <summary>
        /// Azure User Id
        /// </summary>
        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public UserSelectItem()
        {
        }

        public UserSelectItem(BusinessEntities.Users.User entity)
        {
            UserId = entity.UserId;
            UserFullName = entity.FullName;
        }
    }
}
