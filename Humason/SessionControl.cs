using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Humason
{
    public partial class SessionControl
    {
        const string SBQueryFolder = "Software Bisque\\TheSkyX Professional Edition\\Database Queries";
        const string SBFocusDataFolder = "Software Bisque\\TheSkyX Professional Edition\\Focuser Data";
        const string SBFOVIDataFolder = "Software Bisque\\TheSkyX Professional Edition\\Field of View Indicators";
        const string HumasonClearDBQFilename = "ClearTheListTheHardWay.dbq";
        const string HumasonSummaryFilename = "SequenceSummaries.xml";
        const string HumasonSessionControlFilename = "SessionControl.xml";
        const string HumasonDefaultPlanFileName = "Default.TargetPlan.xml";

        const string HumasonTargetPlanSearchPattern = "*.TargetPlan.xml";

        //file data container XNames
        const string HumasonSummaryXCName = "HumasonSummary";
        const string HumasonSessionControlXCName = "HumasonSessionControl";
        const string HumasonTargetPlanXCName = "HumasonTargetPlan";
        const string HumasonDefaultTargetFilePathXName = "DefaultTargetFilePath";
        const string DocumentsDirectoryPathXName = "DocumentsDirectoryPath";
        const string HumasonDirectoryPathXName = "HumasonDirectoryPath";
        const string DatabaseQueryPathXName = "DatabaseQueryPath";

        //common private names
        const string HumasonFolderName = "Humason";

        //Session File Path Element names
        const string CurrentTargetXName = "CurrentTargetName";
        const string SummaryFilePathXName = "SummaryFilePath";

        //private Session Control XML Element Names
        const string LastFileSequenceNumberXName = "LastFileSequenceNumber";

        //Humason main form parameters
        const string HomeMountEnabledXName = "HomeMountEnabled";
        const string ParkMountEnabledXName = "ParkMountEnabled";

        //Session Form and Autorun parameters
        const string AutoRunCheckedXName = "AutoRunChecked";
        const string StagingDateTimeCheckedXName = "StagingDateChecked";
        const string StagingDateTimePickerXName = "StagingDateTime";
        const string StagingFilePathXName = "StagingFilePath";
        const string StartUpDateTimeCheckedXName = "StartUpDateChecked";
        const string StartUpDateTimePickerXName = "StartUpDateTime";
        const string StartUpFilePathName = "StartUpFilePath";
        const string ShutDownDateTimeCheckedXName = "ShutDownDateChecked";
        const string ShutDownDateTimePickerXName = "ShutDownDateTime";
        const string ShutDownFilePathXName = "ShutDownFilePath";
        const string StartUpToolsXName = "StartUpTools";
        const string StartUpToolPathXName = "StartUpToolFilePath";
        const string StagingWaitEnabledXName = "StagingWaitEnabled";
        const string StartUpWaitEnabledXName = "StartUpWaitEnabled";
        const string ShutDownWaitEnabledXName = "ShutDownWaitEnabled";
        const string MinimumAltitudeXName = "MinimumAltitude";

        const string OverheadXName = "Overhead";

        const string RefocusAtTemperatureDifferenceXName = "RefocusAtTemperatureDifference";
        const string GuideStarEdgeMarginXName = "GuideStarEdgeMargin";

        const string FlatManPortNumXName = "FlatManPort";
        const string FlatLightSourceXName = "FlatLightSource";
        const string FlatManBrightnessXName = "FlatManBrightness";
        const string FlatsExposureTimeXName = "FlatsExposureTime";
        const string FlatsRepetitionsXName = "FlatsRepetitions";
        const string FlatsTargetADUXName = "FlatsTargetADU";
        const string FlatManEastCheckedXName = "FlatManEastChecked";
        const string FlatFlipCheckedXName = "FlatFlipChecked";
        const string FlatsRotationCheckedXName = "FlatsRotationChecked";
        const string FlatManManualSetupCheckedXName = "FlatManManualSetupChecked";


        //Rotator Definitions
        const string RotationEnabledXName = "RotationEnabled";
        const string RotatorDirectionXName = "RotatorDirection";

        //Options Definition (for FormOptions)
        const string WeatherCheckedXName = "WeatherMonitorEnabled";
        const string WeatherDataFilePathXName = "WeatherDataFilePath";
        const string DomeAddOnCheckedXName = "DomeAddOnEnabled";
        const string PowerManagerCheckedXName = "PowerManagerEnabled";
        const string RotatorDeviceEnabled = "RotatorDeviceEnabled";

        //Class data -- just saves some file access time

        public string DocumentsDirectoryPath { get; set; }
        public string HumasonDirectoryPath { get; set; }
        public string DatabaseQueryDirectoryPath { get; set; }
        public string FocuserDataFolder { get; set; }
        public string DefaultTargetPlanPath { get; set; }
        public string FOVIDataFolder { get; set; }

        public SessionControl()
        {
            //Create the Humason folder path for the base folder.
            DocumentsDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            HumasonDirectoryPath = DocumentsDirectoryPath + "\\" + HumasonFolderName;
            DatabaseQueryDirectoryPath = DocumentsDirectoryPath + "\\" + SBQueryFolder;
            FocuserDataFolder = DocumentsDirectoryPath + "\\" + SBFocusDataFolder;
            FOVIDataFolder = DocumentsDirectoryPath + "\\" + SBFOVIDataFolder;

            //Install the TSX DBQ to clean out the Observing List
            InstallDBQ(DatabaseQueryDirectoryPath);

            //Create the Humason folder, if it doesnt exist
            if (!Directory.Exists(HumasonDirectoryPath))
            { Directory.CreateDirectory(HumasonDirectoryPath); }

            //Create the session control file, if it doesn't exist, and save the token
            string sCtlPath = HumasonDirectoryPath + "\\" + HumasonSessionControlFilename;
            DirectXcess = new Axess(sCtlPath, HumasonSessionControlXCName);

            //Create the session summary xml file if it doesn't exist
            string tSumPath = HumasonDirectoryPath + "\\" + HumasonSummaryFilename;
            Axess tSum = new Axess(tSumPath, HumasonSummaryXCName);
            DirectXcess.ReplaceItem(SummaryFilePathXName, tSumPath);

            //Create the default target plan path
            DefaultTargetPlanPath = HumasonDirectoryPath + "\\" + HumasonDefaultPlanFileName;
        }

        public Axess DirectXcess { get; }

        public void InstallDBQ(string sbQueryPath)
        {
            //Installs the dbq file in the proper destination folder if it is not installed already.
            //
            //  Generate the install path from the defaults.     
            string dbqInstallPath = sbQueryPath + "\\" + HumasonClearDBQFilename;
            bool fileTest = File.Exists(dbqInstallPath);
            if (!File.Exists(dbqInstallPath))
            {
                Assembly dassembly = Assembly.GetExecutingAssembly();
                //Collect the file contents to be written
                Stream dstream = dassembly.GetManifestResourceStream("Humason." + HumasonClearDBQFilename);
                int dlen = Convert.ToInt32(dstream.Length);
                int doff = 0;
                byte[] dbytes = new byte[dstream.Length];
                int dreadout = dstream.Read(dbytes, doff, dlen);
                FileStream dbqfile = File.Create(dbqInstallPath);
                dbqfile.Close();
                //write to destination file
                File.WriteAllBytes(dbqInstallPath, dbytes);
                dstream.Close();
            }
        }

        public void AddSequenceCompleteSummary()
        {
            //Add a summary entry to the summary file
            string sfPath = DirectXcess.GetItem(SummaryFilePathXName);
            DirectXcess.AddXFileContents(sfPath);
        }

        public List<string> GetTargetFiles()
        {
            //Returns list of configuration filenames for targets
            //Get a list of files from the Humason directory

            List<string> targetNames = new List<string>();
            string[] tgtProspectPaths = Directory.GetFiles(HumasonDirectoryPath, HumasonTargetPlanSearchPattern);
            foreach (string fileProspect in tgtProspectPaths)
            {
                string[] fname = Path.GetFileNameWithoutExtension(fileProspect).Split('.');
                if ((fileProspect.Contains("TargetPlan")) && (!fileProspect.Contains("Active")))
                { targetNames.Add(fname[0]); }
            }
            return targetNames;
        }

        #region Session Control Properties

        public string SequentialFileNumber
        {
            //Gets or sets the file number sequence, as well as increments it for the next pass
            get
            {
                string fileNumStr = DirectXcess.GetItem(LastFileSequenceNumberXName);
                if (fileNumStr == null)
                { fileNumStr = "99"; }
                int fileNumInt = Convert.ToInt32(fileNumStr);
                fileNumInt++;
                DirectXcess.ReplaceItem(LastFileSequenceNumberXName, fileNumInt.ToString());
                return fileNumStr;
            }
            set => DirectXcess.ReplaceItem(LastFileSequenceNumberXName, value);
        }

        public string CurrentTargetName
        {
            //Gets or sets the full path to the Humason folder element
            get => DirectXcess.GetItem(CurrentTargetXName);
            set => DirectXcess.ReplaceItem(CurrentTargetXName, value);
        }

        //public string DefaultTargetPlanPath
        //{
        //    //Gets or sets the full path to the Humason default target plan element
        //    get => DirectXcess.GetItem(SessionControl.HumasonDefaultTargetFilePathXName);
        //    set => DirectXcess.ReplaceItem(SessionControl.HumasonDefaultTargetFilePathXName, value);
        //}

        public bool IsAutoRunEnabled
        {
            get => Convert.ToBoolean(DirectXcess.GetItem(AutoRunCheckedXName));
            set => DirectXcess.ReplaceItem(AutoRunCheckedXName, value);
        }

        public bool IsHomeMountEnabled
        {
            get => Convert.ToBoolean(DirectXcess.GetItem(HomeMountEnabledXName));
            set => DirectXcess.ReplaceItem(HomeMountEnabledXName, value);
        }

        public bool IsParkMountEnabled
        {
            get => Convert.ToBoolean(DirectXcess.GetItem(ParkMountEnabledXName));
            set => DirectXcess.ReplaceItem(ParkMountEnabledXName, value);
        }

        public bool IsWeatherEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(WeatherCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(WeatherCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(WeatherCheckedXName, value);
        }

        public string WeatherDataFilePath
        {
            get => DirectXcess.GetItem(WeatherDataFilePathXName);
            set => DirectXcess.ReplaceItem(WeatherDataFilePathXName, value);
        }

        public string StagingFilePath
        {
            get => DirectXcess.GetItem(StagingFilePathXName);
            set => DirectXcess.SetItem(StagingFilePathXName, value);
        }

        public string StartUpFilePath
        {
            get => DirectXcess.GetItem(StartUpFilePathName);
            set => DirectXcess.SetItem(StartUpFilePathName, value);
        }

        public string ShutDownFilePath
        {
            get => DirectXcess.GetItem(ShutDownFilePathXName);
            set => DirectXcess.SetItem(ShutDownFilePathXName, value);
        }

        public bool IsStagingEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(AutoRunCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(StagingDateTimeCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(StagingDateTimeCheckedXName, value);
        }

        public bool IsStartUpEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(AutoRunCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(StartUpDateTimeCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(StartUpDateTimeCheckedXName, value);
        }

        public bool IsShutDownEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(AutoRunCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(ShutDownDateTimeCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(ShutDownDateTimeCheckedXName, value);
        }

        public bool IsStagingWaitEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(StagingWaitEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(StagingWaitEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(StagingWaitEnabledXName, value);
        }

        public bool IsStartUpWaitEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(StartUpWaitEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(StartUpWaitEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(StartUpWaitEnabledXName, value);
        }

        public bool IsShutDownWaitEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(ShutDownWaitEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(ShutDownWaitEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(ShutDownWaitEnabledXName, value);
        }

        public DateTime StagingTime
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(StagingDateTimeCheckedXName)))
                { return Convert.ToDateTime(DirectXcess.GetItem(StagingDateTimePickerXName)); }
                else { return DateTime.Now; }
            }
            set => DirectXcess.ReplaceItem(StagingDateTimePickerXName, value.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        public DateTime StartUpTime
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(StartUpDateTimeCheckedXName)))
                { return Convert.ToDateTime(DirectXcess.GetItem(StartUpDateTimePickerXName)); }
                else { return DateTime.Now; }
            }
            set => DirectXcess.ReplaceItem(StartUpDateTimePickerXName, value.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        public DateTime ShutDownTime
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(ShutDownDateTimeCheckedXName)))
                { return Convert.ToDateTime(DirectXcess.GetItem(ShutDownDateTimePickerXName)); }
                else { return DateTime.Now; }
            }
            set => DirectXcess.ReplaceItem(ShutDownDateTimePickerXName, value.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        public int MinimumAltitude
        {
            get
            {
                if (DirectXcess.GetItem(MinimumAltitudeXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(MinimumAltitudeXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(MinimumAltitudeXName, value);
        }

        public int FlatManComPort
        {
            get
            {
                if (DirectXcess.GetItem(FlatManPortNumXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(FlatManPortNumXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(FlatManPortNumXName, value);
        }

        public bool IsFlatManEnabled
        {
            get
            {
                if (FlatLightSource == FlatManager.LightSource.lsFlatMan) { return true; }
                else { return false; }
            }
        }

        public bool IsRotationEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(RotationEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(RotationEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(RotationEnabledXName, value);
        }

        public int RotatorDirection
        {
            get
            {
                if (DirectXcess.GetItem(RotatorDirectionXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(RotatorDirectionXName)); }
                else { return -1; }
            }
            set => DirectXcess.ReplaceItem(RotatorDirectionXName, value);
        }

        public double RefocusAtTemperatureDifference
        {
            get
            {
                if (DirectXcess.GetItem(RefocusAtTemperatureDifferenceXName) != null)
                { return Convert.ToDouble(DirectXcess.GetItem(RefocusAtTemperatureDifferenceXName)); }
                else { return 1.0; }
            }
            set => DirectXcess.ReplaceItem(RefocusAtTemperatureDifferenceXName, value);
        }

        public Int16 GuideStarEdgeMargin
        {
            get
            {
                if (DirectXcess.GetItem(GuideStarEdgeMarginXName) != null)
                { return Convert.ToInt16(DirectXcess.GetItem(GuideStarEdgeMarginXName)); }
                else { return 32; }
            }
            set => DirectXcess.ReplaceItem(GuideStarEdgeMarginXName, value);
        }

        public bool IsFlatFlipEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(FlatFlipCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(FlatFlipCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(FlatFlipCheckedXName, value);
        }

        public bool IsFlatManEast
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(FlatManEastCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(FlatManEastCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(FlatManEastCheckedXName, value);
        }

        public bool IsFlatsRotationEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(FlatsRotationCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(FlatsRotationCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(FlatsRotationCheckedXName, value);
        }

        public bool IsFlatManManualSetupEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(FlatManManualSetupCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(FlatManManualSetupCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(FlatManManualSetupCheckedXName, value);
        }

        public FlatManager.LightSource FlatLightSource
        {
            //Returns configured source for flats: 0 = None, 1 = flatman, 2 = dusk, 3 = dawn (default 0)
            get
            {
                if (DirectXcess.GetItem(FlatLightSourceXName) != null)
                { return (FlatManager.LightSource)Enum.Parse(typeof(FlatManager.LightSource), DirectXcess.GetItem(FlatLightSourceXName)); }
                else
                {
                    DirectXcess.SetItem(FlatLightSourceXName, FlatManager.LightSource.lsNone.ToString());
                }
                return FlatManager.LightSource.lsFlatMan;
            }
            set => DirectXcess.SetItem(FlatLightSourceXName, value.ToString());
        }

        public double FlatsExposureTime
        {
            get
            {
                if (DirectXcess.GetItem(FlatsExposureTimeXName) != null)
                { return Convert.ToDouble(DirectXcess.GetItem(FlatsExposureTimeXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(FlatsExposureTimeXName, value);
        }

        public int FlatManBrightness
        {
            get
            {
                if (DirectXcess.GetItem(FlatManBrightnessXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(FlatManBrightnessXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(FlatManBrightnessXName, value);
        }

        public int FlatsTargetADU
        {
            get
            {
                if (DirectXcess.GetItem(FlatsTargetADUXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(FlatsTargetADUXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(FlatsTargetADUXName, value);
        }

        public int FlatsRepetitions
        {
            get
            {
                if (DirectXcess.GetItem(FlatsRepetitionsXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(FlatsRepetitionsXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(FlatsRepetitionsXName, value);
        }

        public double Overhead
        {
            get
            {
                if (DirectXcess.GetItem(OverheadXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(OverheadXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(OverheadXName, value);
        }

        public bool IsDomeAddOnEnabled
        {
            get
            {
                if (DirectXcess.GetItem(DomeAddOnCheckedXName) != null)
                { return Convert.ToBoolean(DirectXcess.GetItem(DomeAddOnCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(DomeAddOnCheckedXName, value);
        }

 
        #endregion

    }
}
