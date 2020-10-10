///Event Publishing Class for a progress bar
///
///Notes;
///Once this class is added to a project, a class that is subscribing to this event must
///instantiate a new object for this class (in this case, inside the SequenceBuilder class instantiation)
///     and add an event handler method to the event subscriber list
///     (during base initialization...)
///         imgseq = new SequenceBuilder();
///         imgseq.PrgUpdate.ProgressUpdateEventHandler += ProgressUpdate_Handler;
///     ...
///     (add method to handle event...)
///            private void ProgressUpdate_Handler(object sender, Progress.ProgressUpdateEventArgs e)
///                 {
///                 Update_Status_Bar(e.ProgressPercent);
///                 }
///
///"SequenceBuilder" class contains a public field
///         public Progress PrgUpdate;
///and
///contains the base code to instantiate it with this event class:
///            PrgUpdate = new Progress();
///
///To update the status in the subscription form from any SequenceBuilder method:
///                PrgUpdate.ProgressIt((int)seriesprogress);
///                
///  where seriesprogress is a number between 0 and 100.
///  

using System;

namespace Humason
{
    public class ProgressEvent
    {
        /// Section for defining publisher-side event handler for passing progress value to SequenceBuilderForm
        /// 

        //Event declaration
        public event EventHandler<ProgressUpdateEventArgs> ProgressUpdateEventHandler;

        //Method for initiating progress update event
        public void ProgressUpdate(int progressPercent)
        {
            OnProgressUpdateEventHandler(new ProgressUpdateEventArgs(progressPercent));
        }

        // Wrap event invocations inside a protected virtual method
        protected virtual void OnProgressUpdateEventHandler(ProgressUpdateEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            ProgressUpdateEventHandler?.Invoke(this, e);
        }

        //Class to handle progress event argument -- Progress complete percetn
        public class ProgressUpdateEventArgs : System.EventArgs
        {
            private readonly int progressData;
            public ProgressUpdateEventArgs(int progressData)
            {
                this.progressData = progressData;
            }

            public int ProgressPercent
            {
                get
                {
                    return progressData;
                }
            }
        }

        public void ProgressIt(int progressPercent)
        {
            FormHumason.fTargetForm.UpdateStatusBar(progressPercent);
            ProgressUpdate(progressPercent);
        }
    }
}
