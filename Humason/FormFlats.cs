using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormFlats : Form
    {

        private FlatManager fSuper;

        public FormFlats()
        {
            InitializeComponent();

            SessionControl openSession = new SessionControl();
            if (openSession.FlatsTargetADU != 0) { FlatsTargetADU.Value = openSession.FlatsTargetADU; }
            else { openSession.FlatsTargetADU = (int)FlatsTargetADU.Value; }
            if (openSession.FlatsRepetitions != 0) { FlatsRepetitionsBox.Value = openSession.FlatsRepetitions; }
            else { openSession.FlatsRepetitions = (int)FlatsRepetitionsBox.Value; }
            if (openSession.FlatManBrightness != 0) { FlatManBrightnessNum.Value = openSession.FlatManBrightness; }
            else { openSession.FlatManBrightness = (int)FlatManBrightnessNum.Value; }
            if (openSession.FlatsExposureTime != 0) { FlatManExposureNum.Value = (decimal)openSession.FlatsExposureTime; }
            else { openSession.FlatsExposureTime = (int)FlatManExposureNum.Value; }
            if (openSession.IsFlatFlipEnabled) { FlatFlipCheckBox.Checked = openSession.IsFlatFlipEnabled; }
            else { openSession.IsFlatFlipEnabled = FlatFlipCheckBox.Checked; }
            if (openSession.IsFlatManEast) { FlatManEastCheckBox.Checked = openSession.IsFlatManEast; }
            else { openSession.IsFlatManEast = FlatManEastCheckBox.Checked; }
            if (openSession.IsFlatsRotationEnabled) { FlatsRotationCheckBox.Checked = openSession.IsFlatsRotationEnabled; }
            else { openSession.IsFlatsRotationEnabled = FlatsRotationCheckBox.Checked; }
            if (openSession.IsFlatManManualSetupEnabled) { FlatManManualSetupCheckbox.Checked = openSession.IsFlatManManualSetupEnabled; }
            else { openSession.IsFlatManManualSetupEnabled = FlatManManualSetupCheckbox.Checked; }

            FlatManager.LightSource flatLightSource = openSession.FlatLightSource;
            // FlatManager.LightSource flatLightSource = (FlatManager.LightSource)Enum.Parse(typeof(FlatManager.LightSource), sflatLightSource);
            switch (flatLightSource)
            {
                case (FlatManager.LightSource.lsNone): { FlatManRadioButton.Checked = false; break; }
                case (FlatManager.LightSource.lsFlatMan): { FlatManRadioButton.Checked = true; break; }
                case (FlatManager.LightSource.lsDawn): { DawnRadioButton.Checked = true; break; }
                case (FlatManager.LightSource.lsDusk): { DuskRadioButton.Checked = true; break; }
                default: { FlatManRadioButton.Checked = false; break; }
            }

            FlatManPortNum.Value = openSession.FlatManComPort;

            //If FlatMan has been chosen for flats, make sure the panel is turned off
            //if (openSession.IsFlatManEnabled)
            //fMan.Light = false; }

            //Paint buttons green
            NHUtil.ButtonGreen(TakeFlatsButton);
            NHUtil.ButtonGreen(FlatManStageButton);
            NHUtil.ButtonGreen(MakeFlatsButton);
            NHUtil.ButtonGreen(ClearFlatsButton);

            fSuper = new FlatManager();

            return;
        }

        private void TakeFlatsButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(TakeFlatsButton);
            SessionControl openSession = new SessionControl();
            fSuper.TakeFlats();
            FlatManBrightnessNum.Value = openSession.FlatManBrightness;
            NHUtil.ButtonGreen(TakeFlatsButton);
            return;
        }

        private void FlatManStageButton_Click(object sender, EventArgs e)
        {

            NHUtil.ButtonRed(FlatManStageButton);
            fSuper.FlatManStage();
            NHUtil.ButtonGreen(FlatManStageButton);
            return;
        }



        private void FlatManBrightnessNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.FlatManBrightness = (int)FlatManBrightnessNum.Value;
        }

        private void FlatManExposureNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.FlatsExposureTime = (int)FlatManExposureNum.Value;
        }

        private void FlatsTargetADU_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.FlatsTargetADU = (int)FlatsTargetADU.Value;
        }

        private void FlatsRepetitionsBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.FlatsRepetitions = (int)FlatsRepetitionsBox.Value;
        }

        private void FlatManEastCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsFlatManEast = FlatManEastCheckBox.Checked;
        }

        private void FlatFlipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsFlatFlipEnabled = FlatFlipCheckBox.Checked;
        }

        private void FlatManPortNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.FlatManComPort = (int)FlatManPortNum.Value;
        }

        private void FlatManCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            //Update the configuration file when this button changes
            if (FlatManRadioButton.Checked)
            {
                openSession.FlatLightSource = FlatManager.LightSource.lsFlatMan;
            }

            return;
        }

        private void DawnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Update the configuration file when this button changes
            //Update the configuration file when this button changes
            SessionControl openSession = new SessionControl();
            if (DawnRadioButton.Checked)
            {
                openSession.FlatLightSource = FlatManager.LightSource.lsDawn;
            }
        }

        private void DuskRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Update the configuration file when this button changes
            //Update the configuration file when this button changes
            SessionControl openSession = new SessionControl();
            if (DuskRadioButton.Checked)
            {
                openSession.FlatLightSource = FlatManager.LightSource.lsDusk;
            }
        }

        private void MakeFlatsButton_Click(object sender, EventArgs e)
        {
            //Make flat request entries based on current target name and rotator info
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
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
            int flatRepetitions = openSession.FlatsRepetitions;
            //If the last image was shot to the west, then the rotator should alread be in the west position,
            //  which will mean that a single set will have the rotator already positioned collectly
            string sop = "East";
            foreach (Filter fi in fset)
            {
                fi.Repeat = flatRepetitions;
                Flat iFlat = new Flat(tname, sop, rPA, fi, openSession.IsFlatFlipEnabled);
                nhFlat.AddFlat(iFlat);
            }
            //Check to see if there is a rotator enabled, if so, and flip is enabled, then make a second set for the east side
            if ((openSession.IsFlatsRotationEnabled) && (openSession.IsFlatFlipEnabled))
            {
                rPA = AstroMath.Transform.NormalizeDegreeRange(rPA + 180);
                sop = "West";
                foreach (Filter fi in fset)
                {
                    fi.Repeat = flatRepetitions;
                    Flat iFlat = new Flat(tname, sop, rPA, fi, openSession.IsFlatFlipEnabled);
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
            SessionControl openSession = new SessionControl();
            openSession.IsFlatsRotationEnabled = FlatsRotationCheckBox.Checked;
        }

        private void FlatManManualSetupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.IsFlatManManualSetupEnabled = FlatManManualSetupCheckbox.Checked;

        }
    }
}
