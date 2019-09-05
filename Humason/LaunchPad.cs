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
            LogEvent lg = FormHumason.lg;
            lg.LogIt("Waiting until " + endTime.ToString("HH:mm"));
            do
            {
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1000); //wait a second
                if (FormHumason.AbortFlag)
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
            if (FormHumason.openSession.IsStagingEnabled)
            {
                if (WaitLoop(FormHumason.openSession.StagingTime)) { RunStagingApp(); }
            }
        }

        public static void WaitStartUp()
        {
            // Wait method gets the start up time from the Humason configuration file,
            //  then runs a one second sleep loop until the current time is greater than the
            //  start up time.

            //Check to see if AutoRun and StartUp executable has been enabled
            //  If so, then wait until the current time is greater than stage system time
            if (FormHumason.openSession.IsStartUpEnabled)
            {
                if (WaitLoop(FormHumason.openSession.StartUpTime)) { RunStartUpApp(); }
            }

            return;
        }

        public static bool IsTimeToShutDown()
        {
            // CheckEnd gets the configured end time and returns true
            //   if the datetime exceeds the end time
            // this is intended to be used to periodically check if 
            // a photoshoot has lasted past the configured shutdown time
            if (FormHumason.openSession.IsAutoRunEnabled && FormHumason.openSession.IsShutDownEnabled)
            {
                DateTime endTime = FormHumason.openSession.ShutDownTime;
                if (endTime < DateTime.Now) { return (true); }
                else { return (false); }
            }
            else { return (false); }
        }

        public static void RunStagingApp()
        {
            //If StageSystemOn is set, then RunStageSystem gets the StageSystem filepath from the Humason config file, if any
            //  then launches it and waits for completion.

            Process pSystemExe = new Process();
            if (FormHumason.openSession.StagingFilePath != null)
            {
                LogEvent lg = FormHumason.lg;
                lg.LogIt("Running Staging Process");
                pSystemExe.StartInfo.FileName = FormHumason.openSession.StagingFilePath;
                pSystemExe.Start();
                if (FormHumason.openSession.IsStagingWaitEnabled) pSystemExe.WaitForExit();
                lg.LogIt("Staging Process Complete");
            }
            return;
        }

        public static void RunStartUpApp()
        {
            //If StageSystemOn is set, then RunStageSystem gets the StageSystem filepath from the Humason config file, if any
            //  then launches it and waits for completion.

            Process pSystemExe = new Process();
            if (FormHumason.openSession.StartUpFilePath != null)
            {
                LogEvent lg = FormHumason.lg;
                lg.LogIt("Running Start Up Process");
                pSystemExe.StartInfo.FileName = FormHumason.openSession.StartUpFilePath;
                pSystemExe.Start();
                if (FormHumason.openSession.IsStartUpWaitEnabled) pSystemExe.WaitForExit();
                lg.LogIt("Start Up Process Complete");
            }
            return;
        }

        public static void RunShutDownApp()
        {
            //If ShutDownOn is set, then RunShutDown gets the postscan filepath from the Humason config file, if any
            //  then launches it and waits for completion.

            Process pSystemExe = new Process();
            if (FormHumason.openSession.ShutDownFilePath != null)
            {
                LogEvent lg = FormHumason.lg;
                lg.LogIt("Running Shut Down Process");
                pSystemExe.StartInfo.FileName = FormHumason.openSession.ShutDownFilePath;
                try
                {
                    pSystemExe.Start();
                    if (FormHumason.openSession.IsShutDownWaitEnabled) pSystemExe.WaitForExit();
                }
                catch { }
                lg.LogIt("Shut Down Process Complete");
            }
            return;
        }

    }
}

