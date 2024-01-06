using System;
using System.IO;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormSessionControl : Form
    {

        public FormSessionControl()
        {
            InitializeComponent();
            SessionControl openSession = new SessionControl();
            //Update the autorun types checked 
            StagingEnabledCheckBox.Checked = openSession.StagingEnabled;
            StartupEnabledCheckBox.Checked = openSession.StartUpEnabled;
            ShutdownEnabledCheckBox.Checked = openSession.ShutDownEnabled;
            MinimumAltitudeBox.Value = openSession.MinimumAltitude;
            EnableMeridianFlipBox.Checked = openSession.IsMeridianFlipEnabled;
            StageSystemFilePathBox.Text = Path.GetFileName(openSession.StagingFilePath);
            StartUpFilePathBox.Text = Path.GetFileName(openSession.StartUpFilePath);
            ShutDownFilePathBox.Text = Path.GetFileName(openSession.ShutDownFilePath);
            //Set the default browse locations to the TSX ToolKit Start Up directory
            string ttdir = "C:\\Users\\" + System.Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\TSXToolkit\\TSXToolkit";
            if (Directory.Exists(ttdir))
            {
                StageSystemFileDialog.InitialDirectory = ttdir;
                StartUpFileDialog.InitialDirectory = ttdir;
                ShutDownFileDialog.InitialDirectory = ttdir;
            }
            openSession.StagingEnabled = StagingEnabledCheckBox.Checked;
            openSession.StartUpEnabled = StartupEnabledCheckBox.Checked;
            openSession.ShutDownEnabled = ShutdownEnabledCheckBox.Checked;
            openSession.IsMeridianFlipEnabled = EnableMeridianFlipBox.Checked;
            return;
        }

        private void OTAButton_Click(object sender, EventArgs e)
        {
            //
            // Beyond The Pole = False = Pier E
            // Beyond The Pole = True = Pier W
            //

            if (TSXLink.Mount.BeyondThePole == TSXLink.Mount.SOP.PierEast)
            { OTAButton.Text = "East"; }
            else
            { OTAButton.Text = "West"; }
            return;
        }

        private void StagingEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.StagingEnabled = StagingEnabledCheckBox.Checked;
        }

        private void StartupEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.StartUpEnabled = StartupEnabledCheckBox.Checked;
        }

        private void ShutdownEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.ShutDownEnabled = ShutdownEnabledCheckBox.Checked;
        }

        private void MinAltitudeBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.MinimumAltitude = (int)MinimumAltitudeBox.Value;
        }

        private void StagingEnabledCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.StagingEnabled = StagingEnabledCheckBox.Checked;
        }

        private void StartupEnabledCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.StartUpEnabled = StartupEnabledCheckBox.Checked;
        }

        private void ShutdownEnabledCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.ShutDownEnabled = ShutdownEnabledCheckBox.Checked;
        }

        private void EnableMeridianFlipBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsMeridianFlipEnabled = EnableMeridianFlipBox.Checked;
        }

        private void StagingBrowseButton_Click(object sender, EventArgs e)
        {
            //Upon clicking the Browse button
            //  A file selection dialog is run to pick up a filepath for the
            //  system staging file.  The result, if chosen, is entered in the associated filename box
            //  in the form, and the superscan configuration file updated accordingly.

            SessionControl openSession = new SessionControl();
            DialogResult stageSystemPathDiag = StageSystemFileDialog.ShowDialog();
            if (stageSystemPathDiag == System.Windows.Forms.DialogResult.OK)
            {
                openSession.StagingFilePath = StageSystemFileDialog.FileName;
                StageSystemFilePathBox.Text = Path.GetFileName(StageSystemFileDialog.FileName);
            }
            else
            {
                openSession.StagingFilePath = null;
                StageSystemFilePathBox.Text = null;
            }
        }

        private void StartUpBrowseButton_Click(object sender, EventArgs e)
        {
            //Upon clicking the Browse button
            //  A file selection dialog is run to pick up a filepath for the
            //  system staging file.  The result, if chosen, is entered in the associated filename box
            //  in the form, and the superscan configuration file updated accordingly.

            SessionControl openSession = new SessionControl();
            DialogResult startupPathDiag = StartUpFileDialog.ShowDialog();
            if (startupPathDiag == System.Windows.Forms.DialogResult.OK)
            {
                openSession.StartUpFilePath = StartUpFileDialog.FileName;
                StartUpFilePathBox.Text = Path.GetFileName(StartUpFileDialog.FileName);
            }
            else
            {
                openSession.StartUpFilePath = null;
                StartUpFilePathBox.Text = null;
            }
        }

        private void ShutDownBrowseButton_Click(object sender, EventArgs e)
        {
            //Upon clicking the Browse button
            //  A file selection dialog is run to pick up a filepath for the
            //  system staging file.  The result, if chosen, is entered in the associated filename box
            //  in the form, and the superscan configuration file updated accordingly.

            SessionControl openSession = new SessionControl();
            DialogResult shutdownPathDiag = ShutDownFileDialog.ShowDialog();
            if (shutdownPathDiag == System.Windows.Forms.DialogResult.OK)
            {
                openSession.ShutDownFilePath = ShutDownFileDialog.FileName;
                ShutDownFilePathBox.Text = Path.GetFileName(ShutDownFileDialog.FileName);
            }
            else
            {
                openSession.ShutDownFilePath = null;
                ShutDownFilePathBox.Text = null;
            }
        }


    }
}
