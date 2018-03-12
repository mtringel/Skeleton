using System;
using System.Net;
using System.Net.Http;

namespace TopTal.JoggingApp.Exceptions
{
    /// <summary>
    /// Root class for all application exceptions
    /// AppUserFriendlyException.Message can be rendered to the client
    /// (other exception messages are logged, but generic error message is displayed)
    /// </summary>
    public abstract class AppException : ApplicationException
    {
        public string Description { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        public bool LogError { get; private set; }

        public string DetailerErrorMessage { get; set; }

        public AppException(HttpStatusCode statusCode, string description, bool logError)
        {
            this.StatusCode = statusCode;
            this.Description = description;
            this.LogError = logError;
        }

        public AppException(HttpStatusCode statusCode, bool logError)
            : this(statusCode, null, logError)
        {
        }

        public override string ToString()
        {
            return $"{StatusCode} {Description}";
        }
    }
}
