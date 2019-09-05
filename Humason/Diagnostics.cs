//Diagnostics class checks various conditions of the application in order to head off
//  silly set up problems

//the primary public method will manage the set of check methods, post a message box if errors are
//  found.  A true is returned if no errors are found, or errors are found and the user doesn't care.

using System.Windows.Forms;

namespace Humason
{
    public partial class Diagnostics
    {
        private static string ErrorList;
        private static bool AllGoodFlag;

        public static bool CheckUp()

        {
            //Set a flag for any errors, if found
            //  these will be set as a messagebox
            AllGoodFlag = true;

            //Check for absence of any targets in the schedule list
            CheckScheduledList();
            CheckTargetLoaded();
            CheckTargetDefaultFile();

            //Done with all checks.  If any errors found (AllGoodFlag is false) then post errors
            //  in a messagebox, return true or false accordingly
            //  otherwise just return true (i.e. all good)
            if (AllGoodFlag)
            { return true; }
            else
            {
                return (PostProblems(ErrorList));
            }
        }

        private static bool PostProblems(string probset)
        {
            probset += "Do you want to continue?";

            DialogResult diagOrder = MessageBox.Show(probset, "Potential problems found", MessageBoxButtons.YesNo);
            if (diagOrder == DialogResult.Yes)
            { return true; }
            else
            { return false; }
        }

        private static string CheckScheduledList()
        {
            //returns either an error message or null string
            string csErrors = null;
            if (FormHumason.fPlanForm.ScheduleListBox.Items.Count == 0)
            {
                AllGoodFlag = false;
                ErrorList += "No target plans have been scheduled.\r\n";
            }
            return csErrors;
        }

        private static string CheckTargetDefaultFile()
        {
            //Checks to see if a default target file exists.  If not, a warning is issued
            //
            string csErrors = null;
            if (!System.IO.File.Exists(FormHumason.openSession.DefaultTargetPlanPath))
            {
                AllGoodFlag = false;
                ErrorList += "Default Target Plan is missing.";
            }
            return csErrors;
        }

        private static string CheckTargetLoaded()
        {
            //returns either an error message or null string
            string csErrors = null;
            if (FormHumason.openSession.CurrentTargetName == "")
            {
                AllGoodFlag = false;
                ErrorList += "No target plan has been entered.\r\n";
            }
            return csErrors;
        }
    }
}
