using Humason;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TheSkyXLib;

//Library of methods and data for interfacing to TSX through .NET (COM) library
namespace Planetarium
{
    public partial class TSXLink
    {

        private static bool AbortFlag { get; set; } = false;

        private void AbortReportEvent_Handler(object sender, AbortEvent.AbortEventArgs e)
        {
            AbortFlag = true;
            return;
        }

        #region Connection Class

        public static class Connection
        {

            public enum Devices
            {
                Mount,
                Camera,
                Guider,
                Focuser,
                Rotator,
                Dome
            }

            public static void DeployMount()
            {
                //Connect, Home and Park mount
                LogEvent lg = new LogEvent();
                SessionControl openSession = new SessionControl();
                sky6RASCOMTele tsxt = new sky6RASCOMTele();

                lg.LogIt("Connecting to Mount");
                //Assume mount is "live"
                try
                { tsxt.ConnectAndDoNotUnpark(); }
                catch
                {
                    lg.LogIt("Aborting.  Attempt to connect to mount failed.");
                    return;
                }
                lg.LogIt("Finding Mount Home");
                tsxt.FindHome();
                if (openSession.IsParkMountEnabled)
                {
                    lg.LogIt("Parking Mount");
                    tsxt.Park();
                }
                return;
            }

            public static void SecureMount()
            {
                //Shuts everything down 
                //Connect to mount but no unpark (if parked)
                //Park mount
                //Disconnect
                LogEvent lg = new LogEvent();
                SessionControl openSession = new SessionControl();
                sky6RASCOMTele tsxt = new sky6RASCOMTele();
                if (openSession.IsParkMountEnabled)
                {
                    lg.LogIt("Parking Mount");
                    tsxt.Connect();
                    tsxt.Park();
                }
                lg.LogIt("Disconconnecting Mount");
                tsxt.Disconnect();
                return;
            }

            public static void ConnectDevice(Devices device)
            {
                //Connects TSX telescope, camera and guider devices individually
                //  if  device is 0,) { connect telescope
                //  if  device is 1,) { connect camera
                //  if  device is 2,) { connect guider
                //  if  device is 3,) { connect focuser
                //  if  device is 4,) { connect rotator
                //  if  device is 5,) { connect dome

                LogEvent lg = new LogEvent();
                SessionControl openSession = new SessionControl();

                //  For each device, set a status in the TSX Wrap window, try { to connect, set error status (if  fail), move on...
                //  return False with first failing connection, return True otherwise

                int status = 0;
                switch (device)
                {
                    case Devices.Mount:
                        sky6RASCOMTele tsxm = new sky6RASCOMTele();
                        lg.LogIt("Connecting mount");
                        tsxm.Asynchronous = 0;
                        try { tsxm.Connect(); }
                        catch (Exception ex) { lg.LogIt("Connecting Mount Failed: " + ex.Message); }
                        break;

                    case Devices.Camera:
                        ccdsoftCamera tsxc = new ccdsoftCamera();
                        lg.LogIt("Connecting camera");
                        tsxc.Asynchronous = 0;
                        try { status = tsxc.Connect(); }
                        catch (Exception ex) { lg.LogIt("Connecting Camera Failed: " + ex.Message); }
                        break;

                    case Devices.Guider:
                        ccdsoftCamera tsxg = new ccdsoftCamera();
                        lg.LogIt("Connecting guider");
                        tsxg.Autoguider = 1;
                        tsxg.Asynchronous = 0;
                        try { status = tsxg.Connect(); }
                        catch (Exception ex) { lg.LogIt("Connecting Guider Failed: " + ex.Message); }
                        break;

                    case Devices.Focuser:
                        ccdsoftCamera tsxf = new ccdsoftCamera();
                        lg.LogIt("Connecting focuser");
                        tsxf.Asynchronous = 0;
                        try { status = tsxf.focConnect(); }
                        catch (Exception ex) { lg.LogIt("Connecting Focuser Failed: " + ex.Message); }
                        break;

                    case Devices.Rotator:
                        ccdsoftCamera tsxr = new ccdsoftCamera();
                        lg.LogIt("Connecting rotator");
                        tsxr.Asynchronous = 0;
                        try { status = tsxr.rotatorConnect(); }
                        catch (Exception ex) { lg.LogIt("Connecting Rotator Failed: " + ex.Message); }
                        break;

                    case Devices.Dome:
                        if (openSession.IsDomeAddOnEnabled)
                        {
                            sky6Dome tsxd = new sky6Dome();
                            lg.LogIt("Connecting Dome");
                            try { tsxd.Connect(); }
                            catch (Exception ex)
                            {
                                lg.LogIt("Connecting Dome Failed: " + ex.Message);
                                return;
                            }
                            tsxd.IsCoupled = 1;
                            lg.LogIt("Coupling Dome to Mount");
                        }
                        break;

                    default:
                        break;
                }
                return;
            }

            public static void DisconnectDevice(Devices device)
            {
                //Disconnects TSX telescope, camera and guider devices individually
                //  if  device is 0,) { connect telescope
                //  if  device is 1,) { connect camera
                //  if  device is 2,) { connect guider
                //  if  device is 3,) { connect focuser
                //  if  device is 4,) { connect rotator
                //  if  device is 5,) { connect dome

                //  For each device, set a status in the TSX Wrap window, try { to disconnect, set error status (if  fail), move on...
                //  return False with first failing connection, return True otherwise

                LogEvent lg = new LogEvent();
                SessionControl openSession = new SessionControl();
                int status = 0;
                switch (device)
                {
                    case Devices.Mount:
                        sky6RASCOMTheSky tsxm = new sky6RASCOMTheSky();
                        lg.LogIt("Disconnecting mount");
                        try { tsxm.DisconnectTelescope(); }
                        catch (Exception ex) { lg.LogIt("Disconnecting Mount Failed: " + ex.Message); }
                        break;

                    case Devices.Camera:
                        ccdsoftCamera tsxc = new ccdsoftCamera();
                        lg.LogIt("Disconnecting camera");
                        tsxc.Abort();
                        tsxc.ShutDownTemperatureRegulationOnDisconnect = 1;
                        tsxc.Asynchronous = 0;
                        try { tsxc.Disconnect(); }
                        catch (Exception ex) { lg.LogIt("Connecting Camera Failed: " + ex.Message); }
                        break;

                    case Devices.Guider:
                        ccdsoftCamera tsxg = new ccdsoftCamera();
                        lg.LogIt("Disconnecting guider");
                        tsxg.Autoguider = 1;
                        //Abort Guider Imaging, 
                        try { tsxg.Asynchronous = 0; }
                        catch (Exception ex) { lg.LogIt("Guider Asynchronous Command Failed: " + ex.Message); return; }
                        tsxg.Abort();
                        try { status = tsxg.Disconnect(); }
                        catch (Exception ex) { lg.LogIt("Disconnecting Guider Failed: " + ex.Message); }
                        break;

                    case Devices.Focuser:
                        lg.LogIt("Disconnecting focuser");
                        ccdsoftCamera tsxf = new ccdsoftCamera();
                        try { tsxf.Asynchronous = 0; }
                        catch (Exception ex) { lg.LogIt("Asynchronous Focuser Command Failed: " + ex.Message); }
                        try { status = tsxf.focDisconnect(); }
                        catch (Exception ex) { lg.LogIt("Disconnecting Focuesr Failed: " + ex.Message); }
                        break;

                    case Devices.Rotator:
                        lg.LogIt("Disconnecting Rotator");
                        ccdsoftCamera tsxr = new ccdsoftCamera();
                        try { tsxr.Asynchronous = 0; }
                        catch (Exception ex) { lg.LogIt("Asynchronous Rotator Command Failed: " + ex.Message); }
                        try { status = tsxr.rotatorDisconnect(); }
                        catch (Exception ex) { lg.LogIt("Disconnecting Rotator Failed: " + ex.Message); }
                        break;

                    case Devices.Dome:
                        if (openSession.IsDomeAddOnEnabled)
                        {
                            lg.LogIt("Disconnecting Dome");
                            sky6Dome tsxd = new sky6Dome();
                            lg.LogIt("Decoupling Dome from Mount");
                            try { tsxd.IsCoupled = 0; }
                            catch (Exception ex)
                            { lg.LogIt("Decoupling Dome failed" + ex.Message); }
                            try { tsxd.Disconnect(); }
                            catch (Exception ex)
                            {
                                lg.LogIt("Disconnecting Rotator Failed: " + ex.Message);
                                return;
                            }
                        }
                        break;
                    default:
                        break;
                }
                return;
            }

            public static bool IsConnected(Devices device)
            {
                //Connects TSX telescope, camera and guider devices individually
                //  if  device is 0,) { check telescope
                //  if  device is 1,) { check camera
                //  if  device is 2,) { check guider
                //  if  device is 3,) { check focuser
                //  if  device is 4,) { c rotator
                //  if  device is 5,) { c dome

                LogEvent lg = new LogEvent();

                //  For each device, set a status in the TSX Wrap window, try { to connect, set error status (if  fail), move on...
                //  return False with first failing connection, return True otherwise

                int status = 0;
                switch (device)
                {
                    case Devices.Mount:
                        sky6RASCOMTele tsxm = new sky6RASCOMTele();
                        lg.LogIt("Checking mount connections");
                        status = tsxm.IsConnected;
                        break;

                    case Devices.Camera:
                        ccdsoftCamera tsxc = new ccdsoftCamera();
                        lg.LogIt("Checking camera");
                        status = tsxc.Connect();
                        break;

                    case Devices.Guider:
                        ccdsoftCamera tsxg = new ccdsoftCamera();
                        lg.LogIt("Checking guider");
                        tsxg.Autoguider = 1;
                        status = tsxg.Connect();
                        break;

                    case Devices.Focuser:
                        ccdsoftCamera tsxf = new ccdsoftCamera();
                        lg.LogIt("Checking focuser");
                        status = tsxf.focIsConnected;
                        break;

                    case Devices.Rotator:
                        ccdsoftCamera tsxr = new ccdsoftCamera();
                        lg.LogIt("Checking rotator");
                        status = tsxr.rotatorIsConnected();
                        break;

                    case Devices.Dome:
                        sky6Dome tsxd = new sky6Dome();
                        lg.LogIt("Checking dome");
                        status = tsxd.IsConnected;
                        break;

                    default:
                        break;
                }
                if (status == 0)
                { return false; }
                else
                { return true; }
            }

            public static void ConnectAllDevices()
            {
                //Open objects for telescope, camera and guider
                //  For each device, set a status in the TSX Wrap window, try { to connect, set error status (if  fail), move on...
                //  return False with first failing connection, return True otherwise
                //
                LogEvent lg = new LogEvent();
                SessionControl openSession = new SessionControl();
                int status = 0;

                lg.LogIt("Connecting mount");
                sky6RASCOMTele tsxm = new sky6RASCOMTele();
                try { tsxm.Connect(); }
                catch (Exception ex) { lg.LogIt("Mount connection error: " + ex.Message); }

                lg.LogIt("Connecting camera");
                ccdsoftCamera tsxc = new ccdsoftCamera();
                try { status = tsxc.Connect(); }
                catch (Exception ex) { lg.LogIt("Camera connection error: " + ex.Message); }

                lg.LogIt("Connecting guide camera");
                ccdsoftCamera tsxg = new ccdsoftCamera { Autoguider = 1 };
                try { status = tsxg.Connect(); }
                catch (Exception ex) { lg.LogIt("Autoguider connection error: " + ex.Message); }

                lg.LogIt("Connecting focuser");
                try { status = tsxc.focConnect(); }
                catch (Exception ex) { lg.LogIt("Focuser connection error: " + ex.Message); }

                if (openSession.IsRotationEnabled)
                {
                    lg.LogIt("Connecting rotator");
                    try { status = tsxc.rotatorConnect(); }
                    catch (Exception ex) { lg.LogIt("Rotator connection error: " + ex.Message); }
                }

                if (openSession.IsDomeAddOnEnabled)
                {
                    sky6Dome tsxd = new sky6Dome();
                    lg.LogIt("Connecting dome");
                    try { tsxd.Connect(); }
                    catch (Exception ex) { lg.LogIt("Dome connection error: " + ex.Message); }
                }
            }

            public static void DisconnectAllDevices()
            {
                LogEvent lg = new LogEvent();
                SessionControl openSession = new SessionControl();
                //Park scope and disconnect devices
                SelectedHardware tsxh = new SelectedHardware();       //hardware inventory object

                //Abort Autoguiding, turn off Temp regulation (if  any) and disconnect
                lg.LogIt("Aborting guiding");
                ccdsoftCamera tsxg = new ccdsoftCamera() { Autoguider = 1 };
                tsxg.Abort();
                lg.LogIt("Shutting down guider temperature regulation, if any");
                tsxg.ShutDownTemperatureRegulationOnDisconnect = 1;
                lg.LogIt("Disconnecting guider");
                try { tsxg.Disconnect(); }
                catch (Exception ex) { lg.LogIt("Connecting Guider Failed: " + ex.Message); }

                //Disconnect the focuser
                ccdsoftCamera tsxc = new ccdsoftCamera();
                lg.LogIt("Disconnecting focuser");
                try { tsxc.focDisconnect(); }
                catch (Exception ex) { lg.LogIt("Connecting Focuser Failed: " + ex.Message); }

                //Disconnect the rotator
                if (openSession.IsRotationEnabled)
                {
                    lg.LogIt("Disconnecting rotator");
                    try { tsxc.rotatorDisconnect(); }
                    catch (Exception ex) { lg.LogIt("Connecting Rotator Failed: " + ex.Message); }
                }

                //Abort Imaging, turn off Temp regulation (if  any) and disconnect camera
                lg.LogIt("Aborting imaging");
                tsxc.Abort();
                lg.LogIt("Turning off camera temperature regulation");
                tsxg.ShutDownTemperatureRegulationOnDisconnect = 1;
                lg.LogIt("Disconnecting camera");
                try { tsxc.Disconnect(); }
                catch (Exception ex) { lg.LogIt("Disconnecting Camera Failed: " + ex.Message); }
                System.Threading.Thread.Sleep(2000);

                //Disconnect mount
                lg.LogIt("Disconnecting mount");
                sky6RASCOMTele tsxm = new sky6RASCOMTele();    //telescope object
                try { tsxm.Disconnect(); }
                catch (Exception ex) { lg.LogIt("Disconnecting Mount Failed: " + ex.Message); }

                System.Threading.Thread.Sleep(2000);
                lg.LogIt("All TSX devices disconnected");
                return;
            }
        }
        #endregion

        #region Star Chart Class

        public partial class StarChart
        {
            public static Target FindTarget()
            {
                //Encapsulates TSX method to perform a find and return target information from
                //  a found target in TSX and to center the star chart on the target
                Target tgt = new Planetarium.TSXLink.Target();
                sky6ObjectInformation tsxo = new sky6ObjectInformation();
                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
                tgt.Name = tsxo.ObjInfoPropOut;
                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
                tgt.RA = tsxo.ObjInfoPropOut;
                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
                tgt.Dec = tsxo.ObjInfoPropOut;
                //Get target events
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_RISE_TIME);
                tgt.Rise = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_SET_TIME);
                tgt.Set = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_TRANSIT_TIME);
                tgt.Transit = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_HA_HOURS);
                tgt.HA = TimeSpan.FromHours((double)tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_START);
                try
                { tgt.Dusk = TimeSpan.FromHours(tsxo.ObjInfoPropOut); }
                catch { tgt.Dusk = TimeSpan.FromHours(0); }
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_END);
                try
                { tgt.Dawn = TimeSpan.FromHours(tsxo.ObjInfoPropOut); }
                catch { tgt.Dawn = TimeSpan.FromHours(0); }
                //Get get observation location information
                sky6StarChart tsxs = new sky6StarChart();
                tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Latitude);
                tgt.Lat = tsxs.DocPropOut;
                tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Longitude);
                tgt.Long = tsxs.DocPropOut;

                CenterStarChart(tgt);
                return (tgt);
            }

            //Methods

            public static Target FindTarget(string targetName)
            {
                //Encapsulates TSX method to perform a find and return target information in
                //  a target object, and to center the star chart on the target

                sky6StarChart tsxs = new sky6StarChart();
                if (targetName != null)
                {
                    //If targetName contains an underscore ("_") then convert to a slash
                    string stargetName = targetName.Replace('_', '/');
                    try
                    { tsxs.Find(targetName.Replace('_', '/')); }
                    catch
                    {
                        return (null);
                    }
                }
                else
                {
                    return (null);
                }

                Target tgt = new Target
                {
                    Name = targetName
                };
                //Get target position
                sky6ObjectInformation tsxo = new sky6ObjectInformation();
                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
                tgt.RA = tsxo.ObjInfoPropOut;
                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
                tgt.Dec = tsxo.ObjInfoPropOut;
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_ALT);
                tgt.Altitude = tsxo.ObjInfoPropOut;
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_AZM);
                tgt.Azimuth = tsxo.ObjInfoPropOut;

                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_RATE_ASPERSEC);
                tgt.DeltaRARate = tsxo.ObjInfoPropOut;
                tsxo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_RATE_ASPERSEC);
                tgt.DeltaDecRate = tsxo.ObjInfoPropOut;

                //Get target events
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_RISE_TIME);
                tgt.Rise = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_SET_TIME);
                tgt.Set = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_TRANSIT_TIME);
                tgt.Transit = TimeSpan.FromHours(tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_HA_HOURS);
                tgt.HA = TimeSpan.FromHours((double)tsxo.ObjInfoPropOut);
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_START);
                try
                { tgt.Dusk = TimeSpan.FromHours(tsxo.ObjInfoPropOut); }
                catch { tgt.Dusk = TimeSpan.FromHours(0); }
                tsxo.Property(TheSkyXLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_TWIL_ASTRON_END);
                try
                { tgt.Dawn = TimeSpan.FromHours(tsxo.ObjInfoPropOut); }
                catch { tgt.Dawn = TimeSpan.FromHours(0); }
                //Get get observation location information
                tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Latitude);
                tgt.Lat = tsxs.DocPropOut;
                tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_Longitude);
                tgt.Long = tsxs.DocPropOut;

                CenterStarChart(tgt);
                return (tgt);
            }

            public static bool IsValidTarget(TargetPlan tPlan)
            {
                //Checks to see if TSX can find the targetName
                sky6StarChart tsxs = new sky6StarChart();
                string targetName;
                //IF the target it adjusted, then we use the RA/Dec coordinates as name
                if (tPlan.TargetAdjustEnabled) targetName = tPlan.TargetRA.ToString() + "," + tPlan.TargetDec.ToString();
                else targetName = tPlan.TargetName;
                //If not adjusted then check for something other than null or empty,
                //  then try a "find"
                if ((targetName != null) && (targetName != ""))
                {
                    try
                    { tsxs.Find(targetName); }
                    catch
                    {
                        return (false);
                    }
                    return true;
                }
                else
                {
                    return (false);
                }
            }

            public static void CenterStarChart(Target target)
            {
                //Centers the star chart on a target coordinates
                sky6StarChart tsxc = new sky6StarChart
                {
                    RightAscension = target.RA,
                    Declination = target.Dec
                };
                return;
            }

            public static void SetClock(double julianDate, bool setNow)
            {
                //if currenttime is true, then set TSX for current local time
                //if current time is false, then set TSX for the julian Date
                //note that mount must be disconnected to set clock, so
                //make sure mount is reconnected if SetClock is called to return to current time
                sky6StarChart tsxs = new sky6StarChart();
                if (setNow)
                {
                    tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_UseComputerClock, 1);
                    //Connection.ConnectDevice(Connection.Devices.Mount);
                }
                else
                {
                    //must disconnect mount to change to non-computer time
                    Connection.DisconnectDevice(Connection.Devices.Mount);
                    tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_UseComputerClock, 0);
                    tsxs.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow, julianDate);
                }
                return;
            }

            //Properties

            public static double JulianDate
            {
                get
                {
                    sky6StarChart tsxs = new sky6StarChart();
                    tsxs.DocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow);
                    double jdn = tsxs.DocPropOut;
                    return jdn;
                }
            }

            public static void SetFOV(double fovsize)
            {
                //sets the starchart FOV to the fovsize
                sky6StarChart tsxc = new sky6StarChart
                {
                    FieldOfView = fovsize
                };
            }

            public static double ChartRA
            {
                get
                {
                    sky6StarChart tsxs = new sky6StarChart();
                    double tRA = tsxs.RightAscension;
                    return tRA;
                }
                set
                {
                    sky6StarChart tsxc = new sky6StarChart
                    {
                        RightAscension = value
                    };
                    return;
                }
            }

            public static double ChartDec
            {
                get
                {
                    sky6StarChart tsxs = new sky6StarChart();
                    double tDec = tsxs.Declination;
                    return tDec;
                }
                set
                {
                    sky6StarChart tsxc = new sky6StarChart
                    {
                        Declination = value
                    };
                    return;
                }
            }

            public static double ChartPA
            {
                //Retrieves starchart rotation
                get
                {
                    sky6StarChart tsxs = new sky6StarChart();
                    double rotation = tsxs.Rotation;
                    return rotation;
                }
            }

            public static DateTime ComputedSiteLocalTime
            {
                get
                {
                    sky6Utils tsxu = new sky6Utils();
                    tsxu.ComputeJulianDate();
                    double jd = (double)tsxu.dOut0;
                    tsxu.ConvertJulianDateToCalender(jd);
                    DateTime siteLocalTime = new DateTime((int)tsxu.dOut0,
                        (int)tsxu.dOut1,
                        (int)tsxu.dOut2,
                        (int)tsxu.dOut3,
                        (int)tsxu.dOut4,
                        (int)tsxu.dOut5);
                    return siteLocalTime;
                }
            }

        }
        #endregion

        #region Image Linking
        public static class ImageSolution
        {
            public static PlateSolution PlateSolve(string path)
            {
                LogEvent lg = new LogEvent();
                ImageLink tsxl = new TheSkyXLib.ImageLink
                {
                    pathToFITS = path
                };
                try
                { tsxl.execute(); }
                catch (Exception ex)
                {
                    lg.LogIt("Plate Solution Failed: " + ex.Message);
                    return null;
                }
                ImageLinkResults tsxr = new ImageLinkResults();
                ccdsoftCamera tcam = new ccdsoftCamera();
                PlateSolution ipa = new PlateSolution
                {
                    ImageRA = tsxr.imageCenterRAJ2000,
                    ImageDec = tsxr.imageCenterDecJ2000,
                    ImagePA = tsxr.imagePositionAngle
                };
                lg.LogIt("Plate Solution Successful");
                //Change zoom to decent size
                StarChart.SetFOV(2);
                return ipa;
            }

            public static int PrecisionSlew(AstroImage asti)
            {
                LogEvent lg = new LogEvent();
                SessionControl openSession = new SessionControl();
                int clsResult = 0;
                //If Dome enabled, check for dome command in progress by clearing the coupling
                if (openSession.IsDomeAddOnEnabled)
                {
                    ToggleDomeCoupling();
                }

                ccdsoftCamera tsxc = new ccdsoftCamera
                {
                    ImageReduction = TheSkyXLib.ccdsoftImageReduction.cdAutoDark,
                    Subframe = 0,
                    FilterIndexZeroBased = asti.Filter,
                    ExposureTime = asti.Exposure,
                    Delay = asti.Delay
                };
                AutomatedImageLinkSettings ails = new AutomatedImageLinkSettings
                {
                    exposureTimeAILS = asti.Exposure
                };
                DataWizard.Clear_Observing_List(asti.TargetName);
                ClosedLoopSlew tsxcls = new ClosedLoopSlew();
                try { clsResult = tsxcls.exec(); }
                catch (Exception ex)
                {
                    clsResult = ex.HResult - 1000;
                    lg.LogIt("CLS Error: " + ex.Message);
                }
                StarChart.SetFOV(2);
                return clsResult;
            }

            private static bool IsDomeTrackingUnderway()
            {
                //Test to see if a dome tracking operation is underway.
                // If so, doing a IsGotoComplete will throw an Error 212.
                // return true
                // otherwise return false
                sky6Dome tsxd = new sky6Dome();
                int testDomeTrack;
                try { testDomeTrack = tsxd.IsGotoComplete; }
                catch { return true; }
                if (testDomeTrack == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private static void ToggleDomeCoupling()
            {
                //Uncouple dome tracking, then recouple dome tracking (synchronously)
                sky6Dome tsxd = new sky6Dome();
                tsxd.IsCoupled = 0;
                System.Threading.Thread.Sleep(1000);
                tsxd.IsCoupled = 1;
                //Wait for all dome activity to stop
                while (IsDomeTrackingUnderway()) { System.Threading.Thread.Sleep(1000); }
                return;
            }
        }
        #endregion

        #region SexTractor

        public class SexTractor
        {
            // Added enumeration of inventory index because TSX doesn't
            public enum SourceExtractionType
            {
                sexX,
                sexY,
                sexMagnitude,
                sexClass,
                sexFWHM,
                sexMajorAxis,
                sexMinorAxis,
                sexTheta,
                sexEllipticity
            }

            public ccdsoftImage timg = null;

            public SexTractor()
            {
                //ccdsoftCamera tsxa = new ccdsoftCamera();
                timg = new ccdsoftImage();
                return;
            }

            public void Close()
            {
                return;
            }

            public int SourceExtractGuider()
            {
                int aStat = timg.AttachToActiveAutoguider();
                int iStat = timg.ShowInventory();
                return iStat;
            }

            //*** Converts an array of generic "objects" to an array of doubles
            private double[] ConvertDoubleArray(object[] oIn)
            {
                double[] dOut = new double[oIn.Length];
                for (int i = 0; i < oIn.Length; i++)
                {
                    dOut[i] = Convert.ToDouble(oIn[i]);
                }
                return dOut;
            }

            //*** Converts an array of generic "objects" to a list of doubles
            private List<double> ConvertDoubleList(object[] oIn)
            {
                List<double> dOut = new List<double>();
                for (int i = 0; i < oIn.Length; i++)
                {
                    dOut.Add(Convert.ToDouble(oIn[i]));
                }
                return dOut;
            }

            //*** returns the index of the largest value found in a list
            public int GetListLargest(List<double> iArray)
            {
                int idx = 0;
                for (int i = 0; i < iArray.Count; i++)
                {
                    if (iArray[i] > iArray[idx]) { idx = i; }
                }
                return idx;
            }

            //*** Get the ADU value at pixel X,Y
            public double GetPixelADU(int xPix, int yPix)
            {
                //get the array height and width
                int aHeight = timg.HeightInPixels;
                int aWidth = timg.WidthInPixels;
                //need to some out of bounds checking here someday
                double[] aRow = timg.scanLine(yPix);
                double aVal = aRow[xPix];
                return aVal;
            }

            public double[] GetSourceExtractionArray(SourceExtractionType dataIndex)
            {
                {
                    object[] iA = timg.InventoryArray((int)dataIndex);
                    double[] sexArray = ConvertDoubleArray(iA);
                    return sexArray;
                }

            }

            public List<double> GetSourceExtractionList(SourceExtractionType dataIndex)
            {
                {
                    object[] iA = timg.InventoryArray((int)dataIndex);
                    List<double> sexArray = ConvertDoubleList(iA);
                    return sexArray;
                }

            }

            public int WidthInPixels => timg.WidthInPixels;

            public int HeightInPixels => timg.HeightInPixels;

        }

        #endregion

        #region filterwheel
        public partial class FilterWheel
        {
            public static List<string> FilterWheelList()
            {

                ccdsoftCamera tsxc = new ccdsoftCamera();
                //Connect the camera, if fails, then just return after clean up
                try
                { tsxc.Connect(); }
                catch
                {
                    return null;
                }
                List<string> tfwList = new List<string>();
                for (int filterIndex = 0; filterIndex < tsxc.lNumberFilters; filterIndex++)
                {
                    tfwList.Add(tsxc.szFilterName(filterIndex));
                }
                return tfwList;
            }

            public static int Filter
            {
                get
                {
                    ccdsoftCamera tsxc = new ccdsoftCamera();
                    int fi = tsxc.FilterIndexZeroBased;
                    return fi;
                }
                set
                {
                    ccdsoftCamera tsxc = new ccdsoftCamera
                    {
                        FilterIndexZeroBased = value
                    };
                    return;
                }
            }
        }


        #endregion

        #region Data Wizard

        public partial class DataWizard
        {
            public static bool Clear_Observing_List(string targetname)
            {
                //Clears all entries in observing list, in absence of same function in TSX automation
                //Upon clearing, the routine will "find" the target name, if  any 
                SessionControl openSession = new SessionControl();
                sky6DataWizard tsxdw = new sky6DataWizard
                {
                    Path = openSession.DatabaseQueryDirectoryPath + "\\ClearTheListTheHardWay.dbq"
                };
                tsxdw.Open();
                sky6ObjectInformation zerolist = tsxdw.RunQuery;
                if (targetname != null)
                {
                    sky6StarChart tsxs = new sky6StarChart();
                    try
                    {
                        tsxs.Find(targetname);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        #endregion

        #region Dome
        public partial class Dome
        {
            public static void AbortDome()
            {
                LogEvent lg = new LogEvent();
                bool dState = DomeControl.AbortDome();
                if (dState)
                {
                    lg.LogIt("Aborted dome commands");
                }
                else
                {
                    lg.LogIt("Dome command abort failed");
                }

                return;
            }

            public static void CloseDome()
            {
                SessionControl openSession = new SessionControl();
                if (openSession.IsDomeAddOnEnabled)
                {
                    LogEvent lg = new LogEvent();
                    //New Code
                    lg.LogIt("Closing Dome");
                    int domeHome = openSession.DomeHomeAz;
                    if (DomeControl.CloseDome(domeHome))
                    {
                        lg.LogIt("Dome successfully closed");
                    }
                    else
                    {
                        lg.LogIt("Dome close failed");
                        //Try one more time;
                        lg.LogIt("Trying to close dome again");
                        if (DomeControl.CloseDome(domeHome))
                        {
                            lg.LogIt("Dome successfully closed");
                        }
                        else
                        {
                            lg.LogIt("Second try at closing dome failed");
                        }
                    }
                }
                return;
            }

            public static void OpenDome()
            {
                SessionControl openSession = new SessionControl();
                if (openSession.IsDomeAddOnEnabled)
                {
                    LogEvent lg = new LogEvent();
                    //New Code
                    lg.LogIt("Opening Dome");
                    int domeHome = openSession.DomeHomeAz;
                    if (DomeControl.OpenDome(domeHome))
                    {
                        lg.LogIt("Dome successfully opened");
                    }
                    else
                    {
                        lg.LogIt("Dome open failed");
                    }
                }
                return;
            }

            public static void HomeDome()
            {
                SessionControl openSession = new SessionControl();
                if (openSession.IsDomeAddOnEnabled)
                {
                    int domeHome = openSession.DomeHomeAz;
                    LogEvent lg = new LogEvent();
                    //New code
                    lg.LogIt("Homing Dome");
                    if (DomeControl.HomeDome(domeHome))
                    {
                        lg.LogIt("Dome successfully homed");
                    }
                    else
                    {
                        lg.LogIt("Dome home failed");
                    }
                }
                return;

                //Old code
                //    sky6RASCOMTele tsxt = new sky6RASCOMTele();
                //    lg.LogIt("Disonconnecting Mount");
                //    tsxt.Disconnect();
                //    sky6Dome tsxd = new sky6Dome();
                //    lg.LogIt("Homing Dome Slit");
                //    try
                //    {
                //        tsxd.Connect();
                //        tsxd.GotoAzEl(domeHome - 20, 0);
                //        //Wait for command to propigate -- could be slow
                //        System.Threading.Thread.Sleep(5000);
                //        while (tsxd.IsGotoComplete == 0) { System.Threading.Thread.Sleep(1000); }
                //        tsxd.FindHome();
                //        //Wait for command to propigate -- could be slow
                //        System.Threading.Thread.Sleep(5000);
                //        // operation is in progress (zero)
                //        while (tsxd.IsFindHomeComplete == 0) { System.Threading.Thread.Sleep(1000); }
                //    }
                //    catch
                //    {
                //        //ignor error for now -- probably no dome or ...
                //    }
                //}
                //return;
            }
        }
        #endregion

        #region Mount

        public partial class Mount
        {
            //Enumerations
            public enum SOP
            {
                East = 0,
                West = 1
            }
            //Methods

            public static void TurnTrackingOff()
            {
                sky6RASCOMTele tsxm = new sky6RASCOMTele();
                try { tsxm.SetTracking(0, 1, 0, 0); }
                catch { }
                return;
            }

            public static void TurnTrackingOn()
            {
                sky6RASCOMTele tsxm = new sky6RASCOMTele();
                try { tsxm.SetTracking(1, 1, 0, 0); }
                catch { }
                return;
            }

            public static void Park()
            {
                //Make sure this is synchronous wait. 
                sky6RASCOMTele tsxm = new sky6RASCOMTele { Asynchronous = 0 };
                try { tsxm.Connect(); }
                catch (Exception ex)
                {
                    MessageBox.Show("Park Error: " + ex.Message + " on attempt to connect to mount");
                    return;
                }
                try { tsxm.Park(); }
                catch (Exception ex) { MessageBox.Show("Park Error: " + ex.Message); }
                return;
            }


            public static void UnPark()
            {
                //Make sure this is synchronous wait.
                sky6RASCOMTele tsxm = new sky6RASCOMTele { Asynchronous = 0 };
                try
                {
                    if (tsxm.IsConnected == 0) { tsxm.Connect(); }
                    tsxm.Unpark();
                }
                catch (Exception ex) { return; }
                return;
            }

            public static void SlewAzAlt(double azm, double alt, string targetName)
            {
                //Make sure this is synchronous wait.
                sky6RASCOMTele tsxm = new sky6RASCOMTele { Asynchronous = 0 };
                tsxm.SlewToAzAlt(azm, alt, targetName);
                TurnTrackingOn();
            }

            public static void SlewRADec(double ra, double dec, string targetName)
            {
                //Make sure this is synchronous wait.
                sky6RASCOMTele tsxm = new sky6RASCOMTele { Asynchronous = 0 };
                tsxm.SlewToRaDec(ra, dec, targetName);
                TurnTrackingOn();
            }

            public static void SetSpecialTracking(double dRA, double dDec)
            {
                //Procedure sets TSX mount to special tracking rate
                sky6RASCOMTele tsxm = new sky6RASCOMTele { Asynchronous = 0 };
                tsxm.SetTracking(1, 0, dRA, dDec);
            }

            public static void ResetSpecialTracking()
            {
                //Method turns off special tracking, if on
                sky6RASCOMTele tsxm = new sky6RASCOMTele { Asynchronous = 0 };
                tsxm.SetTracking(1, 0, 0, 0);
            }

            //Properties

            public static double Azm
            {
                get
                {
                    sky6RASCOMTele tsxm = new sky6RASCOMTele();
                    tsxm.GetAzAlt();
                    double azm = tsxm.dAz;
                    return azm;
                }
            }

            public static double Alt
            {
                get
                {
                    sky6RASCOMTele tsxm = new sky6RASCOMTele();
                    tsxm.GetAzAlt();
                    double alt = tsxm.dAlt;
                    return alt;
                }
            }

            public static SOP OTASideOfPier
            {

                //returns the current side of pier for the mount
                // east is 0 west is 1
                // check to see if mount is connected, if not, then return null;
                get
                {
                    sky6RASCOMTele tsxm = new sky6RASCOMTele();
                    if (TSXLink.Connection.IsConnected(Connection.Devices.Mount))
                    {
                        tsxm.DoCommand(11, "");
                        SOP pole = (SOP)Convert.ToInt32(tsxm.DoCommandOutput);
                        return pole;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

        }

        #endregion

        #region Camera Class

        public class Camera
        {
            private ccdsoftCamera tsxc;

            public Camera(AstroImage asti)
            {
                tsxc = new ccdsoftCamera
                {
                    Autoguider = (int)asti.Camera,
                    BinX = asti.BinX,
                    BinY = asti.BinY,
                    FilterIndexZeroBased = asti.Filter,
                    Delay = asti.Delay,
                    Frame = (ccdsoftImageFrame)asti.Frame,
                    ImageReduction = (ccdsoftImageReduction)asti.ImageReduction,
                    Subframe = asti.SubFrame,
                    SubframeBottom = asti.SubframeBottom,
                    SubframeTop = asti.SubframeTop,
                    SubframeRight = asti.SubframeRight,
                    SubframeLeft = asti.SubframeLeft,
                    AutoSaveOn = asti.AutoSave,
                    ExposureTime = asti.Exposure
                };
                if (asti.Camera == AstroImage.CameraType.Guider)
                { tsxc.AutoguiderExposureTime = asti.Exposure; }
            }

            public int GetImage()
            {
                //Takes an image asynchronously using TSX
                Asynchronous = 1;
                try
                { tsxc.TakeImage(); }
                catch (Exception ex)
                { return ex.HResult; }

                //Wait while the image is being taken, using 1 second naps.  Check each time to see
                //  if (the user has hit abort.  if (so, close everything up.
                int expstatus = 0;
                while (expstatus == 0)
                {
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                    try
                    { expstatus = tsxc.IsExposureComplete; }
                    catch (Exception ex)
                    {
                        tsxc.Abort();
                        AbortEvent ab = new AbortEvent();
                        ab.AbortIt(ex.Message);
                        return ex.HResult;
                    }
                }
                return 0;
            }

            public void CameraAbort()
            {
                tsxc.Asynchronous = 0;
                tsxc.Abort();
                return;
            }

            public string LastImageFilename()
            {
                string path = tsxc.LastImageFileName;
                return path;
            }

            public double CCDTemperature
            {
                //reads the current ccd temperature and writes the set point
                get => tsxc.Temperature;
                set
                {
                    tsxc.TemperatureSetPoint = value;
                    tsxc.RegulateTemperature = 1;
                }
            }

            public int WidthInPixels => tsxc.WidthInPixels;

            public int HeightInPixels => tsxc.HeightInPixels;

            public int ImageADU
            {
                get
                {
                    ccdsoftImage tsxi = new ccdsoftImage();
                    tsxi.AttachToActiveImager();
                    int adu = (int)tsxi.averagePixelValue();
                    return adu;
                }
            }

            public double MaximumPixel => tsxc.MaximumPixel;

            public int Asynchronous
            { set => tsxc.Asynchronous = value; }

            public int AutoSaveOn
            { set => tsxc.AutoSaveOn = value; }

            public int IsExposureComplete => tsxc.IsExposureComplete;

            public int Autoguide()
            {
                tsxc.Asynchronous = 1;
                return tsxc.Autoguide();
            }

            public int Calibrate(int AO)
            { return tsxc.Calibrate(AO); }

            public ccdsoftCameraState State => tsxc.State;

            public int CenterAO()
            {
                int retVal;
                try { retVal = tsxc.centerAO(); }
                catch { retVal = -1; }
                return retVal;
            }

            public double GuideStarX
            {
                get => tsxc.GuideStarX;
                set => tsxc.GuideStarX = value;
            }

            public double GuideStarY
            {
                get => tsxc.GuideStarY;
                set => tsxc.GuideStarY = value;
            }

            public int BinX
            {
                get => tsxc.BinX;
                set => tsxc.BinX = value;
            }

            public int BinY
            {
                get => tsxc.BinY;
                set => tsxc.BinY = value;
            }

            public double CalibrationVectorXPositiveXComponent
            {
                get => tsxc.CalibrationVectorXPositiveXComponent;
                set => tsxc.CalibrationVectorXPositiveXComponent = value;
            }

            public double CalibrationVectorXPositiveYComponent
            {
                get => tsxc.CalibrationVectorXPositiveYComponent;
                set => tsxc.CalibrationVectorXPositiveYComponent = value;
            }

            public double CalibrationVectorYPositiveXComponent
            {
                get => tsxc.CalibrationVectorYPositiveXComponent;
                set => tsxc.CalibrationVectorYPositiveXComponent = value;
            }

            public double CalibrationVectorYPositiveYComponent
            {
                get => tsxc.CalibrationVectorYPositiveYComponent;
                set => tsxc.CalibrationVectorYPositiveYComponent = value;
            }

            public double CalibrationVectorXNegativeXComponent
            {
                get => tsxc.CalibrationVectorXNegativeXComponent;
                set => tsxc.CalibrationVectorXNegativeXComponent = value;
            }

            public double CalibrationVectorXNegativeYComponent
            {
                get => tsxc.CalibrationVectorXNegativeYComponent;
                set => tsxc.CalibrationVectorXNegativeYComponent = value;
            }

            public double CalibrationVectorYNegativeXComponent
            {
                get => tsxc.CalibrationVectorYNegativeXComponent;
                set => tsxc.CalibrationVectorYNegativeXComponent = value;
            }

            public double CalibrationVectorYNegativeYComponent
            {
                get => tsxc.CalibrationVectorYNegativeYComponent;
                set => tsxc.CalibrationVectorYNegativeYComponent = value;
            }

            public double DeclinationAtCalibration => tsxc.DeclinationAtCalibration;

            public int TrackBoxX
            {
                get => tsxc.TrackBoxX;
                set => tsxc.TrackBoxX = value;
            }

            public int TrackBoxY
            {
                get => tsxc.TrackBoxY;
                set => tsxc.TrackBoxY = value;
            }

            public double GuideErrorX => tsxc.GuideErrorX;

            public double GuideErrorY => tsxc.GuideErrorY;

            public int SubframeBottom
            {
                get => tsxc.SubframeBottom;
                set => tsxc.SubframeBottom = value;
            }

            public int SubframeTop
            {
                get => tsxc.SubframeTop;
                set => tsxc.SubframeTop = value;
            }

            public int SubframeLeft
            {
                get => tsxc.SubframeLeft;
                set => tsxc.SubframeLeft = value;
            }

            public int SubframeRight
            {
                get => tsxc.SubframeRight;
                set => tsxc.SubframeRight = value;
            }

            public void Calibrate(bool AO)
            {
                //Make sure there is a delay for the mount to settle
                tsxc.Delay = 2;
                if (!AO)
                {
                    try
                    { int calstat = tsxc.Calibrate(0); } //1 for AO, anything else for not AO(autoguider)            
                    catch { return; }
                }
                //wait for completion;
                while (tsxc.State == TheSkyXLib.ccdsoftCameraState.cdStateCalibrate)
                {
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                }
                //Also run AO calibration, if configured
                if (AO)
                {
                    //lg.LogIt("Calibrating AO ");
                    try
                    { int calstat = tsxc.Calibrate(1); } //1 for AO, anything else for not AO(autoguider)            
                    catch
                    { return; }
                    //wait for completion;
                    while (tsxc.State == TheSkyXLib.ccdsoftCameraState.cdStateCalibrate)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            public int AutoGuiderOn()
            {
                //Turn on Asynchronous, then turn on autoguide and return status
                tsxc.Asynchronous = 1;
                return tsxc.Autoguide();
            }

            public void AutoGuiderOff()
            {
                //Turn off Asynchronous, then turn off autoguide
                tsxc.Asynchronous = 0;
                tsxc.Abort();
                //ccdsoftCameraState agState = tsxc.State;
                //make sure that autoguide is aborted and idle
                while (tsxc.State != ccdsoftCameraState.cdStateNone) { System.Threading.Thread.Sleep(1000); };
                return;
            }

            public bool IsAutoGuideOn()
            {
                //Returns true is the autoguider is running
                ccdsoftCameraState agState = tsxc.State;
                if (agState == ccdsoftCameraState.cdStateAutoGuide)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region Rotator Class
        public class Rotator
        {
            private ccdsoftCamera tsxc;

            public Rotator()
            {
                tsxc = new ccdsoftCamera();
                return;
            }

            public double GetRotatorPositionAngle()
            {
                if (!(tsxc.rotatorIsConnected() == 0))
                {
                    return tsxc.rotatorPositionAngle();
                }
                else
                {
                    return 0;
                }
            }

            public void SetRotatorPositionAngle(double tgtRotationPA)
            {
                if (!(tsxc.rotatorIsConnected() == 0))
                {
                    tsxc.rotatorGotoPositionAngle(tgtRotationPA);
                    //zero if the rotator is not rotating
                    while (!(tsxc.rotatorIsRotating() == 0)) { System.Threading.Thread.Sleep(1000); }
                }
                return;
            }
        }
        #endregion

        #region FOV

        public partial class FOVI
        {
            public static string FOVName
            /// Retrieves name of currently visible FOV
            {
                get
                {
                    string fovName = null;
                    sky6MyFOVs tsxf = new sky6MyFOVs();
                    for (int i = 0; i < tsxf.Count; i++)
                    {
                        tsxf.Name(i);
                        fovName = tsxf.OutString;
                        tsxf.Property(fovName, 0, sk6MyFOVProperty.sk6MyFOVProp_Visible);
                        double vis = tsxf.OutVar;
                        if (vis == 1)
                        {
                            return fovName;
                        }
                    }
                    return null;
                }
            }

            public static double GetFOVPA
            ///Retrieves current FOV PA of the FOV as reported by TSX
            {
                get
                {
                    sky6MyFOVs tsxf = new sky6MyFOVs();
                    tsxf.Property(FOVName, 0, sk6MyFOVProperty.sk6MyFOVProp_PositionAngleDegrees);
                    double fovPA = tsxf.OutVar;
                    return fovPA;
                }
            }

            public static void SetFOVPA(double PA)
            {
                sky6MyFOVs tsxf = new sky6MyFOVs();
                tsxf.setProperty(FOVName, 0, sk6MyFOVProperty.sk6MyFOVProp_PositionAngleDegrees, PA);
            }


        }


        #endregion

        #region Focus Class

        public partial class Focus
        {
            public static bool RunAtFocusAny(AstroImage asti, int aftype)
            {
                //Run @Focus2 or 3 with filternumber.  return True if (successful, false if (not

                //   Before running this method, save the current target name and camera configuration so it can be found again 
                //   Restore current target, using Name with Find method, and reload camera configuration afterwards
                //   as @Focus3 using automatic star search off, overwrites the observating list and object)
                //   Also, may want to run Closed}Slew back to target and Turn on temperature compensation (A)
                //

                //Make sure focuser is connected
                //Create camera object
                ccdsoftCamera tsxc = new ccdsoftCamera
                {
                    Asynchronous = 0,
                    AutoSaveFocusImages = 0,
                    ImageReduction = (ccdsoftImageReduction)asti.ImageReduction,
                    Frame = (ccdsoftImageFrame)asti.Frame,
                    FilterIndexZeroBased = asti.Filter,
                    FocusExposureTime = asti.Exposure,
                    Delay = 0
                };
                //Run @Focus2 or 3
                //   Create a camera object
                //   Launch the autofocus watching out for an exception -- which will be posted in TSX
                switch (aftype)
                {
                    case 2:
                        try
                        { int focstat = tsxc.AtFocus2(); }
                        catch
                        {
                            //Just close up, TSX will spawn error window unless this is an abort
                            //lg.LogIt("@Focus2 fails for " + ex.Message);
                            return (false);
                        }
                        //@Focus2 will generate an observing list.  Clear it.
                        break;

                    case 3:
                        try
                        { int focstat = tsxc.AtFocus3(3, true); }
                        catch
                        {
                            //Just close up, TSX will spawn error window unless this is an abort
                            //lg.LogIt("@Focus3 fails for " + ex.Message);
                            return (false);
                        }
                        //lg.LogIt("@Focus3 successful");
                        break;
                    default:
                        // lg.LogIt("Unknown AtFocus selection -- focus failed");
                        break;
                }
                return true;
            }

            public static void RunTempComp()
            {
                //Enable Temperature compensation with current data to mode A (for Optec)
                // lg = FormHumason.LogReport;

                ccdsoftCamera tsxc = new ccdsoftCamera
                {
                    focTemperatureCompensationMode = ccdsoftfocTempCompMode.cdfocTempCompMode_A
                };
                //lg.LogIt("Focuser Temperature Compensation turned on");
                return;
            }

            public static double GetTemperature()
            {
                ccdsoftCamera tsxc = new ccdsoftCamera();
                double celsius = tsxc.focTemperature;
                return celsius;
            }

            public static double GetPosition()
            {
                ccdsoftCamera tsxc = new ccdsoftCamera();
                double position = tsxc.focPosition;
                return position;
            }

            public static int MoveTo(double position)
            {
                int movestat;
                ccdsoftCamera tsxc = new ccdsoftCamera();
                if (position < tsxc.focPosition)
                {
                    movestat = tsxc.focMoveIn((int)(tsxc.focPosition - position));
                }
                else
                {
                    movestat = tsxc.focMoveOut((int)(position - tsxc.focPosition));
                }
                return movestat;
            }
        }

        #endregion

        #region Target Structure
        public class Target
        {
            //class containing definition of a target -- name, ra, dec
            private string pName;
            private double pRA;
            private double pDec;
            private double pAlt;
            private double pAzm;
            private TimeSpan pHA;
            private TimeSpan pTransit;
            private double pLat;
            private double pLong;
            private TimeSpan pRise;
            private TimeSpan pSet;
            private TimeSpan pDusk;
            private TimeSpan pDawn;

            public Target() //Empty target object
            {
                pName = "";
                pRA = 0;
                pDec = 0;
                return;
            }

            public Target(string Name, double RA, double Dec)
            {
                pName = Name;
                pRA = RA;
                pDec = Dec;
                return;
            }

            public string Name
            {
                get => pName;
                set => pName = value;
            }

            public double RA
            {
                get => pRA;
                set => pRA = value;
            }

            public double Dec
            {
                get => pDec;
                set => pDec = value;
            }

            public double Lat
            {
                get => pLat;
                set => pLat = value;
            }

            public double Long
            {
                get => pLong;
                set => pLong = value;
            }

            public double Altitude
            {
                get => pAlt;
                set => pAlt = value;
            }

            public double Azimuth
            {
                get => pAzm;
                set => pAzm = value;
            }

            public TimeSpan Rise
            {
                get => pRise;
                set => pRise = value;
            }

            public TimeSpan Set
            {
                get => pSet;
                set => pSet = value;
            }

            public TimeSpan Transit
            {
                get => pTransit;
                set => pTransit = value;
            }

            public TimeSpan HA
            {
                get => pHA;
                set => pHA = value;
            }

            public TimeSpan Dusk
            {
                get => pDusk;
                set => pDusk = value;
            }

            public TimeSpan Dawn
            {
                get => pDawn;
                set => pDawn = value;
            }

            public double DeltaRARate { get; set; }
            public double DeltaDecRate { get; set; }

        }
        #endregion

        #region Plate Solution Structure

        public class PlateSolution
        {
            public PlateSolution()
            {
                ImageRA = 0;
                ImageDec = 0;
                ImagePA = 0;
                RotatorPositionAngle = 0;
            }

            public double ImageRA { get; set; } = 0;
            public double ImageDec { get; set; } = 0;
            public double ImagePA { get; set; } = 0;
            public double RotatorPositionAngle { get; set; } = 0;
        }

        #endregion

    }
}


