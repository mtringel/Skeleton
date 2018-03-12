
using System.Net;


namespace TopTal.JoggingApp.Exceptions
{
    public class GenericServerError : AppException 
    {
        public GenericServerError(string resourceName)
            : base(
                  HttpStatusCode.InternalServerError, 
                  string.Format ( Resources.Resources.Error_GenericServerError , resourceName ),
                  true
                  )
        {
        }

    }
}
