using System;

namespace Humason

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
            try { TSXLink.Dome.AbortDomeOperation(); }
            catch (Exception ex) { return false; }
            System.Threading.Thread.Sleep(1000);  //Wait for abort command to clear
            return true;
        }

        public static bool DomeStartUp()
        {
            //Method for connecting and initializing the TSX dome, if any
            // use exception handlers to check for dome commands, opt out if none
            //  couple the dome to telescope if everything works out
            try { TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome); }
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
                try { TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome); }
                catch { return false; }
                return TSXLink.Dome.IsCoupled;
            }
            set
            {
                try { TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome); }
                catch { return; }
                TSXLink.Dome.IsCoupled = value;
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
            TSXLink.Dome.IsCoupled = false;
            TSXLink.Mount.Park();
            return true;
        }

        public static bool OpenDome()
        {
            //Method to open dome
            SessionControl openSession = new SessionControl();
            //Save mount and dome connect states
            bool coupledState = TSXLink.Dome.IsCoupled;
            bool mountedState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Mount);
            bool domeState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Dome);
            //Disconnect the mount so the dome won't chase it
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
            //Connect the dome, assuming it might be disconnected for some reason, if it fails, reset the connection states
            if (!TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome))
            {
                if (mountedState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
                return false;
            }
            //Stop whatever the dome might have been doing, if it fails, reset the connection states
            if (!TSXLink.Dome.AbortDomeOperation())
            {
                if (mountedState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
                return false;
            }
            //Make sure dome decoupled
            IsDomeCoupled = false;
            //Slew dome to home position
            ReliableGoToDomeAz(openSession.DomeHomeAz);
            //Open Slit
            OpenSlitStarter();
            System.Threading.Thread.Sleep(10);  //Workaround for race condition in TSX
            while (!TSXLink.Dome.IsOpenComplete)
            { System.Threading.Thread.Sleep(1000); } //one second wait loop
            IsDomeCoupled = true;
            //Reset device states
            if (domeState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
            if (coupledState) IsDomeCoupled = true;
            if (mountedState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            return true;
        }

        public static bool CloseDome()
        {
            //Method for closing the TSX dome
            //Save mount and dome connect states
            bool coupledState = TSXLink.Dome.IsCoupled;
            bool mountedState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Mount);
            bool domeState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Dome);
            SessionControl openSession = new SessionControl();
            //Disconnect the mount
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
            //Connect dome and decouple the dome from the mount position, if it fails, reset the connection states
            if (!TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome))
            {
                if (mountedState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
                return false;
            }
            //Stop whatever the dome might have been doing, if it fails, reset the connection states
            if (!TSXLink.Dome.AbortDomeOperation())
            {
                if (mountedState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
                return false;
            }
            //Make sure dome decoupled
            IsDomeCoupled = false;
            //Goto home position using goto rather than home
            ReliableGoToDomeAz(openSession.DomeHomeAz);
            //Close slit
            InitiateCloseSlit();
            // Release task thread so TSX can start Close Slit -- Command in Progress exception otherwise
            System.Threading.Thread.Sleep(5000); 
            // Wait for close slit competion
            while (!TSXLink.Dome.IsCloseComplete)
                System.Threading.Thread.Sleep(1000);
            //Reset device states
            if (domeState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
            if (coupledState) IsDomeCoupled = true;
            if (mountedState) TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            return true;
        }

        /// <summary>
        /// Brings the dome to the home position after resetting to azimuth home-20
        /// -- will Park the mount
        /// </summary>
        /// <param name="domeHomeAz"></param>
        /// <returns></returns>
        public static bool HomeDome()
        {
            // use exception handlers to check for dome commands, opt out if none
            SessionControl openSession = new SessionControl();
            //Connect dome and decouple the dome from the mount position
            if (!TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome))
                return false;
            //Stop whatever the dome might have been doing, if any and wait a few seconds for it to clear
            if (!TSXLink.Dome.AbortDomeOperation())
                return false;

            InitiateFindHome();
            System.Threading.Thread.Sleep(1000); // Release task thread so TSX can start FindHome -- Command in Progress exception otherwise
            while (!TSXLink.Dome.IsFindHomeComplete)
                System.Threading.Thread.Sleep(1000);
            return true;
        }

        public static bool ReliableGoToDomeAz(double az)
        {
            //Slews dome to azimuth while avoiding lockup if already there
            //Disconnect the mount
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
            //Abort any other dome operations
            AbortDome();
            //Wait for command to clear
            System.Threading.Thread.Sleep(1000);
            //Decouple the dome
            TSXLink.Dome.IsCoupled = false;
            double currentAz = TSXLink.Dome.CurrentDomeAzm;
            if (currentAz - az > 1)
            {
                InitiateDomeGoTo(az);
                System.Threading.Thread.Sleep(5000);
                while (!TSXLink.Dome.IsGotoAzmComplete)
                    System.Threading.Thread.Sleep(1000);
            }
            return true;
        }

        private static void OpenSlitStarter()
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    failed = !TSXLink.Dome.OpenSlit();
                }
                catch (Exception ex)
                {
                    //Assume goto in progress error, wait until Goto is complete
                    System.Threading.Thread.Sleep(sleepOver);
                }
            }
            return;
        }

        private static void InitiateCloseSlit()
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    failed = !TSXLink.Dome.CloseSlit();
                }
                catch (Exception ex)
                {
                    //Assume goto in progress error, wait until Goto is complete
                    System.Threading.Thread.Sleep(sleepOver);
                }
            }
            return;
        }

        private static void InitiateDomeGoTo(double az)
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    failed = !TSXLink.Dome.GotoDomeAzm(az);
                }
                catch (Exception ex)
                {
                    //Assume goto in progress error, wait until Goto is complete
                    System.Threading.Thread.Sleep(sleepOver);
                }
            }
            return;
        }

        private static void InitiateFindHome()
        {
            //Operation in progress == 0
            int sleepOver = 1000;
            bool failed = true;

            while (failed)
            {
                try
                {
                    failed = !TSXLink.Dome.HomeSlit();
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
