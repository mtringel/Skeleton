using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.Security.Principals
{
    /// <summary>
    /// TopTal.JoggingApp.Security.Principals.Permission must be synchronized with 'permission' enum in Web.UI/ClientApp/app/shared/models/security/permission.ts
    /// </summary>
    public enum Permission
    {
        /// <summary>
        /// Minimum permission for a logged in user
        /// </summary>
        User_LoggedIn,

        /// <summary>
        /// User edits her own profile
        /// </summary>
        User_EditProfile,

        /// <summary>
        /// Manage all user profiles
        /// </summary>
        User_Management,

        /// <summary>
        /// Set user role to other than User
        /// UserManager cannot do it, only Admin can do it.
        /// </summary>
        User_Management_SetRole,

        /// <summary>
        /// UserManager cannot edit Admin users, only Admins can do it
        /// </summary>
        User_Management_EditAdmins,

        /// <summary>
        /// Manage own entries
        /// </summary>
        JogEntry_ManageOwn,

        /// <summary>
        /// Manage all entries
        /// </summary>
        JogEntry_ManageAll
    }

    public static class PermissionHelper
    {
        public static readonly Permission[] AllPermissions = (Permission[])System.Enum.GetValues(typeof(Permission));

        private static readonly string[] AllNames = AllPermissions.Select(t => t.ToString()).ToArray();

        private static readonly string[] AllTitles = AllPermissions.Select(t => t.ToString().Replace('_', ' ')).ToArray();

        #region Security Matrix

        /// <summary>
        /// SecurityMatrix[(int)roleType, (int)permission]
        /// </summary>
        internal static readonly Dictionary<Group, HashSet<Permission>> SecurityMatrix;

        #endregion

        static PermissionHelper()
        {
            SecurityMatrix = new Dictionary<Group, HashSet<Permission>>();

            // User permissions
            SecurityMatrix[Group.None] = new HashSet<Permission>(new[]{
                Permission.User_LoggedIn ,
                Permission.User_EditProfile ,
                Permission.JogEntry_ManageOwn
            });

            // Manager permissions
            SecurityMatrix[Group.Manager] = new HashSet<Permission>(SecurityMatrix[Group.None].Union(new[]{
                Permission.User_Management
            }));

            // Admin has all permissions by default, no need to list it
            SecurityMatrix[Group.Admin] = null;
        }

        #region Helpers

        /// <summary>
        /// Serialized name
        /// </summary>
        public static string Name(this Permission permission)
        {
            return AllNames[(int)permission];
        }

        /// <summary>
        /// Displayed title
        /// </summary>
        public static string Title(this Permission permission)
        {
            return AllTitles[(int)permission];
        }

        #endregion
    }
}
