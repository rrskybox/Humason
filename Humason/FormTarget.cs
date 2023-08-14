//Form for composing and launching image sequence
//
//Author: Rick McAlister
//Date: 6/18/15 (and much, much earlier...)
//
//

using System;
using System.Windows.Forms;
using TheSky64Lib;

namespace Humason
{
    public partial class FormTarget : Form
    {
        public Sequencer imgseq;

        //Open public field for other classes to refer to when raising target reset events to this form
        public static TargetEvent targetreset = new TargetEvent();

        public FormTarget()
        {
            //Build form
            InitializeComponent();
            //Initialize from/to configuration file
            SBInitialConfiguration();
            //Create default sequence
            imgseq = new Sequencer();
            //Subscribe this form to the log event 
            ProgressEvent prgEvent = new ProgressEvent();
            prgEvent.ProgressUpdateEventHandler += ProgressUpdate_Handler;
            //Set command button colors
            NHUtil.ButtonGreen(UpdateButton);
            NHUtil.ButtonGreen(SaveDefaultButton);
            //Add the method for handling target reset events to the queue of such handlers (if any others.._
            targetreset.TargetEventHandler += TargetResetEvent_Handler;
        }

        public void UpdateButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(UpdateButton);
            TabUpdate();
            NHUtil.ButtonGreen(UpdateButton);
        }

        public void TabUpdate()
        {
            LogEvent lg = new LogEvent();
            //If the targetname is empty, then check for a target name in TSX Find,
            //If still empty then just return
            string currentPlan = TargetBox.Text;
            if (currentPlan == "")
            {
                lg.LogIt("Error: Attempted to update empty target sequence");
                MessageBox.Show("Please load a target plan before attempting to update.");
                return;
            }
            lg.LogIt("Updating Sequence");
            UpdateSequence();
            lg.LogIt("Sequence Updated");
            return;
        }

        private void TargetBox_TextChanged(object sender, EventArgs e)
        {
            //Just ignore this change if we are still initializing the forms and such
            //Otherwise, 
            //  Change (in this box) must from loading a new plan from the form or sequencer
            //     (note that the contents could be null)
            //  In any case, open or create a new tPlan and populate the data boxes
            //
            SessionControl openSession = new SessionControl();
            if (FormHumason.InitializingHumason)
            { return; }
            else
            {
                openSession.CurrentTargetName = TargetBox.Text;
            }
        }

        private void LRGBRatioBox_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason) return;
            //If the LRGBRatio is set to more than 1 and the filterset count is only 1 (or zero), then reset to 1
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (LRGBRatioBox.Value > 1 && tPlan.FilterWheelList.Count < 2)
                tPlan.LRGBRatio = 1;
            else
                tPlan.LRGBRatio = (int)LRGBRatioBox.Value;

            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void StartTimeBox_ValueChanged(object sender, EventArgs e)
        {
            //Save to Session
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            tPlan.SequenceStartTime = StartTimeBox.Value;
        }

        private void ExposureVal_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason) return;
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName) { ImageExposureTime = (double)ExposureVal.Value };
            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void LoopsVal_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason) return;
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName) { Loops = (int)LoopsVal.Value };
            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void DelayVal_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason) return;

            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName) { Delay = (double)DelayVal.Value };
            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void UpdateTimesFromSequence()
        {
            SessionControl openSession = new SessionControl();
            //Save the configuration
            UpdateFormFromPlan();
            //Update time boxes if valid (not Default Target Plan)
            try
            {
                //StartTimeBox.Value = imgseq.SiteLocalTime;
                FlipTimeBox.Text = imgseq.FlipText;
                DoneTimeBox.Text = imgseq.DoneText;
                TransitBox.Text = imgseq.TransitText;
                LimitLabel.Text = "Alt<" + openSession.MinimumAltitude;
                DawnTimeBox.Value = imgseq.DawnDateTime;
                SetLimitBox.Text = imgseq.SetLimitText;
                RiseLimitBox.Text = imgseq.RiseLimitText;
            }
            catch { }  //Just ignore, if there is an error
            Show();
        }

        public void UpdateSequence()
        {
            //Update Sequence will load the target name (or adjusted location) into the Find
            //  function and update all the location information, then run a new calculation on times, etc
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            sky6ObjectInformation tsxo = new sky6ObjectInformation();
            string targetName = tPlan.TargetName;
            //if the targetName is null, then pull the targetName from TSX, load into the textbox and store in configuration file
            if (targetName == "")
            {
                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
                string tName = tsxo.ObjInfoPropOut;
                tPlan.TargetName = tName;
                TargetBox.Text = tName;
                return;
            }
            //if the adjust target is checked, then run FInd on the target coordinates
            //otherwise, run a find on the target name
            string targetTitle;
            if (tPlan.TargetAdjustEnabled)
            {
                targetTitle = tPlan.TargetRA.ToString() + "," + tPlan.TargetDec.ToString();
            }
            else
            {
                targetTitle = targetName;
            }
            TSXLink.Target tgto = TSXLink.StarChart.FindTarget(targetTitle);
            if (tgto == null)
            {
                LogEvent lg = new LogEvent();
                lg.LogIt("Target not recognized in TSX Find function -- i.e. not cataloged.");
                return;
            }
            //Update the target Name in box
            TargetBox.Text = targetName;
            TargetRABox.Value = (decimal)tgto.RA;
            TargetDecBox.Value = (decimal)tgto.Dec;

            //Update the start time in the target plan
            tPlan.SequenceStartTime = StartTimeBox.Value;
            tPlan.SequenceDawnTime = DawnTimeBox.Value;

            //Save the new configuration information
            UpdateFormFromPlan();
            RegenerateSequence();
            return;
        }

        public void RegenerateSequence()
        {
            //Close out the current sequence class and open a new one with the current configuration file
            //  including reseting the progress update handler, if needed
            imgseq = new Sequencer();
            //Reset the progress to 0 using the event chain
            ProgressEvent prgEvent = new ProgressEvent();
            prgEvent.ProgressUpdate(0);
            //Recalculate the imaging sequence and display the times.
            imgseq.SequenceGenerator();
            UpdateTimesFromSequence();
        }

        public void UpdateStatusBar(int newlen)
        {
            if (newlen > ProgressBar.Maximum)
            {
                newlen = ProgressBar.Maximum;
            }

            ProgressBar.Value = newlen;
            ProgressBar.Refresh();
            this.Show();
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(10);
        }

        private void SBInitialConfiguration()
        {
            //Initialize form fields from current target plan, which will be the default target plan
            //  when first booting up

            SessionControl openSession = new SessionControl();
            string currentTarget = openSession.CurrentTargetName;
            if (currentTarget != null)
            {
                TargetPlan tPlan = new TargetPlan(currentTarget);
                //TargetBox.Text = "";
                TargetBox.Text = currentTarget;
                TargetRABox.Value = (decimal)tPlan.TargetRA;
                TargetDecBox.Value = (decimal)tPlan.TargetDec;
                TargetPABox.Value = (decimal)tPlan.TargetPA;
                MakeFlatsCheckBox.Checked = tPlan.MakeFlatsEnabled;
                ExposureVal.Value = (decimal)tPlan.ImageExposureTime;
                LoopsVal.Value = (decimal)tPlan.Loops;
                LRGBRatioBox.Value = (decimal)tPlan.LRGBRatio;
                DelayVal.Value = (decimal)tPlan.Delay;
                //Set start time to current time and update the (default) target plan start time
                StartTimeBox.Value = DateTime.Now;
                tPlan.SequenceStartTime = StartTimeBox.Value;
                //Save the dawn time as the hard stop for the sequence, if needed
                tPlan.SequenceDawnTime = DawnTimeBox.Value;
                if (tPlan.TargetAdjustEnabled)
                {
                    AdjustedTargetLabel.Visible = true;
                }
                else
                {
                    AdjustedTargetLabel.Visible = false;
                }
            }
        }

        public void UpdateFormFromPlan()
        {//Update form fields with the content of a new target plan from the session control file
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (tPlan.TargetName == "Default")
            {
                TargetBox.Text = "";
            }
            else
            {
                TargetBox.Text = tPlan.TargetName;
            }

            StartTimeBox.Value = tPlan.SequenceStartTime;
            TargetRABox.Text = tPlan.TargetRA.ToString();
            TargetDecBox.Text = tPlan.TargetDec.ToString();
            TargetPABox.Text = tPlan.TargetPA.ToString();
            MakeFlatsCheckBox.Checked = tPlan.MakeFlatsEnabled;
            ExposureVal.Value = (decimal)tPlan.ImageExposureTime;
            LoopsVal.Value = tPlan.Loops;
            LRGBRatioBox.Value = tPlan.LRGBRatio;
            DelayVal.Value = (decimal)tPlan.Delay;

            if (tPlan.TargetAdjustEnabled)
            {
                AdjustedTargetLabel.Visible = true;
            }
            else
            {
                AdjustedTargetLabel.Visible = false;
            }
        }

        private void MakeFlatsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (FormHumason.InitializingHumason) { return; }
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                MakeFlatsEnabled = MakeFlatsCheckBox.Checked
            };
        }

        private void DawnTimeBox_ValueChanged(object sender, EventArgs e)
        {
            //Save dawn time as session stop time
            SessionControl sessionControl = new SessionControl();
            sessionControl.ShutDownTime = DawnTimeBox.Value;
            return;
        }

        private void TargetRABox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                TargetRA = (double)TargetRABox.Value
            };
        }

        private void TargetDecBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                TargetDec = (double)TargetDecBox.Value
            };
        }

        private void TargetPABox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                TargetPA = (double)TargetPABox.Value
            };
        }

        private void StartTimeBox_DoubleClick(object sender, EventArgs e)
        {
            //Resets the StartTimeBox datetime to the current time when double clicked
            //var picker = new DateTimePicker();
            //Form f = new Form();
            //f.Controls.Add(picker);

            //var result = f.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    StartTimeBox.Value = picker.Value;
            //}
            StartTimeBox.Value = DateTime.Now;
        }

        private void SaveDefaultButton_Click(object sender, EventArgs e)
        {
            //Saves the current active target file as the default file
            SessionControl openSession = new SessionControl();
            NHUtil.ButtonRed(SaveDefaultButton);
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            tPlan.SavePlanAsDefaultPlan();
            NHUtil.ButtonGreen(SaveDefaultButton);
        }


        #region Event Handlers

        private void TargetResetEvent_Handler(object sender, TargetEvent.TargetEventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            TargetBox.Text = tPlan.TargetName;
            UpdateSequence();
            UpdateFormFromPlan();

            this.Show();
            System.Windows.Forms.Application.DoEvents();
            return;
        }

        protected void ProgressUpdate_Handler(object sender, ProgressEvent.ProgressUpdateEventArgs e)
        {
            UpdateStatusBar(e.ProgressPercent);
        }


        #endregion


    }
}