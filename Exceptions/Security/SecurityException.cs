using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace TopTal.JoggingApp.Exceptions.Security
{
    /// <summary>
    /// Root class for all application exceptions
    /// AppUserFriendlyException.Message can be rendered to the client
    /// (other exception messages are logged, but generic error message is displayed)
    /// </summary>
    public abstract class SecurityException : AppException
    {
        public SecurityException(HttpStatusCode status, string statusDescription)
            : base(status, statusDescription, false)
        {
        }

        //public SecurityException(HttpStatusCodeResult result)
        //    : base(result, false)
        //{
        //}
    }
}

