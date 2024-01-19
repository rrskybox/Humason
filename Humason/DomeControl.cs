// --------------------------------------------------------------------------------
// ExoScan module
//
// Description:	
//
// Environment:  Windows 10 executable, 32 and 64 bit
//
// Usage:        TBD
//
// Author:		(REM) Rick McAlister, rrskybox@yahoo.com
//
// Edit Log:     Rev 1.0 Initial Version
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 
// ---------------------------------------------------------------------------------
//

using System;
using TheSky64Lib;

namespace Humason

{
    public static class DomeControl
    {
        public static bool DomeStartUp()
        {
            //Method for connecting and initializing the TSX dome, if any
            // use exception handlers to check for dome commands, opt out if none
            //  couple the dome to telescope if everything works out
            sky6Dome tsxd = new sky6Dome();
            LogEntry("Connecting Dome");
            try { tsxd.Connect(); }
            catch { return false; }
            //If a connection is set, then make sure the dome is coupled to the telescope slews
            LogEntry("Coupling Dome");
            TSXLink.Dome.IsCoupled = true;
            LogEntry("Unparking Dome, if needed");
            System.Threading.Thread.Sleep(5000);
            TSXLink.Dome.UnparkDome();
            System.Threading.Thread.Sleep(5000);
            return true;
        }

        public static bool OpenDome()
        {
            //Method for opening the TSX dome
            //Method to open dome
            //Save mount and dome connect states
            bool mountedState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Mount);
            bool domeState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Dome);
            //Make sure dome decoupled
            LogEntry("Uncoupling Dome from mount (although this doen't work for tracking, yet");
            TSXLink.Dome.IsCoupled = false;
            //Disconnect the mount so the dome won't chase it
            LogEntry("Disconnecting Mount");
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
            //Connect the dome, assuming it might be disconnected for some reason, if it fails, reset the connection states
            LogEntry("Connecting Dome");
            if (!TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome))
            {
                if (TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Mount))
                    return false;
            }
            //Stop whatever the dome might have been doing, if it fails, reset the connection states
            LogEntry("Aborting Dome Commands");
            if (!TSXLink.Dome.AbortDomeOperation())
            {
                if (TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Mount))
                    return false;
            }
            //Park Dome
            LogEntry("Bringing dome to park position");
            TSXLink.Dome.DomeParkReliably();
            //Open Slit
            LogEntry("Opening dome slit");
            TSXLink.Dome.OpenSlit();
            //Give a wait to get goint
            System.Threading.Thread.Sleep(5000);
            while (!TSXLink.Dome.IsOpenComplete)
                System.Threading.Thread.Sleep(1000);
            //Unpark the dome so it can chase the mount
            System.Threading.Thread.Sleep(5000);
            LogEntry("Unparking dome, if parked");
            TSXLink.Dome.UnparkDome();
            System.Threading.Thread.Sleep(5000);
            while (!TSXLink.Dome.IsUnparkComplete)
                System.Threading.Thread.Sleep(1000);
            //Enable mount chasing
            System.Threading.Thread.Sleep(5000);
            LogEntry("Coupling dome to mount");
            TSXLink.Dome.IsCoupled = true;

            //Reset device states
            if (domeState)
                TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
            if (mountedState)
                TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            return true;

        }

        public static bool CloseDome()
        {
            //Method for closing the TSX dome
            //Save mount and dome connect states
            //Note that if close dome fails, the mount is not reconnected nor the dome recoupled
            //  in case it is chasing a target below horizon
            bool mountedState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Mount);
            bool domeState = TSXLink.Connection.IsConnected(TSXLink.Connection.Devices.Dome);
            //Disconnect the mount
            LogEntry("Disconnecting mount");
            TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            //Decouple the dome
            LogEntry("Uncoupling dome to mount -- except for tracking as of now");
            TSXLink.Dome.IsCoupled = false;
            //Connect dome and decouple the dome from the mount position, if it fails, reset the connection states
            LogEntry("Connecting dome, if needed");
            TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
            //Stop whatever the dome might have been doing, if it fails, reset the connection states
            LogEntry("Aborting any outstanding dome commandes");
            if (!TSXLink.Dome.AbortDomeOperation())
                return false;
            //Park Dome
            LogEntry("Bringing dome to home/park positing and unparking there");
            TSXLink.Dome.DomeParkReliably();
            //Close slit
            System.Threading.Thread.Sleep(5000);
            LogEntry("Closing dome slit");
            TSXLink.Dome.CloseSlit();
            // Release task thread so TSX can start Close Slit -- Command in Progress exception otherwise
            System.Threading.Thread.Sleep(5000);
            // Wait for close slit competion or receive timout -- meaning that the battery has failed, probably
            while (!TSXLink.Dome.IsCloseComplete)
                System.Threading.Thread.Sleep(1000);
            //Reset device states
            //Reset device states
            if (domeState)
                TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
            if (mountedState)
                TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            LogEntry("Dome closing complete");
            return true;
        }

        public static bool ReliableGoToDomeAz(double az)
        {
            //Slews dome to azimuth while avoiding lockup if already there
            //  Mount will be disconnect upon return
            //Make sure the mount is disconnected
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
            //Abort any other dome operations
            TSXLink.Dome.AbortDomeOperation();
            //Decouple the dome, although it wont help for tracking
            TSXLink.Dome.IsCoupled = false;
            double currentAz = TSXLink.Dome.CurrentDomeAzm;
            if (currentAz - az > 1)
            {
                TSXLink.Dome.GotoDomeAzm(az - 10);
                System.Threading.Thread.Sleep(5000);
                while (!TSXLink.Dome.IsGotoAzmComplete)
                    System.Threading.Thread.Sleep(1000);
            }
            return true;
        }

        public static bool HomeDome()
        {
            //Straightforward homing -- could fail
            return TSXLink.Dome.HomeDome();
        }

        public static bool IsDomeCoupled
        {
            //Straightforward couple check -- could fail
            get { return TSXLink.Dome.IsCoupled; }
            set { TSXLink.Dome.IsCoupled = value; }
        }

        private static void LogEntry(string upd)
        //Method for projecting log entry to the SuperScan Main Form
        {
            LogEvent lg = new LogEvent();
            lg.LogIt(upd);
        }

    }
}


