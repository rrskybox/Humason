using AtGuider2;
using Planetarium;
using System;
using System.Collections.Generic;

namespace Humason
{
    public static partial class AutoGuide
    {
        //Execute TSX_AutoGuide class
        //  Image guider camera
        //  Calibrate autoguide
        //  Autoguide
        //  Autoguide Profiler

        //MaxPixel-based method for getting the ADU of a guide star
        public static double GuideStarMaxPixel(double exposure)
        {
            //Take a subframe image on the guider, assuming it has been already set
            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);

            lg.LogIt("Creating Guider Subframe Image");
            AstroImage asti = new AstroImage
            {
                Camera = AstroImage.CameraType.Guider,
                Frame = AstroImage.ImageType.Light,
                BinX = tPlan.GuiderBinning,
                BinY = tPlan.GuiderBinning,
                SubFrame = 1,
                Delay = 0,
                Exposure = exposure
            };
            //Set image reduction
            if (tPlan.GuiderAutoDarkEnabled) { asti.ImageReduction = AstroImage.ReductionType.AutoDark; }
            else { asti.ImageReduction = AstroImage.ReductionType.None; }
            //Create camera object and turn turn off autosave, if on      
            TSXLink.Camera gCam = new TSXLink.Camera(asti) { AutoSaveOn = 0 };
            //Center up AO, just in case and if enabled
            if (tPlan.AOEnabled) { gCam.CenterAO(); }

            //Compute the subframe from the trackbox size
            int sizeX = gCam.TrackBoxX;
            int sizeY = gCam.TrackBoxY;
            gCam.SubframeTop = (int)(tPlan.GuideStarY * tPlan.GuiderBinning) - (sizeY / 2);
            gCam.SubframeBottom = (int)(tPlan.GuideStarY * tPlan.GuiderBinning) + (sizeY / 2);
            gCam.SubframeLeft = (int)(tPlan.GuideStarX * tPlan.GuiderBinning) - (sizeX / 2);
            gCam.SubframeRight = (int)(tPlan.GuideStarX * tPlan.GuiderBinning) + (sizeX / 2);

            int tstat = gCam.GetImage();
            if (tstat != 0)
            {
                lg.LogIt("Autoguider Image Error: " + tstat.ToString());
                return 0;
            }
            lg.LogIt("Guider Subframe image successful");

            double maxPixel = gCam.MaximumPixel;
            return maxPixel;
        }

        //SexTractor-based method for getting the ADU of a guide star
        public static double GetGuideStarADU(double exposure)
        {
            //Determines the ADU for the X/Y centroid of the maximum FWHM star in a subframe
            //
            //Take a subframe image on the guider using TSX guide star coordinates and trackbox size 
            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);

            lg.LogIt("Creating Guider Subframe Image");

            AstroImage asti = new AstroImage
            {
                Camera = AstroImage.CameraType.Guider,
                SubFrame = 1,
                Frame = AstroImage.ImageType.Light,
                BinX = tPlan.GuiderBinning,
                BinY = tPlan.GuiderBinning,
                Delay = 0,
                Exposure = exposure
            };
            //Set image reduction
            if (tPlan.GuiderAutoDarkEnabled) { asti.ImageReduction = AstroImage.ReductionType.AutoDark; }
            else { asti.ImageReduction = AstroImage.ReductionType.None; }
            TSXLink.Camera gCam = new TSXLink.Camera(asti)
            {
                AutoSaveOn = 1//Turn on autosave so we can extract a star inventory via ShowInventory()
            };
            //Center up AO, just in case and if enabled
            if (tPlan.AOEnabled)
            {
                gCam.CenterAO();
            }

            //Compute the subframe from the trackbox size
            int sizeX = gCam.TrackBoxX;
            int sizeY = gCam.TrackBoxY;
            gCam.SubframeTop = (int)(tPlan.GuideStarY * tPlan.GuiderBinning) - (sizeY / 2);
            gCam.SubframeBottom = (int)(tPlan.GuideStarY * tPlan.GuiderBinning) + (sizeY / 2);
            gCam.SubframeLeft = (int)(tPlan.GuideStarX * tPlan.GuiderBinning) - (sizeX / 2);
            gCam.SubframeRight = (int)(tPlan.GuideStarX * tPlan.GuiderBinning) + (sizeX / 2);

            //Take an image f
            int tstat = gCam.GetImage();
            if (tstat != 0)
            {
                lg.LogIt("Autoguider Image Error: " + tstat.ToString());
                return 0;
            }
            lg.LogIt("Guider Subframe image successful");

            //Next step is to generate the collection of stars in the subframe
            TSXLink.SexTractor sEx = new TSXLink.SexTractor();
            int xStat = sEx.SourceExtractGuider();
            lg.LogIt("Light source extraction complete");

            List<double> FWHMlist = sEx.GetSourceExtractionList(TSXLink.SexTractor.SourceExtractionType.sexFWHM);
            List<double> CenterX = sEx.GetSourceExtractionList(TSXLink.SexTractor.SourceExtractionType.sexX);
            List<double> CenterY = sEx.GetSourceExtractionList(TSXLink.SexTractor.SourceExtractionType.sexY);
            int iMax = sEx.GetListLargest(FWHMlist);

            double maxStarADU = 0;
            try
            {
                maxStarADU = sEx.GetPixelADU((int)CenterX[iMax], (int)CenterY[iMax]);
            }
            catch (Exception ex)
            {
                lg.LogIt("Light Source error -- no stars?");
            }
            if (maxStarADU == 0)
            { maxStarADU = gCam.MaximumPixel; }
            return maxStarADU;
        }

        //Grease slick to check if autoguiding is already running
        public static bool IsAutoGuideOn()
        {
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            AstroImage asti = new AstroImage { Camera = AstroImage.CameraType.Guider };
            TSXLink.Camera gCam = new TSXLink.Camera(asti);
            return gCam.IsAutoGuideOn();
        }

        //Fires up the autoguider including picking a star and optimizing the exposure time
        public static void AutoGuideStart()
        {
            //Turns on autoguiding, assuming that everything has been initialized correctly
            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            double autoguideExposureTime = tPlan.GuideExposure;

            lg.LogIt("Starting Autoguiding");
            LogVectors();

            AstroImage asti = new AstroImage
            {
                Camera = AstroImage.CameraType.Guider,
                BinX = tPlan.GuiderBinning,
                BinY = tPlan.GuiderBinning,
                Frame = AstroImage.ImageType.Light,
                Exposure = tPlan.GuideExposure
            };
            //Set image reduction
            if (tPlan.GuiderAutoDarkEnabled) { asti.ImageReduction = AstroImage.ReductionType.AutoDark; }
            else { asti.ImageReduction = AstroImage.ReductionType.None; }
            //Compute delay based on guider cycle time.  Zero means no delay;
            double agDelay = tPlan.GuiderCycleTime;
            if (agDelay > asti.Exposure)
            { asti.Delay = agDelay - asti.Exposure; }
            else
            { asti.Delay = 0; }

            //Create new guider object for running the guider
            //then center the AO, if enabled
            TSXLink.Camera gCam = new TSXLink.Camera(asti) { AutoSaveOn = 0 }
            ;
            if (tPlan.AOEnabled)
            {
                try { gCam.CenterAO(); }
                catch (Exception e) { lg.LogIt("AO Centering Error: " + e.Message); }
            }

            //guide star and exposure should have already been run
            //turn on guiding
            int agstat = gCam.AutoGuiderOn();
            if (agstat != 0) { lg.LogIt("Autoguide start up error " + agstat.ToString()); }
            lg.LogIt("Autoguiding Started");
        }

        //Aborts autoguiding, if running
        public static void AutoGuideStop()
        {
            //Halt Autoguiding
            //Open default image so we can turn the guider off then open guider and turn it off
            AstroImage asti = new AstroImage() { Camera = AstroImage.CameraType.Guider };
            TSXLink.Camera gCam = new TSXLink.Camera(asti);
            gCam.AutoGuiderOff();
        }

        //Runs the guider through the calibration routine
        public static void CalibrateAutoguiding(bool subFrameIt, double xSubframeSize, double ySubframeSize)
        {
            //Save current location
            //Make sure Autoguiding is turned off
            //Run local star search and slew to the nearest star of correct magnitude
            //Run autocalibration routine
            //Reload current location and closed loop slew to it
            //Create list of local stars, using the autofocus query
            //  Open DataWizard, set path to AtFocus2.dbq, Open query and run
            LogEvent lg = FormHumason.lg;
            lg.LogIt("Calibrating Autoguider");
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            //Select star for calibration and set guide star location in TSX
            SetAutoGuideStar();
            //Calibrate using existing calibration parameters, i.e. calibration times, etc
            AstroImage asti = new AstroImage
            {
                Camera = AstroImage.CameraType.Guider,
                Frame = AstroImage.ImageType.Light,
                BinX = tPlan.GuiderBinning,
                BinY = tPlan.GuiderBinning,
                SubFrame = 0,
                Exposure = tPlan.GuideExposure
            };
            //Set image reduction
            if (tPlan.GuiderAutoDarkEnabled) { asti.ImageReduction = AstroImage.ReductionType.AutoDark; }
            else { asti.ImageReduction = AstroImage.ReductionType.None; }
            if (subFrameIt)
            {
                asti.SubFrame = 1;
            }
            //Create camera object from parameters, turn on Autosave
            TSXLink.Camera gCam = new TSXLink.Camera(asti) { AutoSaveOn = 0 };
            //Set subframe around star, if enabled.  
            if (subFrameIt)
            {
                //Get width of guider FOV in pixels and arcsec
                GuideCamFOV gfov = new GuideCamFOV();
                double asWidthX = gfov.ArcMinSizeX * 60;
                double asWidthY = gfov.ArcMinSizeY * 60;
                double pixWidthX = gfov.PixelSizeX;
                double pixWidthY = gfov.PixelSizeY;
                double pixPerArcSecX = pixWidthX / asWidthX;
                double pixPerArcSecY = pixWidthY / asWidthY;
                //Compute the size of a box, in pixels, that would be 2X the calibration range
                //TSX does not compensate for binning in the pixelSize so the results
                //must be doubled if binning 2x2
                int binMult = tPlan.GuiderBinning;
                double pixBoxX = 2 * (tPlan.XAxisMoveTime * pixPerArcSecX) * binMult;
                double pixBoxY = 2 * (tPlan.YAxisMoveTime * pixPerArcSecX) * binMult;
                //Compute the subframe using the preset guidestar position
                gCam.SubframeTop = (int)(tPlan.GuideStarY - (pixBoxY / 2));
                gCam.SubframeBottom = (int)(tPlan.GuideStarY + (pixBoxY / 2));
                gCam.SubframeLeft = (int)(tPlan.GuideStarX - (pixBoxX / 2));
                gCam.SubframeRight = (int)(tPlan.GuideStarX + (pixBoxX / 2));
            }
            //Take an image for picking a calibration star, but don't save it
            int tstat = gCam.GetImage();
            //Run Calibration, note that the mount calibration will always be done.  The AO calibration is optional.
            lg.LogIt("Calibrating Direct Guide");
            gCam.Calibrate(false);
            if (tPlan.AOEnabled)
            {
                lg.LogIt("Calibrating AO");
                gCam.Calibrate(true);
            }
            //Store vectors
            tPlan.CalVectorXPosXComponent = gCam.CalibrationVectorXPositiveXComponent;
            tPlan.CalVectorXPosYComponent = gCam.CalibrationVectorXPositiveYComponent;
            tPlan.CalVectorXPosXComponent = gCam.CalibrationVectorXPositiveXComponent;
            tPlan.CalVectorXPosYComponent = gCam.CalibrationVectorXPositiveYComponent;
            tPlan.CalVectorYNegXComponent = gCam.CalibrationVectorXNegativeXComponent;
            tPlan.CalVectorYNegYComponent = gCam.CalibrationVectorXNegativeYComponent;
            tPlan.CalVectorYNegXComponent = gCam.CalibrationVectorXNegativeXComponent;
            tPlan.CalVectorYNegYComponent = gCam.CalibrationVectorXNegativeYComponent;
            lg.LogIt("Guider Calibration Complete");
        }

        //*** SetAutoGuideStar picks a guide star and places a subframe around it
        public static bool SetAutoGuideStar()
        {
            // Subroutine takes a picture, picks a guide star, computes a subframe to put around it,
            //  loads the location and subframe into the autoguider
            //

            // Subroutine picks a guide star, computes a subframe to put around it based on current TSX settings
            // and loads the location and subframe into the autoguider
            // 
            int MagWeight = 1;
            int FWHMWeight = 1;
            int ElpWeight = 1;
            int ClsWeight = 1;

            // Algorithm:
            // 
            //   Compute optimality and normalizaton values (see below) 
            //   Eliminate all points near edge and with neighbors
            //   Compute optimality differential and normalize, and add
            //   Select best (least sum) point
            // 
            // Normalized deviation from optimal where optimal is the best value for each of the four catagories:
            //   Magnitude optimal is lowest magnitude
            //   FWHM optimal is average FWHM
            //   Ellipticity optimal is lowest ellipticity
            //   Class optimal is lowest class
            // 
            // Normalized means adjusted against the range of highest to lowest becomes 1 to 0, unless there is only one datapoint
            // 
            // 

            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);

            lg.LogIt("Finding guide star coordinates");
            AstroImage asti = new AstroImage
            {
                Camera = AstroImage.CameraType.Guider,
                SubFrame = 0,
                Frame = AstroImage.ImageType.Light,
                BinX = tPlan.GuiderBinning,
                BinY = tPlan.GuiderBinning,
                Delay = 0,
                Exposure = tPlan.GuideExposure
            };
            //Set image reduction
            if (tPlan.GuiderAutoDarkEnabled) { asti.ImageReduction = AstroImage.ReductionType.AutoDark; }
            else { asti.ImageReduction = AstroImage.ReductionType.None; }
            TSXLink.Camera guider = new TSXLink.Camera(asti) { AutoSaveOn = 1 };
            //Center AO, if configured
            if (tPlan.AOEnabled)
            {
                guider.CenterAO();
            }

            //Take an image f
            int camResult = guider.GetImage();

            //acquire the current trackbox size (need it later)
            int TrackBoxSize = guider.TrackBoxX;

            TSXLink.SexTractor tsex = new TSXLink.SexTractor();

            try
            {
                int sStat = tsex.SourceExtractGuider();
            }
            catch (Exception ex)
            {
                // Just close up, TSX will spawn error window
                lg.LogIt("Some problem with guider image: " + ex.Message);
                tsex.Close();
                return false;
            }

            int Xsize = tsex.WidthInPixels;
            int Ysize = tsex.HeightInPixels;

            // Collect astrometric light source data from the image linking into single index arrays: 
            //  magnitude, fmhm, ellipsicity, x and y positionc
            //

            double[] MagArr = tsex.GetSourceExtractionArray(TSXLink.SexTractor.SourceExtractionType.sexMagnitude);
            int starCount = MagArr.Length;

            if (starCount == 0)
            {
                lg.LogIt("No astrometric sources found");
                tsex.Close();
                return false;
            }
            double[] FWHMArr = tsex.GetSourceExtractionArray(TSXLink.SexTractor.SourceExtractionType.sexFWHM);
            double[] XPosArr = tsex.GetSourceExtractionArray(TSXLink.SexTractor.SourceExtractionType.sexX);
            double[] YPosArr = tsex.GetSourceExtractionArray(TSXLink.SexTractor.SourceExtractionType.sexY);
            double[] ElpArr = tsex.GetSourceExtractionArray(TSXLink.SexTractor.SourceExtractionType.sexEllipticity);
            double[] ClsArr = tsex.GetSourceExtractionArray(TSXLink.SexTractor.SourceExtractionType.sexClass);

            // Get some useful statistics
            // Max and min magnitude
            // Max and min FWHM
            // Max and min ellipticity
            // max and min class
            // Average FWHM

            double maxMag = MagArr[0];
            double minMag = MagArr[0];
            double maxFWHM = FWHMArr[0];
            double minFWHM = FWHMArr[0];
            double maxElp = ElpArr[0];
            double minElp = ElpArr[0];
            double maxCls = ClsArr[0];
            double minCls = ClsArr[0];

            double avgFWHM = 0;
            double avgMag = 0;

            for (int i = 0; i < starCount; i++)
            {
                if (MagArr[i] < minMag) { minMag = MagArr[i]; }
                if (MagArr[i] > maxMag) { maxMag = MagArr[i]; }
                if (FWHMArr[i] < minFWHM) { minFWHM = FWHMArr[i]; }
                if (FWHMArr[i] > maxFWHM) { maxFWHM = FWHMArr[i]; }
                if (ElpArr[i] < minElp) { minElp = ElpArr[i]; }
                if (ElpArr[i] > maxElp) { maxElp = ElpArr[i]; }
                if (ClsArr[i] < minCls) { minCls = ClsArr[i]; }
                if (ClsArr[i] > maxCls) { maxCls = ClsArr[i]; }
                avgFWHM += FWHMArr[i];
                avgMag += MagArr[i];
            }

            avgFWHM /= starCount;
            avgMag /= starCount;

            // Create a set of "best" values
            double optMag = minMag;       // Magnitudes increase with negative values
            double optFWHM = avgFWHM;     // Looking for the closest to maximum FWHM
            double optElp = minElp;     // Want the minimum amount of elongation
            double optCls = maxCls;      // 1 = star,0 = galaxy
                                         // Create a set of ranges
            double rangeMag = maxMag - minMag;
            double rangeFWHM = maxFWHM - minFWHM;
            double rangeElp = maxElp - minElp;
            double rangeCls = maxCls - minCls;
            // Create interrum variables for weights
            double normMag;
            double normFWHM;
            double normElp;
            double normCls;
            // Count keepers for statistics
            int SourceCount = 0;
            int EdgeCount = 0;
            int NeighborCount = 0;
            int edgekeepout;

            // Create a selection array to store normilized and summed difference values
            double[] NormArr = new double[starCount];

            // Convert all points to normalized differences, checking for zero ranges (e.g.single or identical data points)
            for (int i = 0; i < starCount; i++)
            {
                if (rangeMag != 0) { normMag = 1 - Math.Abs(optMag - MagArr[i]) / rangeMag; }
                else { normMag = 0; }
                if (rangeFWHM != 0) { normFWHM = 1 - Math.Abs(optFWHM - FWHMArr[i]) / rangeFWHM; }
                else { normFWHM = 0; }
                if (rangeElp != 0) { normElp = 1 - Math.Abs(optElp - ElpArr[i]) / rangeElp; }
                else { normElp = 0; }
                if (rangeCls != 0) { normCls = 1 - Math.Abs(optCls - ClsArr[i]) / rangeCls; }
                else { normCls = 0; }

                // Sum the normalized points, weight and store value
                NormArr[i] = (normMag * MagWeight) + (normFWHM * FWHMWeight) + (normElp * ElpWeight) + (normCls * ClsWeight);
                SourceCount += 1;

                // Remove neighbors and edge liers
                edgekeepout = FormHumason.openSession.GuideStarEdgeMargin;

                if (IsOnEdge((int)XPosArr[i], (int)YPosArr[i], Xsize, Ysize, edgekeepout)) { NormArr[i] = -1; }
                else
                {
                    for (int j = i + 1; j < starCount - 1; j++)
                    {
                        if (IsNeighbor((int)XPosArr[i], (int)YPosArr[i], (int)XPosArr[j], (int)YPosArr[j], TrackBoxSize)) { NormArr[i] = -2; }
                    }
                }
            }

            // Now find the best remaining entry

            int bestOne = 0;
            for (int i = 0; i < starCount; i++)
            {
                if (NormArr[i] > NormArr[bestOne])
                {
                    bestOne = i;
                }
            }

            guider.GuideStarX = XPosArr[bestOne] * asti.BinX;
            guider.GuideStarY = YPosArr[bestOne] * asti.BinY;
            asti.SubframeLeft = (int)(XPosArr[bestOne] - (TrackBoxSize / 2)) * asti.BinX;
            asti.SubframeRight = (int)(XPosArr[bestOne] + (TrackBoxSize / 2)) * asti.BinX;
            asti.SubframeTop = (int)(YPosArr[bestOne] - (TrackBoxSize / 2)) * asti.BinY;
            asti.SubframeBottom = (int)(YPosArr[bestOne] + (TrackBoxSize / 2)) * asti.BinY;

            if (NormArr[bestOne] != -1)
            {
                tPlan.GuideStarX = guider.GuideStarX / asti.BinX;
                tPlan.GuideStarY = guider.GuideStarY / asti.BinY;
                lg.LogIt("Guide star coordinates set");
                tsex.Close();
                return true;
            }
            else
            {
                // run statistics -- only if (total failure
                for (int i = 0; i < SourceCount; i++)
                {
                    if (NormArr[i] == -1)
                    {
                        EdgeCount += 1;
                    }
                    if (NormArr[i] == -2)
                    {
                        NeighborCount += 1;
                    }
                }
                lg.LogIt("No Guide star found out of " +
                                        SourceCount.ToString() + " stars, " +
                                        NeighborCount.ToString() + " had neighbors and " +
                                        EdgeCount.ToString() + " were on the edge.");
                lg.LogIt("No Guide star coordinates set");
                tsex.Close();
                return false;
            }
        }

        //*** Optimize Exposure determines the best exposure time for the target star
        public static double OptimizeExposure()
        {
            //Get the best exposure time based on the target ADU
            //
            //Subrountine loops up to 4 times, taking an image, and calulating a new exposure that comes closest to meeting ADU goal
            //
            //Take an image with current exposure set from last guider exposure
            //If the max ADu is 100 or less, then there was no star at all.  Double the exposure upto MaxGuiderExposure
            //If the returned exposure is 64000 or more, then the star was probably saturated.  Halve the exposure down to no less than the minguider exposure.
            //If not within 20% of targetADU, then recalculate and rerun
            //If within 20% then recalculate and done, then update the exposure settings and return the exposure
            //
            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            double exposure = tPlan.GuideExposure;
            double tgtADU = tPlan.GuideStarADU;
            double maxExposure = tPlan.MaximumGuiderExposure;
            double minExposure = tPlan.MinimumGuiderExposure;
            //set the maximum number of iterations based on the maximum number of halves or doubles that could be performed.
            lg.LogIt("Attempting to find optimal guide star exposure");

            //Only take at max 4 shots at getting a good exposure, otherwise return the max or min
            for (int i = 0; i < 4; i++)
            {
                //Start at the initial exposure.  This is a minimum.
                //Take a subframe image
                //Get maximum pixels ADU
                //
                double maxPixel = GetGuideStarADU(exposure);  //Uses SexTractor engine
                lg.LogIt("Guide star ADU at " + maxPixel.ToString())
                    ;
                //Check through too low, too high and just right
                if (maxPixel < 500)  //way too low
                {
                    exposure = LimitMaxMin((exposure * 2), maxExposure, minExposure);
                    lg.LogIt("Guide Star exposure too low. Reset to " + exposure.ToString("0.00") + " secs");
                }
                else if (maxPixel > 60000.0)  //too close to saturation
                {
                    exposure = LimitMaxMin((exposure / 2), maxExposure, minExposure);
                    lg.LogIt("Guide Star exposure set too high. Reset to " + exposure.ToString("0.00") + " secs");
                }
                else if (!(NHUtil.CloseEnough(maxPixel, tgtADU, 20.0)))  //if not quite close enought recalculate exposure try again
                {
                    if (maxPixel > tgtADU)
                    {
                        exposure = LimitMaxMin(((tgtADU / maxPixel) * exposure), maxExposure, minExposure);
                        lg.LogIt("Guide Star ADU is " + maxPixel.ToString("0") + ": Exposure too high at " + exposure.ToString("0.00") + " secs");
                    }
                    else
                    {
                        exposure = LimitMaxMin(((tgtADU / maxPixel) * exposure), maxExposure, minExposure);
                        lg.LogIt("Guide Star ADU is " + maxPixel.ToString("0") + ": Exposure too low at " + exposure.ToString("0.00") + " secs");
                    }
                }
                else
                {
                    exposure = LimitMaxMin(((tgtADU / maxPixel) * exposure), maxExposure, minExposure);
                    break;
                }
            }
            lg.LogIt("Guide Star target exposure set to " + exposure.ToString("0.00") + " secs");
            return LimitMaxMin(exposure, maxExposure, minExposure);
        }

        //*** Determines if the given x,y position is off the border by at least the xsize and y size
        private static bool IsOnEdge(int Xpos, int Ypos, int Xsize, int Ysize, int border)
        {
            if ((Xpos - border > 0) &&
                (Xpos + border < Xsize) &&
                (Ypos - border > 0) &&
                (Ypos + border) < Ysize)
            { return false; }
            else
            { return true; }
        }

        //*** Determines if two x,y positions are within a given distance of each other
        private static bool IsNeighbor(int Xpos1, int Ypos1, int Xpos2, int Ypos2, int subsize)
        {
            int limit = subsize / 2;
            if ((Math.Abs(Xpos1 - Xpos2) >= limit) || (Math.Abs(Ypos1 - Ypos2) >= limit))
            { return false; }
            else
            { return true; };
        }

        //*** Logs calibration vectors in configuration file
        public static void LogVectors()
        {
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            LogEvent lg = FormHumason.lg;
            //Get calibration vectors
            //Make null image def to retrieve directly from TSX
            //Null definition
            AstroImage asti = new AstroImage() { Camera = AstroImage.CameraType.Guider };
            TSXLink.Camera gCam = new TSXLink.Camera(asti);
            //Write vectors to tPlan file
            //Store vectors
            tPlan.CalVectorXPosXComponent = gCam.CalibrationVectorXPositiveXComponent;
            tPlan.CalVectorXPosYComponent = gCam.CalibrationVectorXPositiveYComponent;
            tPlan.CalVectorXPosXComponent = gCam.CalibrationVectorXPositiveXComponent;
            tPlan.CalVectorXPosYComponent = gCam.CalibrationVectorXPositiveYComponent;
            tPlan.CalVectorYNegXComponent = gCam.CalibrationVectorXNegativeXComponent;
            tPlan.CalVectorYNegYComponent = gCam.CalibrationVectorXNegativeYComponent;
            tPlan.CalVectorYNegXComponent = gCam.CalibrationVectorXNegativeXComponent;
            tPlan.CalVectorYNegYComponent = gCam.CalibrationVectorXNegativeYComponent;

            //write a line of the 8 log vectors
            lg.LogIt("Calibration Vectors:  ");
            lg.LogIt("CalVectorXPosXComponent: " + tPlan.CalVectorXPosXComponent);
            lg.LogIt("CalVectorXPosYComponent: " + tPlan.CalVectorXPosYComponent);
            lg.LogIt("CalVectorXNegXComponent: " + tPlan.CalVectorXPosXComponent);
            lg.LogIt("CalVectorXNegYComponent: " + tPlan.CalVectorXPosYComponent);
            lg.LogIt("CalVectorYPosXComponent: " + tPlan.CalVectorXNegXComponent);
            lg.LogIt("CalVectorYPosYComponent: " + tPlan.CalVectorXNegYComponent);
            lg.LogIt("CalVectorYNegXComponent: " + tPlan.CalVectorXNegXComponent);
            lg.LogIt("CalVectorYNegYComponent: " + tPlan.CalVectorXNegYComponent);
        }

        public static bool DitherAndStart()
        {
            //Dithers the camera by a small random amount and waits until complete
            //  returns true if guider has returned to certain bounds in a reasonable
            //  time.  Turns off autoguider and returns false if not.
            const int MinDitherPixels = -5;
            const int MaxDitherPixels = 5;
            const double MinGuiderError = 1;

            //Prep class invocations: configuration, logging and TSX autoguider
            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            lg.LogIt("Dithering: Autoguide off");
            AutoGuideStop();
            AstroImage asti = new AstroImage() { Camera = AstroImage.CameraType.Guider };
            //Set image reduction
            if (tPlan.GuiderAutoDarkEnabled) { asti.ImageReduction = AstroImage.ReductionType.AutoDark; }
            else { asti.ImageReduction = AstroImage.ReductionType.None; }
            //Open guider
            TSXLink.Camera gCam = new TSXLink.Camera(asti);
            //Turn off autoguide
            //Get current X/Y guide star position
            double guideX = gCam.GuideStarX;
            double guideY = gCam.GuideStarY;
            //Compute random pixel offsets and adjust current values
            Random rndm = new Random();
            int offX = rndm.Next(MinDitherPixels, MaxDitherPixels);
            int offY = rndm.Next(MinDitherPixels, MaxDitherPixels);
            gCam.GuideStarX = guideX + offX;
            gCam.GuideStarY = guideY + offY;
            lg.LogIt("Dithering: Dithered by " + offX + " in X and " + offY + " in Y");
            //Restart autoguiding
            lg.LogIt("Dithering: Starting Autoguider");
            AutoGuideStart();
            //Monitor guide errors for 10 cycles, if RMS error not within MinError
            //  within those cycles, then shutdown autoguiding and return false
            double cycleTime = tPlan.GuideExposure;
            for (int cy = 0; cy < 10; cy++)
            {
                double rms = Math.Sqrt(Math.Pow(gCam.GuideErrorX, 2) + Math.Pow(gCam.GuideErrorY, 2));
                lg.LogIt("Dithering: Guider error at " + rms.ToString("0.00"));
                if (rms < MinGuiderError)
                {
                    //Convergence successful -- clean up and return true
                    lg.LogIt("Dithering: Converged");
                    return true;
                }
                else
                { System.Threading.Thread.Sleep((int)(cycleTime * 1000)); }  // sleep in milliseconds
            }
            //Convergence failed, turn off autoguiding, clean up and return false
            lg.LogIt("Dithering: Convergence failed.  Autoguiding stopped");
            AutoGuideStop();
            return false;
        }

        public static double LimitMaxMin(double inVal, double maxLimit, double minLimit)
        {
            //Returns InVal, limited by max and min
            if (inVal > maxLimit)
            { return maxLimit; }
            if (inVal < minLimit)
            { return minLimit; }
            return inVal;
        }
    }
}
