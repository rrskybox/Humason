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
            //done

            optionsFormInit = false;
            return;
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            FormHumason.openSession.IsWeatherEnabled = WeatherCheckBox.Checked;
            FormHumason.openSession.IsRotationEnabled = RotatorCheckBox.Checked;
            FormHumason.openSession.IsDomeAddOnEnabled = DomeAddOnCheckBox.Checked;
            MessageBox.Show("Restart Humason for new settings to take effect");
            settings.Save();
            Close();
        }

        private void Rotator_CheckedChanged(object sender, System.EventArgs e)
        {
            //Record the rotator device control selection in the session control file
            //  and to the settings
            FormHumason.openSession.IsRotationEnabled = RotatorCheckBox.Checked;
            settings.RotatorDeviceEnabled = RotatorCheckBox.Checked;
        }

        private void WeatherCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            //If WeatherCheck is checked, now, then open the file dialog to pick up
            //  the location of the weather data file, then store it
            if (WeatherCheckBox.Checked && (!optionsFormInit))
            {
                DialogResult weatherFilePath = WeatherFileDialog.ShowDialog();
                FormHumason.openSession.WeatherDataFilePath = WeatherFileDialog.FileName;
            }
            //Check to see if the Weather file is valid
            WeatherReader wrf = new WeatherReader(FormHumason.openSession.WeatherDataFilePath);
            if (wrf.IsWeatherValid())
            {
                FormHumason.openSession.IsWeatherEnabled = WeatherCheckBox.Checked;
                settings.WeatherMonitorEnabled = WeatherCheckBox.Checked;
            }
            else
            {
                MessageBox.Show("Invalid Weather Data File");
                FormHumason.openSession.IsWeatherEnabled = false;
                settings.WeatherMonitorEnabled = false;
                WeatherCheckBox.Checked = false;
            }
        }

        private void DomeAddOnCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            FormHumason.openSession.IsDomeAddOnEnabled = DomeAddOnCheckBox.Checked;
            settings.HasDomeAddOn = DomeAddOnCheckBox.Checked;
            return;
        }
    }
}
