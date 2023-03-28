using Planetarium;
using System;
using TheSky64Lib;

namespace Humason
{

    #region AstroImage class
    //AstroImage class is a structure containing all the info to generate an image, and methods for
    //  taking the four types:  Light, Dark, Bias, Flat

    public class AstroImage
    {
        public enum CameraType
        {
            Imaging = 0,
            Guider = 1
        }

        public enum ReductionType
        {
            None = ccdsoftImageReduction.cdNone,
            AutoDark = ccdsoftImageReduction.cdAutoDark,
            BiasDarkFlat = ccdsoftImageReduction.cdBiasDarkFlat
        }

        public enum ImageType
        {
            Bias = ccdsoftImageFrame.cdBias,
            Dark = ccdsoftImageFrame.cdDark,
            Flat = ccdsoftImageFrame.cdFlat,
            Light = ccdsoftImageFrame.cdLight
        }

        private int filter;
        private string filterName;

        //private int subframeRight;

        public AstroImage()
        {
            TargetName = "";
            Camera = 0;
            Exposure = 0;
            BinX = 1;
            BinY = 1;
            filter = 0;
            Delay = 0;
            Frame = ImageType.Light;
            ImageReduction = ReductionType.None;
            SubFrame = 0;
            SubframeLeft = 0;
            SubframeTop = 0;
            SubframeBottom = 0;
            SubframeRight = 0;
            AutoSave = 1;
        }

        public string TargetName { get; set; }
        public double Exposure { get; set; }
        public CameraType Camera { get; set; }
        public int BinX { get; set; }
        public int BinY { get; set; }
        public int FilterIndex { get => filter; set => filter = value; }
        public double Delay { get; set; }
        public ImageType Frame { get; set; }
        public ReductionType ImageReduction { get; set; }
        public int SubFrame { get; set; }
        public int SubframeLeft { get; set; }
        public int SubframeTop { get; set; }
        public int SubframeBottom { get; set; }
        public int SubframeRight { get; set; }
        public int AutoSave { get; set; }
        public string Path { get; set; }
    }
    #endregion

    #region Imaging Class

    public class Imaging
    {
        public int TakeLightFrame(AstroImage asti)
        {
            //Image and save light frame
            //   Turn on autosave
            //   Set exposure length
            //   Set for light frame type
            //   Set for reduction
            //   Check for filter change, if (so) { set for a 5 second delay, otherwise, no delay
            //   Set for asynchronous execution
            //   Start exposure and wait until completed or aborted
            //   Clean up mess and return;
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();

            //Save the current time so we can calculate an overhead later
            DateTime imageStart = DateTime.Now;
            //Set up the filter
            TSXLink.FilterWheel.FilterIndex = asti.FilterIndex;

            lg.LogIt("Imaging Light: Filter " + asti.FilterIndex.ToString("0") + " for " + asti.Exposure.ToString("0.00") + " Sec");

            TSXLink.Camera tcam = new TSXLink.Camera(asti);

            int camResult = tcam.GetImage();
            if (camResult != 0)
            {
                lg.LogIt("Imaging Light Error: " + camResult);
                return camResult;
            }
            System.Windows.Forms.Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            lg.LogIt("Light Image Done");
            //Calculate and save the overhead duration
            DateTime imageEnd = DateTime.Now;
            TimeSpan imageDuration = imageEnd - imageStart;

            openSession.Overhead = imageDuration.TotalSeconds;
            asti.Path = tcam.LastImageFilename();
            return camResult;
        }

        public int TakeDarkFrame(double exposure)
        {
            //Image and save dark frames
            //   Turn on autosave
            //   Set exposure length
            //   Set for Dark frame type
            //   Set for 0 second delay
            //   Set for no image reduction
            //   Set for asynchronous execution
            //   For the number of repetions:
            //       Start exposure and wait until completed or aborted
            //   Clean up mess and return;
            LogEvent lg = new LogEvent();

            AstroImage asti = new AstroImage
            {
                //turn on autosave
                Exposure = exposure,
                Frame = AstroImage.ImageType.Dark,
                Delay = 0,
                ImageReduction = AstroImage.ReductionType.None,
                AutoSave = 1
            };

            lg.LogIt("Imaging Dark: " + exposure.ToString("0.00") + " Sec ");
            TSXLink.Camera tcam = new TSXLink.Camera(asti);
            int camResult = tcam.GetImage();
            if (camResult != 0)
            {
                lg.LogIt("Imaged Dark Error: " + camResult.ToString("0"));
                return camResult;
            }
            lg.LogIt("Dark Imaging Done");
            //return tcam.LastImageFilename();

            return camResult;
        }

        public int TakeBiasFrame()
        {
            //Image and save bias frames
            //   Turn on autosave
            //   Set exposure length to 0.001 (minimum)
            //   Set for bias frame type
            //   Set for 0 second delay
            //   Set for no image reduction
            //   Set for asynchronous execution
            //   For the number of repetions:
            //       Start exposure and wait until completed or aborted
            //   Clean up mess and return;

            LogEvent lg = new LogEvent();
            AstroImage asti = new AstroImage
            {
                Exposure = 0.001,
                Frame = AstroImage.ImageType.Bias,
                Delay = 0,
                ImageReduction = AstroImage.ReductionType.None,
                AutoSave = 1
            };
            lg.LogIt("Imaging Bias");
            TSXLink.Camera tcam = new TSXLink.Camera(asti);
            int camResult = tcam.GetImage();
            if (camResult != 0)
            {
                lg.LogIt("Imaged Bias Error: " + camResult.ToString());
                return camResult;
            }
            lg.LogIt("Bias Imaging Done");
            return camResult;
        }

        public int TakeFlatSample(Filter fltr, double exposure)
        {
            //Take a small subframed flat image and return the average pixel value
            const double subframeFactor = .1;  //fraction of frame that will be subframed
            LogEvent lg = new LogEvent();
            lg.LogIt("Taking Flat Sample Frame");

            AstroImage asti = new AstroImage
            {
                //Leave Autosave on
                Exposure = exposure,
                Frame = AstroImage.ImageType.Flat,
                Delay = 0,
                ImageReduction = AstroImage.ReductionType.None,
                FilterIndex = fltr.Index,
                SubFrame = 0,
                AutoSave = 1
            };
            //Take full image just to start and make sure we have the height and width correct
            lg.LogIt("- Imaging Flat Frame at " + asti.Exposure.ToString("0.00") + "sec");

            TSXLink.Camera tcamF = new TSXLink.Camera(asti);

            //Get image
            int camResultF = tcamF.GetImage();
            if (camResultF != 0)
            {
                lg.LogIt("- Image Subframe Flat Error: " + camResultF.ToString());
                return 0;
            }

            int width = tcamF.WidthInPixels;
            int height = tcamF.HeightInPixels;

            //Set subframe centered on full image of height and width scaled down to the subframe factor
            // The width center is
            asti.SubframeLeft = (width / 2) - (int)(width * subframeFactor / 2);
            asti.SubframeTop = (height / 2) - (int)(width * subframeFactor / 2);
            asti.SubframeBottom = (height / 2) + (int)(width * subframeFactor / 2);
            asti.SubframeRight = (width / 2) + (int)(width * subframeFactor / 2);
            asti.SubFrame = 1;

            lg.LogIt("- Imaging Flat Subframe at " + asti.Exposure.ToString("0.00") + "sec");
            //Prep camera and turn on autosave -- AttachActive doesn't work otherwise
            TSXLink.Camera tcamS = new TSXLink.Camera(asti);
            int camResultS = tcamS.GetImage();
            if (camResultS != 0)
            {
                lg.LogIt("- Image Subframe Flat Error: " + camResultS.ToString());
                return 0;
            }

            int avgADU = tcamS.ImageADU;
            lg.LogIt("Sample Flat Sample Done: Average ADU = " + avgADU.ToString("0"));
            return avgADU;
        }

        public void DoTwilightFlats(Flat iFlat, bool TimeOfDay)
        {
            //Image and save with flat frames
            //   Turn off autosave
            //   Image a flat for each of the filters,depending upon the time of day
            //       that is, if (twilight (true)) { run filters in order, otherwise reverse, i.e. clear first.
            //   Also determine starting and ending times based on time of day
            //  ) {..
            //   Clean up mess and return;
            LogEvent lg = new LogEvent();

            TwilightFlatsLoop(iFlat, TimeOfDay);

            //Clean up
            lg.LogIt("Flats Imaging Done");
            return;
        }

        private void TwilightFlatsLoop(Flat iFlat, bool TimeOfDay)
        {
            //Image a flat for a specified filter (filternumber) for a specified number of images (imagecount)
            //Process loop for the number of images required
            //   Take a frame (initially at 8 seconds)
            //   Compute average ADU
            //   if (twilight... and expsure is max//d with min ADU,) { quit
            //   if (dawn... and exposure is min with max ADU,) { quit
            //   Otherwise
            //   Compute new exposure time
            //   if (new exposure time is max) { wait a minute
            //   if (new exposure time is min) { wait a minute
            //   Loop

            //we//re done here
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();

            int MinExpTime = 4;
            int MaxExpTime = 60;
            //int MinADUVal = 20000;
            //int MaxADUVal = 40000;
            double tgtADU = openSession.FlatsTargetADU;
            int MinADUVal = (int)(tgtADU * 0.8);
            int MaxADUVal = (int)(tgtADU * 1.2);
            //int SaturatedADUVal = 65000

            int avgADU = 0;

            int imagecount = iFlat.FlatFilter.Repeat;
            int filternumber = iFlat.FlatFilter.Index;
            AstroImage asti = new AstroImage
            {
                Exposure = openSession.FlatsExposureTime,
                Frame = AstroImage.ImageType.Flat,
                ImageReduction = AstroImage.ReductionType.None,
                FilterIndex = iFlat.FlatFilter.Index,
                AutoSave = openSession.UseTSXAutoSave
            };

            while (imagecount > 0)
            {
                //Check to see if (the right filter is in, if (not) { set a delay for the
                //   exposure and change the filter and reset the exposure to the initial
                //   exposure time.
                //  otherwise, clear any delay

                //Set the exposure time
                lg.LogIt("Imaging Flat: " + iFlat.FlatFilter.Name +
                                        " Exp: " + asti.Exposure.ToString("0.00"));
                TSXLink.Camera tcam = new TSXLink.Camera(asti);
                int camResult = tcam.GetImage();
                avgADU = tcam.ImageADU;
                lg.LogIt("Imaged Flat: " + asti.FilterIndex +
                                                " Exp: " + asti.Exposure.ToString("0.00") +
                                                "s ADU: " + avgADU.ToString("0") +
                                                " Shot: " + imagecount.ToString("0"));
                if (TimeOfDay)
                {
                    //Twilight
                    if ((asti.Exposure > MaxExpTime) && (avgADU < MinADUVal))
                    {
                        //twilight and too little ADU:  end this
                        return;
                    }
                    else  //Good flat, save it
                    {
                        //Save file as flat
                        if (openSession.UseTSXAutoSave == 0)
                            ImageFileManager.SaveFlatImage(iFlat.TargetName,
                                   iFlat.FlatFilter.Name,
                                   iFlat.RotationPA.ToString("000"),
                                   iFlat.SideOfPier);
                    }
                }
                else
                { //Dawn
                    if ((asti.Exposure < MinExpTime) && (avgADU > MaxADUVal))
                    {
                        //dawn and too much ADU:  end this
                        return;
                    }
                    else  //Good flat, save it
                    {
                        //Save file as flat
                        if (openSession.UseTSXAutoSave == 0)
                            ImageFileManager.SaveFlatImage(iFlat.TargetName,
                                   iFlat.FlatFilter.Name,
                                   iFlat.RotationPA.ToString("000"),
                                   iFlat.SideOfPier);
                    }
                }
                //if (the average ADU is between the min and max, keep it and take the next frame, if (!done
                if ((avgADU >= MinADUVal) && (avgADU < MaxADUVal))
                {
                    imagecount -= 1;
                }
                else
                {
                    //Calculate new exposure
                    asti.Exposure = TwilightExposureCalculate(avgADU, (int)asti.Exposure, MinADUVal, MaxADUVal);
                    //if (image exposure is max//ed or min//d out,) { wait for 1 minute
                    if (asti.Exposure >= MaxExpTime)
                    {
                        if (TimeOfDay)
                        { //Twilight -- it//s !going to get any brighter.We//re done.
                            return;
                        }
                        lg.LogIt("ADU avg = " + avgADU.ToString("0") + " wait one minute to lighten");
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(60000);
                        //set exposure back down to maximum before taking next image
                        asti.Exposure = MaxExpTime;
                    }
                    else
                    {
                        if (asti.Exposure <= MinExpTime)
                        {
                            if (!TimeOfDay)
                            { //Dawn -- it//s !going to get any darker.We//re done.
                                return;
                            }
                            lg.LogIt("ADU avg = " + avgADU.ToString("0") + " wait one minute to darken");
                            System.Windows.Forms.Application.DoEvents();
                            System.Threading.Thread.Sleep(60000);
                            //set exposure back up to minimum before taking next image
                            asti.Exposure = MinExpTime;
                        }
                    }
                }
            }
            return;
        }

        public void DoFlatManFlats(string targetName, double rotationPA, string meridianSide, Filter filter)
        {
            ///Image and save with flat frames
            /// Summary:  Image a set of flat for each of the filters, 
            /// stored in the calibration directory for the current target name/date
            ///
            /// Create a string for the calibration directory path
            /// 

            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();

            //Turn light on, if off
            FlatMan flmn = new FlatMan { Light = true };

            //try to find the best brightness that is near, but less than the target
            int brightness = FlatManBrightnessCalibration(filter, openSession.FlatsExposureTime, openSession.FlatManBrightness, openSession.FlatsTargetADU);
            lg.LogIt("FlatMan brightness level set to " + brightness.ToString("0"));
            //try to find the best exposure that is close to the target
            double exposure = FlatManExposureCalibration(filter, openSession.FlatsExposureTime, brightness, openSession.FlatsTargetADU);
            lg.LogIt("FlatMan exposure set to " + exposure.ToString("0.00"));
            flmn.Bright = brightness;
            int imagecount = filter.Repeat;
            FlatManFlatsLoop(targetName, filter, imagecount, exposure, rotationPA, meridianSide);
            //Clean up
            lg.LogIt("Flats Imaging Done");
            //turn off flatman
            flmn.Light = false;
        }

        private void FlatManFlatsLoop(string targetName, Filter filter, int imagecount, double exposure, double rotationPA, string meridianSide)
        {
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();

            AstroImage asti = new AstroImage
            {
                Frame = AstroImage.ImageType.Flat,
                ImageReduction = AstroImage.ReductionType.None,
                FilterIndex = filter.Index,
                Exposure = exposure,
                AutoSave = openSession.UseTSXAutoSave
            };
            TSXLink.Camera tcam = new TSXLink.Camera(asti);

            for (int i = 0; i < imagecount; i++)
            {
                lg.LogIt("Imaging Flat: " + "Filter " + asti.FilterIndex +
                              " Exp: " + exposure.ToString("0.0") + "  " + i.ToString("0") + " of " + imagecount.ToString("0"));
                int camResult = tcam.GetImage();
                if (camResult != 0)
                {
                    lg.LogIt("Flat Imaging Error: ");
                    break;
                }
                else
                {
                    //Save file as flat
                    if (openSession.UseTSXAutoSave == 0)
                        ImageFileManager.SaveFlatImage(targetName,
                           filter.Name,
                           rotationPA.ToString("000"),
                           meridianSide);
                }
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
            }
        }

        private int FlatManBrightnessCalibration(Filter filter, double exposure, int startingBrightness, int targetADU)
        {
            //Looks for brightness setting that produces something close (80%) to the target ADU at the given exposure
            //The brightness setting starts with the currently configured brightness.
            //The exposure setting is fixed at the curently configured flats exposure setting.
            //1. Take flat image with given filter at exposure and initial brightness level
            //2. It the currentADU is within 20% of the targetADU, and it is less than the targetADU, then return that brightness level
            //3.   Otherwise, increment the brightness level up or down by 5 and try again.

            LogEvent lg = new LogEvent();

            int currentADU;
            int currentBrightness = startingBrightness;
            //
            //Neither the exposure nor the brightness is linear -- this is a problem
            lg.LogIt("Calibrating FlatMan brightness");
            //Try no more than 8 times to get a good brightness
            FlatMan flmn = new FlatMan();
            for (int i = 0; i < 8; i++)
            {
                lg.LogIt("Brightness reset to " + currentBrightness.ToString("0"));
                //initially set brightness to the starting brightness, and wait a second for the FlatMan
                flmn.Bright = currentBrightness;
                System.Threading.Thread.Sleep(500);
                //Get the ADU of a sample image (subframe)
                currentADU = TakeFlatSample(filter, exposure);
                //If ADU is not close enough (greater than 20%) or is greater than target then
                //  increase or decrease the brightness accordingly
                //  Otherwise, we're done with it
                if (!(NHUtil.CloseEnough(targetADU, currentADU, 20.0)) || (currentADU > targetADU))
                {
                    if (currentADU > targetADU)
                    { currentBrightness = currentBrightness - 5; }
                    else
                    { currentBrightness = currentBrightness + 5; }
                }
                else { break; }
            }
            lg.LogIt("FlatMan brightness calibration done");
            return (currentBrightness);
        }

        private double FlatManExposureCalibration(Filter filter, double startingexposure, int brightness, int targetADU)
        {
            //Looks for exposure setting that produces something close (90%) to the target ADU at the given exposure
            //The exposure setting starts with the currently configured brightness.
            //The brightness level is fixed at the curently configured flats exposure setting.
            //The ADU setting is fixed at the currently configured flats ADU
            //  (Note: there could be an error to deal with if the brightness result exceeds the brightness capability, min or max)
            //
            //1. Take flat image with given filter at exposure and initial brightness level
            //2. Calculate new exposure needed to that is within 90% of Max ADU
            //
            LogEvent lg = new LogEvent();
            lg.LogIt("Calibrating FlatMan exposure");
            int currentADU;
            double currentExposure = startingexposure;
            //double nextexposure = startingexposure;
            //
            //Neither the exposure nor the brightness is linear -- this is a problem
            //Try no more than 8 times to get a good exposure
            FlatMan flmn = new FlatMan();
            for (int i = 0; i < 8; i++)
            {
                //Set Flatman brightness level
                lg.LogIt("Flatman brightness set to " + brightness.ToString("0"));
                flmn.Bright = brightness;
                //Take flats sample with current exposure and filter
                currentADU = TakeFlatSample(filter, currentExposure);
                if (NHUtil.CloseEnough(targetADU, currentADU, 10.0))
                { break; }
                else
                {
                    //compute new exposure based on current ADU
                    //  if the result of the flat sample was 0, meaning a failed test, double and try again
                    //  otherwise, adjust proportionally and result and try again
                    // 
                    if (currentADU > 100)
                    { currentExposure = currentExposure * targetADU / currentADU; }
                    else
                    { currentExposure = currentExposure * 2; }
                    //If the exposure is greater than 30 seconds then log it and exit
                    if (currentExposure > 30)
                    {
                        lg.LogIt("Flatman exposure exceeds 30 second limit");
                        break;
                    }
                }
            }
            lg.LogIt("FlatMan exposure calibration done");
            return (currentExposure);
        }

        private int TwilightExposureCalculate(double currentADU, int currentExposure, int minADU, int maxADU)
        {
            //Determines a new exposure time based on the current ADU and exposure time, towards a target median ADU
            //Calculate seconds per ADU
            double SecPerADU = currentExposure / currentADU;
            int nextExposure;
            if (currentADU < minADU)
            {
                nextExposure = (int)(((minADU + maxADU) / 2) * SecPerADU);
                return (int)(((minADU + maxADU) / 2) * SecPerADU);
            }
            else
            {
                if (currentADU > maxADU)
                {
                    nextExposure = (int)(((minADU + maxADU) / 2) * SecPerADU);
                    return (int)(((minADU + maxADU) / 2) * SecPerADU);
                }
                else
                {
                    return (currentExposure);
                }
            }
        }
    }

    #endregion
}
