using System;

namespace TopTal.JoggingApp.Exceptions.Validation
{
    /// <summary>
    /// Root class for all application exceptions
    /// AppUserFriendlyException.Message can be rendered to the client
    /// (other exception messages are logged, but generic error message is displayed)
    /// </summary>
    public class InputDataMissingException : ValidationException 
    {
        public InputDataMissingException(string resourceName, Type expectedDataType)
            : base(string.Format (Resources.Resources.Validation_MissingInputData , resourceName, expectedDataType.FullName ))
        {
        }

    }
}

