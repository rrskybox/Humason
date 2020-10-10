using System;
using TheSkyXLib;

namespace Planetarium
{
    public static class DomeControl
    {
        /// <summary>
        /// Aborts any outstanding dome operation
        /// </summary>
        /// <returns></returns>
        public static bool AbortDome()
        {
            sky6Dome tsxd = new sky6Dome();
            try { tsxd.Abort(); }
            catch (Exception ex) { return false; }
            System.Threading.Thread.Sleep(5000);  //Wait for abort command to clear
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

        /// <summary>
        /// Opens the dome slit after rotating the dome to dome home position
        /// to align power connections
        /// </summary>
        /// <param name="domeHomeAz">Home aximuth of dome in degrees</param>
        public static bool OpenDome(int domeHomeAz)
        {
            //Method for opening the TSX dome
            // use exception handlers to check for dome commands, opt out if none
            //  couple the dome to telescope if everything works out
            sky6Dome tsxd = new sky6Dome();
            //Decouple the dome from the telescope
            IsDomeCoupled = false;
            //Move dome to 20 degrees short of home position
            try
            {
                tsxd.GotoAzEl(domeHomeAz - 20, 0);
                //Wait until complete (Sync mode doesn't seem to work)
                System.Threading.Thread.Sleep(5000);
                while (tsxd.IsGotoComplete == 0) { System.Threading.Thread.Sleep(5000); };
                //Home the dome,wait for the command to propogate, then wait until the dome reports it is homed
            }
            catch (Exception ex) { return false; }
            try
            {
                tsxd.FindHome();
                System.Threading.Thread.Sleep(5000);
                while (tsxd.IsFindHomeComplete == 0) { System.Threading.Thread.Sleep(5000); };
            }
            catch (Exception ex) { return false; }

            // open the dome shutter
            try
            {
                tsxd.OpenSlit();
                System.Threading.Thread.Sleep(10000);  //Wait for close command to clear TSX and ASCOM driver
                while (tsxd.IsOpenComplete == 0)
                { System.Threading.Thread.Sleep(5000); } //five second wait loop
            }
            catch (Exception ex) { return false; }
            return true;
        }

        /// <summary>
        /// Closes the dome slit and disconnects by
        /// decoupling the dome slews from the mount
        /// then slewing the dome to home to align power strips
        /// then closing the dome slit, leaving the dome control disconnected
        /// </summary>
        /// <param name="domeHomeAz">Azimuth of home position of dome</param>
        public static bool CloseDome(int domeHomeAz)
        {
            //Method for closing the TSX dome
            // use exception handlers to check for dome commands, opt out if none
            //
            //Decouple the dome from the mount then park the mount
            //Park Mount, if not parked already
            ParkAndDecouple();

            //Connect the dome (if disconected), 
            sky6Dome tsxd = new sky6Dome();
            try { tsxd.Connect(); }
            catch { return false; }
            //Stop whatever the dome is doing, if any and wait a few seconds for it to clear
            if (!AbortDome()) return false;

            //Close up the dome:  Connect, Home (so power is to the dome), Close the slit
            if (tsxd.IsConnected == 1)
            {
                //Move the dome to 20 degrees short of home
                HomeDome(domeHomeAz);
                System.Threading.Thread.Sleep(5000);
                while (tsxd.IsFindHomeComplete == 0) { System.Threading.Thread.Sleep(5000); };
                //Close slit
                bool slitClosed = false;
                try
                {
                    tsxd.CloseSlit();
                    System.Threading.Thread.Sleep(10000);
                    while (tsxd.IsCloseComplete == 0) { System.Threading.Thread.Sleep(5000); }
                    //Report success  
                    slitClosed = true;
                }
                catch
                {
                    slitClosed = false;
                }

                //Check to see if slit got closed, if not, then try one more time
                if (!slitClosed)
                {
                    tsxd.CloseSlit();
                    System.Threading.Thread.Sleep(10000);
                    try
                    {
                        while (tsxd.IsCloseComplete == 0) { System.Threading.Thread.Sleep(5000); }
                        //Report success  
                    }
                    catch
                    {
                    }
                }
            }
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
            try
            {
                tsxd.GotoAzEl(domeHomeAz - 20, 0);
                System.Threading.Thread.Sleep(5000); // Wait for dome controller to catch up
                while (tsxd.IsGotoComplete == 0) { System.Threading.Thread.Sleep(1000); }
            }
            catch { return false; };
            //Find Home
            try
            {
                tsxd.FindHome();
                System.Threading.Thread.Sleep(5000); // Wait for dome controller to catch up
                while (tsxd.IsFindHomeComplete == 0) { System.Threading.Thread.Sleep(1000); }
            }
            catch { return false; }
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
            System.Threading.Thread.Sleep(5000);
            try { tsxd.GotoAzEl(domeHomeAz - 20, 0); }
            catch (Exception ex) { return false; }
            System.Threading.Thread.Sleep(5000); // Wait for dome controller to catch up
            while (tsxd.IsGotoComplete == 0)
            {
                System.Threading.Thread.Sleep(1000);
            }
            return true;
        }

    }
}
