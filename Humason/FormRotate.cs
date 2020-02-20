using Planetarium;
using System;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormRotate : Form
    {
        public FormRotate()
        {
            InitializeComponent();
            //bool rstat = Rotator.Connect();  //get around to handling problems later...
            if (FormHumason.openSession.RotatorDirection == 1)
            {
                RotatorDirectionBox.Text = "CW";
            }
            else
            {
                RotatorDirectionBox.Text = "CCW";
            }

            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            if (tPlan.TargetPlanPath != null)
            {
                if (tPlan.PlateSolveExposureTime != 0)
                {
                    PlateSolveExposure.Value = (decimal)tPlan.PlateSolveExposureTime;
                }
                else
                {
                    tPlan.PlateSolveExposureTime = (double)PlateSolveExposure.Value;
                }

                NHUtil.ButtonGreen(PlateSolveButton);
                NHUtil.ButtonGreen(InitializeButton);
                NHUtil.ButtonGreen(CheckButton);
                NHUtil.ButtonGreen(InitializeButton);
                NHUtil.ButtonGreen(TargetButton);
                NHUtil.ButtonGreen(RotateToIPAButton);
                NHUtil.ButtonGreen(RotateToRPAButton);
                DisplayResults();
            }

            return;
        }

        private void PlateSolveButton_Click(object sender, EventArgs e)
        {
            //Run a plate solve (image link) to determine the current rotator PA
            //Connect to camera.
            //Set autosave.
            //Take an image
            NHUtil.ButtonRed(PlateSolveButton);
            LogEvent lg = FormHumason.lg;
            if (!Rotator.PlateSolveIt())
            {

                NHUtil.ButtonGreen(PlateSolveButton);
                return;
            }
            DisplayResults();
            NHUtil.ButtonGreen(PlateSolveButton);
            return;
        }

        private void PlateSolveExposure_ValueChanged(object sender, EventArgs e)
        {
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName)
            {
                PlateSolveExposureTime = (double)PlateSolveExposure.Value
            };
            return;
        }

        private void InitializeButton_Click(object sender, EventArgs e)
        {
            //Get location and PA of FOV on chart
            NHUtil.ButtonRed(InitializeButton);
            LogEvent lg = FormHumason.lg;
            DisplayResults();
            Show();
            Rotator.CalibrateRotator();
            DisplayResults();
            Show();
            NHUtil.ButtonGreen(InitializeButton);
            return;
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            //Rotator rot = new Rotator();
            DisplayResults();
            return;
        }

        private void TargetButton_Click(object sender, EventArgs e)
        {
            //POint telescope at target RA/Dec and rotate to PA
            //Slew scope to RA,Dec
            NHUtil.ButtonRed(TargetButton);
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);

            TSXLink.StarChart.ChartRA = tPlan.TargetRA;
            TSXLink.StarChart.ChartDec = tPlan.TargetDec;
            //tsxs.Rotation = Convert.ToDouble(tPlan.GetItem(Configuration.sbTargetPAName));
            Rotator.PlateSolveIt();
            DisplayResults();
            Rotator.RotateToImagePA(tPlan.TargetPA);
            DisplayResults();
            NHUtil.ButtonGreen(TargetButton);
            return;
        }

        private void RotateToIPAButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(RotateToIPAButton);
            Rotator.PlateSolveIt();
            DisplayResults();
            double rotate = (double)MoveToIPANum.Value;
            Rotator.RotateToImagePA(rotate);
            Rotator.PlateSolveIt();
            DisplayResults();
            NHUtil.ButtonGreen(RotateToIPAButton);
            return;
        }

        private void RotateToRPAButton_Click(object sender, EventArgs e)
        {
            NHUtil.ButtonRed(RotateToRPAButton);
            Rotator.PlateSolveIt();
            DisplayResults();
            double rotate = (double)MoveToRPANum.Value;
            Rotator.RotateToRotatorPA(rotate);
            Rotator.PlateSolveIt();
            DisplayResults();
            NHUtil.ButtonGreen(RotateToRPAButton);
            return;
        }

        private void DisplayResults()
        {
            double ipa = Rotator.ImagePA;
            ImagePABox.Text = AstroMath.Transform.NormalizeDegreeRange(ipa).ToString("0.000") + TestDegrees(ipa);
            double rpa = Rotator.RealRotatorPA;
            RotatorPABox.Text = AstroMath.Transform.NormalizeDegreeRange(rpa).ToString("0.000") + TestDegrees(rpa);
            double fpa = TSXLink.FOVI.GetFOVPA;
            FOVPABox.Text = AstroMath.Transform.NormalizeDegreeRange(fpa).ToString("0.000") + TestDegrees(fpa);
            double rof = Rotator.RotatorOffset;
            RotatorOffsetBox.Text = rof.ToString("0.000");
            if (Rotator.RotatorDirection == 1)
            {
                RotatorDirectionBox.Text = "CW";
            }
            else
            {
                RotatorDirectionBox.Text = "CCW";
            }

            TSXLink.StarChart.SetFOV(2);
            return;
        }

        private string TestDegrees(double degrees)
        {
            if (degrees < 0)
            {
                return ("  (-)");
            }
            else if (degrees >= 360)
            {
                return ("  (^)");
            }
            else
            {
                return "";
            }
        }

    }
}
