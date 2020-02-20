/// Class for creating event handler for raising and distributing an abort event 
/// a subscriber form (HumasonForm) and saving it to a file
/// 
/// The subscribing (listening) form class (which wants to instantiate this abort
/// 
/// 1) instantiates a public field for this abort event object as a reference so other classes can post their aborts
///         public static AbortEvent abortreport = new AbortEvent();
///         
/// 2) Subscribes a handler to the event subscriber list when initializing the form class  
///             abortstatus.AbortEventHandler += AbortEvent_Handler;
///             
/// 3) installs a method for handling the event
///         private void AbortEvent_Handler(object sender, AbortEvent.EventArgs e)
///         {
///            //do something with the abort arguments e.???? //;
///            return;
///         }
///
/// The Then an abort event publisher gets the AbortEvent object from the reference field of the controlling form
///            AbortEvent abrt = ControllingForm.abortreport;
///And generates an event whenever it needs to
///            abrt.AbortIt("Im going down...");
///            
///
///

///

using System;

namespace Humason
{
    public class AbortEvent
    {
        //Event declaration
        public event EventHandler<AbortEventArgs> AbortEventHandler;

        //Local method for generating the abort event to all listeners
        private void RaiseAbortEvent(string abortmessage)
        {
            OnAbortEventHandler(new AbortEventArgs(abortmessage));
        }

        // Wrap event invocations inside a protected virtual method
        protected virtual void OnAbortEventHandler(AbortEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            AbortEventHandler?.Invoke(this, e);
            //         LogEventHandler thisEvent = ThisEvent;  //assign the event to a local variable
            //         If(thisEvent != null)
            //         {
            //             thisEvent(this, args);
            //         }
        }

        //Class to hold logging event arguments, i.e. log entry string
        public class AbortEventArgs : System.EventArgs
        {
            private string privateAbortMessage;
            public AbortEventArgs(string AbortMessage)
            {
                this.privateAbortMessage = AbortMessage;
            }

            public string AbortEntry
            {
                get
                {
                    return privateAbortMessage;
                }
            }
        }

        //Method for initiating an abort event as called from a method that wants to speak one
        public void AbortIt(string abortmessage)
        {
            //Logs the abort event
            LogEvent lg = FormHumason.lg;
            lg.LogIt("Abort set: " + abortmessage);

            //Raises an abort event for anyone who is listening
            RaiseAbortEvent(abortmessage);
            System.Windows.Forms.Application.DoEvents();
        }

    }
}
