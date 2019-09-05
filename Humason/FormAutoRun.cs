using System;
using System.IO;
using System.Windows.Forms;


namespace Humason
{
    public partial class FormAutoRun : Form
    {
        private SessionControl SessionData { get; set; }

        public FormAutoRun()
        {
            //When an instance of the autostart form is created, 
            // the text boxes -- start up filepath, shutdown filepath, start up time
            // are filled in from the superscan configuration file.  That's it.

            InitializeComponent();
            SessionData = new SessionControl();
            StageSystemFilePathBox.Text = Path.GetFileName(SessionData.StagingFilePath);
            StartUpFilePathBox.Text = Path.GetFileName(SessionData.StartUpFilePath);
            ShutDownFilePathBox.Text = Path.GetFileName(SessionData.ShutDownFilePath);
            StagingWaitCheckBox.Checked = SessionData.IsStagingWaitEnabled;
            StartupWaitCheckBox.Checked = SessionData.IsStartUpWaitEnabled;
            ShutdownWaitCheckBox.Checked = SessionData.IsShutDownWaitEnabled;
            //Set the default browse locations to the TSX ToolKit Start Up directory
            string ttdir = "C:\\Users\\" + System.Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\TSXToolkit\\TSXToolkit";
            if (Directory.Exists(ttdir))
            {
                StageSystemFileDialog.InitialDirectory = ttdir;
                StartUpFileDialog.InitialDirectory = ttdir;
                ShutDownFileDialog.InitialDirectory = ttdir;
            }
        }

        private void StageSystemBrowseButton_Click(object sender, EventArgs e)
        {
            //Upon clicking the Browse button on the Stage System filename box,
            //  A file selection dialog is run to pick up a filepath for the
            //  system staging file.  The result, if chosen, is entered in the stage System filename box
            //  in the form, and the superscan configuration file updated accordingly.

            DialogResult stageSystemPathDiag = StageSystemFileDialog.ShowDialog();
            if (stageSystemPathDiag == System.Windows.Forms.DialogResult.OK)
            {
                SessionData.StagingFilePath = StageSystemFileDialog.FileName;
                StageSystemFilePathBox.Text = Path.GetFileName(StageSystemFileDialog.FileName);
            }
        }

        private void StartUpBrowseButton_Click(object sender, EventArgs e)
        {
            //Upon clicking the Browse button on the Start Up filename box,
            //  A file selection dialog is run to pick up a filepath for the
            //  start up file.  The result, if chosen, is entered in the start up filename box
            //  in the form, and the superscan configuration file updated accordingly.

            DialogResult startUpPathDiag = StartUpFileDialog.ShowDialog();
            if (startUpPathDiag == System.Windows.Forms.DialogResult.OK)
            {
                SessionData.StartUpFilePath = StartUpFileDialog.FileName;
                StartUpFilePathBox.Text = Path.GetFileName(StartUpFileDialog.FileName);
            }
        }

        private void ShutDownBrowseButton_Click(object sender, EventArgs e)
        {
            //Upon clicking the Browse button on the Shutdown filename box,
            //  A file selection dialog is run to pick up a filepath for the
            //  shutdown file.  The result, if chosen, is entered in the shutdown filename box
            //  in the form, and the superscan configuration file updated accordingly.

            DialogResult shutDownPathDiag = ShutDownFileDialog.ShowDialog();
            if (shutDownPathDiag == System.Windows.Forms.DialogResult.OK)
            {
                SessionData.ShutDownFilePath = ShutDownFileDialog.FileName;
                ShutDownFilePathBox.Text = Path.GetFileName(ShutDownFileDialog.FileName);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //SessionData.StagingFilePath = StageSystemFilePathBox.Text;
            //SessionData.StartUpFilePath = StartUpFilePathBox.Text;
            //SessionData.ShutDownFilePath = ShutDownFilePathBox.Text;
            SessionData.IsStagingWaitEnabled = StagingWaitCheckBox.Checked;
            SessionData.IsStartUpWaitEnabled = StartupWaitCheckBox.Checked;
            SessionData.IsShutDownWaitEnabled = ShutdownWaitCheckBox.Checked;
            Close();
            return;
        }

        private void StagingWaitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionData.IsStagingWaitEnabled = StagingWaitCheckBox.Checked;
        }

        private void StartupWaitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionData.IsStartUpWaitEnabled = StartupWaitCheckBox.Checked;
        }

        private void ShutdownWaitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionData.IsShutDownWaitEnabled = ShutdownWaitCheckBox.Checked;
        }
    }
}

