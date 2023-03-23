using Planetarium;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeatherWatch;


namespace Humason
{
    public class Sequencer
    {

        //Generates and runs imaging sequences through tsx
        //
        //Author: Rick McAlister
        //Date: 6/18/15 (and much, much earlier...)dome        //
        //
        //Primary purpose is to set up and run quick sequences and calibration frames when CCDAP is either too
        //    slow or fails.
        //
        //Build this routine in several steps:
        //1. Compute and display a sequence for the current telescope location based on form input -- start time, target, filters, repetitions, exposure
        //2. Add minimum altitude calculation
        //3.
        //Etc, etc  

        //Target name, if (any for the sequence file prefix -- default value if (none and user hasn't input any

        public string FlipText { get; set; }
        public string DoneText { get; set; }
        public string TransitText { get; set; }
        public string SetLimitText { get; set; }
        public string RiseLimitText { get; set; }
        public DateTime DawnDateTime { get; set; }
        public double SiteLat { get; set; }
        public double SiteLon { get; set; }
        public DateTime SiteLocalTime { get; set; }
        public TimeZoneInfo SiteLocalTZ { get; set; }
        public DateTime UTCTime { get; set; }
        public TimeSpan UTCOffset { get; set; }
        public TimeSpan SunRise { get; set; }
        public TimeSpan SunSet { get; set; }
        public TimeSpan ATwilightStart { get; set; }
        public TimeSpan ATwilightEnd { get; set; }
        public double TargetRA { get; set; }
        public double TargetDec { get; set; }
        public TimeSpan TargetRise { get; set; }
        public TimeSpan TargetTransit { get; set; }
        public TimeSpan TargetSet { get; set; }
        public TimeSpan TargetHA { get; set; }
        public TimeSpan TargetLimitSetting { get; set; }
        public TimeSpan TargetLimitRising { get; set; }

        public double Progress_Percent { get; set; }

        public bool LastTargetSideWest { get; set; }    //Records where the mount was pointing for last shot:  true = east, false = West

        public bool CurrentTargetSideWest { get; set; }//Records where the mount is pointing for current shot:  true = east, false = West

        //Set up array of frames for imaging
        //Each member of the array will be the definition for one image, or frame
        //The definition will consist of five items: filter, exposure, binning, frame type and delay.
        //The array will be redimensioned when populated

        public int[,] ImageSeries;

        //Array indices for rows: (parameters)
        int si_Filter = 0;
        int si_Exposure = 1;
        int si_Binning = 2;
        int si_Frame = 3;
        int si_Delay = 4;

        int sb_1x1 = 0;

        //public DateTime SiteLocalTime { get => siteLocalTime; set => siteLocalTime = value; }

        public Sequencer()
        {
            //Clear abort, if any
            //Load Abort Event Handler
            //AbortEvent abortEvent = new AbortEvent();
            // abortEvent.AbortEventHandler += SequencerAbortEvent_Handler;
            //Open target plan for this sequence
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            //No plan, then just quit
            if (tPlan.TargetPlanPath == null)
            {
                return;
            }

            LogEvent lg = new LogEvent();
            //Fill in site and target data from TSX
            //Create star chart and object information class instantiations in TSX
            //Set TSX Star Chart for the current sequence start time
            //Make sure TSX is at current time or at least the start time is later than the current time
            TSXLink.StarChart.SetClock(0, true);  //set to computer (current) time
            DateTime currentTime = DateTime.Now;
            DateTime startTime = tPlan.SequenceStartTime;
            if (startTime < currentTime)
            { startTime = currentTime; }
            double currentJD = TSXLink.StarChart.JulianDate;
            double seqJD = currentJD + (startTime - currentTime).TotalDays;
            //Disable the computer clock so we can look forward
            TSXLink.StarChart.SetClock(seqJD, false);
            //Wait for a second for the starchart to set the clock
            System.Threading.Thread.Sleep(1000);

            //Find the target by target name.  This should be close enough even if it has been adjusted
            string targetName = tPlan.TargetName;
            if (tPlan.TargetAdjustEnabled)
            { targetName = tPlan.TargetRA.ToString() + "," + tPlan.TargetDec.ToString(); }

            TSXLink.Target tgto = TSXLink.StarChart.FindTarget(targetName);
            if (tgto == null)
            {
                lg.LogIt("Target Not Found");
                return;
            }

            //Target Data
            SiteLat = (2 * Math.PI / 360) * tgto.Lat; //Site Latitude (in degrees) 
            SiteLon = (2 * Math.PI / 360) * tgto.Long; //Site Longitude (in degrees)    
            TargetDec = tgto.Dec; //Dec_2000
            TargetRA = tgto.RA; //RA_2000
            TargetRise = tgto.Rise; //Target Rise Time
            TargetTransit = tgto.Transit;//Target Transit Time
            TargetSet = tgto.Set;//Target Set Time
            TargetHA = tgto.HA;//Target Hour Angle

            //Sun Data
            TSXLink.Target suno = TSXLink.StarChart.FindTarget("Sun");
            // Get astronmical twilight start (morning);
            ATwilightStart = suno.Dusk;
            // Get astronmical twilight end (evening);
            ATwilightEnd = suno.Dawn;
            // Get Rise Time
            SunRise = suno.Rise;
            // Get Set Time
            SunSet = suno.Set;
            //Reload target
            TSXLink.StarChart.CenterStarChart(tgto);

            //Compute time data:  timezone, local time and universal time
            //Local timezone comes from the computer system time
            SiteLocalTZ = TimeZoneInfo.Local;
            UTCOffset = SiteLocalTZ.BaseUtcOffset;
            SiteLocalTime = TSXLink.StarChart.ComputedSiteLocalTime;
            UTCTime = SiteLocalTime.ToUniversalTime();
            TimeSpan minAltHourAngle = CalcMinAltHourAngle();
            if (minAltHourAngle != TimeSpan.FromHours(0))
            {
                TargetLimitSetting = TargetTransit + minAltHourAngle;
                TargetLimitRising = TargetTransit - minAltHourAngle;
            }
            else
            {
                TargetLimitSetting = TimeSpan.FromHours(24);
                TargetLimitRising = TimeSpan.FromHours(24);
            }
            //turn computer clock back on to return to local time
            TSXLink.StarChart.SetClock(0, true);
            //return Find to current target
            //TSXLink.Target tReturn = TSXLink.StarChart.FindTarget(tPlan.TargetName);
        }

        public void SequenceGenerator()
        {
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            //Check to see if we have a valid target, if not, then just returnd
            if (!(TSXLink.StarChart.IsValidTarget(tPlan)))
            {
                lg.LogIt("Target Not Found");
                return;
            }
            //Get the configured filter set
            List<Filter> fSet = ImageFilterGroup();
            if (fSet == null)
            {
                lg.LogIt("Cannot sequence: No filters have been selected");
                MessageBox.Show("Cannot generation sequence: \r\nNo filters have been configured.", "Humason Error Report", MessageBoxButtons.OK);
                return;
            }

            //Figure out how many filters will be used
            int filters = fSet.Count;
            //Calculate number of frames, and redimension ImageFrames to that size
            int loops = tPlan.Loops;
            int ratio = tPlan.LRGBRatio;
            double exposure = tPlan.ImageExposureTime;
            double delay = tPlan.Delay;
            double overhead = tPlan.Overhead;

            //Convert the sequence parameters to timespan values
            int frames = loops * ((filters - 1) + ratio);
            TimeSpan duration = new TimeSpan(0, 0, (int)(frames * (exposure + delay + overhead)));  //in timespan

            //Set the starting time for the image sequence as the configured starting time, converted to UTC
            // and set the ending time for the image sequence as the current time + the duration of the sequence
            // and set the transit time for the image sequence as the current time - the target hour angle

            //DateTime sequenceStartUTC = UTCTime;
            //DateTime sequenceTransitUTC = UTCTime - TargetHA;
            DateTime sequenceEndUTC = UTCTime + duration;
            DateTime sequenceStartLocal = SiteLocalTime;
            DateTime sequenceEndLocal = TimeZoneInfo.ConvertTimeFromUtc(sequenceEndUTC, SiteLocalTZ);

            //Calculate the local time for the sequence to end, and the current date for the site
            DateTime siteDateLocal = new DateTime(SiteLocalTime.Year, SiteLocalTime.Month, SiteLocalTime.Day, 0, 0, 0);

            //Calculate the transit datetime by adding the 
            DateTime sequenceTransitLocal;
            if (TargetHA < TimeSpan.FromHours(0)) { sequenceTransitLocal = SiteLocalTime - TargetHA; }
            else { sequenceTransitLocal = SiteLocalTime + TimeSpan.FromHours(12) - TargetHA; }

            //Calculate the limit (minimum altitude) time by adding the local date and limit time of day
            //  if (the limit datetime is less than the current datetime,{ add a half day
            //  if the target never gets above the minimum then set to "never"
            DateTime sequenceLimitSettngLocal = siteDateLocal + TargetLimitSetting;
            DateTime sequenceLimitRisingLocal = siteDateLocal + TargetLimitRising;

            if (TargetLimitSetting == TimeSpan.FromHours(24)) { sequenceLimitSettngLocal = sequenceLimitSettngLocal.AddDays(2); }
            //check for astronomical twilight start is earlier than current time, if so add a day
            if (DateTime.Compare(SiteLocalTime, sequenceLimitSettngLocal) > 0) { sequenceLimitSettngLocal = sequenceLimitSettngLocal.AddDays(1); }
            //Calculate the twilight time by adding the local date and twilight start time of day
            //  if (the twilight datetime is less than the current datetime,{ add a day
            DateTime sequenceDawnLocal = siteDateLocal + ATwilightStart;
            //if astronomical twilight start is earlier than current time, add a day
            if (DateTime.Compare(SiteLocalTime, sequenceDawnLocal) > 0) { sequenceDawnLocal = sequenceDawnLocal.AddDays(1); }
            //Get the current local time and time zone, set the form start time value
            //if transit is after dawn, set daytime
            if (sequenceTransitLocal > sequenceDawnLocal) { TransitText = "Daytime"; }
            else { TransitText = sequenceTransitLocal.ToString("HH:mm"); }
            //if target lime  greater than 24 hours, then limit is never reached
            if (TargetLimitSetting == TimeSpan.FromHours(24)) { SetLimitText = "Never"; }
            else { SetLimitText = sequenceLimitSettngLocal.ToString("HH:mm"); }
            if (TargetLimitSetting == TimeSpan.FromHours(24)) { RiseLimitText = "Never"; }
            else { RiseLimitText = sequenceLimitRisingLocal.ToString("HH:mm"); }

            DawnDateTime = sequenceDawnLocal;

            //Construct relative time comparisions: start to dawn, end to dawn, flip to end, etc
            //int cStartToTwilight = DateTime.Compare(sequenceStartLocal, sequenceDawnLocal);
            //int cStartToLimit = DateTime.Compare(sequenceStartLocal, sequenceLimitSettngLocal);
            int cEndToDawn = DateTime.Compare(sequenceEndLocal, sequenceDawnLocal);          //Less than zero if end before dawn
            int cStartToFlip = DateTime.Compare(sequenceStartLocal, sequenceTransitLocal);       //Less than zero if start before transit
            int cFlipToEnd = DateTime.Compare(sequenceTransitLocal, sequenceEndLocal);           //Less than zero if transit before end
            int cEndToLimit = DateTime.Compare(sequenceEndLocal, sequenceLimitSettngLocal);        //Less than zero if end before limit 
            int cFlipToLimit = DateTime.Compare(sequenceTransitLocal, sequenceLimitSettngLocal);  //Less than zero if limit before transit
            int cFlipToDawn = DateTime.Compare(sequenceTransitLocal, sequenceDawnLocal);        //Less than zero if dawn before transit

            //Preset the flip and done textbox times to transit and end datetimes
            FlipText = sequenceTransitLocal.ToString("HH:mm");
            DoneText = sequenceEndLocal.ToString("HH:mm");
            //Store a text flag for done textbox (after dawn or after limit) if end is either after dawn or limit
            string doneFlag = "";
            if (cEndToDawn > 0) { doneFlag = "After Dawn"; }
            if (cEndToLimit > 0) { doneFlag = "After Limit"; }
            //Set flip textbox to N/A if flip is after dawn, after limit or after end of imaging
            if ((cFlipToDawn > 0) || (cFlipToLimit > 0) || (cFlipToEnd > 0))
            {
                if (cFlipToDawn > 0) { FlipText = "After Dawn"; }
                if (cFlipToLimit > 0) { FlipText = "After Limit"; }
                if (cFlipToEnd > 0) { FlipText = "None"; }
            }
            else
            //Otherwise, determine where the transit might be relative to start and end
            {
                if (cFlipToEnd > 0)
                //transit is after end of imaging so set flip textbox to None
                //  and set the done textbox to the end of imaging time, if not flagged for limit or dawn
                {
                    FlipText = "None";
                    if (doneFlag == "") { DoneText = sequenceEndLocal.ToString("HH:mm"); }
                    else { DoneText = doneFlag; }
                }
                else
                //transit is before the end of imaging
                //  check to see if it is after the start of imaging
                {
                    if (cStartToFlip < 0)
                    //transit is after start of imaging (Flip required)
                    //set the flip textbox to the transit time
                    // and set the done textbox to the end of imaging time, if not flagged for limit or dawn
                    {
                        FlipText = sequenceTransitLocal.ToString("HH:mm");
                        if (doneFlag == "") { DoneText = sequenceEndLocal.ToString("HH:mm"); }
                        else { DoneText = doneFlag; }
                    }
                    else
                    //transit is before start of imaging (Flip not required)
                    //set the flip textbox to None
                    // and set the done textbox to the end of imaging time, if not flagged for limit or dawn
                    {
                        FlipText = "None";
                        if (doneFlag == "") { DoneText = sequenceEndLocal.ToString("HH:mm"); }
                        else { DoneText = doneFlag; }
                    }
                }

            }
        }

        public bool SeriesGenerator()
        {
            //Method to populate imaging series control array from sequenceform
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            //Figure out how many filters will be used
            List<Filter> fSet = ImageFilterGroup();
            if (fSet == null)
            {
                return false;
            }
            int filters = fSet.Count;
            //Calculate number of frames, and redimension ImageFrames to that size
            int loops = tPlan.Loops;
            int ratio = tPlan.LRGBRatio;
            double exposure = tPlan.ImageExposureTime;
            double delay = tPlan.Delay;

            int frames = loops * ((filters - 1) + ratio);
            //create array with five members: filter index, binning, exposure, frame type, delay
            ImageSeries = new int[frames, 5];
            //Loop on the number of loops
            int imageidx = 0;
            //loop on the number of image loops to make
            for (int lp = 0; lp < loops; lp++)
            {
                //Loop on the filters in fSet, repeating as set in the x.Repeat
                foreach (Filter filter in fSet)
                {
                    for (int i = 0; i < filter.Repeat; i++)
                    {
                        ImageSeries[imageidx, si_Filter] = filter.Index;
                        ImageSeries[imageidx, si_Binning] = sb_1x1;
                        ImageSeries[imageidx, si_Exposure] = (int)exposure;
                        ImageSeries[imageidx, si_Frame] = (int)TheSky64Lib.ccdsoftImageFrame.cdLight;
                        ImageSeries[imageidx, si_Delay] = (int)delay;
                        imageidx += 1;
                    }
                }
            }
            //Set the progress bar granularity
            Progress_Percent = (100.0 / frames);
            return true;
        }

        public void PhotoShoot()
        {
            //Determine the side of pier at the start
            //Run image series of photos
            //Walk through the array, filter by filter, until no shot is taken, or weather problem
            //if (the side of pier changes,{ meridian flip.
            //  This version assumes that the exposure time is less than the
            //  over shoot
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            //Prepare for focusing, if (selected
            //Save the current temperature
            double lastFocusTemperature = TSXLink.Focus.GetTemperature();
            if (tPlan.AutoFocusEnabled)
            {
                if (!RunAutoFocus())
                {
                    lg.LogIt("Autofocus failed during sequencing");
                    return;
                }
            }
            //If autoguiding is checked and calibration requested, then calibrate the guider
            if (tPlan.AutoGuideEnabled && tPlan.GuiderCalibrateEnabled)
            { AutoGuiding.CalibrateAutoguiding(tPlan.GuiderSubframeEnabled, tPlan.XAxisMoveTime, tPlan.YAxisMoveTime); }

            //Set up for shoot loop:
            //For each filter column,
            // if (any exposures left to take,
            //   Set camera to the filter type
            //   Set camera to the frame type
            //   Set camera exposure length
            //   Take picture (asynchronous)
            //   Increment the picture count
            // go to the next column, or back to the first if (at the ene (#of of filters)
            //quit if (no exposures were made.

            // Do the loop with an asynchronous takeimage so that one can opt out if (necessary.
            // Poll the camera occasionally to check for completion.
            //
            double seriesprogress = 0;
            ProgressEvent pgrEvent = new ProgressEvent();
            pgrEvent.ProgressIt((int)Math.Ceiling(seriesprogress));
            int totalImageCount = ImageSeries.GetUpperBound(0) + 1;
            //Assuming all is good, CLS the 
            for (int frmdef = 0; frmdef < totalImageCount; frmdef++)
            {
                //Check weather:  If the weather monitor deems the weather "unsafe"
                //  then close the dome and break from this imaging sequence.
                //  the assumption will be that if there are more targets, then they
                //  too will hit the weather unsafe condition and break out.
                //  After all targets have cleared, then the normal shut down will proceed.
                //  Mount will be returned to current target position if Park'ed during the interrum
                if (openSession.IsWeatherEnabled)
                {
                    WeatherReader wrf = new WeatherReader(openSession.WeatherDataFilePath);
                    if (!wrf.IsWeatherSafe())  //Unsafe condition
                    {
                        //Unsafe weather condition:
                        //  Park telescope
                        //  Home dome
                        //  Close dome
                        lg.LogIt("Waiting on unsafe weather conditions...");
                        lg.LogIt("Parking telescope, if park is enabled");
                        TSXLink.Mount.Park();
                        if (openSession.IsDomeAddOnEnabled)
                        {
                            //Close dome -- dome is homed before closing
                            // Note that the mount will be left in the Park position
                            lg.LogIt("Closing Dome");
                            TSXLink.Dome.CloseDome();
                        }
                        do
                        //Wait for conditions to improve by running a five minute wait
                        // but enable the form for input, ect every second
                        {
                            for (int i = 0; i < 300; i++) //Five minute wait loop
                            {
                                Application.DoEvents();
                                System.Threading.Thread.Sleep(1000);  //one second wait loop
                                //Check for shutdown time
                                if (LaunchPad.IsTimeToShutDown())
                                { break; };
                                //Check for abort
                                if (FormHumason.IsAborting())
                                {
                                    lg.LogIt("Sequence aborted");
                                    break;
                                }
                            }
                        } while (!wrf.IsWeatherSafe());
                        if (LaunchPad.IsTimeToShutDown()) { break; };
                        if (wrf.IsWeatherSafe())
                        {
                            lg.LogIt("Weather conditions safe");
                            if (openSession.IsDomeAddOnEnabled)
                            {
                                lg.LogIt("Opening Dome");
                                TSXLink.Dome.OpenDome();
                            }
                            lg.LogIt("Unparking telescope");
                            TSXLink.Mount.UnPark();
                            //CLS to target.  If fails, then the weather must be bad (or something worse) so aboart
                            if (!CLSToTargetPlanCoordinates())
                            {
                                FormHumason.SetAbort();
                                break;
                            };
                        }
                    }
                }
                //Check for abort, again
                if (FormHumason.IsAborting())
                {
                    lg.LogIt("Sequence aborted");
                    break;
                }
                //Check for shut down time
                if (LaunchPad.IsTimeToShutDown())
                {
                    lg.LogIt("Exceeded Shut Down Time");
                    break;
                }
                //Check for minimum altitude
                if (TSXLink.Mount.Alt < openSession.MinimumAltitude)
                {
                    lg.LogIt("Target Too Low");
                    break;
                }
                //Check for refocus -- if (the current temperature is more than a degree greater than the last temperature,{ refocus
                lastFocusTemperature = CheckAutoFocus(lastFocusTemperature);
                lg.LogIt("Last autofocus temperature at: " + lastFocusTemperature.ToString("0.0") + " degC");
                lg.LogIt("Current focuser temperature at: " + TSXLink.Focus.GetTemperature().ToString("0.0") + " degC");

                //Store the current location
                CacheTargetSideOfMeridian();

                //Check for passing the meridian, either direction
                //if (target is currently on the west side of meridian and the mount Side of Pier is also east, then flip east to west
                if (IsTargetWest())
                { //target is West, check for OTA on West side, if so, then flip
                  //
                  // Beyond The Pole = False = Pier E
                  // Beyond The Pole = True = Pier W
                  //
                    if (TSXLink.Mount.BeyondThePole == TSXLink.Mount.SOP.PierWest)
                    {
                        MeridianFlipper(true);   //OTA flips to east side of pier
                    }
                }
                else //target is East, check for OTA on East side, if so, then flip
                {
                    if (TSXLink.Mount.BeyondThePole == TSXLink.Mount.SOP.PierEast)
                    {
                        MeridianFlipper(false); //OTA goes to west side of pier
                    }
                }

                //if AutoGuide is selected:
                //if resync is enabled, then stop, resync and restart autoguiding
                // otherwise,  if this is frame 0, then start autoguiding
                // Note that if trying to start autoguiding fails, then we will do a 
                // precision slew to the target coordinates before continuing just
                // to make sure that we haven't drifted off somewhere.
                // If the CLS fails, then we might as well abort.
                //If Autoguide is not enabled, then we must be running unguided on purpose.
                //  if so (and the user wants to resync), 
                //      make a precision slew in order to make up for any incremental drift
                //  that has occurred since starting the last frame.
                //if the CLS fails, then we might as well abort
                //If ReSync is not enabled, then this is our last chance to slew to the target before imaging
                //  on the first image.  The rest will follow accordingly.  if the CLS fails, then we might as well abort
                if (tPlan.AutoGuideEnabled)
                {
                    if (tPlan.ResyncEnabled)
                    {
                        StopAutoguiding();
                        if (!StartAutoguiding())
                        {
                            if (!CLSToTargetPlanCoordinates())
                            {
                                FormHumason.SetAbort();
                                break;
                            };
                        }
                    }
                    else
                    {
                        //If not the first frame in a series, then autoguiding should be already running, 
                        //    or it was stopped because of several possible reasons. 
                        //  StartAutoguiding will return true if either autoguiding is already running
                        //     or if autoguiding was successfully started up.
                        //If not already running or successfully started (StartAutoguiding returns false),
                        //    thenjust CLS to the target to center up
                        //If the CLS fails, then we might as well abort
                        if (!StartAutoguiding())
                        {
                            if (!CLSToTargetPlanCoordinates())
                            {
                                FormHumason.SetAbort();
                                break;
                            };
                        }
                    }
                }
                else if (tPlan.ResyncEnabled)  //(and Autoguide is not enabled)
                {
                    if (!CLSToTargetPlanCoordinates())
                    {
                        FormHumason.SetAbort();
                        break;
                    }
                }
                else if (frmdef == 0) //(and Autoguide is not enabled, and ReSync is not enabled)
                    if (!CLSToTargetPlanCoordinates())
                    {
                        FormHumason.SetAbort();
                        break;
                    }

                //Check for SmallSolarSystemObject
                //  if so, then set the tracking rates based on the sequence deltaRA and deltaDec values, if any
                if (tPlan.SmallSolarSystemBodyEnabled)
                {
                    TSXLink.Mount.SetSpecialTracking(tPlan.DeltaRARate, tPlan.DeltaDecRate);
                }
                //Set up for light shot: 
                //  Store the target position (for checking for flip)
                //  Set filter from the image series
                //  Set exposure from the image series
                //  Set delay from the image series
                //  Set TSX for an asychronous photo
                //  Set the image type to Light

                AstroImage asti = new AstroImage
                {
                    Camera = AstroImage.CameraType.Imaging,
                    ImageReduction = (AstroImage.ReductionType)openSession.ImageReductionType,
                    Exposure = ImageSeries[frmdef, si_Exposure],
                    BinX = 1,//set binning to 1x1
                    BinY = 1,
                    Filter = ImageSeries[frmdef, si_Filter],
                    Delay = ImageSeries[frmdef, si_Delay],
                    Frame = (AstroImage.ImageType)ImageSeries[frmdef, si_Frame],
                    AutoSave = openSession.UseTSXAutoSave
                };
                lg.LogIt("Imaging Filter " + asti.Filter.ToString() +
                                    " @ " + asti.Exposure.ToString() + " sec " +
                                    "(# " + (frmdef + 1).ToString("0") + " of " + totalImageCount.ToString("0") + ")");
                //Save the start time for the overhead calculation a bit later
                DateTime imageStart = DateTime.Now;

                //Start the imaging
                Imaging imgo = new Imaging();
                int camResult = imgo.TakeLightFrame(asti);
                //Check for a user abort
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                //Check for abort
                if (FormHumason.IsAborting())
                {
                    lg.LogIt("Sequence aborted by user");
                    TSXLink.Camera tcam = new TSXLink.Camera(asti);
                    tcam.CameraAbort();
                    StopAutoguiding();
                    return;
                }
                //Check for successful image.  If not then abort imaging (synchronous) and just continue
                //  Otherwise save image
                if (camResult != 0)
                {
                    lg.LogIt("Imaging Error: " + camResult.ToString());
                    TSXLink.Camera tcam = new TSXLink.Camera(asti);
                    tcam.CameraAbort();
                }
                else
                {
                    //Save the image before doing anything else
                    //Gather substrings for file naming: target name, filter name, filter index,image position angle and rotator position angle
                    string tname = tPlan.TargetName;
                    string tfilterName = GetFilterName(asti.Filter);
                    int tfilterIndex = GetFilterIndex(asti.Filter);
                    string tPA = tPlan.TargetPA.ToString();
                    string rPA = Rotator.RealRotatorPA.ToString(); // zero if no rotator connected
                                                                   //Figure out the current side of pier, save with filename and save for flatfile operation, if any
                    string flatSide;
                    if (LastTargetSideWest)
                    {
                        if (openSession.UseTSXAutoSave == 0)
                            ImageFileManager.SaveLightImage(tname, tfilterName, tPA, "W");
                        flatSide = "West";
                    }
                    else
                    {
                        if (openSession.UseTSXAutoSave == 0)
                            ImageFileManager.SaveLightImage(tname, tfilterName, tPA, "E");
                        flatSide = "East";
                    }
                    //Save a flats requirement in the configuration file
                    //Check for flat definition in configuration, add if not
                    if (tPlan.MakeFlatsEnabled)
                    {
                        int flatRepetitions = openSession.FlatsRepetitions;
                        Filter fFilter = new Filter(tfilterName, tfilterIndex, flatRepetitions);
                        Flat imgFlat = new Flat(tname, flatSide, Convert.ToDouble(rPA), fFilter, tPlan.RotatorEnabled);
                        FlatManager flm = new FlatManager();
                        flm.AddFlat(imgFlat);
                    }
                }
                //Image complete. Increment the count for this filter in the image series array.
                // Set the shot flag as true to loop back through.
                // Update the progress bar and increment the shot count (remember that seriesprogress is zero-based)
                seriesprogress = seriesprogress + Progress_Percent;
                pgrEvent.ProgressIt((int)Math.Ceiling(seriesprogress));
                //Calculate and save the overhead duration
                DateTime imageEnd = DateTime.Now;
                TimeSpan imageDuration = imageEnd - imageStart;
                double overhead = imageDuration.TotalSeconds - asti.Exposure;
                if (overhead >= 0 && overhead < tPlan.ImageExposureTime)
                    tPlan.Overhead = overhead;
                else
                    tPlan.Overhead = 0;

                //  Next image:  loop back to start
            }
            //Clean up, starting with letting the forms update
            Application.DoEvents();
            lg.LogIt("Imaging Session Done");
            if (tPlan.AutoGuideEnabled)
            {
                StopAutoguiding();
            }
            if (tPlan.SmallSolarSystemBodyEnabled)
            {
                TSXLink.Mount.ResetSpecialTracking();
            }
        }

        public bool MeridianFlipper(bool WestOTAtoEastOTA)
        {
            //
            //This routine executes a meridian flip and resumes session and imaging.
            //Flip is from west SOP to eastSOP if WestOTAtoAEastOTA is true
            //
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            if (!openSession.IsMeridianFlipEnabled)
            {
                lg.LogIt("Meridian flip conditions detected, but not enabled.\r\n  User intervention required.");
                //Possibly take some action here -- Abort?
                return false;
            }

            lg.LogIt("Meridian Flip Underway");
            // stop guiding, if (on
            if (tPlan.AutoGuideEnabled) { AutoGuiding.AutoGuideStop(); }

            //if (WestOTAtoEastOTA is true,{ first slew the scope to point to the west hemisphere, or the inverse
            // in order to imitate an ASCOM "Flip" command
            if (WestOTAtoEastOTA)
            {
                lg.LogIt("Slewing to point towards West from the East side)");
                TSXLink.Mount.SlewAzAlt(270, 45, "");
            }
            else
            {
                lg.LogIt("Slewing to point towards East from the West side");
                TSXLink.Mount.SlewAzAlt(90, 45, "");
            }

            //finish flip by finding the target again
            //Relocate to target with a CLS
            //If the CLS fails, then we might as well abort
            lg.LogIt("CLS to target after flip");
            if (!CLSToTargetPlanCoordinates())
            {
                FormHumason.SetAbort();
                return false;
            };

            //Rotate the camera, if enabled
            //Now lets get the rotator positioned properly, plate solve, then rotate, then plate solve
            if (tPlan.RotatorEnabled)
            {
                lg.LogIt("Rotating to new PA @ " + tPlan.TargetPA.ToString("0.00"));
                //If rotation fails, set abort and flee
                if (!Rotator.RotateToImagePA(tPlan.TargetPA))
                {
                    lg.LogIt("Failed rotation");
                    FormHumason.SetAbort();
                    return false;
                };
                // Because rotation may not be quite symmetrical, do another CLS to make sure
                //  the guide star and target is still centered
                lg.LogIt("CLS to center target after rotation");
                if (!CLSToTargetPlanCoordinates())
                {
                    lg.LogIt("Failed to center target after rotation");
                    FormHumason.SetAbort();
                    return false;
                };
                //
                // Before we go, because of an apparent TSX AO bug, must recalibrate guider if using AO
                //
                if (tPlan.RecalibrateAfterFlipEnabled && tPlan.AutoGuideEnabled && tPlan.AOEnabled)
                { AutoGuiding.CalibrateAutoguiding(tPlan.GuiderSubframeEnabled, tPlan.XAxisMoveTime, tPlan.YAxisMoveTime); }
                //
                //////////////////////
            }
            //All done
            lg.LogIt("Meridian Flip Completed");
            return true;
        }

        private bool IsTargetWest()
        {
            // returns true if (the current target is west of the meridian, 
            //  i.e. East of meridian is 0 to 180 degees
            //  West of meridian is 180 to 360 degrees
            //Assume that the scope is connected
            //Get Altitude and Azimuth
            if (TSXLink.Mount.Azm >= 180)
            { //target is west of meridian
                return (true);
            }
            else
            { //target is east of meridian
                return (false);
            }
        }

        private TimeSpan EarliestTargetView()
        {
            //Function to compute earliest time of this night that the target can be imaged at this location
            //Base Formula:   hourangle = arccos((sin(altitude) - (sin(latitude)*sin(dec))/(cos(latitude)*(cos(dec)))
            //  Looking for a solution at minAltitude
            TimeSpan targetRiseTime = ATwilightEnd;  //Default if (never sets
            return (targetRiseTime);
        }

        private TimeSpan LatestTargetView()
        {
            //Function to compute last time of this night that the target can be imaged at this location
            TimeSpan targetSetTime = ATwilightStart; //Default if (never sets
            return (targetSetTime);
        }

        /// <summary>
        /// Computes the hours after (and before) transit that the target crosses the minimum altitude
        /// </summary>
        /// <returns>TimeSpan in hours</returns>
        private TimeSpan CalcMinAltHourAngle()
        {
            SessionControl openSession = new SessionControl();
            double TelescopeLimit = openSession.MinimumAltitude;
            //Create celestial ra/dec and terretrial lat/lon coordinate objects
            AstroMath.Celestial.RADec tSpot = new AstroMath.Celestial.RADec(AstroMath.Transform.DegreesToRadians(TargetRA), AstroMath.Transform.DegreesToRadians(TargetDec));
            AstroMath.Celestial.LatLon tLoc = new AstroMath.Celestial.LatLon(SiteLat, SiteLon);
            //Calculate the altitude at zero hour angle (meridian)
            //Return 0 if the target never makes it to the minimum altitude
            double meridianAltD = AstroMath.Transform.RadiansToDegrees(tSpot.Altitude(0.0, tLoc));
            if (meridianAltD < TelescopeLimit)
                return TimeSpan.FromHours(0);

            TimeSpan hoursToMinAlt;
            // If rise and set are zero, then the target never sets and worse, never drops below the telescope limit
            //   but it doesnt matter, it seems
            if ((TargetRise == TimeSpan.FromHours(0)) && (TargetSet == TimeSpan.FromHours(0)))
            {
                hoursToMinAlt = TimeSpan.FromHours(0);
            }
            else
            {
                hoursToMinAlt = TimeSpan.FromHours(tSpot.HourAngle(AstroMath.Transform.DegreesToRadians(TelescopeLimit), tLoc));
            }
            return hoursToMinAlt;
        }

        public bool CLSToTargetPlanCoordinates()
        {
            //Generates a TSX closed loop slew to the current target
            // as defined in the configuration file
            // in this case we will use the RA,Dec version of find
            // just in case this target RA/Dec has been shifted for the shot
            //Make sure the mount is connected

            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);

            int clsstat = 0;
            //Save current camera parameters
            //TTUtility.SaveTSXState();
            //Create TSX Objects
            string suffix = "";
            if (tPlan.TargetAdjustEnabled)
            {
                suffix = "  (Adj)";
            }

            lg.LogIt("Initiating coordinate CLS to " + tPlan.TargetName + suffix);
            //perform a find to the target RA and Dec.  This  sets up the CLS target parameters
            string sRA = tPlan.TargetRA.ToString();
            string sDec = tPlan.TargetDec.ToString();
            string sRADecName = sRA + "," + sDec;
            string targetPosition = sRADecName;
            TSXLink.StarChart.FindTarget(targetPosition);
            //
            //
            //TSX has some problems with letting the dome catch up with the telescope in CLS mode
            //  So, as a work around, slew to the coordinates synchronously, then do the CLS
            //
            //Set the exposure, filter and reduction, unless already set up
            AstroImage asti = new AstroImage
            {
                Camera = AstroImage.CameraType.Imaging,
                ImageReduction = (AstroImage.ReductionType)openSession.ImageReductionType,
                TargetName = sRADecName,
                SubFrame = 0,
                Filter = tPlan.ClearFilter,
                Exposure = tPlan.PlateSolveExposureTime,
                Delay = 0,
                AutoSave = 1
            };
            //Launch CLS 
            clsstat = TSXLink.ImageSolution.PrecisionSlew(asti);
            //If it fails, take one more shot at it
            if (clsstat != 0)
            {
                //Launch CLS -- again
                lg.LogIt("CLS Failed: Error " + clsstat.ToString() + " -- trying again");
                clsstat = TSXLink.ImageSolution.PrecisionSlew(asti);
            }
            //If it fails again, then just end it
            if (clsstat != 0)
            {
                lg.LogIt("CLS Failed: " + clsstat.ToString());
                return false;
            }
            else
            {
                lg.LogIt("Closed Loop Slew Successful");
                return true;
            }
        }

        private void CacheTargetSideOfMeridian()
        {
            //Store the location of the target prior to imaging

            LogEvent lg = new LogEvent();

            if (!IsTargetWest())
            {  //target is east of meridian
                lg.LogIt("Target is east of meridian");
                LastTargetSideWest = false;
            }
            else
            {   //target is west of meridian
                lg.LogIt("Target is west of meridian");
                LastTargetSideWest = true;
            }
        }

        private bool StartAutoguiding()
        {
            //Find guidestar, optimize exposure and turn on autoguiding
            //   returns true if autoguiding was successfully prep'ed up and turned on
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            LogEvent lg = new LogEvent();
            lg.LogIt("Checking to see if autoguiding is already running");
            if (AutoGuiding.IsAutoGuideOn())
            {
                lg.LogIt("Autoguider is already runnning");
                return true;
            }

            lg.LogIt("Autoguide is off.  Starting autoguiding");
            //Try to find guide star, if not found
            //  then if the current exposure less than the maximum exposure,
            //    then double it or set it to the max exposure, whichever is less.
            //    and try again.

            lg.LogIt("Attempting to find guide star");
            while (!AutoGuiding.SetAutoGuideStar())
            {
                if (tPlan.GuideExposure == tPlan.MaximumGuiderExposure)
                {
                    lg.LogIt("Cannot find a guide star at maximum exposure -- running unguided");
                    return false;
                }
                else
                {
                    lg.LogIt("No guide star: increasing exposure if possible");
                    tPlan.GuideExposure = Math.Min(tPlan.GuideExposure * 2, tPlan.MaximumGuiderExposure);
                }
            }

            //Got a star
            lg.LogIt("Determining optimal guide camera exposure");
            double agExposure = AutoGuiding.OptimizeExposure();
            FormHumason.fGuideForm.GuideExposureTimeBox.Value = (decimal)agExposure;
            tPlan.GuideExposure = agExposure;
            if (tPlan.DitherEnabled)
            {
                if (!AutoGuiding.DitherAndStart())
                {
                    lg.LogIt("Dither Failed: Runninng unguided");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                AutoGuiding.AutoGuideStart();
                return true;
            }

        }

        private void StopAutoguiding()
        {
            //Turn off autoguiding
            LogEvent lg = new LogEvent();
            lg.LogIt("Stopping autoguider");
            AutoGuiding.AutoGuideStop();
        }

        private double CheckAutoFocus(double lastFocusTemp)
        {
            //Checks for need to refocus();
            //Get Maximum Temperature Variation value from the Session database
            SessionControl openSession = new SessionControl();
            double atDiff = openSession.RefocusAtTemperatureDifference;
            if (Math.Abs(TSXLink.Focus.GetTemperature() - lastFocusTemp) >= atDiff)
            {
                //Run autofocus fails, just return like nothing happened.
                // If it was because of a failed CLS, then an Abort will have been filed
                //  Otherwise just let the current focus stay
                if (!RunAutoFocus())
                    lastFocusTemp = TSXLink.Focus.GetTemperature();
                else
                    lastFocusTemp = TSXLink.Focus.GetTemperature();
            }
            return lastFocusTemp;
        }

        private bool RunAutoFocus()
        {
            //Sets up to run either atfocus2 or 3 based on configuration
            //  Save state, selects at focus type, runs it, restores state and collects
            //  **** Turn off Autoguiding, assuming it is on
            // Return true if successful, false otherwise
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            LogEvent lg = new LogEvent();
            //TTUtility.SaveTSXState(tPlan.getitem);
            //Filter chosen for autofocus
            lg.LogIt("Running @Focus" + tPlan.AtFocusSelect.ToString("0"));
            StopAutoguiding();
            AutoFocus.FocusIt(tPlan.AtFocusSelect);
            if (tPlan.AtFocusSelect == 2)
            {
                //@focus2 will probably need to let us CLS back to the target
                lg.LogIt("CLS back to target after @Focus2");
                if (!CLSToTargetPlanCoordinates())
                {
                    lg.LogIt("Could not CLS after @Focus2.  Aborting.");
                    FormHumason.SetAbort();
                    return false;
                };
            }
            return true;
        }

        private List<Filter> ImageFilterGroup()
        {
            //Creates an array of filter indexes one loop of imaging
            //If the LRGB Ratio is set to greater than 1, then the "Clear" filter is replicated
            //for that number of times.
            //
            //For each filter in the filter list, add the filter index to the output array,
            //  make that LRGBRatio times for the ClearFilter.
            SessionControl openSession = new SessionControl();
            TargetPlan tPlan = new TargetPlan(openSession.CurrentTargetName);
            List<Filter> filterGroup = tPlan.FilterWheelList;
            int repeatFilter = tPlan.ClearFilter;
            int repeats = tPlan.LRGBRatio;
            if (filterGroup != null)
            {
                foreach (Filter f in filterGroup)
                {
                    if (f.Index == repeatFilter)
                    {
                        f.Repeat = repeats;
                        break;
                    }
                }
            }
            else { filterGroup = null; }
            return filterGroup;
        }

        private string GetFilterName(int fidx)
        {
            //Read the configured filter set and return the TSX filter name for the
            //  fidx'th filter in the filter set.  Return null if not there.
            List<Filter> fSet = ImageFilterGroup();
            if (fSet == null)
            {
                return null;
            }

            foreach (Filter f in fSet)
            {
                if (f.Index == fidx)
                {
                    return f.Name;
                }
            }
            return null;
        }

        private int GetFilterIndex(int fidx)
        {
            //Read the configured filter set and return the TSX filter index number for the
            //  fidx'th filter in the filter set.  Return 0 if not there.
            List<Filter> fSet = ImageFilterGroup();
            if (fSet == null)
            {
                return 0;
            }

            foreach (Filter f in fSet)
            {
                if (f.Index == fidx)
                {
                    return f.Index;
                }
            }
            return 0;
        }


    }
}



