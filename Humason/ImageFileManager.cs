///Class for all the file management methods needed by Humason
/// 
/// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TheSkyXLib;

namespace Humason
{
    public partial class ImageFileManager
    {

        /// <summary>
        /// Saves the most recent image capture to Humason image directory for that night (as defined by start time)
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="filterName"></param>
        /// <param name="targetPA"></param>
        /// <param name="sidePoint"></param>
        public static void SaveLightImage(string targetName, string filterName, string targetPA, string sidePoint)
        {
            //The NH image directory originates from the NH form and stored in the Session class.
            //Get Humason directory name, create image directory if it doesn't exist yet
            LogEvent lg = FormHumason.lg;
            string nhDirName = FormHumason.openSession.HumasonDirectoryPath;
            string nhImageDirName = nhDirName + "\\Images";
            if (!Directory.Exists(nhImageDirName))
            { Directory.CreateDirectory(nhImageDirName); }

            //Create date name for image sub-directory, create if it doesn't exist yet
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);

            DateTime sequenceStartDate = tPlan.SequenceStartTime;
            string targetImageDir = nhImageDirName + "\\" + sequenceStartDate.ToString("yyyyMMdd") + "_" + targetName;
            if (!Directory.Exists(targetImageDir))
            { Directory.CreateDirectory(targetImageDir); }
            //Create Data Files directory if it doesn't exit yet
            string targetImageDataDir = targetImageDir + "\\Data Files";
            if (!Directory.Exists(targetImageDataDir))
            { Directory.CreateDirectory(targetImageDataDir); }

            //Reduce target PA to integer string, i.e. scrape off the decimal
            targetPA = (Convert.ToDouble(targetPA)).ToString("0");
            string targetImageDataPath = targetImageDataDir + "\\" +
                                            filterName +
                                            targetName +
                                            "_" +
                                            targetPA +
                                            sidePoint +
                                            "." +
                                            FormHumason.openSession.SequentialFileNumber.ToString() +
                                            ".fit";
            //open TSX camera and get the last image
            ccdsoftImage tsxi = new ccdsoftImage();
            int camStatus = tsxi.AttachToActiveImager();
            //save handling an exception here until some future date

            //Add some FITSKeywords for future reference
            //Correct the OBJECT Keyword if using coordinates instead of a target name
            tsxi.setFITSKeyword("OBJECT", targetName);
            //Enter the rotator angle
            if (tPlan.RotatorEnabled)
            {
                tsxi.setFITSKeyword("ROTATOR", Rotator.RealRotatorPA.ToString());
            }
            //Enter Image Position Angle as saved
            tsxi.setFITSKeyword("ORIENTAT", tPlan.TargetPA);

            //Set save path and save
            tsxi.Path = targetImageDataPath;
            tsxi.Save();
            lg.LogIt("Image saved: " + targetImageDataPath);
        }

        public static void SaveFlatImage(string targetName, string filterName, string targetPA, string sidePoint)
        {
            //The NH image directory originates from the SetUp form and stored in the
            //Configuration file.
            LogEvent lg = FormHumason.lg;
            TargetPlan tPlan = new TargetPlan(FormHumason.openSession.CurrentTargetName);
            //Get Humason directory name, create image directory if it doesn't exist yet
            string nhDirName = FormHumason.openSession.HumasonDirectoryPath;
            string nhImageDirName = nhDirName + "\\Images";
            if (!Directory.Exists(nhImageDirName))
            { Directory.CreateDirectory(nhImageDirName); }
            //Create date name for image sub-directory, create if it doesn't exist yet
            DateTime sequenceStartDate = tPlan.SequenceStartTime;
            string targetImageDir = nhImageDirName + "\\" + sequenceStartDate.ToString("yyyyMMdd");
            if (!Directory.Exists(targetImageDir))
            { Directory.CreateDirectory(targetImageDir); }
            //Create Data Files directory if it doesn't exit yet
            string targetImageDataDir = targetImageDir + "\\Calibration Files";
            if (!Directory.Exists(targetImageDataDir))
            { Directory.CreateDirectory(targetImageDataDir); }

            //Reduce target PA to integer string, i.e. scrape off the decimal
            targetPA = (Convert.ToDouble(targetPA)).ToString("0");
            string targetImageDataPath = targetImageDataDir + "\\" +
                                            filterName +
                                            targetName +
                                            "_" +
                                            targetPA +
                                            "PA" +
                                            sidePoint +
                                            "." +
                                            FormHumason.openSession.SequentialFileNumber.ToString() +
                                            ".fit";
            //open TSX camera and get the last image
            ccdsoftImage tsxi = new ccdsoftImage();
            int camStatus = tsxi.AttachToActiveImager();
            //save handling an exception here until some future date
            tsxi.setFITSKeyword("OBJECT", "Humason Flat Field");
            AstroImage tsxc = new AstroImage();
            if (tPlan.RotatorEnabled)
            {
                tsxi.setFITSKeyword("ROTATOR", Rotator.RealRotatorPA.ToString());
            }
            //Set save path and save
            tsxi.Path = targetImageDataPath;
            tsxi.Save();
            lg.LogIt("Flat saved: " + targetImageDataPath);
        }
    }



}
