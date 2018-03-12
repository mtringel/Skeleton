using System;
using TopTal.JoggingApp.Service.Models.Security;
using TopTal.JoggingApp.BusinessLogic.Users;
using TopTal.JoggingApp.Service.Api.Helpers;
using TopTal.JoggingApp.Service.Models.Helpers;
// don't refer to BusinessEntities namespaces here (to avoid confusion with Service.Models)

namespace TopTal.JoggingApp.Service.Api.Security
{
    public sealed class AuthService : ServiceBase
    {
        public AuthService(
            CallContext.ICallContext callContext,
            Configuration.AppConfig appConfig,
            DataAccess.Helpers.ITransactionManager transactionManager,
            IServiceModelValidator serviceModelValidator,
            JoggingApp.Security.Managers.IAuthProvider authProvider
            ) : 
            base(callContext, appConfig, transactionManager, serviceModelValidator, authProvider)
        {
        }

        #region Services

        #endregion

        /// <summary>
        /// Returns current user, if logged in.
        /// Can be requested to log off the current user.
        /// Others initialization parameters can be returned by inheriting from AntiForgeryTokenResult.
        /// </summary>
        public AppUser Get()
        {
            try
            {
                AuthProvider.Authenticate(); // throws UnauthenticatedException or we have CurrentUser after this

                using (var context = GetContext(false))
                {
                    return OK(new AppUser(AuthProvider.CurrentUser));
                }
            }
            catch (Exception ex)
            {
                return HandleException<AppUser>(ex);
            }
        }       

    }
}