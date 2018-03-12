using System;
using System.Linq;
using System.Net.Http;
using TopTal.JoggingApp.Service.Models.Users;
using TopTal.JoggingApp.BusinessLogic.Users;
using TopTal.JoggingApp.Security.Principals;
using TopTal.JoggingApp.Service.Models;
using System.Collections.Generic;
using TopTal.JoggingApp.Service.Api.Helpers;
using TopTal.JoggingApp.Service.Models.Helpers;
// don't refer to BusinessEntities namespaces here (to avoid confusion with Service.Models)

namespace TopTal.JoggingApp.Service.Api.Users
{
    public sealed class UserService : ServiceBase
    {
        public UserService(
            CallContext.ICallContext callContext,
            Configuration.AppConfig appConfig,
            DataAccess.Helpers.ITransactionManager transactionManager,
            IServiceModelValidator serviceModelValidator,
            JoggingApp.Security.Managers.IAuthProvider authProvider,
            UserManager userManager
            ) :
            base(callContext, appConfig, transactionManager, serviceModelValidator, authProvider)
        {
            this.UserManager = userManager;
        }

        #region Services

        private UserManager UserManager;

        #endregion

        #region Get (collection)

        /// <summary>
        /// Get entity collection
        /// freeTextSearch - looks for in any text field
        /// </summary>
        /// <returns></returns>
        public UserListData Get(bool filter, string freeTextSearch)
        {
            try
            {
                AuthProvider.Authenticate(); // throws UnauthenticatedException or we have CurrentUser after this
                var result = new UserListData();

                using (var context = GetContext(false))
                {
                    // authorize
                    AuthProvider.Demand(JoggingApp.Security.Principals.Permission.User_Management);

                    // process
                    var maxRows = AppConfig.WebApplication.GridMaxRows;
                    var list = UserManager.GetList(filter ? freeTextSearch : null, maxRows + 1);

                    result.TooMuchData = list.Count() > maxRows;
                    result.List = list.Take(maxRows).Select(t => new User(t)).ToArray();

                    context.Complete();
                    return OK(result);
                }
            }
            catch (Exception ex)
            {
                return HandleException<UserListData>(ex);
            }
        }

        #endregion

        #region Get (single entity)

        /// <summary>
        /// Get single entity
        /// id == userId or "new" or "profile" (own)
        /// </summary>
        public UserFormData Get(string id)
        {
            try
            {
                AuthProvider.Authenticate(); // throws UnauthenticatedException or we have CurrentUser after this
                var result = new UserFormData();

                using (var context = GetContextWithToken(false, result))
                {
                    // prepare
                    var isNew = (string.IsNullOrEmpty(id) || id == "new");
                    var ownProfile = !isNew && (id == "profile" || string.Compare(id, AuthProvider.CurrentUser.UserId, true) == 0); 

                    // authorize
                    if (ownProfile)
                        AuthProvider.Demand(Permission.User_EditProfile, Permission.User_Management);
                    else
                        AuthProvider.Demand(Permission.User_Management);

                    // process
                    result.Entity = isNew ? new User() : new User(UserManager.Get(ownProfile ? AuthProvider.CurrentUser.UserId : id, true));
                    result.RoleTitles = GroupHelper.AllGroups.Select(t => new KeyValuePair<Group, string>(t, t.Title())).ToArray();

                    context.Complete();
                    return OK(result);
                }
            }
            catch (Exception ex)
            {
                return HandleException<UserFormData>(ex);
            }
        }

        #endregion

        #region Post (create single)

        /// <summary>
        /// Create new entity
        /// </summary>
        public ServiceResult Post(User user)
        {
            try
            {
                AuthProvider.Authenticate(); // throws UnauthenticatedException or we have CurrentUser after this

                using (var context = PostContext(true, true))
                {
                    // prepare
                    Expect(user);

                    // authorize
                    AuthProvider.Demand(Permission.User_Management);

                    // only Admin can set roles other than User
                    if (!AuthProvider.Authorized(Permission.User_Management_SetRole) && user.Group != Group.None)
                        return ValidationError("Group cannot be set.");

                    // validate
                    if (!ServiceModelValidator.ModelState.IsValid)
                        return ValidationError(ServiceModelValidator.ModelState);

                    // process
                    UserManager.Add(user.ToEntity());

                    context.Complete();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion

        #region Put (update single entity)

        /// <summary>
        /// Update single entity
        /// </summary>
        public ServiceResult Put(User user)
        {
            try
            {
                AuthProvider.Authenticate(); // throws UnauthenticatedException or we have CurrentUser after this

                using (var context = PostContext(true, true))
                {
                    // prepare
                    Expect(user, user.UserId);

                    var ownProfile = string.Compare(user.UserId, AuthProvider.CurrentUser.UserId, true) == 0;
                    var oldUser = UserManager.Get(user.UserId, true); // throws EntityNotFoundException

                    // authorize
                    if (ownProfile)
                        AuthProvider.Demand(Permission.User_EditProfile);
                    else if (oldUser.Group == Group.Admin)
                        AuthProvider.Demand(Permission.User_Management_EditAdmins); // only Admin can edit Admin
                    else
                        AuthProvider.Demand(Permission.User_Management);

                    // only Admin can set roles other than User
                    if (!AuthProvider.Authorized(Permission.User_Management_SetRole) && oldUser.Group != user.Group)
                        return ValidationError("Group cannot be set.");

                    // validate
                    if (!ServiceModelValidator.ModelState.IsValid)
                        return ValidationError(ServiceModelValidator.ModelState);

                    // process
                    UserManager.Update(user.ToEntity());

                    context.Complete();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion

        #region Delete (single entity)

        /// <summary>
        /// Delete single entity
        /// </summary>
        public ServiceResult Delete(string id)
        {
            try
            {
                AuthProvider.Authenticate(); // throws UnauthenticatedException or we have CurrentUser after this

                using (var context = PostContext(true, true))
                {
                    // prepare
                    Expect(typeof(User), id);

                    var ownProfile = string.Compare(id, AuthProvider.CurrentUser.UserId, true) == 0;
                    var oldUser = UserManager.Get(id, true);

                    // authorize
                    if (ownProfile)
                        AuthProvider.Demand(Permission.User_EditProfile);
                    else if (oldUser.Group == Group.Admin)
                        AuthProvider.Demand(Permission.User_Management_EditAdmins); // only Admin can edit Admin
                    else
                        AuthProvider.Demand(Permission.User_Management);

                    // process
                    UserManager.Delete(id);

                    context.Complete();
                    return OK();
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion
    }
}