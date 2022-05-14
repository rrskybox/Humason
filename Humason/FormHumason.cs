using Planetarium;
using System;
using System.Deployment.Application;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormHumason : Form
    {
        private Properties.Settings settings;

        public static bool InitializingHumason;

        public static FormDevices fDeviceForm;
        public static FormFlats fFlatsForm;
        public static FormTarget fTargetForm;
        public static FormAutoGuide fGuideForm;
        public static FormAutoFocus fFocusForm;
        public static FormRotate fRotateForm;
        public static FormPlan fPlanForm;
        public static FormSessionControl fSessionForm;
        public static FormDome fDomeForm;


        //public static bool AbortFlag;
        //Open public field for other classes to refer to when raising abort events to this form
        //

        public static LogEvent StatusReportEvent;
        //public static LogEvent lg;

        public FormHumason()
        {
            settings = new Properties.Settings();

            InitializingHumason = true;

            InitializeComponent();
            ColorButtonsGreen();

            //Event Handlers
            //Add the method for handling target reset events to the queue of such handlers (if any others.._
            //AbortEvent ag = new AbortEvent();
            //ag.AbortEventHandler += AbortReportEvent_Handler;

            //Add log event generator
            StatusReportEvent = new LogEvent();
            StatusReportEvent.LogEventHandler += LogReportUpdate_Handler;

            //Open session and default target xml data
            SessionControl openSession = new SessionControl();
            TargetPlan dtPlan = new TargetPlan("Default");
            openSession.DefaultTargetPlanPath = dtPlan.DefaultPlanPath;

            //Initialize tab forms
            fSessionForm = new FormSessionControl { TopLevel = false };
            SessionTab.Controls.Add(fSessionForm);
            fSessionForm.Show();

            fTargetForm = new FormTarget { TopLevel = false };
            TargetTab.Controls.Add(fTargetForm);
            fTargetForm.Show();

            fDeviceForm = new FormDevices { TopLevel = false };
            DevicesTab.Controls.Add(fDeviceForm);
            fDeviceForm.Show();

            fFlatsForm = new FormFlats { TopLevel = false };
            FlatsTab.Controls.Add(fFlatsForm);
            fFlatsForm.Show();

            fGuideForm = new FormAutoGuide { TopLevel = false };
            GuideTab.Controls.Add(fGuideForm);
            fGuideForm.Show();

            fFocusForm = new FormAutoFocus { TopLevel = false };
            FocusTab.Controls.Add(fFocusForm);
            fFocusForm.Show();

            if (settings.RotatorDeviceEnabled)
            {
                fRotateForm = new FormRotate { TopLevel = false };
                RotatorTab.Controls.Add(fRotateForm);
                fRotateForm.Show();
            }
            else { HumasonTabs.TabPages.Remove(RotatorTab); }

            fPlanForm = new FormPlan { TopLevel = false };
            PlanTab.Controls.Add(fPlanForm);
            fPlanForm.Show();

            if (settings.HasDomeAddOn)
            {
                fDomeForm = new FormDome { TopLevel = false };
                DomeTab.Controls.Add(fDomeForm);
                fDomeForm.Show();
            }
            else { HumasonTabs.TabPages.Remove(DomeTab); }

            //Open log and subscribe this form to the log event 
            StatusReportEvent.CreateLog();

            // Acquire the version information and put it in the form header
            try { this.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(); }
            catch { this.Text = " in Debug"; } //probably in debug, no version info available
            this.Text = "Humason V" + this.Text;
            // Place initial inforamtion in Log
            StatusReportEvent.LogIt("********* New Humason Run **********\r\n");
            StatusReportEvent.LogIt("* " + this.Text + "  " + DateTime.Now.ToShortDateString() + " **" + "\r\n");
            StatusReportEvent.LogIt("******* Humason Initialized ********");
            // All done with initialization
            InitializingHumason = false;
            TopMost = true;
        }

        #region command handlers

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (NHUtil.IsButtonGreen(ConnectButton))
            {
                NHUtil.ButtonRed(ConnectButton);
                Operations.InitializeSystem();
                NHUtil.ButtonGreen(ConnectButton);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            LogEvent lg = new LogEvent();
            NHUtil.ButtonRed(DisconnectButton);
            lg.LogIt("Disconnecting");
            if (ParkMountCheckBox.Checked)
            {
                lg.LogIt("Parking mount");
                TSXLink.Connection.SecureMount();
                ParkMountCheckBox.Checked = false;
            }
            TSXLink.Connection.DisconnectAllDevices();
            NHUtil.ButtonGreen(DisconnectButton);
            return;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            //disconnnect devices and turn off power
            DisconnectButton_Click(sender, e);
            Close();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(StartButton);
            StartButton.Text = "Running";
            SetRunning();
            //One can minimize TSX to help with performance, but don't
            //ManageTSX.MinimizeTSX();
            //Set the form to display the target tab
            HumasonTabs.SelectedIndex = 1;
            SessionState = SessionStateFlag.Running;
            //Light off the overall imaging control process
            bool successReport = Operations.ImagingControl();
            //All done
            SessionState = SessionStateFlag.Stopped;
            //Clear the Running and Abort buttons
            StartButton.Text = "Start";
            NHUtil.ButtonGreen(StartButton);
            AbortButton.Text = "Stop";
            NHUtil.ButtonGreen(AbortButton);
        }

        private void ColorButtonsGreen()
        {
            NHUtil.ButtonGreen(ConnectButton);
            NHUtil.ButtonGreen(CloseButton);
            NHUtil.ButtonGreen(DisconnectButton);
            NHUtil.ButtonGreen(StartButton);
            NHUtil.ButtonGreen(AbortButton);
            NHUtil.ButtonGreen(CloseButton);
            NHUtil.ButtonGreen(OptionsButton);
            NHUtil.ButtonGreen(AboutButton);
            return;
        }

        private void OnTopCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (OnTopCheck.Checked)
            { TopMost = true; }
            else
            { TopMost = false; }
            Show();
            return;
        }

        private void AttendedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsAttended = AttendedCheckBox.Checked;
            return;
        }
        private void OptionsButton_Click(object sender, EventArgs e)
        {
            //Open the options form to change options
            FormOptions fOptionsForm = new FormOptions();
            fOptionsForm.Show();
            return;
        }

        private void HomeMountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsHomeMountEnabled = HomeMountCheckBox.Checked;
        }

        private void ParkMountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsParkMountEnabled = ParkMountCheckBox.Checked;
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Humason Astro-Imaging Control Application" + "\n" + "Created by R.McAlister, 2017");
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(AbortButton);
            //AbortEvent ab = new AbortEvent();
            SetAbort();
            //ab.AbortIt("Abort Button Clicked");
            NHUtil.ButtonGreen(AbortButton);
            StartButton.Text = "Aborted";
            NHUtil.ButtonRed(StartButton);
        }

        private void TabPageSelected_Click(object sender, TabControlEventArgs e)
        {
            //a tab page has been selected, if it is the target page and a session is not running, then update the current target
            if (e.TabPage.Name == "TargetTab" && SessionState != SessionStateFlag.Running) fTargetForm.TabUpdate();
            return;
        }

        #endregion

        #region event handlers

        public void LogReportUpdate_Handler(object sender, LogEvent.LogEventArgs e)
        {
            StatusBox.AppendText(e.LogEntry + "\r\n");
            this.Show();
            return;
        }
        #endregion

        #region sessionstate

        public enum SessionStateFlag { Stopped, Running, Aborting }

        public static SessionStateFlag SessionState { get; set; } = SessionStateFlag.Stopped;

        public static bool IsAborting()
        {
            if (SessionState == SessionStateFlag.Aborting) return true;
            else return false;
        }

        public static void SetAbort() { SessionState = SessionStateFlag.Aborting; }

        public static void SetRunning() { SessionState = SessionStateFlag.Running; }

        public static void SetStopped() { SessionState = SessionStateFlag.Stopped; }

        #endregion

    }
}

