
namespace TopTal.JoggingApp.Exceptions.Validation
{
    /// <summary>
    /// Root class for all application exceptions
    /// AppUserFriendlyException.Message can be rendered to the client
    /// (other exception messages are logged, but generic error message is displayed)
    /// </summary>
    public class AntiforgeryTokenValidationException : ValidationException 
    {
        public AntiforgeryTokenValidationException()
            : base(Resources.Resources.Validation_InvalidAntiForgeryToken)
        {
        }
    }
}

