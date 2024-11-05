using System;
using System.Diagnostics;
using System.Windows.Forms;
using static Humason.TSXLink.Connection;

namespace Humason
{
    public class FlatMan
    {
        private int flatManComPort = 6;
        private ASCOM.DriverAccess.CoverCalibrator device;

        public FlatMan()
        {
            SessionControl openSession = new SessionControl();
            Port = openSession.FlatManComPort;
            CreateFlatManDevice();
            return;
        }

        public void CreateFlatManDevice()
        {
            // Create this device
            try
            {
                device = new ASCOM.DriverAccess.CoverCalibrator("ASCOM.OptecAlnitak.CoverCalibrator");
                device.Connected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Flat Man driver error: " + ex.Message);
            }
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
                    device.CalibratorOn(50);
                else
                    device.CalibratorOff();
            }
        }

        public int Bright
        {
            set => device.CalibratorOn(value);
        }

    }
}
