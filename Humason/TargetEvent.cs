/// Class for creating event handler for passing Log value to 
/// a subscriber form (HumasonForm) and saving it to a file
/// 
/// 
/// The subscribing form class (which wants to display the log entry
/// 1) instantiates a public field for this class object
///         public static Logger logstatus = new Logger();
/// 2) Creates the log file and subscribes a handler to the event subscriber list when initializing the form class
///             logstatus.CreateLog();
///             logstatus.TargetEventHandler += StatusLogUpdate_Handler;
/// 3) installs a method for handling the event
///         private void StatusLogUpdate_Handler(object sender, Logger.TargetEventArgs e)
///         {
///            StatusStripLine.Text = e.TargetEntry;
///            Show();
///            return;
///         }
///Then an event publisher gets the Logger object from the controlling form
///            Logger lg = HumasonForm.logstatus;
///And generates an event whenever it needs to
///            lg.targetName("Acquiring guide star");
///            

using System;

namespace Humason
{
    public class TargetEvent
    {
        //Event declaration for new target plan event
        public event EventHandler<TargetEventArgs> TargetEventHandler;

        //Method for initiating a new target plan event
        public void TargetEntry(string targetName)
        {
            OnTargetEventHandler(new TargetEventArgs(targetName));
        }

        // Wrap event invocations inside a protected virtual method
        protected virtual void OnTargetEventHandler(TargetEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            TargetEventHandler?.Invoke(this, e);
        }

        //Class to hold new target plan event arguments
        public class TargetEventArgs : System.EventArgs
        {
            private readonly string privateEntry;
            public TargetEventArgs(string privateEntry) { this.privateEntry = privateEntry; }
            public string TargetEntry => privateEntry;
        }

        public void RaiseNewTargetPlan(string target)
        {
            //Method to raise a new target plan event for anyone who is listening
            TargetEntry(target);
            //Update the target form
            FormHumason.fTargetForm.UpdateFormFromPlan();
            return;
        }

    }

}

