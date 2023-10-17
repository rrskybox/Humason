using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWatch;

namespace Humason
{
    class WeatherHold
    {
        //Return true is delayed for weather.  Return false if not delayed.
        public static bool WeatherCheckAndDelay(SessionControl openSession)
        {
            LogEvent lg = new LogEvent();
            WeatherReader wrf = new WeatherReader(openSession.WeatherDataFilePath);
            if (!wrf.IsWeatherSafe())  //Unsafe condition
            {
                //Return true if a delayed

                //Unsafe weather condition:
                //  Park telescope
                //  Home dome
                //  Close dome

                lg.LogIt("Waiting on unsafe weather conditions...");
                lg.LogIt("Parking telescope, if park is enabled");
                //Park and disconnect the mount.  Homing will not work otherwise
                TSXLink.Mount.Park();
                if (openSession.HasDome)
                {
                    //Close dome -- dome is homed before closing
                    // Note that the mount will be left in the Park position
                    lg.LogIt("Closing Dome");
                    DomeControl.CloseDome();
                }
                do
                //Wait for conditions to improve by running a five minute wait
                // but enable the form for input, ect every second
                {
                    for (int i = 0; i < 300; i++) //Five minute wait loop
                    {
                        System.Windows.Forms.Application.DoEvents();
                        System.Threading.Thread.Sleep(1000);  //one second wait loop
                                                              //Check for shutdown time
                        if (LaunchPad.IsTimeToShutDown())
                        { break; };
                        //Check for abort
                        if (FormHumason.IsAborting())
                        {
                            lg.LogIt("Weather delay forced abort");
                            break;
                        }
                    }
                } while (!wrf.IsWeatherSafe());

                if (wrf.IsWeatherSafe())
                {
                    lg.LogIt("Weather conditions safe");
                    if (openSession.HasDome)
                    {
                        lg.LogIt("Opening Dome");
                        TSXLink.Dome.OpenSlit();
                    }
                    lg.LogIt("Unparking telescope");
                    TSXLink.Mount.UnPark();
                }
                return true;
            }
            else
                return false;
        }
    }
}
