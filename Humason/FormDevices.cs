using Planetarium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormDevices : Form
    {

        public bool IsInitializing = false;
        public FormDevices()
        {
            IsInitializing = true;
            InitializeComponent();
            //Populate entries with stored entries, if any
            ResetConfiguration();
            IsInitializing = false;
            NHUtil.ButtonGreen(RefreshFiltersButton);
        }

        public void RefreshFilterList()
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            //Read the filter list in from TSX -- maximum 24 for no good reason
            //the section will be SU-Filter, 
            //the entry name will be the filter name
            //the entry will be the filter index
            //
            //For initalization, if the filter is already configured, then
            //  it is assumed active and the index will be set.
            //if the filter is not there then the filter will not be added to the list.
            //
            //

            //Clear the current filter list box
            FilterListBox.Items.Clear();
            //Clear the filter set from the current target plan
            List<Filter> oldFilters = tPlan.FilterWheelList;
            foreach (Filter fltr in oldFilters)
                tPlan.SetFilter(fltr, false);
            //Generate a list of filters from tsx, in index order
            List<string> fwlist = TSXLink.FilterWheel.FilterWheelList();
            if (fwlist == null)
                return;
            List<Filter> tsxFilterList = new List<Filter>();
            if (fwlist.Count > 0)
            {
                for (int filterIndex = 0; filterIndex < fwlist.Count(); filterIndex++)
                { tsxFilterList.Add(new Filter(fwlist[filterIndex], filterIndex, 1)); }
                //For each filter from tsx, add it to the list, and set it checked, if it is also in the configuration file
                foreach (Filter tsxFilter in tsxFilterList)
                {
                    if (!tsxFilter.Name.StartsWith("Filter") && !(tsxFilter.Name == ""))  //weed out unnamed filter entries
                    {
                        //check the target plan this filter name, if found, then add the filter to the box and check it
                        if (tPlan.CheckFilter(tsxFilter))
                        { FilterListBox.Items.Add(tsxFilter.Name.Replace(" ", "") + "-" + tsxFilter.Index, true); }
                        else
                        { FilterListBox.Items.Add(tsxFilter.Name.Replace(" ", "") + "-" + tsxFilter.Index, false); }
                    }
                }
            }
        }

        private void FocusFilterNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                FocusFilter = (int)FocusFilterNum.Value
            };
        }

        private void ClearFilterNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                ClearFilter = (int)ClearFilterNum.Value
            };
        }

        public void ResetConfiguration()
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (tPlan.TargetPlanPath != null)
            {
                AutoguideCheck.Checked = tPlan.AutoGuideEnabled;
                AutofocusCheck.Checked = tPlan.AutoFocusEnabled;
                int atfocusPick = tPlan.AtFocusSelect;
                if (atfocusPick == 3)
                { AtFocus3RadioButton.Checked = true; }
                else
                { AtFocus2RadioButton.Checked = true; }
                RefocustTemperatureChangeBox.Value = (decimal)openSession.RefocusAtTemperatureDifference;
                RotatorCheckBox.Checked = tPlan.RotatorEnabled;
                RecalibrateAfterFlipCheckbox.Checked = tPlan.RecalibrateAfterFlipEnabled;
                DitherCheck.Checked = tPlan.DitherEnabled;
                GuiderCalibrateCheck.Checked = tPlan.GuiderCalibrateEnabled;
                ResyncCheck.Checked = tPlan.ResyncEnabled;
                CameraTemperatureSet.Value = (decimal)tPlan.CameraTemperatureSet;
                //Pre set clear and filter numbers from configuration file
                ClearFilterNum.Value = tPlan.ClearFilter;
                FocusFilterNum.Value = tPlan.FocusFilter;
                //Clear the filter list
                FilterListBox.Items.Clear();
                //Read in configured filter set -- do not try pulling them from TSX until camera can connect (i.e. after power on)
                List<Filter> fset = tPlan.FilterWheelList;
                if (fset != null)
                {
                    foreach (Filter filter in fset)
                    {
                        //Add the filter to the filter list
                        FilterListBox.Items.Add(filter.Name + "-" + filter.Index, true);
                    }
                }
            }
        }

        public void UploadDevicesConfiguration()
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                AutoGuideEnabled = AutoguideCheck.Checked,
                RotatorEnabled = RotatorCheckBox.Checked,
                DitherEnabled = DitherCheck.Checked,
                AutoFocusEnabled = AutofocusCheck.Checked,
                GuiderCalibrateEnabled = GuiderCalibrateCheck.Checked,
                ResyncEnabled = ResyncCheck.Checked,
                CameraTemperatureSet = (double)CameraTemperatureSet.Value
            };
            openSession.RefocusAtTemperatureDifference = (double)RefocustTemperatureChangeBox.Value;
            if (!IsInitializing)
            {
                if (AtFocus2RadioButton.Checked)
                {
                    tPlan.AtFocusSelect = 2;
                }
                else
                {
                    tPlan.AtFocusSelect = 3;
                }
            }
        }

        private void FilterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Handles picking filters out of filter list
            //  Toggle the checked status then save it to the configuration list accordingly
            //  
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            string flistname = FilterListBox.GetItemText(FilterListBox.SelectedItem);
            string fname = (flistname.Split('-')[0]);
            int findex = Convert.ToInt32(flistname.Split('-')[1]);
            Filter fobj = new Filter(fname, findex, 1);
            //Get the current filter list
            //Add to configuration, assuming that it wasn't already

            if (FilterListBox.GetItemChecked(FilterListBox.SelectedIndex))
            { tPlan.SetFilter(fobj, true); }
            else
            { tPlan.SetFilter(fobj, false); }
        }

        private void AutoguideCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                AutoGuideEnabled = AutoguideCheck.Checked
            };
        }

        private void AutofocusCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                AutoFocusEnabled = AutofocusCheck.Checked
            };
        }

        private void RotatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (openSession.IsRotationEnabled)
            {
                tPlan.RotatorEnabled = RotatorCheckBox.Checked;
            }
            else
            {
                RotatorCheckBox.Checked = false;
            }
        }

        private void RecalibrateAfterFlipCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the session configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (openSession.IsRotationEnabled)
            {
                tPlan.RecalibrateAfterFlipEnabled = RecalibrateAfterFlipCheckbox.Checked;
            }
            else
            {
                RecalibrateAfterFlipCheckbox.Checked = false;
            }
        }

        private void CameraTemperatureSet_ValueChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                CameraTemperatureSet = (double)CameraTemperatureSet.Value
            };
        }

        private void AtFocus2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Update the configuration file when this button changes
            if (!IsInitializing)
            {
                SessionControl openSession = new SessionControl();
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
                if (AtFocus2RadioButton.Checked)
                {
                    tPlan.AtFocusSelect = 2;
                }
            }
        }

        private void AtFocus3RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Update the configuration file when this button changes
            if (!IsInitializing)
            {
                SessionControl openSession = new SessionControl();
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
                if (AtFocus3RadioButton.Checked)
                {
                    tPlan.AtFocusSelect = 3;
                }
            }
        }

        private void DitherCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                DitherEnabled = DitherCheck.Checked
            };
        }

        private void RefreshFiltersButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(RefreshFiltersButton);
            RefreshFilterList();
            NHUtil.ButtonGreen(RefreshFiltersButton);
        }

        private void CalibrateCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                GuiderCalibrateEnabled = GuiderCalibrateCheck.Checked
            };
        }

        private void ResyncCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            _ = new TargetPlan(openSession.CurrentTargetName)
            {
                ResyncEnabled = ResyncCheck.Checked
            };
        }

        private void RefocusTemperatureChangeBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl
            {
                RefocusAtTemperatureDifference = (double)RefocustTemperatureChangeBox.Value
            };
        }

    }
}

