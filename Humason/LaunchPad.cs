/// Launcher Class
///
/// ------------------------------------------------------------------------
/// Module Name: Launcher 
/// Purpose: Methods for awaiting for a delayed staging, start up and shut down
///     executables
/// Developer: Rick McAlister
/// Creation Date:  6/6/2017
/// Major Modifications:
/// Copyright: Rick McAlister, 2017
/// 
/// Description:
/// Static Launcher Class contains all functions for working with autorun times 
/// 
/// Each wait method first checks that AutoRun is enabled, if not, it just returns
/// 
/// 
/// ------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace Humason
{
    class LaunchPad
    {
        public static bool WaitLoop(DateTime endTime)
        {
            LogEvent lg = new LogEvent();
            lg.LogIt("Waiting until " + endTime.ToString("HH:mm"));
            do
            {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1000); //wait a second
                if (FormHumason.IsAborting())
                {
                    lg.LogIt("Wait Loop Aborted");
                    return false;
                }
            } while (DateTime.Now < endTime);
            return true;
        }

        public static void WaitStaging()
        {
            // Wait method gets the staging time from the Humason configuration file,
            //  then runs a one second sleep loop until the current time is greater than thed
            //  staging time.

            //Check to see if AutoRun and Staging executable has been enabled
            //  If so, then wait until the current time is greater than stage system time
            SessionControl openSession = new SessionControl();
            if (WaitLoop(openSession.StagingTime)) { RunStagingApp(); }

        }

        public static void WaitStartUp()
        {
            // Wait method gets the start up time from the Humason configuration file,
            //  then runs a one second sleep loop until the current time is greater than the
            //  start up time.

            //Check to see if AutoRun and StartUp executable has been enabled
            //  If so, then wait until the current time is greater than stage system time
            SessionControl openSession = new SessionControl();
            if (WaitLoop(openSession.StartUpTime)) { RunStartUpApp(); }
            return;
        }

        public static bool IsTimeToShutDown()
        {
            // CheckEnd gets the configured end time and returns true
            //   if the datetime exceeds the end time
            // this is intended to be used to periodically check if 
            // a photoshoot has lasted past the configured shutdown time
            SessionControl openSession = new SessionControl();

            DateTime endTime = openSession.ShutDownTime;
            if (endTime < DateTime.Now)
                return (true);
            else
                return (false);
        }

        public static void RunStagingApp()
        {
            //If StageSystemOn is set, then RunStageSystem gets the StageSystem filepath from the Humason config file, if any
            //  then launches it and waits for completion.

            SessionControl openSession = new SessionControl();
            Process pSystemExe = new Process();
            if (openSession.StagingEnabled && openSession.StagingFilePath != null)
            {
                LogEvent lg = new LogEvent();
                lg.LogIt("Running Staging Process");
                pSystemExe.StartInfo.FileName = openSession.StagingFilePath;
                pSystemExe.Start();
                if (openSession.IsStagingWaitEnabled)
                {
                    pSystemExe.WaitForExit();
                }

                lg.LogIt("Staging Process Complete");
            }
            return;
        }

        public static void RunStartUpApp()
        {
            //If StageSystemOn is set, then RunStageSystem gets the StageSystem filepath from the Humason config file, if any
            //  then launches it and waits for completion.

            SessionControl openSession = new SessionControl();
            Process pSystemExe = new Process();
            if (openSession.StartUpEnabled && openSession.StartUpFilePath != null)
            {
                LogEvent lg = new LogEvent();
                lg.LogIt("Running Start Up Process");
                pSystemExe.StartInfo.FileName = openSession.StartUpFilePath;
                pSystemExe.Start();
                if (openSession.IsStartUpWaitEnabled)
                {
                    pSystemExe.WaitForExit();
                }

                lg.LogIt("Start Up Process Complete");
            }
            return;
        }

        public static void RunShutDownApp()
        {
            //If ShutDownOn is set, then RunShutDown gets the postscan filepath from the Humason config file, if any
            //  then launches it and waits for completion.

            SessionControl openSession = new SessionControl();
            LogEvent lg = new LogEvent();
            Process pSystemExe = new Process();
            lg.LogIt("Checking on Shut Down app");
            if (openSession.ShutDownEnabled && openSession.ShutDownFilePath != null)
            {
                lg.LogIt("Running Shut Down Process");
                pSystemExe.StartInfo.FileName = openSession.ShutDownFilePath;
                try
                {
                    pSystemExe.Start();
                    if (openSession.IsShutDownWaitEnabled)
                    {
                        pSystemExe.WaitForExit();
                    }
                }
                catch (Exception ex)
                {
                    lg.LogIt("Shutdown app failed: " + ex.Message);
                }

                lg.LogIt("Shut Down Process Complete");
            }
            return;
        }
    }
}

