using System;


namespace TopTal.JoggingApp.Logging
{
    /// <summary>
    /// Use .Net Core dependency injecton to get implementation.
    /// Current implementation: TopTal.JoggingApp.AzureHelper.ApplicationInsights.TelemetryClient
    /// </summary>
    public interface ILogger
    {
        void LogError(Exception ex);
    }
}
