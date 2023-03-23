using Planetarium;
using System;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormAutoFocus : Form
    {
        public FormAutoFocus()
        {
            InitializeComponent();
            {
                ResetConfiguration();
                //Set button colors
                NHUtil.ButtonGreen(Presetbutton);
                NHUtil.ButtonGreen(AtFocus2Button);
                NHUtil.ButtonGreen(AtFocus3Button);
            }
        }

        //ResetConfiguration loads values of the current target plan, but saves default values to
        //  the plan if not already saved
        public void ResetConfiguration()
        {
            //Populate entries with stored entries, if any
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (tPlan.TargetPlanPath != null)
            {
                FocusFilterBox.Text = tPlan.FocusFilter.ToString();
                FocusExposureBox.Value = (decimal)tPlan.FocusExposure;
            }
        }

        private void Presetbutton_Click(object sender, EventArgs e)
        {
            //Moves focuser to critical focus position for current temperature based on "Current.foc" focus training file:
            //   Checks for focuser connection
            //   Gets current temperature from focuser
            //   Calls function to compute new position from a selected focus training data file and current temperature
            //   Moves focuser to new position from current position

            NHUtil.ButtonRed(Presetbutton);
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            //Get a filter focus file path from the configuration file, then validate/change via dialog
            FilterFileDialog.InitialDirectory = openSession.FocuserDataFolder;
            FilterFileDialog.FileName = "";

            DialogResult focusfilelist = FilterFileDialog.ShowDialog();
            //Check for a null return (e.g. no file selected)
            //  just log it and return if nothing there
            if (focusfilelist != DialogResult.OK)
            {
                lg.LogIt("No focus preset file selected");
                NHUtil.ButtonGreen(Presetbutton);
                return;
            }
            //Else...
            string focusfile = FilterFileDialog.FileNames[0];

            //Save the new (or unchanged) file path
            int baseFilterId = tPlan.FocusFilter;
            lg.LogIt("Presetting focus position");

            double currenttemp = TSXLink.Focus.GetTemperature();
            double newfocusposition = AutoFocus.ComputeNewFocusPosition(focusfile, currenttemp, baseFilterId);

            if (newfocusposition == 0)
            {
                //Insufficient data to compute new position so, just leave it.
                lg.LogIt("Focus preset aborted -- insufficient data to compute new position");
                NHUtil.ButtonGreen(Presetbutton);
                return;
            }
            else
            {
                TSXLink.Focus.MoveTo(newfocusposition);
            }
            lg.LogIt("Focus preset completed");
            NHUtil.ButtonGreen(Presetbutton);
            return;
        }

        private void AtFocus2Button_Click(object sender, EventArgs e)
        {
            //Execute TSX_AutoFocus class
            //  Save current object information
            //  Open and connect Autofocus
            //  Run Focus@2 for all five filters
            //  Save datepoints
            //  Turn on temperature compensation
            //  Return telescope to object with CloseLoopSlew

            NHUtil.ButtonRed(AtFocus2Button);
            AutoFocus.FocusIt(2);
            NHUtil.ButtonGreen(AtFocus2Button);
            return;
        }

        private void AtFocus3Button_Click(object sender, EventArgs e)
        {
            //Execute TSX_AutoFocus class
            //  Save current object information
            //  Open and connect Autofocus
            //  Run Focus@2 for all five filters
            //  Save datepoints
            //  Turn on temperature compensation
            //  Return telescope to object with CloseLoopSlew

            NHUtil.ButtonRed(AtFocus3Button);
            AutoFocus.FocusIt(3);
            NHUtil.ButtonGreen(AtFocus3Button);
            return;
        }

        private void FocusExposureBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                FocusExposure = (double)FocusExposureBox.Value
            };
            return;
        }
    }
}

