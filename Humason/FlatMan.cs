using Planetarium;
using System;
using System.Diagnostics;
using TheSkyXLib;

namespace Humason
{
    public class FlatMan
    {
        private int flatManComPort = 6;

        public FlatMan()
        {
            Port = FormHumason.openSession.FlatManComPort;
            return; }

        public int Port
        {
            get => flatManComPort;
            set
            {
                flatManComPort = value;
                FormHumason.openSession.FlatManComPort = value;
                return;
            }
        }

        public bool Light
        {
            set
            {
                if (value == true)
                { FMCommand(flatManComPort, "L", null); }
                else
                { FMCommand(flatManComPort, "D", null); }
            }
        }

        public int Bright
        {
            set => FMCommand(flatManComPort, "B", value.ToString());
        }

        /// <summary>
        /// FMSetUp
        /// Prepares imaging for flats.  Closes dome, points telescope at MyFlat
        /// </summary>
        public void FlatManStage()
        {
            //Console routine to set up the scope to use the FlatMan
            //  to be called from CCDAP or other apps prior to running flats
            //
            //Routine will find the "My Flat Field" location via TSX, after
            //  it has been installed in the SDB (see instructions)
            //The mount will then be sent to that position and tracking turned off

            //Then the slit will be closed, if open and dome homed and disconnected
            LogEvent lg = new LogEvent();

            lg.LogIt("Establishing TSX interfaces: star chart, mount, dome.");
            if (FormHumason.openSession.IsDomeAddOnEnabled)
            {
                //Abort any dome command in progress, wait for it to take then disconnect the dome so it doesn//t chase the mount
                lg.LogIt("Aborting any current dome activity and disconnecting from the dome");
                TSXLink.Dome.AbortDome();
                System.Threading.Thread.Sleep(5000);
                //tsxd.IsCoupled = 0;
                TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Dome);
            }
            //Connect to telescope, Unpark (if parked), look up My Flat Field, slew to it, turn off tracking
            lg.LogIt("Connecting mount");
            TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);

            TSXLink.Mount.UnPark();
            lg.LogIt("Looking up flat panel position");
            TSXLink.Target ffTarget = TSXLink.StarChart.FindTarget("MyFlatField");
            if (ffTarget == null)
            {
                lg.LogIt("Could not find My Flat Field");
                return;
            }
            double altitude = ffTarget.Altitude;
            double azimuth = ffTarget.Azimuth;
            lg.LogIt("Slewing to flat panel position");
            TSXLink.Mount.SlewRADec(ffTarget.RA, ffTarget.Dec, ffTarget.Name);
            lg.LogIt("Turning off tracking");
            TSXLink.Mount.TurnTrackingOff();
            //Disconnect from mount
            lg.LogIt("Disconnecting Mount");
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
            if (FormHumason.openSession.IsDomeAddOnEnabled)
            {
                lg.LogIt("Reconnecting Dome");
                TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
                //No idea how we could be here, but lg.LogIt the error and quit
                //Clear any operation that might be underway for whatever bogus reason
                lg.LogIt("Aborting any active dome command... again");
                TSXLink.Dome.AbortDome();
                //Wait for five seconds for everything to clear (Maxdome is a bit slow)
                lg.LogIt("Waiting for dome operations to abort, if any");
                System.Threading.Thread.Sleep(5000);
                //Home the dome
                TSXLink.Dome.HomeDome();
                //Close Dome (if open)
                TSXLink.Dome.CloseDome();
                //Uncouple the dome from the mount
                //tsxd.IsCoupled = 0;
                lg.LogIt("Disconnecting Dome");
                TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Dome);
            }
            lg.LogIt("Connecting Mount");
            TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            lg.LogIt("Turning Tracking Off");
            TSXLink.Mount.TurnTrackingOff();
            lg.LogIt("FlatMan positioning complete");

            //All done -- garbage collect and exit
            return;

        }

        /// </summary>
        /// FMCommand
        /// FlatMan Command Sequence using AAcmd.exe
        /// </summary> 
        /// <param name="fmPort"></param>
        /// <param name="fmCommand"></param>
        /// <param name="fmParam"></param>
        /// <returns></returns>
        private static string FMCommand(int fmPort, string fmCommand, string fmParam)
        {
            LogEvent lg = new LogEvent();
            Process cmd = new Process();
            cmd.StartInfo.FileName = "C:\\Program Files (x86)\\Optec\\Alnitak Astrosystems Controller\\AACmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Arguments = fmPort.ToString() + " " + fmCommand + fmParam;
            try
            {
                cmd.Start();
            }
            catch (Exception ex)
            {
                lg.LogIt("FlatMan command failed: " + ex.Message);
                return null;
            }
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return (cmd.StandardOutput.ReadToEnd());
        }

    }
}
