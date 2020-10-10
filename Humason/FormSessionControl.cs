using Planetarium;
using System;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormSessionControl : Form
    {
        private bool sessionFormInit = false;

        public FormSessionControl()
        {
            sessionFormInit = true;
            InitializeComponent();
            SessionControl openSession = new SessionControl();
            AutoRunCheck.Checked = openSession.IsAutoRunEnabled;
            //Update the autorun types checked 
            StagingEnabledCheckBox.Checked = openSession.IsStagingEnabled;
            StartupEnabledCheckBox.Checked = openSession.IsStartUpEnabled;
            ShutdownEnabledCheckBox.Checked = openSession.IsShutDownEnabled;
            MinimumAltitudeBox.Value = openSession.MinimumAltitude;
            sessionFormInit = false;
            return;
        }

        private void AutoRunCheck_CheckedChanged(object sender, EventArgs e)
        {
            //If this is a check, then record the check in the configuration times and open the autorun form window
            //If this is an uncheck, then just record the uncheck in configuration
            //In either case, save the selections and times to the session file anyway

            //First, if the forms are still initializing, then just ignore
            if (sessionFormInit)
            { return; }
            SessionControl openSession = new SessionControl();
            openSession.IsAutoRunEnabled = AutoRunCheck.Checked;
            if (AutoRunCheck.Checked)
            {
                FormAutoRun arf = new FormAutoRun();
                arf.ShowDialog();
            }
            openSession.IsStagingEnabled = StagingEnabledCheckBox.Checked;
            openSession.IsStartUpEnabled = StartupEnabledCheckBox.Checked;
            openSession.IsShutDownEnabled = ShutdownEnabledCheckBox.Checked;
            return;
        }

        private void OTAButton_Click(object sender, EventArgs e)
        {
            if (TSXLink.Mount.OTASideOfPier == TSXLink.Mount.SOP.East)
            { OTAButton.Text = "East"; }
            else
            { OTAButton.Text = "West"; }
            return;
        }

        private void StagingEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsStagingEnabled = StagingEnabledCheckBox.Checked;
        }

        private void StartupEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsStartUpEnabled = StartupEnabledCheckBox.Checked;
        }

        private void ShutdownEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsShutDownEnabled = ShutdownEnabledCheckBox.Checked;
        }

        private void MinAltitudeBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.MinimumAltitude = (int)MinimumAltitudeBox.Value;
        }
    }
}
