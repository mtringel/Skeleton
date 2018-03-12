using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TopTal.JoggingApp.Exceptions.Entities
{
    /// <summary>
    /// Root class for all application exceptions
    /// AppUserFriendlyException.Message can be rendered to the client
    /// (other exception messages are logged, but generic error message is displayed)
    /// </summary>
    public abstract class EntityException : AppException
    {
        public EntityException(HttpStatusCode status, string statusDescription)
            : base(status, statusDescription, false)
        {
        }

        //public EntityException(HttpStatusCodeResult result)
        //    : base(result, false)
        //{
        //}
    }
}

