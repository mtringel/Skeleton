using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TopTal.JoggingApp.Logging.EventLog
{
    public class WindowsEventLogLogger : ILogger 
    {
        internal WindowsEventLogLogger()
        {
        }

        public void LogError(Exception ex)
        {
            //if (ex != null)
            //{
            //    // Writing to an event log from .NET without the Description for event id nonsense
            //    // https://www.jitbit.com/alexblog/266-writing-to-an-event-log-from-net-without-the-description-for-event-id-nonsense/

            //    try
            //    {
            //        System.Diagnostics.EventLog.WriteEntry(
            //            Configuration.AppConfig.Current.Logging.EventSource,  
            //            ex.ToString(),
            //            EventLogEntryType.Error,
            //            1000 // magic
            //            );
            //    }
            //    catch
            //    {
            //        // maybe we need this the first time only to register the source (?)
            //        System.Diagnostics.EventLog.WriteEntry(
            //            ".NET Runtime", // magic
            //            ex.ToString(),
            //            EventLogEntryType.Error,
            //            1000 // magic
            //            );
            //    }
            //}
        }

    }
}
