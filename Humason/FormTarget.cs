//Form for composing and launching image sequence
//
//Author: Rick McAlister
//Date: 6/18/15 (and much, much earlier...)
//
//

using Planetarium;
using System;
using System.Windows.Forms;
using TheSkyXLib;

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
            imgseq.PrgUpdate.ProgressUpdateEventHandler += ProgressUpdate_Handler;
            FormHumason.AbortFlag = false;
            //Set command button colors
            NHUtil.ButtonGreen(UpdateButton);
            NHUtil.ButtonGreen(SaveDefaultButton);
            //Add the method for handling target reset events to the queue of such handlers (if any others.._
            targetreset.TargetEventHandler += TargetResetEvent_Handler;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(UpdateButton);
            LogEvent lg = FormHumason.lg;
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
            NHUtil.ButtonGreen(UpdateButton);
        }

        private void TargetBox_TextChanged(object sender, EventArgs e)
        {
            //Just ignore this change if we are still initializing the forms and such
            //Otherwise, 
            //  Change (in this box) must from loading a new plan from the form or sequencer
            //     (note that the contents could be null)
            //  In any case, open or create a new tPlan and populate the data boxes
            //
            if (FormHumason.InitializingHumason)
            { return; }
            else
            {
                FormHumason.openSession.CurrentTargetName = TargetBox.Text;
                TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            }
        }

        private void LRGBRatioBox_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName)
            {
                LRGBRatio = (int)LRGBRatioBox.Value
            };
            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void StartTimeBox_TextChanged(object sender, EventArgs e)
        {
            //Do nothing
        }

        private void ExposureVal_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName) { ImageExposureTime = (double)ExposureVal.Value };
            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void LoopsVal_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName) { Loops = (int)LoopsVal.Value };
            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void DelayVal_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName) { Delay = (double)DelayVal.Value };
            try { imgseq.SequenceGenerator(); }
            catch { return; }
            UpdateTimesFromSequence();
        }

        private void UpdateTimesFromSequence()
        {
            //Save the configuration
            UpdateFormFromPlan();
            //Update time boxes if valid (not Default Target Plan)
            try
            {
                //StartTimeBox.Value = imgseq.SiteLocalTime;
                FlipTimeBox.Text = imgseq.FlipText;
                DoneTimeBox.Text = imgseq.DoneText;
                TransitBox.Text = imgseq.TransitText;
                LimitBox.Text = imgseq.LimitText;
                LimitLabel.Text = "Alt<" + FormHumason.openSession.MinimumAltitude;
                DawnTimeBox.Value = imgseq.DawnDateTime;
            }
            catch { }  //Just ignor, if there is an error
            Show();
        }

        public void UpdateSequence()
        {
            //Update Sequence will load the target name (or adjusted location) into the Find
            //  function and update all the location information, then run a new calculation on times, etc
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
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

            //Update the target Name in box
            TargetBox.Text = targetName;
            TargetRABox.Value = (decimal)tgto.RA;
            TargetDecBox.Value = (decimal)tgto.Dec;

            //Update the start time in the target plan
            tPlan.SequenceStartTime = StartTimeBox.Value;

            //Save the new configuration information
            UpdateFormFromPlan();
            RegenerateSequence();
            FormHumason.AbortFlag = false;
        }

        public void RegenerateSequence()
        {
            //Close out the current sequence class and open a new one with the current configuration file
            //  including reseting the progress update handler, if needed
            //  and the abort flag
            imgseq = new Sequencer();
            //Subscribe this form to the log event 
            imgseq.PrgUpdate.ProgressUpdateEventHandler += ProgressUpdate_Handler;
            //Recalculate the imaging sequence and display the times.  Clear abort flag.
            imgseq.SequenceGenerator();
            UpdateTimesFromSequence();
            FormHumason.AbortFlag = false;
        }

        public void Update_Status_Bar(int newlen)
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

            string currentTarget = FormHumason.openSession.CurrentTargetName;
            if (currentTarget != null)
            {
                TargetPlan tPlan = new TargetPlan(currentTarget);
                //TargetBox.Text = "";
                TargetBox.Text = currentTarget;
                TargetRABox.Value = (decimal)tPlan.TargetRA;
                TargetDecBox.Value = (decimal)tPlan.TargetDec;
                TargetPABox.Value = (decimal)tPlan.TargetPA;
                AutoDarkCheck.Checked = tPlan.AutoDarkEnabled;
                MakeFlatsCheckBox.Checked = tPlan.MakeFlatsEnabled;
                ExposureVal.Value = (decimal)tPlan.ImageExposureTime;
                LoopsVal.Value = (decimal)tPlan.Loops;
                LRGBRatioBox.Value = (decimal)tPlan.LRGBRatio;
                DelayVal.Value = (decimal)tPlan.Delay;
                //Set start time to current time and update the (default) target plan start time
                StartTimeBox.Value = DateTime.Now;
                tPlan.SequenceStartTime = StartTimeBox.Value;
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
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
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
            AutoDarkCheck.Checked = tPlan.AutoDarkEnabled;
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

        private void ProgressUpdate_Handler(object sender, Progress.ProgressUpdateEventArgs e)
        {
            Update_Status_Bar(e.ProgressPercent);
        }

        private void MakeFlatsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason) { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName)
            {
                MakeFlatsEnabled = MakeFlatsCheckBox.Checked
            };
        }

        private void TargetRABox_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName)
            {
                TargetRA = (double)TargetRABox.Value
            };
        }

        private void TargetDecBox_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName)
            {
                TargetDec = (double)TargetDecBox.Value
            };
        }

        private void TargetPABox_ValueChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName)
            {
                TargetPA = (double)TargetPABox.Value
            };
        }

        private void AutoDarkCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (FormHumason.InitializingHumason)
            { return; }
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName)
            {
                AutoDarkEnabled = AutoDarkCheck.Checked
            };
        }

        private void StartTimeBox_DoubleClick(object sender, EventArgs e)
        {
            //Resets the StartTimeBox datetime to the current time when double clicked
            StartTimeBox.Value = DateTime.Now;
        }

        #region Event Handlers

        private void TargetResetEvent_Handler(object sender, TargetEvent.TargetEventArgs e)
        {
            //TargetBox.Text = e.TargetEntry;
            //AdjustTargetCheck.Checked = e.TargetAdjust;
            //TargetRABox.Value = (decimal)e.TargetRA;
            //TargetDecBox.Value = (decimal)e.TargetDec;
            //TargetPABox.Value = (decimal)e.TargetPA;

            RegenerateSequence();
            UpdateFormFromPlan();

            this.Show();
            System.Windows.Forms.Application.DoEvents();
            return;
        }

        #endregion

        private void SaveDefaultButton_Click(object sender, EventArgs e)
        {
            //Saves the current active target file as the default file
            NHUtil.ButtonRed(SaveDefaultButton);
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            tPlan.SavePlanAsDefaultPlan();
            NHUtil.ButtonGreen(SaveDefaultButton);
        }


    }
}