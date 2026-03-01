using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeatherWatch;

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
            if (oldFilters != null)
                foreach (Filter fltr in oldFilters)
                    tPlan.SetFilter(fltr, false);
            //Generate a list of filters from tsx or mdl, in index order
            List<string> fwlist = new List<string>();
            fwlist = TSXLink.FilterWheel.FilterWheelList();

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
                RefocusIntervalBox.Value = openSession.RefocusAfterInterval;
                RefocusTriggerBox.SelectedItem = openSession.RefocusTriggerType;    
                UseRotatorCheckBox.Checked = tPlan.RotatorEnabled;
                RecalibrateAfterFlipCheckbox.Checked = tPlan.RecalibrateAfterFlipEnabled;
                DitherCheck.Checked = tPlan.DitherEnabled;
                GuiderCalibrateCheck.Checked = tPlan.GuiderCalibrateEnabled;
                ResyncCheck.Checked = tPlan.ResyncEnabled;
                ResyncPeriodBox.Value = tPlan.ResyncPeriod;
                //Pre set clear and filter numbers from configuration file
                CLSFilterNum.Value = tPlan.CLSFilter;
                LumFilterNum.Value = tPlan.LumFilter;
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
                //Fill in checkboxes with existing settings
                HasRotatorCheckBox.Checked = Convert.ToBoolean(openSession.HasRotator);
                HasWeatherCheckBox.Checked = Convert.ToBoolean(openSession.HasWeather);
                HasDomeCheckBox.Checked = Convert.ToBoolean(openSession.HasDome);
                NoFilterWheelCheckBox.Checked = Convert.ToBoolean(openSession.NoFilterWheel);
                return;
            }
        }

        public void UploadDevicesConfiguration()
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                AutoGuideEnabled = AutoguideCheck.Checked,
                RotatorEnabled = UseRotatorCheckBox.Checked,
                DitherEnabled = DitherCheck.Checked,
                AutoFocusEnabled = AutofocusCheck.Checked,
                GuiderCalibrateEnabled = GuiderCalibrateCheck.Checked,
                ResyncEnabled = ResyncCheck.Checked,
                ResyncPeriod = (int)ResyncPeriodBox.Value,
                //CameraTemperatureSet = (double)CameraTemperatureSet.Value
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

        #region CheckboxChanged

        private void FocusFilterNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (FilterListBox.Items.Count == 0)
                return;
            if (FocusFilterNum.Value >= FilterListBox.Items.Count)
                FocusFilterNum.Value = Math.Max(FilterListBox.Items.Count - 1, 0);
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                FocusFilter = (int)FocusFilterNum.Value
            };
        }

        private void CLSFilterNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (FilterListBox.Items.Count == 0)
                return;
            if (CLSFilterNum.Value >= FilterListBox.Items.Count)
                CLSFilterNum.Value = Math.Max(FilterListBox.Items.Count - 1, 0);
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                CLSFilter = (int)CLSFilterNum.Value
            };
        }

        private void LumFilterNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (FilterListBox.Items.Count == 0)
                return;
            if (LumFilterNum.Value >= FilterListBox.Items.Count)
                LumFilterNum.Value = Math.Max(FilterListBox.Items.Count - 1, 0);
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                LumFilter = (int)LumFilterNum.Value
            };
        }

        private void Rotator_CheckedChanged(object sender, System.EventArgs e)
        {
            //Record the rotator device control selection in the session control file
            //  and to the settings
            SessionControl openSession = new SessionControl();
            openSession.IsRotationEnabled = UseRotatorCheckBox.Checked;
            return;
        }

        private void DomeAddOnCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.HasDome = HasDomeCheckBox.Checked;
            return;
        }

        private void NoFilterWheelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.NoFilterWheel = NoFilterWheelCheckBox.Checked;
        }

        private void WeatherCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            //If WeatherCheck is checked, now, then open the file dialog to pick up
            //  the location of the weather data file, then store it
            SessionControl openSession = new SessionControl();
            if (HasWeatherCheckBox.Checked)
            {
                DialogResult weatherFilePath = WeatherFileDialog.ShowDialog();
                openSession.WeatherDataFilePath = WeatherFileDialog.FileName;
            }
            //Check to see if the Weather file is valid
            WeatherReader wrf = new WeatherReader(openSession.WeatherDataFilePath);
            if (wrf.IsWeatherValid())
            {
                openSession.IsWeatherEnabled = HasWeatherCheckBox.Checked;
            }
            else
            {
                MessageBox.Show("Invalid Weather Data File");
                openSession.IsWeatherEnabled = false;
                HasWeatherCheckBox.Checked = false;
            }
            return;
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

        private void HasRotatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.HasRotator = HasRotatorCheckBox.Checked;
            return;
        }

        private void RotatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (openSession.HasRotator)
            {
                openSession.IsRotationEnabled = true;
                tPlan.RotatorEnabled = UseRotatorCheckBox.Checked;
            }
            else
            {
                openSession.IsRotationEnabled = false;
                UseRotatorCheckBox.Checked = false;
                tPlan.RotatorEnabled = false;
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
                private void RefocusIntervalBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl
            {
                RefocusAfterInterval = (int)RefocusIntervalBox.Value
            };

        }

        private void NoFilterWheelCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.NoFilterWheel = NoFilterWheelCheckBox.Checked;
            return;
        }

        private void HasWeatherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.HasWeather = HasWeatherCheckBox.Checked;
            return;

        }

        private void HasDomeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.HasDome = HasDomeCheckBox.Checked;
            return;

        }

        #endregion

        private void ResyncPeriodBox_ValueChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            _ = new TargetPlan(openSession.CurrentTargetName)
            {
                ResyncPeriod = (int)ResyncPeriodBox.Value
            };

        }

        private void RefocusTriggerBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            openSession.RefocusTriggerType = RefocusTriggerBox.SelectedItem.ToString();
        }
    }
}

