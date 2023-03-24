using System;
using System.Windows.Forms;
using WeatherWatch;

namespace Humason
{
    public partial class FormOptions : Form
    {

        private Properties.Settings settings;
        private bool optionsFormInit = false;

        public FormOptions()

        {
            optionsFormInit = true;
            InitializeComponent();
            settings = new Properties.Settings();
            //Fill in checkboxes with existing settings
            RotatorCheckBox.Checked = settings.RotatorDeviceEnabled; ;
            WeatherCheckBox.Checked = settings.WeatherMonitorEnabled;
            DomeAddOnCheckBox.Checked = settings.HasDomeAddOn;
            //Fill in calibrations with existing settings
            SessionControl openSession = new SessionControl();
            ImageReductionComboBox.SelectedIndex = openSession.ImageReductionType;
            FocusReductionComboBox.SelectedIndex = openSession.FocusReductionType;
            GuiderReductionComboBox.SelectedIndex = openSession.GuiderReductionType;
            CLSReductionComboBox.SelectedIndex = openSession.CLSReductionType;
            UseTSXAutoSaveCheckbox.Checked = Convert.ToBoolean(openSession.UseTSXAutoSave);
            NoFilterWheelCheckBox.Checked = Convert.ToBoolean(openSession.NoFilterWheel);
            //done
            optionsFormInit = false;
            return;
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsWeatherEnabled = WeatherCheckBox.Checked;
            openSession.IsRotationEnabled = RotatorCheckBox.Checked;
            openSession.IsDomeAddOnEnabled = DomeAddOnCheckBox.Checked;
            openSession.NoFilterWheel = NoFilterWheelCheckBox.Checked;
            openSession.ImageReductionType = ImageReductionComboBox.SelectedIndex;
            openSession.CLSReductionType = CLSReductionComboBox.SelectedIndex;
            settings.Save();
            Close();
        }

        private void Rotator_CheckedChanged(object sender, System.EventArgs e)
        {
            //Record the rotator device control selection in the session control file
            //  and to the settings
            SessionControl openSession = new SessionControl();
            openSession.IsRotationEnabled = RotatorCheckBox.Checked;
            settings.RotatorDeviceEnabled = RotatorCheckBox.Checked;
            if (!optionsFormInit)
                MessageBox.Show("Restart Humason for new settings to take effect");
        }

        private void WeatherCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            //If WeatherCheck is checked, now, then open the file dialog to pick up
            //  the location of the weather data file, then store it
            SessionControl openSession = new SessionControl();
            if (WeatherCheckBox.Checked && (!optionsFormInit))
            {
                DialogResult weatherFilePath = WeatherFileDialog.ShowDialog();
                openSession.WeatherDataFilePath = WeatherFileDialog.FileName;
            }
            //Check to see if the Weather file is valid
            WeatherReader wrf = new WeatherReader(openSession.WeatherDataFilePath);
            if (wrf.IsWeatherValid())
            {
                openSession.IsWeatherEnabled = WeatherCheckBox.Checked;
                settings.WeatherMonitorEnabled = WeatherCheckBox.Checked;
                if (!optionsFormInit)
                    MessageBox.Show("Restart Humason for new settings to take effect");
            }
            else
            {
                if (!optionsFormInit)
                    MessageBox.Show("Invalid Weather Data File");
                openSession.IsWeatherEnabled = false;
                settings.WeatherMonitorEnabled = false;
                WeatherCheckBox.Checked = false;
            }
        }

        private void DomeAddOnCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsDomeAddOnEnabled = DomeAddOnCheckBox.Checked;
            settings.HasDomeAddOn = DomeAddOnCheckBox.Checked;
            if (!optionsFormInit)
                MessageBox.Show("Restart Humason for new settings to take effect");
            return;
        }

        private void NoFilterWheelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.NoFilterWheel = NoFilterWheelCheckBox.Checked;
            settings.NoFilterWheel = NoFilterWheelCheckBox.Checked;
            if (!optionsFormInit)
                MessageBox.Show("Restart Humason for new settings to take effect");
            return;
        }

        private void ImageReductionComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.ImageReductionType = ImageReductionComboBox.SelectedIndex;
            return;
        }

        private void FocusReductionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.FocusReductionType = FocusReductionComboBox.SelectedIndex;
            return;
        }

        private void CLSReductionComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.CLSReductionType = CLSReductionComboBox.SelectedIndex;
            return;
        }

        private void GuiderReductionComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.GuiderReductionType = GuiderReductionComboBox.SelectedIndex;
            return;
        }

        private void UseTSXAutoSaveCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            if (UseTSXAutoSaveCheckbox.Checked)
                openSession.UseTSXAutoSave = 1;
            else
                openSession.UseTSXAutoSave = 0;
            return;
        }



    }
}
