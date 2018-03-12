﻿
namespace TopTal.JoggingApp.Exceptions.Security
{
    /// <summary>
    /// Returns 401 Unauthorized, if not authenticated (HandleException), which will trigger a login page redirect with returnUrl (application.webApiResult) or
    /// </summary>
    public class UnauthenticatedException : SecurityException
    {
        public string ResourceName { get; private set; }

        public string Permission { get; private set; }

        public UnauthenticatedException(string resourceName, string permission)
            : base(System.Net.HttpStatusCode.Unauthorized, string.Format(Resources.Resources.Security_Unauthenticated  , resourceName, permission))
        {
            this.ResourceName = resourceName;
            this.Permission = permission;
        }
    }
}
