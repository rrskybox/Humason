using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planetarium;
using System.IO;

namespace Humason
{
    public partial class AutoFocus
    {
        //Class encapsulates autofocus 
        // Note that @Focus must be already configured for:
        //   1. Automatically slew to appropriate focus star or already centered on an appropriate focus star
        //   2. Automatically setting exposure time

        public static void FocusIt(int aftype)
        {
            //Execute TSX @Focus2 (ftype = 2) or @Focus3 (ftype = 3)
            //  Save current object information
            //  Open and connect Autofocus
            //  Turn on temperature compensation
            //  Move filter to clear filter
            //  Run Focus@2 or AtFocus 3 for all five filters

            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);

            lg.LogIt("Initiating Auto Focus" + aftype.ToString());
            AstroImage asti = new Humason.AstroImage
            {
                Camera = AstroImage.CameraType.Imaging,
                ImageReduction = AstroImage.ReductionType.AutoDark,
                Frame = AstroImage.ImageType.Light,
                Filter = tPlan.FocusFilter,
                Exposure = tPlan.FocusExposure,
                Delay = 0
            };

            TSXLink.Focus.RunTempComp();
            lg.LogIt("Focusing with filter " + asti.Filter.ToString());
            switch (aftype)
            {
                case 2:
                    TSXLink.Focus.RunAtFocusAny(asti, 2);
                    if (!TSXLink.DataWizard.Clear_Observing_List(tPlan.TargetName))
                    { lg.LogIt("Clear Observing List Failed"); }
                    break;
                case 3:
                    TSXLink.Focus.RunAtFocusAny(asti, 3);
                    break;
                default:
                    lg.LogIt("Unknown AtFocus selection -- focus failed");
                    break;
            }
            lg.LogIt("@Focus" + aftype.ToString() + " complete");
        }

        public static int ComputeNewFocusPosition(string focFilePath, double CurrentTemp, int filterIndex)
        {
            //Opens and interpolates .foc file for luminance filter, returns computed position in steps based on Current Temperature 
            LogEvent lg = FormHumason.lg;

            //File and filter data structure
            int FtextFields = 2;
            int FtextColorField = 0;
            int FtextDataField = 1;
            int FdataFieldCount = 4;
            int FposDataOffset = 1;
            int FtempDataOffset = 3;

            //Open foc file
            lg.LogIt("Reading focus file: " + focFilePath);
            StreamReader sr = new StreamReader(focFilePath);
            try
            {
                string FocuserCount = sr.ReadLine();
            }
            catch (Exception ex)
            {
                lg.LogIt("The focus file could not be found: " + ex.Message);
                return (0);
            }
            lg.LogIt("Focus file read");

            //Open, read in and partially parse the focus file to a text array,) { close it up 
            string ConfigName = sr.ReadLine();
            string FocuserName = sr.ReadLine();
            string FilterCountText = sr.ReadLine();
            int filtercount = Convert.ToInt32(FilterCountText);

            string[,] ftextdata = new string[filtercount, FtextFields];

            //int ftextrecord = 0;
            for (int ftextrecord = 0; ftextrecord < filtercount; ftextrecord++)
            {
                ftextdata[ftextrecord, FtextColorField] = sr.ReadLine();
                ftextdata[ftextrecord, FtextDataField] = sr.ReadLine();
            }
            sr.Close();

            //Parse out the Luminance data
            string[] fdata_lum = ftextdata[filterIndex, FtextDataField].Split(',');
            int focdatacount = (fdata_lum.Length - 1) / FdataFieldCount;
            if (focdatacount < 2)
            {
                lg.LogIt("Too few datapoints to compute focus");
                return (0);
            }
            //parse out temp and position tuples for luminance
            double[] tempdata = new double[30];
            double[] posdata = new double[30];
            for (int i = 0; i < focdatacount; i++)
            {
                posdata[i] = Convert.ToDouble(fdata_lum[(i * FdataFieldCount) + FposDataOffset + 1]);
                tempdata[i] = Convert.ToDouble(fdata_lum[(i * FdataFieldCount) + FtempDataOffset + 1]);
            }

            //Compute Least Mean Squares slope and intercept for focus data
            double posmean = 0;
            double tempmean = 0;
            for (int i = 0; i < focdatacount; i++)
            {
                posmean += posdata[i];
                tempmean += tempdata[i];
            }
            posmean = posmean / focdatacount;
            tempmean = tempmean / focdatacount;
            double sumtemppos = 0;
            double sumtemp = 0;
            for (int i = 0; i < focdatacount; i++)
            {
                sumtemppos += (posdata[i] - posmean) * (tempdata[i] - tempmean);
                sumtemp += Math.Pow((tempdata[i] - tempmean), 2);
            }
            double slope = sumtemppos / sumtemp;
            double intercept = posmean - (slope * tempmean);
            //Compute position for current temp
            double currentposition = intercept + slope * CurrentTemp;
            lg.LogIt("Focus position computed: " + ((int)currentposition).ToString());

            //return the computed position for the given temperature
            return ((int)currentposition);
        }

    }
}
