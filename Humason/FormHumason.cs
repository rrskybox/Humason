using Planetarium;
using System;
using System.Deployment.Application;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormHumason : Form
    {
        private Properties.Settings settings;

        public static SessionControl openSession;

        public static bool InitializingHumason;
        public static FormDevices fCameraForm;
        public static FormFlats fCalibrationForm;
        public static FormTarget fSequenceForm;
        public static FormAutoGuide fGuideForm;
        public static FormAutoFocus fFocusForm;
        public static FormRotate fRotateForm;
        public static FormPlan fPlanForm;
        public static FormSessionControl fSessionForm;
        public static FormDome fDomeForm;

        //Open public variable for logging from all subforms
        public static LogEvent lg = new LogEvent();

        public static bool AbortFlag;
        //Open public field for other classes to refer to when raising abort events to this form
        public static AbortEvent SequenceAbort = new AbortEvent();

        public FormHumason()
        {
            settings = new Properties.Settings();

            InitializingHumason = true;

            InitializeComponent();
            ColorButtonsGreen();

            //Add the method for handling target reset events to the queue of such handlers (if any others.._
            SequenceAbort.AbortEventHandler += AbortReportEvent_Handler;

            //Initialize tab forms

            openSession = new SessionControl();

            fSessionForm = new FormSessionControl { TopLevel = false };
            SessionTab.Controls.Add(fSessionForm);
            fSessionForm.Show();

            //Initialize Default Target Plan
            TargetPlan dtPlan = new TargetPlan("Default");  //code for default
                                                            //openSession.CurrentTargetName = "Default";

            openSession.DefaultTargetPlanPath = dtPlan.DefaultPlanPath;

            fSequenceForm = new FormTarget { TopLevel = false };
            TargetTab.Controls.Add(fSequenceForm);
            fSequenceForm.Show();

            fCameraForm = new FormDevices { TopLevel = false };
            DevicesTab.Controls.Add(fCameraForm);
            fCameraForm.Show();

            fCalibrationForm = new FormFlats { TopLevel = false };
            FlatsTab.Controls.Add(fCalibrationForm);
            fCalibrationForm.Show();

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
            lg.CreateLog();
            lg.LogEventHandler += LogReportUpDate_Handler;

            // Acquire the version information and put it in the form header
            try { this.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(); }
            catch { this.Text = " in Debug"; } //probably in debug, no version info available
            this.Text = "Humason V" + this.Text;
            // Place initial inforamtion in Log
            lg.LogIt("********* New Humason Run **********\r\n");
            lg.LogIt("* " + this.Text + "  " + DateTime.Now.ToShortDateString() + " **" + "\r\n");
            lg.LogIt("******* Humason Initialized ********");
            // All done with initialization
            InitializingHumason = false;
            return;
        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (NHUtil.IsButtonGreen(ConnectButton))
            {
                NHUtil.ButtonRed(ConnectButton);
                InitializeSystem();
                NHUtil.ButtonGreen(ConnectButton);
            }
        }

        private void InitializeSystem()
        {
            //Power on and connect devices as configured
            lg.LogIt("Connecting");
            if (HomeMountCheckBox.Checked) { TSXLink.Connection.DeployMount(); }
            //Make sure themount is Parked
            TSXLink.Mount.Park();
            TSXLink.Connection.ConnectAllDevices();
            fCameraForm.RefreshFilterList();
            lg.LogIt("Devices Connected");
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
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
            //Runs the photo shoot shebang
            //First a little housekeeping -- configuration update and make sure the clock is set right
            //if Autorun staging is set, wait on autostaging time, then run the staging app
            //if Autorun start is set, wait on autostart time, then run the starting app
            //Wait on Sequence start
            //
            NHUtil.ButtonRed(StartButton);
            LogEvent lg = FormHumason.lg;

            //Run a quick check on all the things that might be wrong, based on past history
            //  the main thing being that there is at least one target plan queued.
            StartButton.Text = "Checking";
            lg.LogIt("Running diagnostics");
            if (!Diagnostics.CheckUp())
            {
                lg.LogIt("Diagnostics abort");
                NHUtil.ButtonGreen(StartButton);
                StartButton.Text = "Start";
                return;
            }
            lg.LogIt("Diagnostics success");

            //All good -- start running
            StartButton.Text = "Running";
            //Clear abortflag (if necessary)
            AbortFlag = false;

            //All selected devices should be on-line and connected
            //Load the first target plan so we can pull some information for this session
            lg.LogIt("Loading first session target plan");
            if (!FormHumason.fPlanForm.IsTopPlanTargetName())
            {
                lg.LogIt("No Target Plan");
                NHUtil.ButtonGreen(StartButton);
                StartButton.Text = "Start";
                return;
            }
            FormHumason.openSession.CurrentTargetName = FormHumason.fPlanForm.GetTopPlanTargetName();
            TargetPlan ftPlan = new TargetPlan(openSession.CurrentTargetName);
            //Flush it out, if we have to
            if (ftPlan.IsSparsePlan())
            {
                lg.LogIt("Sparse Plan -- filling out from default");
                ftPlan.FlushOutFromDefaultPlan();
            }
            //Update the form and regenerate the sequence
            fSequenceForm.TargetBox.Text = ftPlan.TargetName;
            fSequenceForm.RegenerateSequence();
            fSequenceForm.UpdateFormFromPlan();
            //Set the form to display the target tab
            HumasonTabs.SelectedIndex = 1;
            //Get the staging, start and shut down times from this initial target, although may not be used
            openSession.StagingTime = DateTime.Now;
            openSession.StartUpTime = ftPlan.SequenceStartTime;
            openSession.ShutDownTime = fSequenceForm.DawnTimeBox.Value;

            //Save configuration set up, reset the progess bar and make sure that the TSX clock is set to current time
            fSequenceForm.UpdateFormFromPlan();
            TSXLink.StarChart.SetClock(0, true);

            //Await Staging and Start Up
            //Potentially nothing is powered or initialized at this point
            AwaitStagingAndStartUp();

            //Both Staging and Start Up have been run. All devices should be powered, but not necessarily connected
            //Power up and connect devices (if not done already)
            lg.LogIt("Initializing system");
            InitializeSystem();

            //Remove all flat requests from the flats file
            //   No, don't
            //FlatManager nhFlat = new FlatManager();
            //nhFlat.FlatSetClearAll();
            //nhFlat = null;

            //Check for proximity to meridian flip
            //  if HA of target is within 10 minutes, then just wait it out at 
            //Get target HA
            TSXLink.Target tgto;
            lg.LogIt("Checking for new target to clear meridian");
            tgto = TSXLink.StarChart.FindTarget(ftPlan.TargetName);
            double ha = tgto.HA.TotalMinutes;
            while ((ha >= -10) && (ha <= 0))
            {
                System.Threading.Thread.Sleep(10000);
                tgto = TSXLink.StarChart.FindTarget(ftPlan.TargetName);
                ha = tgto.HA.TotalMinutes;
            }
            lg.LogIt("New target is clear of meridian");

            //Make sure the mount is unparked
            TSXLink.Mount.UnPark();
            //Make sure tracking is turned on
            TSXLink.Mount.TurnTrackingOn();

            //Bring camera to temperature (if not already), then clear the objects
            AstroImage asti = new AstroImage() { Camera = AstroImage.CameraType.Imaging };
            TSXLink.Camera cCam = new TSXLink.Camera(asti);
            cCam.CCDTemperature = ftPlan.CameraTemperatureSet;
            cCam = null; asti = null;

            //************************  Starting up the target plans  *************************
            //
            //  This loop is to run each of the plans in the schedule list sequentially.
            //  The current plan has already progressed through any staging and starting scripts
            //      and the next step will be to wait on the starting time, if it hasn't passed already
            //  At the end of the loop, the current plan will be deleted from the schedule and the next one, if any
            //      loaded.  If none, then the shutdown script of the last plan will be run (if autorun is set for that plan).
            //
            while (FormHumason.fPlanForm.IsTopPlanTargetName())
            {
                //Change the displayed form to the imaging form
                HumasonTabs.SelectedIndex = 1;

                //Load the top scheduled plan
                string tgtName = FormHumason.fPlanForm.GetTopPlanTargetName();
                FormHumason.openSession.CurrentTargetName = tgtName;
                TargetPlan tPlan = new TargetPlan(tgtName);

                lg.LogIt(" ******************* Imaging Target: " + openSession.CurrentTargetName);
                if (tPlan.IsSparsePlan())
                {
                    lg.LogIt("Sparse Plan -- filling out from default");
                    tPlan.FlushOutFromDefaultPlan();
                }
                fSequenceForm.TargetBox.Text = tPlan.TargetName;
                fSequenceForm.RegenerateSequence();
                fSequenceForm.UpdateFormFromPlan();
                HumasonTabs.SelectedIndex = 1;

                //Wait for target sequence starting time
                LaunchPad.WaitLoop(tPlan.SequenceStartTime);
                //check for abort
                if (AbortFlag)
                {
                    NHUtil.ButtonGreen(StartButton);
                    StartButton.Text = "Start";
                    return;
                }

                //Try to move to target, if this fails just abort
                if (!fSequenceForm.imgseq.CLSToTargetPlanCoordinates())
                {
                    AbortFlag = true;
                }

                //check for abort
                if (AbortFlag)
                {
                    NHUtil.ButtonGreen(StartButton);
                    StartButton.Text = "Start";
                    return;
                }

                //Now lets get the rotator positioned properly, plate solve, then rotate, then plate solve
                if (tPlan.RotatorEnabled)
                {
                    Rotator.PlateSolveIt();
                    Rotator.RotateToImagePA(tPlan.TargetPA);
                    Rotator.PlateSolveIt();
                }
                //check for abort
                if (AbortFlag)
                {
                    NHUtil.ButtonGreen(StartButton);
                    StartButton.Text = "Start";
                    return;
                }

                //Update the sequence for whatever time it is now
                try
                { fSequenceForm.imgseq.SeriesGenerator(); }
                catch
                {
                    lg.LogIt("Make Series Error");
                    NHUtil.ButtonGreen(StartButton);
                    StartButton.Text = "Start";
                    return;
                }

                //Set Progress to zero
                fSequenceForm.ProgressBar.Value = 0;
                //
                // Run Imaging Sequence
                fSequenceForm.imgseq.PhotoShoot();
                //
                //
                //All done.  Abort autoguiding, assuming is running -- should be off, but you never know
                AutoGuide.AutoGuideStop();
                //Run a check on abort, if set, just return
                if (AbortFlag)
                {
                    NHUtil.ButtonGreen(StartButton);
                    StartButton.Text = "Start";
                    return;
                }
                //Done with imaging on this plan.  
                //Store the ending time
                tPlan.SequenceEndTime = DateTime.Now;
                //Save the plan in the sequence complete summary file
                openSession.AddSequenceCompleteSummary();
                //string tName = tPlan.GetItem(TargetPlan.sbTargetNameName); //why??
                //Remove the plan from the schedule and look for the next
                FormHumason.fPlanForm.RemoveTopPlan();
            }

            //done with imaging.  Check on flats  See if any flats have been requested

            FlatManager fmgr = new FlatManager();
            if (fmgr.HaveFlatsToDo())
            { fmgr.TakeFlats(); }

            //If autorun set, then run it, or... just park the mount
            if (openSession.IsAutoRunEnabled)
            { LaunchPad.RunShutDownApp(); }
            else
            {
                try { TSXLink.Mount.Park(); }
                catch (Exception ex) { lg.LogIt("Could not Park: " + ex.Message); }
            }
            NHUtil.ButtonGreen(StartButton);
            StartButton.Text = "Start";
            return;
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

        private void AbortButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(AbortButton);
            LogEvent lg = FormHumason.lg;
            AbortEvent ab = SequenceAbort;
            ab.AbortIt("Abort Button Clicked");
            //ProgressBar.Value = 0;
            //lg.LogIt("Imaging Run Aborted");
            //AbortFlag = true;
            NHUtil.ButtonGreen(AbortButton);
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

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            //Open the options form to change options
            FormOptions fOptionsForm = new FormOptions();
            fOptionsForm.Show();
            return;
        }

        private void HomeMountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.IsHomeMountEnabled = HomeMountCheckBox.Checked;
        }

        private void ParkMountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.IsParkMountEnabled = ParkMountCheckBox.Checked;
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Humason Astro-Imaging Control Application" + "\n" + "Created by R.McAlister, 2017");
        }

        private void LogReportUpDate_Handler(object sender, LogEvent.LogEventArgs e)
        {
            StatusStripLine.Text = e.LogEntry;
            this.Show();
            Application.DoEvents();
            return;
        }

        private void AbortReportEvent_Handler(object sender, AbortEvent.AbortEventArgs e)
        {
            AbortFlag = true;
            return;
        }

        /// <summary>
        /// Wait loop control for Staging and Start Up
        /// </summary>
        private void AwaitStagingAndStartUp()
        {
            //Wait until Staging Time
            LaunchPad.WaitLoop(openSession.StagingTime);
            //check for abort
            if (AbortFlag)
            {
                NHUtil.ButtonGreen(StartButton);
                StartButton.Text = "Start";
                return;
            }
            //  If autorun enabled, then run the staging time autorun script/app
            if (openSession.IsAutoRunEnabled)
            {
                if (openSession.IsStagingEnabled)
                { LaunchPad.WaitStaging(); }
            }
            //check for abort
            if (AbortFlag)
            {
                NHUtil.ButtonGreen(StartButton);
                StartButton.Text = "Start";
                return;
            }

            //Wait until Start Up Time
            LaunchPad.WaitLoop(openSession.StartUpTime);
            //check for abort
            if (AbortFlag)
            {
                NHUtil.ButtonGreen(StartButton);
                StartButton.Text = "Start";
                return;
            }
            //  If autorun enabled, then run the start up time autorun script/app
            if (openSession.IsAutoRunEnabled)
            {
                if (openSession.IsStartUpEnabled)
                { LaunchPad.WaitStartUp(); }
            }
            //check for abort
            if (AbortFlag)
            {
                NHUtil.ButtonGreen(StartButton);
                StartButton.Text = "Start";
                return;
            }

        }


    }
}

