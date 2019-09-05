using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormFlats : Form
    {
        public FormFlats()
        {
            InitializeComponent();

            FlatMan flmn = new FlatMan();
            if (FormHumason.openSession.FlatsTargetADU != 0) { FlatsTargetADU.Value = FormHumason.openSession.FlatsTargetADU; }
            else { FormHumason.openSession.FlatsTargetADU = (int)FlatsTargetADU.Value; }
            if (FormHumason.openSession.FlatsRepetitions != 0) { FlatsRepetitionsBox.Value = FormHumason.openSession.FlatsRepetitions; }
            else { FormHumason.openSession.FlatsRepetitions = (int)FlatsRepetitionsBox.Value; }
            if (FormHumason.openSession.FlatManBrightness != 0) { FlatManBrightnessNum.Value = FormHumason.openSession.FlatManBrightness; }
            else { FormHumason.openSession.FlatManBrightness = (int)FlatManBrightnessNum.Value; }
            if (FormHumason.openSession.FlatsExposureTime != 0) { FlatManExposureNum.Value = (decimal)FormHumason.openSession.FlatsExposureTime; }
            else { FormHumason.openSession.FlatsExposureTime = (int)FlatManExposureNum.Value; }
            if (FormHumason.openSession.IsFlatFlipEnabled) { FlatFlipCheckBox.Checked = FormHumason.openSession.IsFlatFlipEnabled; }
            else { FormHumason.openSession.IsFlatFlipEnabled = FlatFlipCheckBox.Checked; }
            if (FormHumason.openSession.IsFlatManEast) { FlatManEastCheckBox.Checked = FormHumason.openSession.IsFlatManEast; }
            else { FormHumason.openSession.IsFlatManEast = FlatManEastCheckBox.Checked; }
            if (FormHumason.openSession.IsFlatsRotationEnabled) { FlatsRotationCheckBox.Checked = FormHumason.openSession.IsFlatsRotationEnabled; }
            else { FormHumason.openSession.IsFlatsRotationEnabled = FlatsRotationCheckBox.Checked; }
            if (FormHumason.openSession.IsFlatManManualSetupEnabled) { FlatManManualSetupCheckbox.Checked = FormHumason.openSession.IsFlatManManualSetupEnabled; }
            else { FormHumason.openSession.IsFlatManManualSetupEnabled = FlatManManualSetupCheckbox.Checked; }

            FlatManager.LightSource flatLightSource = FormHumason.openSession.FlatLightSource;
            // FlatManager.LightSource flatLightSource = (FlatManager.LightSource)Enum.Parse(typeof(FlatManager.LightSource), sflatLightSource);
            switch (flatLightSource)
            {
                case (FlatManager.LightSource.lsNone): { FlatManRadioButton.Checked = false; break; }
                case (FlatManager.LightSource.lsFlatMan): { FlatManRadioButton.Checked = true; break; }
                case (FlatManager.LightSource.lsDawn): { DawnRadioButton.Checked = true; break; }
                case (FlatManager.LightSource.lsDusk): { DuskRadioButton.Checked = true; break; }
                default: { FlatManRadioButton.Checked = false; break; }
            }

            FlatManPortNum.Value = FormHumason.openSession.FlatManComPort;

            //If FlatMan has been chosen for flats, make sure the panel is turned off
            if (FormHumason.openSession.IsFlatManEnabled)
            { flmn.Light = false; }

            //Paint buttons green
            NHUtil.ButtonGreen(TakeFlatsButton);
            NHUtil.ButtonGreen(FlatManOnButton);
            NHUtil.ButtonGreen(FlatManStageButton);
            NHUtil.ButtonGreen(MakeFlatsButton);
            NHUtil.ButtonGreen(ClearFlatsButton);
            return;
        }

        private void TakeFlatsButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(TakeFlatsButton);
            FlatManager fmgr = new FlatManager();
            fmgr.TakeFlats();
            FlatManBrightnessNum.Value = FormHumason.openSession.FlatManBrightness;
            NHUtil.ButtonGreen(TakeFlatsButton);
            return;
        }

        private void FlatManStageButton_Click(object sender, EventArgs e)
        {
            FlatMan flmn = new FlatMan();
            NHUtil.ButtonRed(FlatManStageButton);
            flmn.FlatManStage();
            NHUtil.ButtonGreen(FlatManStageButton);
            return;
        }

        private void FlatManOnButton_Click(object sender, EventArgs e)
        {
            SessionControl tplan = FormHumason.openSession;
            if (NHUtil.IsButtonGreen(FlatManOnButton))
            {
                FlatMan flmn = new FlatMan();
                NHUtil.ButtonRed(FlatManOnButton);
                FlatManOnButton.Text = "Turn Off";
                flmn.Light = true;
            }
            else
            {
                NHUtil.ButtonGreen(FlatManOnButton);
                FlatManOnButton.Text = "Turn On";
                FlatMan flmn = new FlatMan();
                flmn.Light = false;
            }
        }

        private void FlatManBrightnessNum_ValueChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.FlatManBrightness = (int)FlatManBrightnessNum.Value;
        }

        private void FlatManExposureNum_ValueChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.FlatsExposureTime = (int)FlatManExposureNum.Value;
        }

        private void FlatsTargetADU_ValueChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.FlatsTargetADU = (int)FlatsTargetADU.Value;
        }

        private void FlatsRepetitionsBox_ValueChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.FlatsRepetitions = (int)FlatsRepetitionsBox.Value;
        }

        private void FlatManEastCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.IsFlatManEast = FlatManEastCheckBox.Checked;
        }

        private void FlatFlipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.IsFlatFlipEnabled = FlatFlipCheckBox.Checked;
        }

        private void FlatManPortNum_ValueChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.FlatManComPort = (int)FlatManPortNum.Value;
        }

        private void FlatManCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Update the configuration file when this button changes
            if (FlatManRadioButton.Checked)
                FormHumason.openSession.FlatLightSource = FlatManager.LightSource.lsFlatMan;

            return;
        }

        private void DawnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Update the configuration file when this button changes
            //Update the configuration file when this button changes
            if (DawnRadioButton.Checked)
                FormHumason.openSession.FlatLightSource = FlatManager.LightSource.lsDawn;
        }

        private void DuskRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Update the configuration file when this button changes
            //Update the configuration file when this button changes
            if (DuskRadioButton.Checked)
                FormHumason.openSession.FlatLightSource = FlatManager.LightSource.lsDusk;
        }

        private void MakeFlatsButton_Click(object sender, EventArgs e)
        {
            //Make flat request entries based on current target name and rotator info
            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            FlatManager nhFlat = new FlatManager();

            //Get filter set
            List<Filter> fset = tPlan.FilterWheelList;
            if (fset == null)
            {
                lg.LogIt("No filters have been configured");
                return;
            }
            string tname = tPlan.TargetName;
            double rPA = (double)RotatorPANum.Value;
            int flatRepetitions = FormHumason.openSession.FlatsRepetitions;
            //If the last image was shot to the west, then the rotator should alread be in the west position,
            //  which will mean that a single set will have the rotator already positioned collectly
            string sop = "East";
            foreach (Filter fi in fset)
            {
                fi.Repeat = flatRepetitions;
                Flat iFlat = new Flat(tname, sop, rPA, fi, FormHumason.openSession.IsFlatFlipEnabled);
                nhFlat.AddFlat(iFlat);
            }
            //Check to see if there is a rotator enabled, if so, and flip is enabled, then make a second set for the east side
            if ((FormHumason.openSession.IsFlatsRotationEnabled) && (FormHumason.openSession.IsFlatFlipEnabled))
            {
                rPA = NHUtil.ReduceTo360(rPA + 180);
                sop = "West";
                foreach (Filter fi in fset)
                {
                    fi.Repeat = flatRepetitions;
                    Flat iFlat = new Flat(tname, sop, rPA, fi, FormHumason.openSession.IsFlatFlipEnabled);
                    nhFlat.AddFlat(iFlat);
                }
            }
        }

        private void ClearFlatsButton_Click(object sender, EventArgs e)
        {
            //Remove all flats from the configuration file
            FlatManager nhFlat = new FlatManager();
            nhFlat.FlatSetClearAll();
        }

        private void FlatsRotationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.IsFlatsRotationEnabled = FlatsRotationCheckBox.Checked;
        }

        private void FlatManManualSetupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            FormHumason.openSession.IsFlatManManualSetupEnabled = FlatManManualSetupCheckbox.Checked;

        }
    }
}
