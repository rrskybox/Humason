using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using TheSkyXLib;
using Planetarium;
using WeatherWatch;

namespace Humason
{

    public static class NHUtil
    {

        //Common utilities for TSX connections
        //
        // ConnectDevice():  Opens TSX (if  not already open) and connects mount, camera, filter, focuser or guider
        // ConnectAllDevices():  Opens TSX (if  not already open) and connects mount, camera, filter, focuser and guider
        //
        // DisconnectDevice:  return specif ic hardware to default state and disconnect
        // DisconnectAllDevices:  return all hardware to default state and disconnect
        //
        // Clear_Observing_List(targetname As String): Wipes out the current observing list and restores target name to list
        //
        //lg.LogIt appends a new line to the log file

        public static double ReduceTo360(double degrees)
        {
            degrees = Math.IEEERemainder(degrees, 360);
            if (degrees < 0)
            { degrees += 360; }
            return degrees;
        }

        public static void ButtonRed(Button genericButton)
        {
            genericButton.ForeColor = Color.Black;
            genericButton.BackColor = Color.LightSalmon;
            return;
        }

        public static void ButtonGreen(Button genericButton)
        {
            genericButton.ForeColor = Color.Black;
            genericButton.BackColor = Color.PaleGreen;
            return;
        }

        public static void ButtonNeutral(Button genericButton)
        {
            genericButton.ForeColor = Color.Black;
            genericButton.BackColor = Color.Turquoise;
            return;
        }

        public static bool IsButtonRed(Button genericButton)
        {
            if (genericButton.BackColor == Color.LightSalmon)
            { return true; }
            else
            { return false; }
        }

        public static bool IsButtonGreen(Button genericButton)
        {
            if (genericButton.BackColor == Color.PaleGreen)
            { return true; }
            else
            { return false; }
        }

        public static bool CloseEnough(double testval, double targetval, double percentnear)
        {
            //Cute little method for determining if a value is withing a certain percentatge of
            // another value.
            //testval is the value under consideration
            //targetval is the value to be tested against
            //npercentnear is how close (in percent of target val, i.e. x100) the two need to be within to test true
            // otherwise returns false

            if ((Math.Abs(targetval - testval)) <= (Math.Abs((targetval * percentnear / 100))))
            { return true; }
            else
            { return false; }
        }

        public static DateTime DateOfTime(DateTime theTime)
        {
            //Returns a new date time based on whether theTime is AM or PM
            //  Check the current AM or PM and compare against the program AM or PM
            //    if the same then leave the date as the same, 
            //    if different, then set date as next day (i.e. morning)
            DateTime upDate = DateTime.Now.Date + theTime.TimeOfDay;
            if (IsTomorrowAM(upDate))
            {
                upDate = upDate.AddDays(1);
            }
            return upDate;
        }

        private static bool IsTomorrowAM(DateTime dayTime)
        {
            //Returns true is this dayTime is between 0 and 11:59 and the current time is not, that is in the AM.
            if ((dayTime.Hour < 12) && (DateTime.Now.Hour > 12))
            { return true; }
            else
            { return false; }
        }
    }
}

