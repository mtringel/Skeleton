using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TopTal.JoggingApp.Exceptions.Validation
{
    /// <summary>
    /// Root class for all application exceptions
    /// AppUserFriendlyException.Message can be rendered to the client
    /// (other exception messages are logged, but generic error message is displayed)
    /// </summary>
    public class ValidationException : AppException
    {
        public ValidationException()
            : this(string.Empty)
        {
        }

        public ValidationException(string validationMessage)
            : base(
                  HttpStatusCode.BadRequest,
                  validationMessage.Contains('|') ? validationMessage : string.Format(Resources.Resources.Validation_Error, validationMessage),
                  false
                  )
        {
        }

        public ValidationException(IEnumerable<string> validationMesssages)
             : this(string.Join(" ", validationMesssages))
        {
        }
    }
}

