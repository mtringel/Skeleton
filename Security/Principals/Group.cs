using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.Security.Principals
{
    /// <summary>
    /// Order is important!
    /// TopTal.JoggingApp.Security.Principals.RoleType must be synchronized with 'roleType' enum in Web.UI/ClientApp/app/shared/models/security/roleType.ts
    /// </summary>
    public enum Group
    {
        /// <summary>
        /// When logged in, a user can see, edit and delete his times he entered.
        /// A regular user would only be able to CRUD on their owned records.
        /// User must be the first (default).
        /// </summary>
        None ,

        /// <summary>
        /// A user manager would be able to CRUD users.
        /// </summary>
        Manager,

        /// <summary>
        /// An admin would be able to CRUD all records and users.
        /// </summary>
        Admin
    }

    public static class GroupHelper
    {
        public static readonly Group[] AllGroups = (Group[])System.Enum.GetValues(typeof(Group));

        private static readonly string[] AllNames = AllGroups.Select(t => t.ToString()).ToArray();

        private static readonly string[] AllTitles = AllGroups.Select(t => t.ToString().Replace('_', ' ')).ToArray();

        #region Helpers

        /// <summary>
        /// Serialized name
        /// </summary>
        public static string Name(this Group role)
        {
            return AllNames[(int)role];
        }

        /// <summary>
        /// Displayed title
        /// </summary>
        public static string Title(this Group role)
        {
            return AllTitles[(int)role];
        }

        /// <summary>
        /// Parse name or title
        /// </summary>
        public static Group Parse(string name)
        {
            // RoleType.User is the default
            if (string.IsNullOrEmpty(name))
                return Group.None;
            else
                return AllGroups.FirstOrDefault(t => string.Compare(t.Name(), name, true) == 0 || string.Compare(t.Title(), name, true) == 0);
        }

        #endregion

        #region Authorization

        public static bool Authorized(this Group role, params Permission[] permissions)
        {
            return Authorized(role, permissions, false);
        }

        public static bool Authorized(this Group role, Permission[] permissions, bool all)
        {
            if (all)
                return role == Group.Admin || permissions.All(t => role.Permissions().Contains(t));
            else
                return role == Group.Admin || permissions.Any(t => role.Permissions().Contains(t));
        }

        /// <summary>
        /// Returns null for Admin, since she is authorized for everything
        /// </summary>
        public static HashSet<Permission> Permissions(this Group role)
        {
            return PermissionHelper.SecurityMatrix[role];
        }

        #endregion
    }
}
