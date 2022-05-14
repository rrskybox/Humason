﻿using Humason;
using Planetarium;
using System;
using TheSky64Lib;

namespace AtGuider2
{
    public static class AtGuider2
    {
        const double searchAreaDeg = 10;

        public static void CalibrateGuider()
        {

            //AtGuider2 Application for automated guider camera calibration
            /*
             * This application locates a suitable calibration star for guider calibration,
             *   slews the mount to frame the star in the guider FOV,
             *   adjusts the guide camera exposure for the targer,
             *   runs a calibration.
             *   
             */

            const double InitialGuiderExposure = 0.5; //Initial exposure level for guider camera images.
            const double OptGuiderADU = 20000; //Target ADU for guide star in guider camera images.

            //Open text output form

            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();

            //Lets get started...
            //plate solve current location to prime target star search and to acquire image camera position angle

            lg.LogIt("Plate Solving for current position and position angle");
            double imagePA = PlateSolve();
            lg.LogIt("Position Angle: " + imagePA.ToString("0.000"));
            //Create an FOV object for the guider from the "My equipment.txt" Field of View Indicators file
            lg.LogIt("Parsing My equipment.txt file for FOVI definitions");
            FOVMiracles guiderFOV = new FOVMiracles();
            lg.LogIt("Active Guider found: " + guiderFOV.FOVName);
            //Set the chart size for something pleasant around the FOVI's
            guiderFOV.SetStarChartSize();
            //Find a calibration star near the current position
            //  that is sufficiently isolated from other similar stars
            lg.LogIt("Looking for proximate star to use for calibration");
            DBQStar foundStar = FindStar(guiderFOV.FOVIsolation);
            if (foundStar == null)
            {
                lg.LogIt("No calibration star found.  Try another location.");
            }
            else
            {
                lg.LogIt("Calibration star found: " + foundStar.StarName);
                //Closed Loop Slew (following standard slew -- see notes) to the target guide star
                lg.LogIt("Centering imaging camera on calibration star");
                bool slewDone1 = SlewToStar(foundStar.StarName, foundStar.StarRA, foundStar.StarDec);
                if (slewDone1)
                {
                    lg.LogIt("Calibration star centered");
                }
                else
                {
                    lg.LogIt("There was a problem centering the calibration star");
                }
                //Calculate a pointing position that would put the target star in the guider FOV
                lg.LogIt("Calculating offset for centering star in guider FOV");
                DBQStar tgtPosition = guiderFOV.OffsetCenter(foundStar, imagePA);
                lg.LogIt("Offset calculated for pointing at " +
                    tgtPosition.StarRA.ToString("0.000") + " , " +
                    tgtPosition.StarDec.ToString("0.000"));

                //Closed Loop Slew (following standard slew -- see notes) to that offset position
                lg.LogIt("Centering calibration star in guider FOV");
                bool slewDone = SlewToPosition(tgtPosition.StarRA, tgtPosition.StarDec);
                if (slewDone1)
                {
                    lg.LogIt("Calibration star centered in guider FOV");
                }
                else
                {
                    lg.LogIt("Could not recenter the calibration star");
                }
                //plate solve current location -- not necessary but it sets up the star chart nicely for viewing
                //  note that we are not in such a hurry that we can't mess around a bit
                lg.LogIt("Checking offset position with a plate solve");
                imagePA = PlateSolve();
                //Reset the chart size for something pleasant around the FOVI's
                guiderFOV.SetStarChartSize();
                //center the star chart on the pointing location ==  once again, for esthetic purposes
                lg.LogIt("Recentering chart");
                sky6StarChart tsxsc = new sky6StarChart
                {
                    RightAscension = tgtPosition.StarRA,
                    Declination = tgtPosition.StarDec
                };
                //Take a guider image and adjust the exposure to an optimal level
                lg.LogIt("Adjusting guider exposure to achieve ADU of " + OptGuiderADU.ToString());
                double optExposure = OptimizeExposure(InitialGuiderExposure, OptGuiderADU);
                lg.LogIt("Best guider exposure determined to be " + optExposure.ToString("0.00") + " secs");
                //Calibrate the guider
                lg.LogIt("Starting direct guide calibration");
                string calDone = CalibrateGuideCam(optExposure, false); //No AO
                lg.LogIt("Direct guide " + calDone);
                //Calibrate the AO, if enabled
                TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
                lg.LogIt("Starting AO guide calibration");
                calDone = CalibrateGuideCam(optExposure, true); // AO
                lg.LogIt("AO guide " + calDone);
            }
        }

        static double PlateSolve()
        {
            //runs an image link on the current location to get PA data
            //assume camera, mount etc are connected and properly configured.  
            ccdsoftCamera tsxcc = new ccdsoftCamera
            {
                Autoguider = 0,
                Frame = ccdsoftImageFrame.cdLight,
                ExposureTime = 10,
                Delay = 0,
                Asynchronous = 0,
                AutoSaveOn = 1
            };

            tsxcc.TakeImage();

            ccdsoftImage tsxi = new ccdsoftImage();
            ImageLink tsxil = new ImageLink
            {
                scale = TSXLink.FOVI.GetFOVScale(),   //set Scale
                pathToFITS = tsxcc.LastImageFileName
            };
            tsxil.execute();
            ImageLinkResults tsxir = new ImageLinkResults();
            double iPA = tsxir.imagePositionAngle;

            //Check for image link success, return 0 if not.
            if (tsxir.succeeded == 1)
            { return iPA; }
            else
            { return 0; }

        }

        static DBQStar FindStar(double gIsolation)
        {
            /* Locates a nearby star for running a guider calibration
             *
             * Generate and observing list from the AtFocus database search near the current location
             *   (sometime in the future, cull this list for stars on the wrong side of the meridian.
             * Get the calibration distances (arc min for now) to use for framing
             * Look through the observing list array for a star isolated from it's neighbors by at least
             *   twice the calibration distance.  This means that once the FOV is centered on this ster,
             *   you can move the mount at least the calibration distance without the other star coming into
             *   the FOV.
             */

            //Find the first star that is isolated from all the other stars on the list
            //  and from the edge of the search area.  Note that the search area is in degrees and
            //  centered on the star chart center.  
            //  The star position <-> center position <= search radius - isolation radius. 
            //
            sky6StarChart tsxsc = new sky6StarChart();
            double gMaxRadius = (searchAreaDeg * 60) - gIsolation;  //arc mins
            double scRA = tsxsc.RightAscension;
            double scDec = tsxsc.Declination;
            sky6Utils tsxut = new sky6Utils();

            //Loop through all the stars in the prospect list
            //  For each star, loop through from that star forward to see if any other star is
            //  within gMinRadius of the center of the star chart
            //  if so, then look through the rest of the list for any star within the gisolation
            //    distance.
            //
            //  Eventually, need to check for meridian side as well
            //
            //  if not, then that's our star
            int gStarIdx = 0;
            double gStarRadius;
            double gRA;
            double gDec;
            StarProspects starList = new StarProspects(searchAreaDeg);
            for (int i = 0; i < starList.Count; i++)
            {
                gStarIdx = i;
                gRA = starList.StarRA(i);
                gDec = starList.StarDec(i);
                tsxut.ComputeAngularSeparation(scRA, scDec, gRA, gDec);
                gStarRadius = tsxut.dOut0 * 60;
                if (gStarRadius < gMaxRadius)
                {
                    for (int j = i + 1; j < starList.Count; j++)
                    {
                        tsxut.ComputeAngularSeparation(starList.StarRA(j), starList.StarDec(j), gRA, gDec);
                        if ((tsxut.dOut0 * 60) < gIsolation)
                        { break; }
                        if (j == starList.Count - 1)
                        { return starList.Star(gStarIdx); }
                    }
                }
            }
            return null;
        }

        static bool SlewToStar(string starName, double starRA, double starDec)
        {
            //Moves the mount to center the calibration star in the guider FOV
            //Async slew to target (letting dome catch up), then CLS to align (does not coordinate with dome)
            //
            //First, convert RA and Dec to topocentric (Epoch now) as that is what the slew expects
            sky6Utils tsxu = new sky6Utils();
            tsxu.Precess2000ToNow(starRA, starDec);
            starRA = tsxu.dOut0;
            starDec = tsxu.dOut1;
            sky6RASCOMTele tsxm = new sky6RASCOMTele
            {
                Asynchronous = 0
            };
            try
            {
                tsxm.SlewToRaDec(starRA, starDec, starName);
            }
            catch (Exception ex)
            { return false; }
            return true;
        }

        static bool SlewToPosition(double starRA, double starDec)
        {
            //Moves the mount to center the calibration star in the guider FOV
            //Async slew to target (letting dome catch up), then CLS to align (does not coordinate with dome)
            //
            sky6RASCOMTele tsxm = new sky6RASCOMTele
            {
                Asynchronous = 0
            };
            if (tsxm.IsConnected == 0) { tsxm.Connect(); }
            tsxm.SlewToRaDec(starRA, starDec, "Target Position");

            sky6StarChart tsxsc = new sky6StarChart();
            string RADecname = starRA.ToString() + "," + starDec.ToString();
            ClosedLoopSlew tsxcls = new ClosedLoopSlew();
            tsxsc.Find(RADecname);
            try
            { tsxcls.exec(); }
            catch (Exception ex)
            { return false; }
            return true;
        }

        static double OptimizeExposure(double exposure, double ADU)
        {
            //Adjusts the exposure level to the target ADu
            return (GuideControl.OptimizeExposure(exposure, ADU));
        }

        static bool SetSubFrame(double xCenter, double yCenter, double edgePixels)
        {
            //Sizes and centers the subframe of the guider imaging
            return true;
        }

        static string CalibrateGuideCam(double guiderExposure, bool AOEnabled)
        {
            //Launches the guider calibration
            return GuideControl.Calibrate(guiderExposure, AOEnabled);
        }

    }

    /// <summary>
    /// Class GuideCamFOV 
    /// 
    /// Used to encapsulate all the FOV data and functions for guide camera FOV
    /// 
    /// </summary>
    public class GuideCamFOV
    {
        //Instantiation picks up the guider FOV size, center, offset and PA
        //Open "my equipment.txt" file in TSX.  Translate to XML version
        //  for ease of parsing contents

        //Guide camera FOVI name could be "Autoguider" but maybe not
        public const string GuiderName = "Autoguider";
        //Guide camera FOVI should be second element (zero based) -- this may be a must, don't know yet
        public const int GuiderElementNumber = 1;

        //Gonna pick up an instance of the FOV data
        private FOVX gFOV;

        public GuideCamFOV()
        {
            //Open and populate the FOV database from the TSX my equipment.txt file
            gFOV = new FOVX();
            //Get the FOV name found for the first active entry (should only be one)
            Name = gFOV.GetActiveFOVHeaderEntry(FOVX.Description1FieldXName);
            //Get the second FOV element name for this entry -- should be the guide camera
            string aGuiderName = gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.ElementDescriptionFieldXName);
            //Populate the position fields for the FOV
            PA = Convert.ToDouble(gFOV.GetActiveFOVHeaderEntry(FOVX.PositionAngleFieldXName));
            CenterX = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.CenterOffsetXFieldXName));
            CenterY = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.CenterOffsetYFieldXName));
            PixelSizeX = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.PixelsXFieldXName));
            PixelSizeY = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.PixelsYFieldXName));
            ArcMinSizeX = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.SizeXFieldXName));
            ArcMinSizeY = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.SizeYFieldXName));
        }

        //Create automatic properties to hold FOV data in the class instance
        public string Name { get; set; }
        public double PA { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double PixelSizeX { get; set; }
        public double PixelSizeY { get; set; }
        public double ArcMinSizeX { get; set; }
        public double ArcMinSizeY { get; set; }
    }
}



