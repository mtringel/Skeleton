using System;
using System.Collections.Generic;
using System.Text;

namespace TopTal.JoggingApp.Azure.ApplicationInsights
{
    /// <summary>
    /// Diagnose exceptions in your web apps with Application Insights
    /// https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net-exceptions
    /// </summary>
    public sealed class TelemetryClient : Logging.ILogger
    {
        public void LogError(Exception exception)
        {
            if (exception != null)
            {
                //If customError is Off, then AI HTTPModule will report the exception
                //if (filterContext.HttpContext.IsCustomErrorEnabled)
                //{
                // Note: A single instance of telemetry client is sufficient to track multiple telemetry items.
                var ai = new Microsoft.ApplicationInsights.TelemetryClient();
                ai.TrackException(exception);
                //}
            }
        }
    }
}
