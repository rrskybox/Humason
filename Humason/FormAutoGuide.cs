using System;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormAutoGuide : Form
    {
        public FormAutoGuide()
        {
            InitializeComponent();
            ColorButtonsGreen();
            //Populate entries with stored entries, if any
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (tPlan.TargetPlanPath != null)
            {
                try { ResetConfiguration(); } catch { };  //ignore problems in target plan file, fix later
            }
        }

        public void ResetConfiguration()
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (tPlan.HasValue(TargetPlan.GuideExposureTimeXName))
            { GuideExposureTimeBox.Value = (decimal)tPlan.GuideExposure; }
            else
            { tPlan.GuideExposure = (double)GuideExposureTimeBox.Value; }

            if (tPlan.HasValue(TargetPlan.MaxGuideExposureTimeXName))
            { MaximumGuideExposureTimeBox.Value = (decimal)tPlan.MaximumGuiderExposure; }
            else
            { tPlan.MaximumGuiderExposure = (double)MaximumGuideExposureTimeBox.Value; }

            if (tPlan.HasValue(TargetPlan.MinGuideExposureTimeXName))
            { MinimumGuideExposureTimeBox.Value = (decimal)tPlan.MinimumGuiderExposure; }
            else
            { tPlan.MinimumGuiderExposure = (double)MinimumGuideExposureTimeBox.Value; }

            if (tPlan.HasValue(TargetPlan.GuideStarADUXName))
            { GuideStarADUNum.Value = (decimal)tPlan.GuideStarADU; }
            else
            { tPlan.GuideStarADU = (int)GuideStarADUNum.Value; }

            if (tPlan.HasValue(TargetPlan.AOCheckedXName))
            { AOCheckBox.Checked = tPlan.AOEnabled; }
            else
            { tPlan.AOEnabled = AOCheckBox.Checked; }

            if (tPlan.HasValue(TargetPlan.GuiderSubframeEnabledXName))
            { SubframeCheckBox.Checked = tPlan.GuiderSubframeEnabled; }
            else
            { tPlan.GuiderSubframeEnabled = SubframeCheckBox.Checked; }

            if (tPlan.HasValue(TargetPlan.XAxisMoveTimeXName))
            { XAxisMoveTime.Value = (decimal)tPlan.XAxisMoveTime; }
            else
            { tPlan.XAxisMoveTime = (double)XAxisMoveTime.Value; }

            if (tPlan.HasValue(TargetPlan.YAxisMoveTimeXName))
            { YAxisMoveTime.Value = (decimal)tPlan.YAxisMoveTime; }
            else
            { tPlan.YAxisMoveTime = (double)YAxisMoveTime.Value; }

            if (tPlan.HasValue(TargetPlan.GuideStarXXName))
            { GuideStarXBox.Text = tPlan.GuideStarX.ToString("0.0"); }
            else
            { tPlan.GuideStarX = Convert.ToDouble(GuideStarXBox.Text); }

            if (tPlan.HasValue(TargetPlan.GuideStarYXName))
            { GuideStarYBox.Text = tPlan.GuideStarY.ToString("0.0"); }
            else
            { tPlan.GuideStarY = Convert.ToDouble(GuideStarYBox.Text); }

            if (tPlan.HasValue(TargetPlan.GuideCycleTimeXName))
            { GuiderCycleTimeNum.Value = (decimal)tPlan.GuiderCycleTime; }
            else
            { tPlan.GuiderCycleTime = (double)GuiderCycleTimeNum.Value; }

            if (tPlan.HasValue(TargetPlan.GuiderBinningXName))
            {
                if (tPlan.GuiderBinning == 2)
                { Binning2x2RadioButton.Checked = true; }
                else
                {
                    Binning1x1RadioButton.Checked = true;
                    tPlan.GuiderBinning = 1;
                }
            }
            else if (Binning1x1RadioButton.Checked)
            {
                tPlan.GuiderBinning = 1;
            }
            else
            {
                tPlan.GuiderBinning = 2;
            }
        }

        public double GuideExposure()
        {
            return ((double)MinimumGuideExposureTimeBox.Value);
        }

        private void CloseButton_Click(Object sender, EventArgs e)
        {
            Close();
        }

        private void AutoGuideOnButton_Click(object sender, EventArgs e)
        {
            //Execute TSX_AutoGuide class
            //  Open and connect to autoguider
            //  if (not calibrated,) { abort
            //  Find guidestar
            //  Turn on Autoguide
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            if (NHUtil.IsButtonRed(AutoGuideOnButton))
            {
                AutoGuiding.AutoGuideStop();
                AutoGuideOnButton.Text = "Start\r\nAutoguiding";
                NHUtil.ButtonGreen(AutoGuideOnButton);
            }
            else
            {
                //First, save the guider cycle time and anything else in the future that might be hanging around
                tPlan.GuiderCycleTime = (double)GuiderCycleTimeNum.Value;
                if (tPlan.DitherEnabled)
                { AutoGuiding.DitherAndStart(); }
                else
                { AutoGuiding.AutoGuideStart(); }
                AutoGuideOnButton.Text = "Stop\r\nAutoguiding";
                NHUtil.ButtonRed(AutoGuideOnButton);
            }
        }

        private void FindStarButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(FindStarButton);

            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            bool fsbresult = AutoGuiding.SetAutoGuideStar();
            //if there is an error, then assume that the exposure is just too low
            // reset the guide exposure to maximum and try again
            if (!fsbresult)
            {
                tPlan.GuideExposure = tPlan.MaximumGuiderExposure;
                GuideExposureTimeBox.Value = (decimal)tPlan.MaximumGuiderExposure;
                fsbresult = AutoGuiding.SetAutoGuideStar();
            }
            //If it worked this time then update the guide star position, otherwise just leave it
            if (fsbresult)
            {
                GuideStarXBox.Text = Convert.ToInt32(tPlan.GuideStarX).ToString();
                GuideStarYBox.Text = Convert.ToInt32(tPlan.GuideStarY).ToString();
            }
            NHUtil.ButtonGreen(FindStarButton);
        }

        private void CalibrateButton_Click(object sender, EventArgs e)
        {
            //Calibrate the mount (direct guide), then calibrate AO, if set
            NHUtil.ButtonRed(CalibrateButton);
            AutoGuiding.CalibrateAutoguiding(SubframeCheckBox.Checked, (double)XAxisMoveTime.Value, (double)YAxisMoveTime.Value);
            //Restore original target position if (a new star was used
            NHUtil.ButtonGreen(CalibrateButton);
        }

        private void OptimizeExposureButton_Click(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            NHUtil.ButtonRed(OptimizeExposureButton);
            double optExposure = AutoGuiding.OptimizeExposure();
            GuideExposureTimeBox.Value = (decimal)optExposure;
            tPlan.GuideExposure = optExposure;
            NHUtil.ButtonGreen(OptimizeExposureButton);

        }

        private void AOCheck_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                AOEnabled = AOCheckBox.Checked
            };
        }

        private void GuideExposureTimeBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                GuideExposure = (double)GuideExposureTimeBox.Value
            };
        }

        private void MaximumGuideExposureTimeBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                MaximumGuiderExposure = (double)MaximumGuideExposureTimeBox.Value
            };
        }

        private void MinimumGuideExposureTimeBox_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                MinimumGuiderExposure = (double)MinimumGuideExposureTimeBox.Value
            };
        }

        private void GuideStarADUNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                GuideStarADU = (Int32)GuideStarADUNum.Value
            };
        }

        private void ColorButtonsGreen()
        {
            NHUtil.ButtonGreen(AutoGuideOnButton);
            NHUtil.ButtonGreen(FindStarButton);
            NHUtil.ButtonGreen(OptimizeExposureButton);
            NHUtil.ButtonGreen(CalibrateButton);
            NHUtil.ButtonGreen(GuiderAutoCalibrateButton);
        }

        private void Binning1x1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                GuiderBinning = 1
            };
        }

        private void Binning2x2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                GuiderBinning = 2
            };
        }

        private void GuiderAutoCalibrateButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(GuiderAutoCalibrateButton);
            //AtGuider2.AtGuider2.CalibrateGuider();
            AutoGuiding.CalibrateAutoguiding(SubframeCheckBox.Checked, (double)XAxisMoveTime.Value, (double)YAxisMoveTime.Value);
            NHUtil.ButtonGreen(GuiderAutoCalibrateButton);
        }

        private void SubframeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                GuiderSubframeEnabled = SubframeCheckBox.Checked
            };

        }

        private void XAxisMoveTime_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                XAxisMoveTime = (double)XAxisMoveTime.Value
            };
        }

        private void YAxisMoveTime_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                YAxisMoveTime = (double)YAxisMoveTime.Value
            };
        }

        private void GuideStarEdgeMarginNum_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.GuideStarEdgeMargin = (Int16)GuideStarEdgeMarginNum.Value;
        }

        private void SubframeCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName)
            {
                GuiderSubframeEnabled = SubframeCheckBox.Checked
            };
        }
    }
}

