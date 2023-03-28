using System;
using TheSky64Lib;

namespace Planetarium
{
    public static class DomeControl
    {
        public const int HOMEPOSITION = 220;

        /// <summary>
        /// Aborts any outstanding dome operation
        /// </summary>
        /// <returns></returns>
        public static bool AbortDome()
        {
            sky6Dome tsxd = new sky6Dome();
            try { tsxd.Abort(); }
            catch (Exception ex) { return false; }
            System.Threading.Thread.Sleep(1000);  //Wait for abort command to clear
            return true;
        }

        public static bool DomeStartUp()
        {
            //Method for connecting and initializing the TSX dome, if any
            // use exception handlers to check for dome commands, opt out if none
            //  couple the dome to telescope if everything works out
            sky6Dome tsxd = new sky6Dome();
            try { tsxd.Connect(); }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// //Property for coupling the TSX dome, if any
        // use exception handlers to check for dome commands, opt out if none
        //  couple the dome to telescope if everything works out
        /// </summary>
        public static bool IsDomeCoupled
        {
            get
            {
                sky6Dome tsxd = new sky6Dome();
                try { tsxd.Connect(); }
                catch { return false; }
                int cState = tsxd.IsCoupled;
                if (cState == 0)
                {
                    return false;
                }
                else
                {
                    return (true);
                };
            }
            set
            {
                sky6Dome tsxd = new sky6Dome();
                try { tsxd.Connect(); }
                catch { return; }
                //If a connection is set, then make sure the dome is coupled to the telescope slews
                tsxd.IsCoupled = Convert.ToInt32(true);
                return;
            }
        }

        /// <summary>
        /// Parks the mount and decouples the dome (if not automatic)
        /// </summary>
        /// <returns>true if successful</returns>
        public static bool ParkAndDecouple()
        {
            //Decouple the dome from the mount position and park the mount
            sky6RASCOMTele tsxt = new sky6RASCOMTele();
            sky6Dome tsxd = new sky6Dome();
            IsDomeCoupled = false;
            TSXLink.Mount.Park();
            return true;
        }

        public static bool OpenDome(int domeHomeAz)
        {
            //Method to open dome
            //Assume the dome is properly positioned for power
            //Position the dome with at home (wipers on pads)
            // open the dome shutter
            sky6RASCOMTele tsxt = new sky6RASCOMTele();
            //Make sure dome is connected and decoupled
            IsDomeCoupled = false;
            //Disconnect the mount
            tsxt.Disconnect();

            sky6Dome tsxd = new sky6Dome();
            try { tsxd.Connect(); }
            catch { return false; }
            //Stop whatever the dome might have been doing, if any and wait a few seconds for it to clear
            try { tsxd.Abort(); }
            catch { }
            System.Threading.Thread.Sleep(10);
            //Goto home position using goto rather than home
            ReliableGoTo(domeHomeAz);
            //Open Slit
            OpenSlitStarter(tsxd);
            System.Threading.Thread.Sleep(10);  //Workaround for problme in TSX
            while (tsxd.IsOpenComplete == 0) { System.Threading.Thread.Sleep(1000); } //one second wait loop
            IsDomeCoupled = true;
            return true;
        }

        public static bool CloseDome(int domeHomeAz)
        {
            //Method for closing the TSX dome
            // use exception handlers to check for dome commands, opt out if none
            //Park Mount, if not parked already
            sky6RASCOMTele tsxt = new sky6RASCOMTele();
            //Connect dome and decouple the dome from the mount position
            IsDomeCoupled = false;
            //Disconnect the mount
            tsxt.Disconnect();

            sky6Dome tsxd = new sky6Dome();
            try { tsxd.Connect(); }
            catch { return false; }
            //Stop whatever the dome is doing, if any and wait a few seconds for it to clear
            try { tsxd.Abort(); }
            catch { }
            //Goto home position using goto rather than home
            ReliableGoTo(domeHomeAz);
            CloseSlitStarter(tsxd);
            System.Threading.Thread.Sleep(5000); // Release task thread so TSX can start Close Slit -- Command in Progress exception otherwise
            while (tsxd.IsCloseComplete == 0)
                System.Threading.Thread.Sleep(1000);
            //Check to see if slit got closed, if not, then try one more time
            //disconnect dome controller
            tsxd.Disconnect();
            return true;
        }

        /// <summary>
        /// Brings the dome to the home position after resetting to azimuth home-20
        /// -- will Park the mount
        /// </summary>
        /// <param name="domeHomeAz"></param>
        /// <returns></returns>
        public static bool HomeDome(int domeHomeAz)
        {
            // use exception handlers to check for dome commands, opt out if none
            //Decouple the dome from the mount position and park the mount
            ParkAndDecouple();

            //Connect to the dome and abort any dome commands
            sky6Dome tsxd = new sky6Dome();
            try { tsxd.Connect(); }
            catch { return false; }
            //Stop whatever the dome is doing, if any and wait a few seconds for it to clear
            if (!AbortDome()) return false;
            //Move dome to 20 degrees short of home position
            FindHomeStarter(tsxd);
            System.Threading.Thread.Sleep(1000); // Release task thread so TSX can start FindHome -- Command in Progress exception otherwise
            while (tsxd.IsFindHomeComplete == 0)
                System.Threading.Thread.Sleep(1000);
            return true;
        }

        /// <summary>
        /// Rotates the dome to the target Azimuth
        /// </summary>
        /// <param name="domeHomeAz"></param>
        /// <returns></returns>
        public static bool GoToDomeAz(int domeHomeAz)
        {
            //Method for opening the TSX dome
            // use exception handlers to check for dome commands, opt out if none
            //  couple the dome to telescope if everything works out
            sky6Dome tsxd = new sky6Dome();
            //Park Mount
            TSXLink.Mount.Park();
            //Abort any dome commands
            AbortDome();
            //Wait for command to clear
            System.Threading.Thread.Sleep(1000);
            GoToLoopStarter(tsxd, domeHomeAz);
            System.Threading.Thread.Sleep(1000); // Wait for dome controller to catch up
            while (tsxd.IsGotoComplete == 0)
            {
                System.Threading.Thread.Sleep(1000);
            }
            System.Threading.Thread.Sleep(1000);
            return true;
        }

        private static void ReliableGoTo(double az)
        {
            //Slews dome to azimuth while avoiding lockup if already there
            sky6Dome tsxd = new sky6Dome();
            //Abort any dome command and wait for it to clear
            tsxd.Abort();
            System.Threading.Thread.Sleep(1000);
            //Decouple the dome
            tsxd.IsCoupled = 0;
            tsxd.GetAzEl();
            double currentAz = tsxd.dAz;
            if (currentAz - az > 1)
            {
                GoToLoopStarter(tsxd, (int)az);
                System.Threading.Thread.Sleep(5000);
                while (tsxd.IsGotoComplete == 0)
                    System.Threading.Thread.Sleep(1000);
            }
            return;
        }

        private static void OpenSlitStarter(sky6Dome tsxd)
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    tsxd.OpenSlit();
                    failed = false;
                }
                catch (Exception ex)
                {
                    //Assume goto in progress error, wait until Goto is complete
                    System.Threading.Thread.Sleep(sleepOver);
                }
            }
            return;
        }

        private static void CloseSlitStarter(sky6Dome tsxd)
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    tsxd.CloseSlit();
                    failed = false;
                }
                catch (Exception ex)
                {
                    //Assume goto in progress error, wait until Goto is complete
                    System.Threading.Thread.Sleep(sleepOver);
                }
            }
            return;
        }

        private static void GoToLoopStarter(sky6Dome tsxd, int az)
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    tsxd.GotoAzEl(az, 0);
                    failed = false;
                }
                catch (Exception ex)
                {
                    //Assume goto in progress error, wait until Goto is complete
                    System.Threading.Thread.Sleep(sleepOver);
                }
            }
            return;
        }

        private static void FindHomeStarter(sky6Dome tsxd)
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    tsxd.FindHome();
                    failed = false;
                }
                catch (Exception ex)
                {
                    //Assume goto in progress error, wait until Goto is complete
                    System.Threading.Thread.Sleep(sleepOver);
                }
            }
            return;
        }



    }
}
