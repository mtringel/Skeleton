using Microsoft.AspNetCore.Mvc.Filters;
using TopTal.JoggingApp.Logging;

namespace TopTal.JoggingApp.Exceptions
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        public ExceptionFilter(ILogger logger)
        {
            this.Logger = logger;
        }

        #region Services

        private ILogger Logger;

        #endregion

        /// <summary>
        /// Global exception handler
        /// </summary>
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
                Logger.LogError(filterContext.Exception);
        }
    }
}
