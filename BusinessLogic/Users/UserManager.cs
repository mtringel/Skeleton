using System.Collections.Generic;
using TopTal.JoggingApp.BusinessEntities.Users;
using TopTal.JoggingApp.DataAccess.Users;
using TopTal.JoggingApp.BusinessLogic.Helpers;
using TopTal.JoggingApp.CallContext;

namespace TopTal.JoggingApp.BusinessLogic.Users
{
    /// <summary>
    /// Lifetime: n/a (transient or current request, undetermined, don't rely on internal state)
    /// </summary>
    public sealed class UserManager : BusinessLogicManagerBase
    {
        #region Services

        /// <summary>
        /// BusinessLogicManagers should not call each others DataProvider, but can call each other.
        /// </summary>
        private UserDataProvider UserDataProvider;

        #endregion

        public UserManager(
            ICallContext callContext,
            UserDataProvider userDataProvider
            )
            : base(callContext)
        {
            this.UserDataProvider = userDataProvider;
        }
        
        /// <summary>
        /// Read from the JoggingApp.V_User view (selecting from the ASP NET Identity database)
        /// </summary>
        public IEnumerable<User> GetList()
        {
            return GetList(null, null);
        }

        /// <summary>
        /// Read from the JoggingApp.V_User view (selecting from the ASP NET Identity database)
        /// </summary>
        public IEnumerable<User> GetList(string freeTextSearch, int? maxRows)
        {
            // keep lazy loaded, don't fetch until needed
            return UserDataProvider.GetList(freeTextSearch, maxRows);
        }

        /// <summary>
        /// Read from the JoggingApp.V_User view (selecting from the ASP NET Identity database)
        /// </summary>
        public User Get(string id, bool throwExceptionIfNotFound)
        {
            return UserDataProvider.Get(id, throwExceptionIfNotFound);
        }


        /// <summary>
        /// Saves modified user data into the ASP Net Identity database
        /// </summary>
        public void Add(User user)
        {
            UserDataProvider.Add(user);
        }

        /// <summary>
        /// Saves modified user data into the ASP Net Identity database
        /// </summary>
        public void Update(User user)
        {
            UserDataProvider.Update(user);
        }

        public void Delete(string userId)
        {
            // TODO 
            // delete related entities

            UserDataProvider.Delete(userId);
        }

    }
}
