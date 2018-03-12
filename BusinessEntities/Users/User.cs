using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TopTal.JoggingApp.BusinessEntities.Helpers;
using TopTal.JoggingApp.Security.Principals;

namespace TopTal.JoggingApp.BusinessEntities.Users
{
    /// <summary>
    /// Internal entity, not sent to the client
    /// Contains all information for user management forms.
    /// Only data mapping attributes are used here. Validation is done in the Service.Api by MVC using the model from Service.Model.
    /// </summary>
    public sealed class User : Entity
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Azure user Id
        /// </summary>
        [StringLength(100)]
        [Required]
        [Key]
        public string UserId { get; set; }

        [StringLength(50)]
        [Required]
        public string TenantId { get; set; }

        [StringLength(50)]
        [Required]
        public string GroupName
        {
            get { return Group.Name(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Group = Group.None;
                else
                    Group = GroupHelper.Parse(value);
            }
        }

        [StringLength(50)]
        [NotMapped]
        public string GroupTitle
        {
            get { return Group.Title(); }
        }

        [NotMapped]
        public Group Group { get; set; }

        public override object[] Keys()
        {
            return new object [] { UserId };
        }

        public User(
            string userId,
            string tenantId,
            string firstName,
            string lastName,
            string fullName,
            string email,
            Group group
            )
        {
            this.UserId = userId;
            this.TenantId = tenantId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FullName = fullName;
            this.Email = email;
            this.Group = group;
        }
    }
}
