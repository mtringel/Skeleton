using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopTal.JoggingApp.Security.Principals;
using TopTal.JoggingApp.Service.Models.Helpers;

namespace TopTal.JoggingApp.Service.Models.Users
{
    /// <summary>
    /// This model is SENT to the client
    /// Contains all information for user management forms.
    /// MVC validation attributes are used here with JsonIgnore for not serialized members.
    /// DO NOT not expose entities from the BusinessEntities project to the clients!
    /// </summary>
    public sealed class User : Model
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Azure User Id
        /// </summary>
        [StringLength(100)]
        [Required]
        [Key]
        public string UserId { get; set; }

        [StringLength(50)]
        [Required]
        public string TenantId { get; set; }

        [Display(Name = "Group")]
        public string GroupTitle
        {
            get { return Group.Title(); }
        }

        [Required]
        public Group Group { get; set; }

        public User()
        {
        }

        #region Entity conversion

        public User(BusinessEntities.Users.User entity)
        {
            this.UserId = entity.UserId;
            this.FirstName = entity.FirstName;
            this.LastName = entity.LastName;
            this.Email = entity.Email;
            this.Group = entity.Group;
            this.TenantId = entity.TenantId;
            this.FullName = entity.FullName;
        }

        public BusinessEntities.Users.User ToEntity()
        {
            return new BusinessEntities.Users.User(UserId, TenantId, FirstName, LastName, FullName, Email, Group);
        }

        #endregion
    }
}
