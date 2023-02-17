using Planetarium;
using System;

namespace Humason
{
    public static class Rotator
    {

        public static void Connect()
        {
            TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Rotator);
            return;
            ;
        }

        private static double StartRotatorAngle { get; set; } = 0;
        private static double StartImagePA { get; set; } = 0;
        private static double EndRotatorAngle { get; set; } = 0;
        private static double EndImagePA { get; set; } = 0;
        public static double ImageRA { get; set; } = 0;
        public static double ImageDec { get; set; } = 0;
        public static double ImagePA { get; set; } = 0;
        public static double RotatorOffset { get; set; } = 0;
        public static int RotatorDirection { get; set; } = 1;

        public static bool CalibrateRotator()
        {
            ///Calibration procedure for rotator
            ///The purpose is to determine the direction of rotation for positive
            ///  angular input, and the degree the absolute position angle of the rotator
            ///  is offset from the position angle of the camera.
            ///
            ///Plate-solve for the image position angle of the current rotator position
            ///  save the rotator position angle
            ///Rotate +10 degrees (add 10 to current angle reading)
            ///Platesolve the image position angle for this new rotator angle.
            ///  if the new image position angle is 10 greater than the old position angle (mod 360)
            ///    then set the rotator direction vector to 1 (Clockwise), otherwise it's -1 (Counterclockwise).
            ///Rotate -10 degrees to get back to the original position
            ///Plate-solve again to get another image position angle
            ///Average the two angle changes and save the result as the rotator offset value.

            //At this point, the PlateSolve should have "set" the position angle of the rotator

            const double TestAngle = 10;

            //Turn on the logger
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            lg.LogIt("Plate solving current rotator position");
            if (!PlateSolveIt()) { return false; }

            //double rotatorOffset = NHUtil.ReduceTo360(ImagePA - RealRotatorPA);
            double rotatorOffset = ImagePA - RealRotatorPA;
            int rotatorDirection = Math.Sign(RealRotatorPA);
            lg.LogIt("Current rotator position solved");

            StartRotatorAngle = RealRotatorPA;
            StartImagePA = ImagePA;
            //Rotate by +X degrees
            lg.LogIt("Rotating by +" + TestAngle.ToString("0") + " Degrees");
            RotateToRotatorPA(StartRotatorAngle + TestAngle);
            lg.LogIt("Plate solving new position");
            if (!PlateSolveIt()) { return false; }
            lg.LogIt("Rotated position successfully solved");
            EndRotatorAngle = RealRotatorPA;
            EndImagePA = ImagePA;
            //endImagePA = pSolve2.ImagePA;
            //endRotatorAngle = pSolve2.RotatorPositionAngle;

            //Rotate by -X degrees
            lg.LogIt("Rotating by " + (-TestAngle).ToString("0") + " Degrees");
            RotateToRotatorPA(StartRotatorAngle);
            TSXLink.PlateSolution pSolve3 = new TSXLink.PlateSolution();
            if (pSolve3 == null) { return false; }

            rotatorOffset = (StartImagePA - StartRotatorAngle);
            if (StartImagePA + TestAngle < 360)
            {
                if (StartImagePA > EndImagePA) { rotatorDirection = -1; }
                else { rotatorDirection = 1; }
            }
            else if (StartImagePA < EndImagePA) { rotatorDirection = -1; }
            else { rotatorDirection = 1; }

            RotatorDirection = rotatorDirection;
            openSession.RotatorDirection = rotatorDirection;
            lg.LogIt("Rotator behavior successfully calibrated");
            return true;
        }

        public static bool RotateToImagePA(double tgtImagePA)
        {
            //Move the rotator to a position that gives an image position angle of tImagePA
            //  Assumes that a plate solve has been performed, and/or rotator position angle variables
            //  are current
            //Returns false if failure, true if good

            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            TSXLink.Rotator trot = new TSXLink.Rotator();
            int rotDir = Convert.ToInt32(openSession.RotatorDirection);
            //Plate solve for current PA
            if (!PlateSolveIt()) return false;
            //target rotation PA = current image PA + current rotator PA - target image PA 
            // double tgtRotationPA = ((startImagePA - endImagePA) * rotdir) + rotPA;
            double destRotationPA = ((ImagePA - tgtImagePA) * -rotDir) + AstroMath.Transform.NormalizeDegreeRange(RealRotatorPA);
            double destRotationPAnormalized = AstroMath.Transform.NormalizeDegreeRange(destRotationPA);
            trot.SetRotatorPositionAngle(destRotationPAnormalized);
            //Plate solve for current PA
            if (!PlateSolveIt()) return false;
            return true;
        }

        public static void RotateToRotatorPA(double tgtRotatorPA)
        {
            //Move the rotator to new PA position

            TSXLink.Rotator trot = new TSXLink.Rotator();
            trot.SetRotatorPositionAngle(tgtRotatorPA);
            //ImagePA = RealRotatorPA + RotatorOffset;
            return;
        }

        public static bool PlateSolveIt()
        {
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            AstroImage asti = new AstroImage
            {
                Exposure = tPlan.PlateSolveExposureTime,
                Filter = tPlan.ClearFilter,
                ImageReduction = (AstroImage.ReductionType)openSession.ImageReductionType,
                Delay = 0
            };
            lg.LogIt("Plate Solve: Imaging");
            Imaging imgo = new Imaging();

            string path = imgo.TakeLightFrame(asti);

            lg.LogIt("Plate Solve: Image Linking");
            //tsxl.scale = TSXLink.FOVI.GetFOVScale();

            //tsxl.unknownScale = true;
            TSXLink.PlateSolution dSolve = TSXLink.ImageSolution.PlateSolve(path);
            if (dSolve == null)
            {
                lg.LogIt("Plate Solve: Image Link Failed: ");
                return false;
            }
            lg.LogIt("Plate Solve: Image Link Successful");
            ImagePA = dSolve.ImagePA;
            ImageRA = dSolve.ImageRA;
            ImageDec = dSolve.ImageDec;

            if (tPlan.RotatorEnabled)
            {
                //RotatorOffset = ImagePA - NHUtil.ReduceTo360( RealRotatorPA);
                RotatorOffset = AstroMath.Transform.NormalizeDegreeRange(ImagePA - RealRotatorPA);
            }
            return true;
        }

        public static double RealRotatorPA
        ///Retrieves current PA of the Rotator
        {
            get
            {
                TSXLink.Rotator rot = new TSXLink.Rotator();
                return rot.GetRotatorPositionAngle();
            }
        }

    }
}
