using System;

namespace Humason
{
    public partial class Operations
    {
        //public static SessionControl openSession;

        public static bool ImagingControl()
        {
            //Runs the photo shoot shebang
            //First a little housekeeping -- configuration update and make sure the clock is set right
            //if Autorun staging is set, wait on autostaging time, then run the staging app
            //if Autorun start is set, wait on autostart time, then run the starting app
            //Wait on Sequence start
            //
            //LogEvent lg = new LogEvent();
            LogEvent lg = FormHumason.StatusReportEvent;

            TargetEvent tg = new TargetEvent();

            SessionControl openSession = new SessionControl();

            //Run a quick check on all the things that might be wrong, based on past history
            //  the main thing being that there is at least one target plan queued.
            lg.LogIt("Running diagnostics");
            if (!Diagnostics.CheckUp())
            {
                lg.LogIt("Diagnostics abort");
                return false; ;
            }

            lg.LogIt("Diagnostics success");
            //All selected devices should be on-line and connected
            //Load the first target plan so we can pull some information for this session
            lg.LogIt("Loading first session target plan");
            if (!FormHumason.fPlanForm.IsTopPlanTargetName())
            {
                lg.LogIt("No Target Plan");
                return false;
            }
            openSession.CurrentTargetName = FormHumason.fPlanForm.GetTopPlanTargetName();
            TargetPlan ftPlan = new TargetPlan(openSession.CurrentTargetName);
            //Flush it out, if we have to
            if (ftPlan.IsSparsePlan())
            {
                lg.LogIt("Sparse Plan -- filling out from default");
                ftPlan.FlushOutFromDefaultPlan();
            }
            //Update the form and regenerate the sequence
            tg.RaiseNewTargetPlan(ftPlan.TargetName);

            //Get the staging, start and shut down times from this initial target, although may not be used
            openSession.StagingTime = DateTime.Now;
            openSession.StartUpTime = ftPlan.SequenceStartTime;
            //openSession.ShutDownTime = ftPlan.SequenceDawnTime;

            //Save configuration set up, reset the progess bar and make sure that the TSX clock is set to current time
            TSXLink.StarChart.SetClock(0, true);

            //Await Staging and Start Up
            //   run the staging time autorun script/app
            LaunchPad.WaitStaging();
            //check for abort having been set.  Gracefully shut everything back down if it has.
            if (FormHumason.IsAborting())
            {
                GracefulAbort();
                return false;
            }

            //  run the start up time autorun script/app
            LaunchPad.WaitStartUp();
            //check for abort having been set.  Gracefully shut everything back down if it has.
            if (FormHumason.IsAborting())
            {
                GracefulAbort();
                return false;
            }

            //Both Staging and Start Up have been run. All devices should be powered, but not necessarily connected
            //Power up and connect devices (if not done already)
            lg.LogIt("Initializing system");
            InitializeSystem();

            //Check for proximity to meridian flip
            //  if HA of target is within 10 minutes, then just wait it out at 
            //Get target HA
            TSXLink.Target tgto;
            lg.LogIt("Checking for new target to clear meridian");
            string raS = ftPlan.TargetRA.ToString();
            string decS = ftPlan.TargetDec.ToString();
            tgto = TSXLink.StarChart.FindTarget(raS + "," + decS);
            double ha = tgto.HA.TotalMinutes;
            while ((ha >= -10) && (ha <= 0))
            {
                System.Threading.Thread.Sleep(10000);
                tgto = TSXLink.StarChart.FindTarget(ftPlan.TargetName);
                ha = tgto.HA.TotalMinutes;
            }
            lg.LogIt("New target is clear of meridian");

            //Make sure the mount is unparked
            TSXLink.Mount.UnPark();
            lg.LogIt("UnPark Mount");

            //Make sure tracking is turned on
            TSXLink.Mount.TurnTrackingOn();
            lg.LogIt("Sidereal Tracking On");

            //Bring camera to temperature (if not already), then clear the objects
            TSXLink.Camera.CCDTemperature = openSession.CameraTemperatureSet;
            lg.LogIt("Camera Temperature set to " + openSession.CameraTemperatureSet.ToString("0.0"));

            //************************  Starting up the target plans  *************************
            //
            //  This loop is to run each of the plans in the schedule list sequentially.
            //  The current plan has already progressed through any staging and starting scripts
            //      and the next step will be to wait on the starting time, if it hasn't passed already
            //  At the end of the loop, the current plan will be deleted from the schedule and the next one, if any
            //      loaded.  If none, then the shutdown script of the last plan will be run (if autorun is set for that plan).
            //
            while (FormHumason.fPlanForm.IsTopPlanTargetName())
            {

                //Load the top scheduled plan
                string tgtName = FormHumason.fPlanForm.GetTopPlanTargetName();
                openSession.CurrentTargetName = tgtName;
                TargetPlan tPlan = new TargetPlan(tgtName);

                lg.LogIt(" ******************* Imaging Target: " + openSession.CurrentTargetName);
                if (tPlan.IsSparsePlan())
                {
                    lg.LogIt("Sparse Plan -- filling out from default");
                    tPlan.FlushOutFromDefaultPlan();
                }
                //Try to move to target, if this fails just abort
                Sequencer imgseq = new Sequencer();

                //check for abort having been set.  Gracefully shut everything back down if it has.
                if (FormHumason.IsAborting())
                {
                    GracefulAbort();
                    return false;
                }

                lg.LogIt("CLS to center target");
                if (!imgseq.CLSToTargetPlanCoordinates())
                {
                    lg.LogIt("Failed to center target");
                    GracefulAbort();
                    return false;
                }
                ;

                //Now lets get the rotator positioned properly, plate solve, then rotate, then plate solve
                if (tPlan.RotatorEnabled)
                {
                    lg.LogIt("Rotating to PA @ " + tPlan.TargetPA.ToString("0.00"));
                    if (!Rotator.RotateToImagePA(tPlan.TargetPA))
                    {
                        lg.LogIt("Failed to properly rotate. Aborting.");
                        GracefulAbort();
                        return false;
                    }
                    else
                    {
                        lg.LogIt("Rotation complete and verified");
                        // Because rotation may not be quite symmetrical, do another CLS to make sure
                        //  the guide star and target is still centered

                        lg.LogIt("CLS to center target after rotation");
                        if (!imgseq.CLSToTargetPlanCoordinates())
                        {
                            lg.LogIt("Failed to center target after rotation");
                            GracefulAbort();
                            return false;
                        }
                        ;
                    }
                }
                else lg.LogIt("Rotator not enabled");

                //check for abort having been set.  Gracefully shut everything back down if it has.
                if (FormHumason.IsAborting())
                {
                    GracefulAbort();
                    return false;
                }

                //Update the sequence for whatever time it is now
                try { imgseq.SeriesGenerator(); }
                catch
                {
                    lg.LogIt("Make Series Error");
                    GracefulAbort();
                    return false;
                }
                //
                // Run Imaging Sequence
                imgseq.PhotoShoot();
                //
                //
                //All done.  Abort autoguiding, assuming is running -- should be off, but you never know
                AutoGuiding.AutoGuideStop();
                //check for abort having been set.  Gracefully shut everything back down if it has.
                if (FormHumason.IsAborting())
                {
                    GracefulAbort();
                    return false;
                }
                //Done with imaging on this plan.  
                //Set the ending time
                tPlan.SequenceEndTime = DateTime.Now;
                //Save the plan in the sequence complete summary file
                openSession.AddSequenceCompleteSummary();
                //string tName = tPlan.GetItem(TargetPlan.sbTargetNameName); //why??
                //Remove the plan from the schedule and look for the next
                FormHumason.fPlanForm.RemoveTopPlan();
            }

            //done with imaging.  Check on flats  See if any flats have been requested

            FlatManager fSuper = new FlatManager();
            if (fSuper.HaveFlatsToDo() && !FormHumason.IsAborting())
            { fSuper.TakeFlats(); }

            //If autorun set, then run it, then... just park the mount and disconnect
            if (openSession.ShutDownEnabled && !openSession.IsAttended)
                LaunchPad.RunShutDownApp();
            else
            {
                try { TSXLink.Mount.Park(); }
                catch (Exception ex)
                { lg.LogIt("Could not Park: " + ex.Message); }
                //Disconnect devices
                TSXLink.Connection.DisconnectAllDevices();
            }
            if (openSession.SessionEndParkEnabled)
            {
                lg.LogIt("Parking Mount");
                try { TSXLink.Mount.Park(); }
                catch (Exception ex) { lg.LogIt("Could not Park: " + ex.Message); }
            }

            return true;
        }

        private static void GracefulAbort()
        {
            //Abort has been pushed or automatically called by procedure due to some error
            //If start is not active or attended is checked then we want to just park the scope and disconnect all
            //devices.
            //If start is active, then we want to treat this as a catastrophic event and simply
            //stop everything by parking
            SessionControl openSession = new SessionControl();

            LogEvent lg = new LogEvent();
            lg.LogIt("Aborting and Closing Down");
            FormHumason.SetAbort();

            //All done.  Abort autoguiding, assuming is running -- should be off, but you never know
            AutoGuiding.AutoGuideStop();

            //If autorun set and we're running unattended, then shut down, or... just park the mount, home the dome and disconnect
            if (FormHumason.SessionState != FormHumason.SessionStateFlag.Stopped)
            {
                lg.LogIt("Parking Mount");
                try { TSXLink.Mount.Park(); }
                catch (Exception ex) { lg.LogIt("Could not Park: " + ex.Message); }

                lg.LogIt("Disconnecting all devices");
                TSXLink.Connection.DisconnectAllDevices();
                FormHumason.SetStopped();
            }
            if ((!openSession.IsAttended) && openSession.ShutDownEnabled && LaunchPad.IsTimeToShutDown())
                LaunchPad.RunShutDownApp();
            if (openSession.SessionEndParkEnabled)
            {
                lg.LogIt("Parking Mount");
                try { TSXLink.Mount.Park(); }
                catch (Exception ex) { lg.LogIt("Could not Park: " + ex.Message); }
            }
            lg.LogIt("Disconnecting all devices");
            TSXLink.Connection.DisconnectAllDevices();
            FormHumason.SetStopped();
            lg.LogIt("Abort Completed -- awaiting new orders, Captain");
            return;
        }

        public static void InitializeSystem()
        {
            SessionControl openSession = new SessionControl();

            //Power on and connect devices as configured
            LogEvent lg = new LogEvent();
            lg.LogIt("Connecting");
            openSession = new SessionControl();
            if (openSession.IsHomeMountEnabled)
                TSXLink.Connection.DeployMount();
            //Make sure themount is Parked (could be redundant)
            TSXLink.Mount.Park();
            TSXLink.Connection.ConnectAllDevices();
            lg.LogIt("Devices Connected");
        }

    }
}

