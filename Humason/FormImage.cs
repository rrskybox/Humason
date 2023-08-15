using System;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormImage : Form
    {

        public FormImage()

        {
            InitializeComponent();
            //Fill in calibrations with existing settings
            SessionControl openSession = new SessionControl();
            ImageReductionComboBox.SelectedIndex = openSession.ImageReductionType;
            FocusReductionComboBox.SelectedIndex = openSession.FocusReductionType;
            GuiderReductionComboBox.SelectedIndex = openSession.GuiderReductionType;
            CLSReductionComboBox.SelectedIndex = openSession.CLSReductionType;
            UseTSXAutoSaveCheckbox.Checked = Convert.ToBoolean(openSession.UseTSXAutoSave);
            CameraTemperatureSet.Value = Convert.ToDecimal(openSession.CameraTemperatureSet);
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

        private void CameraTemperatureSet_ValueChanged_1(object sender, EventArgs e)
        {
            //Store it in the configuration and move on
            SessionControl openSession = new SessionControl();
            openSession.CameraTemperatureSet = (int)CameraTemperatureSet.Value;
            return;

        }
    }
}
