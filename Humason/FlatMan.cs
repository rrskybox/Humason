using Planetarium;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Humason
{
    public class FlatMan
    {
        private int flatManComPort = 6;

        public FlatMan()
        {
            SessionControl openSession = new SessionControl();
            Port = openSession.FlatManComPort;
            return;
        }

        public int Port
        {
            get => flatManComPort;
            set
            {
                flatManComPort = value;
                SessionControl openSession = new SessionControl();
                openSession.FlatManComPort = value;
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
        public bool FlatManStage()
        {
            //Console routine to set up the scope to use the FlatMan
            //  to be called from CCDAP or other apps prior to running flats
            //
            //Routine will find the "My Flat Field" location via TSX, after
            //  it has been installed in the SDB (see instructions)
            //Then the slit will be closed, if open and dome homed and disconnected
            //The mount will then be sent to that position and tracking turned off

            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();

            lg.LogIt("Establishing TSX interfaces: star chart, mount, dome.");
            //If the dome is enabled,  Home it and disconnect
            if (openSession.IsDomeAddOnEnabled)
            {
                //Complete any dome commands, including homing and closing the dome, if needed
                //The mount will be parked and disconnected during this operation
                //Dome will be decoupled from mount
                if (openSession.IsDomeAddOnEnabled)
                {
                    lg.LogIt("Homing and Closing Dome");
                    lg.LogIt("Connecting Dome");
                    TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
                    //No idea how we could be here, but lg.LogIt the error and quit
                    //Clear any operation that might be underway for whatever bogus reason
                    lg.LogIt("Aborting any active dome command... again");
                    TSXLink.Dome.AbortDome();
                    //Wait for five seconds for everything to clear (Maxdome is a bit slow)
                    lg.LogIt("Waiting for dome operations to abort, if any");
                    System.Threading.Thread.Sleep(5000);
                    //Close Dome (if open) -- Close dome will home the dome before closing
                    TSXLink.Dome.CloseDome();
                    //Uncouple the dome from the mount
                    lg.LogIt("Disconnecting Dome");
                    TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Dome);
                }
            }
            //Connect to telescope, 
            // if manual set up, then park the mount (to keep it from tracking someplace bad)
            //  and ask the user for permission to continue, abort if user cancels, otherwise continue.
            lg.LogIt("Connecting mount");
            TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            if (openSession.IsFlatManManualSetupEnabled)
            {
                lg.LogIt("Parking mount to wait for manual flatman set up");
                TSXLink.Mount.Park();
                DialogResult dr = MessageBox.Show("Manual FlatMan set up selected:  Continue?", "Manual FlatMan Preparation", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel) return false;
            }
            //Unpark (if parked), look up My Flat Field, slew to it, turn off tracking
            TSXLink.Mount.UnPark();
            lg.LogIt("Looking up flat panel position");
            TSXLink.Target ffTarget = TSXLink.StarChart.FindTarget("MyFlatField");
            if (ffTarget == null)
            {
                lg.LogIt("Could not find My Flat Field");
                return false;
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
            lg.LogIt("FlatMan positioning complete");

            //All done -- garbage collect and exit
            return true;


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
