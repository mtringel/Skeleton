using System;


namespace TopTal.JoggingApp.Logging
{
    public interface ILogger
    {
        void LogError(Exception ex);
    }
}
