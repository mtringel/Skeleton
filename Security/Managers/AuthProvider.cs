using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TopTal.JoggingApp.CallContext;
using TopTal.JoggingApp.Security.Principals;

namespace TopTal.JoggingApp.Security.Managers
{
    /// <summary>
    /// Lifetime: Scoped (current request)
    /// </summary>
    public sealed class AuthProvider : IAuthProvider
    {
        public AuthProvider(
            ICallContext callContext
            )
        {
            this.CallContext = callContext;
        }

        #region Services        

        private ICallContext CallContext;

        #endregion

        #region CurrentUser

        public string UserName { get { return CallContext.Identity.Identity.Name; } }

        private AppUser _CurrentUser;

        /// <summary>
        /// Scope: HttpContext
        /// Must be here, so all projects can access it. The Web project will set it after successful login.
        /// In a load balanced environment, it is not necessarily present in HttpContext.
        /// HttpContext.User is always set by ASP.Net by auth.cookie.
        /// </summary>
        public AppUser CurrentUser
        {
            get
            {
                if (_CurrentUser == null && IsAuthenticated)
                    _CurrentUser = new AppUser(CallContext.Identity, Group.None); // TODO Group from Azure

                return _CurrentUser;
            }
        }

        #endregion

        #region Authentication and authorization

        /// <summary>
        /// It's faster than checking CurrentUser (CurrentUser has to retrieve the user entity from the database for the current request)
        /// </summary>
        public bool IsAuthenticated { get { return CallContext.Identity.Identity.IsAuthenticated; } }

        public bool Authorized(params Permission[] permissions)
        {
            return IsAuthenticated && CurrentUser.Group.Authorized(permissions);
        }

        public bool Authorized(Permission[] permissions, bool all)
        {
            return IsAuthenticated && CurrentUser.Group.Authorized(permissions, all);
        }

        /// <summary>
        /// Throws UnauthenticatedException, which returns 401 Unauthorized, if not authenticated (HandleException), which will trigger a login page redirect with returnUrl (application.webApiResult) or
        /// </summary>
        public void Authenticate()
        {
            if (!IsAuthenticated)
                throw new Exceptions.Security.UnauthenticatedException(CallContext.ResourceUri, Permission.User_LoggedIn.ToString());
        }

        /// <summary>
        /// Demands any of the permissions.
        /// Either,
        /// Throws UnauthenticatedException, which returns 401 Unauthorized, if not authenticated (HandleException), which will trigger a login page redirect with returnUrl (application.webApiResult) or
        /// Throws UnauthorizedException, which returnns 403 Forbidden, if not authorized, which will result in an error message displayed (application.webApiResult)
        /// </summary>
        public void Demand(params Permission[] permissions)
        {
            Demand(permissions, false);
        }

        /// <summary>
        /// /// Demands any or all of the permissions.
        /// Either,
        /// Throws UnauthenticatedException, which returns 401 Unauthorized, if not authenticated (HandleException), which will trigger a login page redirect with returnUrl (application.webApiResult) or
        /// Throws UnauthorizedException, which returnns 403 Forbidden, if not authorized, which will result in an error message displayed (application.webApiResult)
        /// </summary>
        public void Demand(Permission[] permissions, bool demandAll)
        {
            if (!IsAuthenticated)
                throw new Exceptions.Security.UnauthenticatedException(CallContext.ResourceUri, string.Join(", ", permissions));

            if (!CurrentUser.Group.Authorized(permissions, demandAll))
                throw new Exceptions.Security.UnauthorizedException(CallContext.ResourceUri, string.Join(", ", permissions));
        }

        #endregion
    }
}
