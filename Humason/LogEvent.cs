/// Class for creating event handler for passing Log value to 
/// a subscriber form (HumasonForm) and saving it to a file
/// 
/// 
/// The subscribing form class (which wants to display the log entry
/// 1) instantiates a public field for this class object
///         public static Logger logstatus = new Logger();
/// 2) Creates the log file and subscribes a handler to the event subscriber list when initializing the form class
///             logstatus.CreateLog();
///             logstatus.LogEventHandler += StatusLogUpdate_Handler;
/// 3) installs a method for handling the event
///         private void StatusLogUpdate_Handler(object sender, Logger.LogEventArgs e)
///         {
///            StatusStripLine.Text = e.LogEntry;
///            Show();
///            return;
///         }
///Then an event publisher gets the Logger object from the controlling form
///            Logger lg = HumasonForm.logstatus;
///And generates an event whenever it needs to
///            lg.LogIt("Acquiring guide star");
///            

using System;
using System.IO;

namespace Humason
{
    public class LogEvent
    {
        //Event declaration
        public event EventHandler<LogEventArgs> LogEventHandler;

        private void LogEntry(string logit)
        {
            OnLogEventHandler(new LogEventArgs(logit));
        }

        // Wrap event invocations inside a protected virtual method
        protected virtual void OnLogEventHandler(LogEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            LogEventHandler?.Invoke(this, e);
            //         LogEventHandler thisEvent = ThisEvent;  //assign the event to a local variable
            //         If(thisEvent != null)
            //         {
            //             thisEvent(this, args);
            //         }
        }

        //Class to hold logging event arguments, i.e. log entry string
        public class LogEventArgs : System.EventArgs
        {
            private string privateEntry;
            public LogEventArgs(string privateEntry)
            {
                this.privateEntry = privateEntry;
            }

            public string LogEntry
            {
                get
                {
                    return privateEntry;
                }
            }
        }

        //Method for initiating log event as called from form or class that wants to raise it
        public void LogIt(string logline)
        {
            //Gets the current date/time
            //Creates a new log file, if  not created
            //Opens log file and appends date-time, log line and crlf
            //Closes log file
            //Raises a log event for anyone who is listening

            string logdirpath = FormHumason.openSession.HumasonDirectoryPath +"\\Logs";
            if (!Directory.Exists(logdirpath))
            { Directory.CreateDirectory(logdirpath); }
            string logdate = DateTime.Now.ToString("yyyy-MM-dd");
            string logtime = DateTime.Now.ToString("HH:mm:ss");
            string logfilepath = logdirpath + "\\" + logdate + ".log";
            if (!File.Exists(logfilepath))
            {
                StreamWriter sfw = File.CreateText(logfilepath);
                sfw.Close();
            }
            File.AppendAllText(logfilepath, (logtime + " " + logline + "\r\n"));
            LogEntry(logline);
            System.Windows.Forms.Application.DoEvents();
            return;
        }

        public void CreateLog()
        {
            //Creates a new log directory if  not created
            //Creates a new log file, if  not created

            string logdirpath = FormHumason.openSession.HumasonDirectoryPath + "\\Logs";
            if (!Directory.Exists(logdirpath))
            { Directory.CreateDirectory(logdirpath); }
            return;
        }


    }

}

