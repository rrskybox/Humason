using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Humason
{
    public partial class SessionControl
    {
        const string HumasonQueryFolder = "Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries";
        const string HumasonFocusDataFolder = "Documents\\Software Bisque\\TheSkyX Professional Edition\\Focuser Data";
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

        //common private names
        const string HumasonFolderName = "Humason";

        //Session File Path Element names
        const string CurrentTargetXName = "CurrentTargetName";
        const string SummaryFilePathXName = "SummaryFilePath";

        //private Session Control XML Element Names
        const string LastFileSequenceNumberXName = "LastFileSequenceNumber";

        const string AutoRunCheckedXName = "AutoRunChecked";
        const string HomeMountEnabledXName = "HomeMountEnabled";
        const string ParkMountEnabledXName = "ParkMountEnabled";
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

        const string OverheadXName = "Overhead";

        const string RefocusAtTemperatureDifferenceXName = "RefocusAtTemperatureDifference";

        const string FlatManPortNumXName = "FlatManPort";
        const string FlatLightSourceXName = "FlatLightSource";
        const string FlatManBrightnessXName = "FlatManBrightness";
        const string FlatsExposureTimeXName = "FlatsExposureTime";
        const string FlatsRepetitionsXName = "FlatsRepetitions";
        const string FlatsTargetADUXName = "FlatsTargetADU";
        const string FlatManEastCheckedXName = "FlatManEastChecked";
        const string FlatFlipCheckedXName = "FlatFlipChecked";
        const string FlatsRotationCheckedXName = "FlatsRotationChecked";

        const string CameraPowerCheckedXName = "CameraPowerChecked";
        const string OTAFanPowerCheckedXName = "OTAFanPowerChecked";
        const string DewHeaterCheckedXName = "DewHeaterChecked";
        const string PowerManagerCOMPortXName = "PowerManagerCOMPort";
        const string PowerManagerMinimumVoltageXName = "PowerManagerMinimumVoltage";
        const string PowerManagerVoltageAdjustmentXName = "PowerManageVoltageAdjustment";
        const string PrimaryHeaterDutyCycleXName = "PrimaryHeaterDutyCycle";
        const string SecondaryHeaterDutyCycleXName = "SecondaryHeaterDutyCycle";

        //Rotator Definitions
        const string RotatorEnabledXName = "RotatorEnabled";
        const string RotatorDirectionXName = "RotatorDirection";

        //Options Definition (for FormOptions)
        const string WeatherCheckedXName = "WeatherChecked";
        const string WeatherDataFilePathXName = "WeatherDataFilePath";
        const string DomeAddOnCheckedXName = "DomeAddOnChecked";
        const string PowerManagerCheckedXName = "PowerManagerChecked";

        //Class data -- just saves some file access time

        public static string hDirectoryPath;  //path to Humason directory

        public SessionControl()
        {
            //Create the Humason folder path for the base folder.
            hDirectoryPath = "C:\\Users\\" + System.Environment.UserName + "\\Documents\\" + HumasonFolderName;
            //Install the TSX DBQ to clean out the Observing List
            InstallDBQ();

            //Create the Humason folder, if it doesnt exist
            if (!Directory.Exists(hDirectoryPath + "\\" + HumasonFolderName))
            { Directory.CreateDirectory(hDirectoryPath); }

            //Create the session control file, if it doesn't exist, and save the token
            string sCtlPath = hDirectoryPath + "\\" + HumasonSessionControlFilename;
            DirectXcess = new Axess(sCtlPath, HumasonSessionControlXCName);

            //Create the session summary xml file if it doesn't exist
            string tSumPath = hDirectoryPath + "\\" + HumasonSummaryFilename;
            Axess tSum = new Axess(tSumPath, HumasonSummaryXCName);
            DirectXcess.ReplaceItem(SummaryFilePathXName, tSumPath);
        }

        public string HumasonDirectoryPath => hDirectoryPath;

        public Axess DirectXcess { get; }

        public void InstallDBQ()
        {
            //Installs the dbq file in the proper destination folder if it is not installed already.
            //
            //  Generate the install path from the defaults.            
            string DBQInstallPath = "C:\\Users\\" + System.Environment.UserName + "\\" + HumasonQueryFolder + "\\" + HumasonClearDBQFilename;
            //string DBQInstallPath = "This PC" + "\\" + HumasonQueryFolder + "\\" + HumasonClearDBQFilename;
            if (!File.Exists(DBQInstallPath))
            {
                Assembly dassembly = Assembly.GetExecutingAssembly();
                //Collect the file contents to be written
                Stream dstream = dassembly.GetManifestResourceStream("Humason." + HumasonClearDBQFilename);
                int dlen = Convert.ToInt32(dstream.Length);
                int doff = 0;
                byte[] dbytes = new byte[dstream.Length];
                int dreadout = dstream.Read(dbytes, doff, dlen);
                FileStream dbqfile = File.Create(DBQInstallPath);
                dbqfile.Close();
                //write to destination file
                File.WriteAllBytes(DBQInstallPath, dbytes);
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
            string[] tgtProspectPaths = Directory.GetFiles(hDirectoryPath, HumasonTargetPlanSearchPattern);
            foreach (string fileProspect in tgtProspectPaths)
            {
                string[] fname = Path.GetFileNameWithoutExtension(fileProspect).Split('.');
                if ((fileProspect.Contains("TargetPlan")) && (!fileProspect.Contains("Active")))
                { targetNames.Add(fname[0]); }
            }
            return targetNames;
        }

        #region Session Control Properties

        public string DefaultFilePath => hDirectoryPath + "\\" + "Default" + "." + TargetPlan.HumasonTargetPlanFilename;

        //public string DefaultFilePath => DirectXcess.GetItem(SessionControl.HumasonDefaultTargetFilePathXName);

        public string FocuserDataFolder => (hDirectoryPath + "\\" + HumasonFocusDataFolder);

        public string SequentialFileNumber
        {
            //Gets or sets the file number sequence, as well as increments it for the next pass
            get
            {
                string fileNumStr = DirectXcess.GetItem(SessionControl.LastFileSequenceNumberXName);
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
            get => DirectXcess.GetItem(SessionControl.CurrentTargetXName);
            set => DirectXcess.ReplaceItem(SessionControl.CurrentTargetXName, value);
        }

        public string DefaultTargetPlanPath
        {
            //Gets or sets the full path to the Humason default target plan element
            get => DirectXcess.GetItem(SessionControl.HumasonDefaultTargetFilePathXName);
            set => DirectXcess.ReplaceItem(SessionControl.HumasonDefaultTargetFilePathXName, value);
        }

        public bool IsAutoRunEnabled
        {
            get => Convert.ToBoolean(DirectXcess.GetItem(SessionControl.AutoRunCheckedXName));
            set => DirectXcess.ReplaceItem(SessionControl.AutoRunCheckedXName, value);
        }

        public bool IsHomeMountEnabled
        {
            get => Convert.ToBoolean(DirectXcess.GetItem(SessionControl.HomeMountEnabledXName));
            set => DirectXcess.ReplaceItem(SessionControl.HomeMountEnabledXName, value);
        }

        public bool IsParkMountEnabled
        {
            get => Convert.ToBoolean(DirectXcess.GetItem(SessionControl.ParkMountEnabledXName));
            set => DirectXcess.ReplaceItem(SessionControl.ParkMountEnabledXName, value);
        }

        public bool IsWeatherEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.WeatherCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.WeatherCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.WeatherCheckedXName, value);
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
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.AutoRunCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StagingDateTimeCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.StagingDateTimeCheckedXName, value);
        }

        public bool IsStartUpEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.AutoRunCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StartUpDateTimeCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.StartUpDateTimeCheckedXName, value);
        }

        public bool IsShutDownEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.AutoRunCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.ShutDownDateTimeCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.ShutDownDateTimeCheckedXName, value);
        }

        public bool IsStagingWaitEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StagingWaitEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StagingWaitEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.StagingWaitEnabledXName, value);
        }

        public bool IsStartUpWaitEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StartUpWaitEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StartUpWaitEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.StartUpWaitEnabledXName, value);
        }

        public bool IsShutDownWaitEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.ShutDownWaitEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.ShutDownWaitEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.ShutDownWaitEnabledXName, value);
        }

        public DateTime StagingTime
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StagingDateTimeCheckedXName)))
                { return Convert.ToDateTime(DirectXcess.GetItem(SessionControl.StagingDateTimePickerXName)); }
                else { return DateTime.Now; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.StagingDateTimePickerXName, value.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        public DateTime StartUpTime
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.StartUpDateTimeCheckedXName)))
                { return Convert.ToDateTime(DirectXcess.GetItem(SessionControl.StartUpDateTimePickerXName)); }
                else { return DateTime.Now; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.StartUpDateTimePickerXName, value.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        public DateTime ShutDownTime
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.ShutDownDateTimeCheckedXName)))
                { return Convert.ToDateTime(DirectXcess.GetItem(SessionControl.ShutDownDateTimePickerXName)); }
                else { return DateTime.Now; }
            }

            set => DirectXcess.ReplaceItem(SessionControl.ShutDownDateTimePickerXName, value.ToString("yyyy/MM/dd HH:mm:ss"));
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

        public bool IsRotatorEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.RotatorEnabledXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.RotatorEnabledXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.RotatorEnabledXName, value);
        }

        public int RotatorDirection
        {
            get
            {
                if (DirectXcess.GetItem(RotatorDirectionXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(RotatorDirectionXName)); }
                else { return -1; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.RotatorDirectionXName, value);
        }

        public double RefocusAtTemperatureDifference
        {
            get
            {
                if (DirectXcess.GetItem(RefocusAtTemperatureDifferenceXName) != null)
                { return Convert.ToDouble(DirectXcess.GetItem(RefocusAtTemperatureDifferenceXName)); }
                else { return 1.0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.RefocusAtTemperatureDifferenceXName, value);
        }

        public bool IsFlatFlipEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.FlatFlipCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.FlatFlipCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.FlatFlipCheckedXName, value);
        }

        public bool IsFlatManEast
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(SessionControl.FlatManEastCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(SessionControl.FlatManEastCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.FlatManEastCheckedXName, value);
        }

        public bool IsFlatsRotationEnabled
        {
            get
            {
                if (Convert.ToBoolean(DirectXcess.GetItem(FlatsRotationCheckedXName)))
                { return Convert.ToBoolean(DirectXcess.GetItem(FlatsRotationCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.FlatsRotationCheckedXName, value);
        }

        public FlatManager.LightSource FlatLightSource
        {
            //Returns configured source for flats: 0 = None, 1 = flatman, 2 = dusk, 3 = dawn (default 0)
            get
            {
                if (DirectXcess.GetItem(SessionControl.FlatLightSourceXName) != null)
                { return (FlatManager.LightSource)Enum.Parse(typeof(FlatManager.LightSource), DirectXcess.GetItem(FlatLightSourceXName)); }
                else
                {
                    DirectXcess.SetItem(SessionControl.FlatLightSourceXName, FlatManager.LightSource.lsNone.ToString());
                }
                return FlatManager.LightSource.lsFlatMan;
            }
            set => DirectXcess.SetItem(SessionControl.FlatLightSourceXName, value.ToString());
        }

        public double FlatsExposureTime
        {
            get
            {
                if (DirectXcess.GetItem(SessionControl.FlatsExposureTimeXName) != null)
                { return Convert.ToDouble(DirectXcess.GetItem(SessionControl.FlatsExposureTimeXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.FlatsExposureTimeXName, value);
        }

        public int FlatManBrightness
        {
            get
            {
                if (DirectXcess.GetItem(SessionControl.FlatManBrightnessXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(SessionControl.FlatManBrightnessXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.FlatManBrightnessXName, value);
        }

        public int FlatsTargetADU
        {
            get
            {
                if (DirectXcess.GetItem(FlatsTargetADUXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(FlatsTargetADUXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.FlatsTargetADUXName, value);
        }

        public int FlatsRepetitions
        {
            get
            {
                if (DirectXcess.GetItem(FlatsRepetitionsXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(FlatsRepetitionsXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.FlatsRepetitionsXName, value);
        }

        public double Overhead
        {
            get
            {
                if (DirectXcess.GetItem(OverheadXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(OverheadXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.OverheadXName, value);
        }

        public bool IsDomeAddOnEnabled
        {
            get
            {
                if (DirectXcess.GetItem(DomeAddOnCheckedXName) != null)
                { return Convert.ToBoolean(DirectXcess.GetItem(DomeAddOnCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.DomeAddOnCheckedXName, value);
        }

        public bool IsPowerManagerEnabled
        {
            get
            {
                if (DirectXcess.GetItem(PowerManagerCheckedXName) != null)
                { return Convert.ToBoolean(DirectXcess.GetItem(PowerManagerCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.PowerManagerCheckedXName, value);
        }

        public bool CameraPowerEnabled
        {
            get
            {
                if (DirectXcess.GetItem(CameraPowerCheckedXName) != null)
                { return Convert.ToBoolean(DirectXcess.GetItem(CameraPowerCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.CameraPowerCheckedXName, value);
        }

        public bool OTAFanPowerEnabled
        {
            get
            {
                if (DirectXcess.GetItem(OTAFanPowerCheckedXName) != null)
                { return Convert.ToBoolean(DirectXcess.GetItem(OTAFanPowerCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.OTAFanPowerCheckedXName, value);
        }

        public bool DewHeaterEnabled
        {
            get
            {
                if (DirectXcess.GetItem(DewHeaterCheckedXName) != null)
                { return Convert.ToBoolean(DirectXcess.GetItem(DewHeaterCheckedXName)); }
                else { return false; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.DewHeaterCheckedXName, value);
        }

        public int PowerManagerCOMPort
        {
            get
            {
                if (DirectXcess.GetItem(PowerManagerCOMPortXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(PowerManagerCOMPortXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.PowerManagerCOMPortXName, value);
        }

        public double PowerManagerMinimumVoltage
        {
            get
            {
                if (DirectXcess.GetItem(PowerManagerMinimumVoltageXName) != null)
                { return Convert.ToDouble(DirectXcess.GetItem(PowerManagerMinimumVoltageXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.PowerManagerMinimumVoltageXName, value);
        }

        public double PowerManagerVoltageAdjustment
        {
            get
            {
                if (DirectXcess.GetItem(PowerManagerVoltageAdjustmentXName) != null)
                { return Convert.ToDouble(DirectXcess.GetItem(PowerManagerVoltageAdjustmentXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.PowerManagerVoltageAdjustmentXName, value);
        }

        public int PrimaryHeaterDutyCycle
        {
            get
            {
                if (DirectXcess.GetItem(PrimaryHeaterDutyCycleXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(PrimaryHeaterDutyCycleXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.PrimaryHeaterDutyCycleXName, value);
        }

        public int SecondaryHeaterDutyCycle
        {
            get
            {
                if (DirectXcess.GetItem(SecondaryHeaterDutyCycleXName) != null)
                { return Convert.ToInt32(DirectXcess.GetItem(SecondaryHeaterDutyCycleXName)); }
                else { return 0; }
            }
            set => DirectXcess.ReplaceItem(SessionControl.SecondaryHeaterDutyCycleXName, value);
        }

        #endregion

    }
}
