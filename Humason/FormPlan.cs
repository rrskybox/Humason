using Planetarium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormPlan : Form
    {
        public List<string> Schedule = new List<string>();

        public FormPlan()
        {
            InitializeComponent();

            PlanTargetBox.Text = FormHumason.fTargetForm.TargetBox.Text;
            NHUtil.ButtonGreen(AcquireButton);
            NHUtil.ButtonGreen(SelectButton);
            NHUtil.ButtonGreen(AdjustButton);
            NHUtil.ButtonGreen(MosaicButton);
            NHUtil.ButtonGreen(LoadPlanButton);
            NHUtil.ButtonGreen(ImageForecastButton);
            LoadTargetPlanList();
        }

        private void PlanTargetBox_TextChanged(object sender, EventArgs e)
        {
            //Disconnect telescope so a new plan can be seen in star chart
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
        }

        private void FromTSXButton_Click(object sender, EventArgs e)
        {
            /* Method gets whatever target is showing in the find function in TSX
             * normally by using a click on a star chart object.  If no object has been
             * choosen (Mouse click position) or simply no target (null) then return
             * Otherwise, clear the spaces out of the name and check for characters (like ":")
             * that won't work in a file name.  If illegal characters, then return
             * Otherwise, find an existing or open new target plan for this target name
             * and make it the target plan for the current session
             */

            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            //get the current target name from TSX and save it.
            NHUtil.ButtonRed(AcquireButton);
            //Retrieve current target from TSX
            TSXLink.Target tgt = TSXLink.StarChart.FindTarget();
            //Check to see if we're looking at a "Mouse Click Postion"
            //   if so, just get out of this
            if ((tgt.Name != "Mouse click position") && (tgt != null))
            {
                //Remove spaces from target name if any
                tgt.Name = tgt.Name.Replace(" ", "");
                //Remove known "illegal" characters
                if (tgt.Name.Contains(":"))
                {
                    lg.LogIt("Unsupported filename characters in target name " + tgt.Name);
                    MessageBox.Show("Unsupported characters in target name : " + tgt.Name);
                }
                else
                {
                    TargetPlan newtPlan = new TargetPlan(tgt.Name)
                    {
                        TargetAdjustEnabled = false,
                        TargetRA = tgt.RA,
                        TargetDec = tgt.Dec
                    };
                    newtPlan.TargetPA = TSXLink.FOVI.GetFOVPA;
                    PlanTargetBox.Text = newtPlan.TargetName;
                    TSXLink.StarChart.SetFOV(2);
                    LoadTargetPlanList();
                    openSession.CurrentTargetName = newtPlan.TargetName;
                    lg.LogIt("A new target plan has been created for " + newtPlan.TargetName);
                }
            }
            NHUtil.ButtonGreen(AcquireButton);
            this.Show();
        }

        private void ToTSXButton_Click(object sender, EventArgs e)
        {
            //Tries to look up the name in the target box.  If found, then a new target plan is
            //opened.  Disconnect the telescope (in case centering is forced), use the target box to find 
            //and and center the star chart and FOV on the target.
            //If not throw a log entry and return;
            //Remove spaces from target name if any
            //PlanTargetBox.Text = PlanTargetBox.Text.Replace(" ", "");
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            NHUtil.ButtonRed(SelectButton);
            TSXLink.Target tgt = TSXLink.StarChart.FindTarget(PlanTargetBox.Text);
            if (tgt != null)
            {
                TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
                TargetPlan newtPlan = new TargetPlan(tgt.Name);
                newtPlan.TargetPA = TSXLink.FOVI.GetFOVPA;
                newtPlan.TargetAdjustEnabled = false;
                PlanTargetBox.Text = newtPlan.TargetName;
                TSXLink.StarChart.SetFOV(2);
                LoadTargetPlanList();
                openSession.CurrentTargetName = newtPlan.TargetName;
                lg.LogIt("A new target plan has been created for " + newtPlan.TargetName);
            }
            else
            {
                lg.LogIt(PlanTargetBox.Text + ": target not found.");
            }
            NHUtil.ButtonGreen(SelectButton);
            Show();
        }

        private void AdjustButton_Click(object sender, EventArgs e)
        {
            //Adjust button depends on prior state:
            //  if button green, then an adjustment of RA/Dec/PA is to be run
            //      Reset the tPlan name to the orginal name,
            //          and save tPlan RA/Dec/PA with those StarChart values (via TSXFind)
            //      wait until button is pushed again, essentially
            //  if button red, then an adjustment has been completed.
            //      Get the current StarChart values from TSX
            //          and saves tPlan RA/Dec/PA with those StarChart values.
            SessionControl openSession = new SessionControl();
            if (NHUtil.IsButtonGreen(AdjustButton))
            {
                NHUtil.ButtonRed(AdjustButton);
                this.AdjustButton.Text = "Save Adjustment";
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
                TSXLink.Target tgt = TSXLink.StarChart.FindTarget(tPlan.TargetName);
                double totalPA = AstroMath.Transform.NormalizeDegreeRange(Rotator.RealRotatorPA + TSXLink.FOVI.GetFOVPA);
                tPlan.TargetRA = tgt.RA;
                tPlan.TargetDec = tgt.Dec;
                tPlan.TargetPA = totalPA;
                tPlan.TargetAdjustEnabled = false;
                TSXLink.StarChart.SetFOV(2.0);
            }
            else
            {
                TSXLink tLink = new TSXLink();
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
                double totalPA = AstroMath.Transform.NormalizeDegreeRange(Rotator.RealRotatorPA + TSXLink.FOVI.GetFOVPA);
                tPlan.TargetRA = TSXLink.StarChart.ChartRA;
                tPlan.TargetDec = TSXLink.StarChart.ChartDec;
                tPlan.TargetPA = totalPA;
                tPlan.TargetAdjustEnabled = true;
                UpdateHumasonSequencer();
                NHUtil.ButtonGreen(AdjustButton);
                this.AdjustButton.Text = "Adjust Target";
            }
        }

        private void DeletePlanButton_Click(object sender, EventArgs e)
        {
            //Deletes the plan selected in the TargetListBox
            //Remove spaces from target name if any
            //PlanTargetBox.Text = PlanTargetBox.Text.Replace(" ", "");
            SessionControl openSession = new SessionControl();
            if (!(PlanListBox.SelectedItem == null))
            {
                string tname = PlanListBox.SelectedItem.ToString();
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
                tPlan.DeleteTargetPlan(tname);
                LoadTargetPlanList();
            }
        }

        private void MosaicButton_Click(object sender, EventArgs e)
        {
            //Reads and constructs mosaic entry as a set of target plans after a mosaic has
            //  been constructed and copied to the clipboard in TSX
            // 
            // Button will be held red until the user has prepared mosaic in TSX and
            //  copied to clipboard
            SessionControl openSession = new SessionControl();
            if (NHUtil.IsButtonGreen(MosaicButton))
            {
                //Verify that a target has been loaded, if not, post error and return
                //  otherwise, set the button color to read, change the text and zero the FOVI
                //  in anticipation of loading a mosaic target set
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
                if (TSXLink.StarChart.IsValidTarget(tPlan))
                {
                    NHUtil.ButtonRed(MosaicButton);
                    MosaicButton.Text = "Clipboard Ready";
                    TSXLink.FOVI.SetFOVPA(0);
                }
            }
            else
            {
                Mosaic nhms = new Mosaic();
                List<Mosaic.MosaicTarget> nhmtgts = nhms.ReadTSXMosaic();
                //Just return if no targets are found
                if (nhmtgts != null)
                {
                    //Save the mosaic entries as versions of the current configuration file
                    // For each of the entries in the mosaic,
                    //  Load the values into the configuration file
                    //  Save the configuration file with a prefix tName-Set-Frame
                    string tName = openSession.CurrentTargetName;
                    foreach (Mosaic.MosaicTarget mt in nhmtgts)
                    {
                        string prefixName = tName + "-" + mt.Set + "-" + mt.Frame;
                        TargetPlan tPlan = new TargetPlan(prefixName)
                        {
                            TargetName = prefixName,
                            TargetRA = mt.RA,
                            TargetDec = mt.Dec,
                            TargetPA = mt.PositionAngle,
                            TargetAdjustEnabled = true
                        };
                    }
                }
                LoadTargetPlanList();
                MosaicButton.Text = "Build Mosaic";
                NHUtil.ButtonGreen(MosaicButton);
            }
        }

        private void LoadPlanButton_Click(object sender, EventArgs e)
        {
            //Sets the active plan to the plan selected in the target list box
            NHUtil.ButtonRed(LoadPlanButton);
            if (!(PlanListBox.SelectedItem == null))
            {
                string tname = PlanListBox.SelectedItem.ToString();
                LoadNewTargetPlan(tname);
            }
            LoadTargetPlanList();
            NHUtil.ButtonGreen(LoadPlanButton);
        }

        private void PlanListBox_DoubleClick(object sender, EventArgs e)
        {
            //Loads the currently selected item
            LoadPlanButton_Click(sender, e);
        }

        private void DownScheduleButton_Click(object sender, EventArgs e)
        {
            PlanUpDownOnSchedule(1);
        }

        private void UpScheduleButton_Click(object sender, EventArgs e)
        {
            PlanUpDownOnSchedule(-1);
        }

        private void SchedulePlanButton_Click(object sender, EventArgs e)
        {
            //Get targetname from list, if any, and put in schedule queue
            if (!(PlanListBox.SelectedItem == null))
            { AddTargetPlanToSchedule(PlanListBox.SelectedItem.ToString()); }
        }

        private void UnschedulePlanButton_Click(object sender, EventArgs e)
        {
            //remove targetname from schedule list
            if (!(ScheduleListBox.SelectedItem == null))
            { ScheduleListBox.Items.Remove(ScheduleListBox.SelectedItem); }
        }

        private void ImageForecastButton_Click(object sender, EventArgs e)
        {
            //Launches ImageForecast to create target plans
            NHUtil.ButtonRed(ImageForecastButton);
            string toolName = "Image Planner.appref-ms";
            string ttdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\TSXToolkit\\TSXToolkit";
            Process pSystemExe = new Process();
            string ifbPath = ttdir + "\\" + toolName;
            pSystemExe.StartInfo.FileName = ifbPath;
            if (System.IO.File.Exists(ifbPath))
            { pSystemExe.Start(); }
            NHUtil.ButtonGreen(ImageForecastButton);
        }

        public bool IsTopPlanTargetName()
        {
            //Return true if there are any more scheduled plans
            if (ScheduleListBox.Items.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetTopPlanTargetName()
        {
            //Return name of topmost plan as configuration file
            if (ScheduleListBox.Items.Count > 0)
            {
                string tname = ScheduleListBox.Items[ScheduleListBox.TopIndex].ToString();
                return tname;
            }
            else { return null; }
        }

        public void RemoveTopPlan()
        {
            //Remove topmost plan as configuration file
            if (ScheduleListBox.Items.Count > 0)
            { ScheduleListBox.Items.Remove(ScheduleListBox.Items[ScheduleListBox.TopIndex]); }
        }

        public void LoadNewTargetPlan(string tname)
        {
            //sets up a target plan for the session. 
            //If that target plan file does not have enough entries (e.g. from Image Planner)
            //then merge the default target file into it.  Update the other forms with the new
            //target plan fields.  lthen reload the target plan list.

            SessionControl openSession = new SessionControl();
            openSession.CurrentTargetName = tname;
            TargetPlan tPlan = new TargetPlan(tname);
            if (tPlan.IsSparsePlan())
            { tPlan.FlushOutFromDefaultPlan(); }

            UpdateHumasonSequencer();
            try //If there are problems in the target plan file, this is where they show up
            {
                FormHumason.fDeviceForm.ResetConfiguration();
                FormHumason.fFocusForm.ResetConfiguration();
                FormHumason.fGuideForm.ResetConfiguration();
            }
            catch { } //ignore them
            PlanTargetBox.Text = tname;
            //Update the small solar system enabled field, if any
            SolarSystemBodyCheckBox.Checked = tPlan.SmallSolarSystemBodyEnabled;
            //Reload the target plan list
            LoadTargetPlanList();
        }

        private void LoadTargetPlanList()
        {
            //loads the list of target files into the the target list box
            PlanListBox.Items.Clear();
            SessionControl openSession = new SessionControl();
            List<string> tgtfiles = openSession.GetTargetFiles();
            foreach (string fn in tgtfiles)
            {
                if (fn.Split('.')[0] != "Default")
                {
                    PlanListBox.Items.Add(fn);
                }
            }
        }

        public void AddTargetPlanToSchedule(string targetName)
        {
            //Adds a configuration file to the plan list
            Schedule.Add(targetName);
            ScheduleListBox.Items.Add(targetName);
            Show();
        }

        private void PlanUpDownOnSchedule(int direction)
        {
            // Checking selected item
            if ((ScheduleListBox.SelectedItem != null) && (ScheduleListBox.SelectedIndex >= 0))
            {// Calculate new index using move direction
                int newIndex = ScheduleListBox.SelectedIndex + direction;

                // Checking bounds of the range
                if ((newIndex >= 0) && (newIndex < ScheduleListBox.Items.Count))
                {
                    object selected = ScheduleListBox.SelectedItem;
                    // Removing removable element
                    ScheduleListBox.Items.Remove(selected);
                    // Insert it in new position
                    ScheduleListBox.Items.Insert(newIndex, selected);
                    // Restore selection
                    ScheduleListBox.SetSelected(newIndex, true);
                    Show();
                }
            }
        }

        private void UpdateHumasonSequencer()
        {
            //Causes the sequencer form to be updated with new values
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            //Raise target event so target sequence form can update its fields accordingly
            TargetEvent reTarget = FormTarget.targetreset;
            reTarget.TargetEntry(tPlan.TargetName);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            //Reloads the target plan list
            LoadTargetPlanList();
            Show();
        }

        private void SolarSystemBodyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!FormHumason.InitializingHumason)
            {
                SessionControl openSession = new SessionControl();
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
                {
                    SmallSolarSystemBodyEnabled = SolarSystemBodyCheckBox.Checked
                };
                //If this is getting checked, then acquire the deltaRA and deltaDec rates
                //  Find target name, then get rates
                TSXLink.Target tsxtgt = TSXLink.StarChart.FindTarget(openSession.CurrentTargetName);
                tPlan.DeltaRARate = tsxtgt.DeltaRARate;
                tPlan.DeltaDecRate = tsxtgt.DeltaDecRate;
            }
        }
    }
}
