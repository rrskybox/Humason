//
//Author: Rick McAlister
//Date: June 18, 2015 (or earlier... much earlier)
//
//Public methods for analyzing image data
//
//Method ExposureAnalyis() computes optimal exposure times
//  for the current camera and sky conditions
// based on a 60 second image

// A recommendation window will open to display computed
//   optimal exposure length and duration for a one hour shoot,
//   based on analysis on the image currently open in TSX.
//
// The algorithm is based on work by ... John Smith: http://www.hiddenloft.com/notes/SubExposures.pdf
// and Charles Anstey: http://www.cloudynights.com/item.php?item_id=1622
//    
// Note:  Required parameters like "gain" are not supplied by TSX, they are arbitrarily set for
//   SBIG STF8300
//


//Revision notes:
//
//7/14/14: Added third algorithm from Steve Cannistra (http://www.starrywonders.com/snr.html)
//
//

using System;
using TheSkyXLib;

namespace Humason
{
    public class ImageAnalysis
    {
        public double i_smith_exp;
        public double i_smith_rep;
        public double i_anstey_exp;
        public double i_anstey_rep;
        public double i_cannistra_exp;
        public double i_cannistra_rep;

        public ImageAnalysis()
        { }

        public void ExposureAnalysis()
        {
            //Create image object
            ccdsoftImage tsx_im = new ccdsoftImage();
            int imgerr = 0;
            //Open the active image, if any
            try
            {
                imgerr = tsx_im.AttachToActiveImager();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("No Image Available:  " + imgerr.ToString());
                return;
            }

            string resultstr;
            int totalexp = 60; //Minutes for total exposure sequence

            double gain = 0.37;   //electrons per ADU for SBIG STF8000M as spec//d
            double pedestal = 0;  //base pedestal
            double rnoise = 9.3;  //read out noise in electrons
            double noisefac = 0.05; //maximum tolerable contribution of readout noise
            double slambda = 15;   //Faint target ADU minimum
            double snrtgt = 0.9;   //fraction of maximum achievable signal to noise ratio (Cannistra)

            int px;
            int py;
            int pxbin;
            int pybin;

            double exptime;

            double avgABU;
            double esky;
            double torn;

            int exp1;
            int reps1;
            int exp2;
            int reps2;
            int exp3;
            int reps3;

            //Presumably an Image Link has already been performed
            //Check on this is TBD

            px = tsx_im.FITSKeyword("NAXIS1");
            py = tsx_im.FITSKeyword("NAXIS2");
            pxbin = tsx_im.FITSKeyword("XBINNING");
            pybin = tsx_im.FITSKeyword("YBINNING");
            //gain = tsx_im.FITSKeyword("EGAIN");
            exptime = tsx_im.FITSKeyword("EXPTIME");

            px = px - 1;
            py = py - 1;

            avgABU = tsx_im.averagePixelValue();
            esky = ((avgABU - pedestal) * gain) / exptime;
            torn = Math.Pow(rnoise, 2) / ((Math.Pow((1 + noisefac), 2) - 1) * esky);

            exp1 = (int)(torn / 2);
            reps1 = (int)((((totalexp * 60) / torn) - 1) * 2);
            exp2 = (int)((slambda * Math.Sqrt(totalexp * 60)) / (2 * Math.Sqrt(avgABU / exptime)));
            reps2 = (int)((totalexp * 60) / exp2);
            exp3 = (int)((Math.Pow(snrtgt, 2) * Math.Pow(rnoise, 2)) / ((esky) * (1 - Math.Pow(snrtgt, 2))));
            reps3 = (int)((totalexp * 60) / exp3);

            i_smith_exp = exp1;
            i_smith_rep = reps1;
            i_anstey_exp = exp2;
            i_anstey_rep = reps2;
            i_anstey_exp = exp3;
            i_anstey_rep = reps3;

            resultstr = ("Smith Model (at tolerable noise factor = 0.05):" + "\r\n" +
                         "     " + exp1.ToString() + " second exposure with" + "\r\n" +
                         "     " + reps1.ToString() + " repetitions per hour." + "\r\n" + "\r\n" +
                         "Anstey Model (at faint target minimum = 15):" + "\r\n" +
                         "     " + exp2.ToString() + " second exposure with" + "\r\n" +
                         "     " + reps2.ToString() + " repetitions per hour." + "\r\n" + "\r\n" +
                         "Cannestra Model (at SNR = 90% of maximum):" + "\r\n" +
                         "     " + exp3.ToString() + " second exposure with" + "\r\n" +
                         "     " + reps3.ToString() + " repetitions per hour.");

            System.Windows.Forms.MessageBox.Show(resultstr, "Results", System.Windows.Forms.MessageBoxButtons.OK);
        }
    }
}

