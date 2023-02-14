using Planetarium;
/// Sequence Plan Class
///
/// ------------------------------------------------------------------------
/// Module Name: Configuration 
/// Purpose: Store and retrieve configuration data
/// Developer: Rick McAlister
/// Creation Date:  6/6/2017
/// Major Modifications:
/// Copyright: Rick McAlister, 2017
/// 
/// Description: TargetPlan class encapsulates all data and methods for reading
/// and writing a target plan data file
/// 
/// All heavy lifting for the XML stuff is done through the Axess class
/// 
/// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Humason
{
    public partial class TargetPlan
    {
        //Private data
        public const string HumasonTargetPlanFilename = "TargetPlan.xml";
        public const string HumasonTargetPlanXCName = "HumasonTargetPlan";

        #region XElementNames
        //Configuration Element Names

        public const string FocusExposureXName = "FocusExposure";
        public const string FocusFilterXName = "FocusFilter";
        public const string AtFocusCheckedXName = "AtFocusChecked";

        public const string GuideExposureTimeXName = "GuideExposureTime";
        public const string MinGuideExposureTimeXName = "MinGuideExposureTime";
        public const string MaxGuideExposureTimeXName = "MaxGuideExposureTime";
        public const string GuideCycleTimeXName = "GuideCycleTime";
        public const string GuideStarADUXName = "GuideStarADU";
        public const string AOCheckedXName = "AOChecked";
        public const string GuiderFocuserCheckedXName = "GuiderFocuserChecked";
        public const string XAxisMoveTimeXName = "XAxisMoveTime";
        public const string YAxisMoveTimeXName = "YAxisMoveTime";
        public const string GuideStarXXName = "GuideStarX";
        public const string GuideStarYXName = "GuideStarY";
        public const string GuiderBinningXName = "GuiderBinning";
        public const string GuiderSubframeEnabledXName = "GuiderSubframeEnabled";

        public const string CalVectorXPosXComponentXName = "CalVectorXPosXComponent";
        public const string CalVectorXPosYComponentXName = "CalVectorXPosYComponent";
        public const string CalVectorYPosXComponentXName = "CalVectorXPosXComponent";
        public const string CalVectorYPosYComponentXName = "CalVectorXPosYComponent";
        public const string CalVectorXNegXComponentXName = "CalVectorYNegXComponent";
        public const string CalVectorXNegYComponentXName = "CalVectorYNegYComponent";
        public const string CalVectorYNegXComponentXName = "CalVectorYNegXComponent";
        public const string CalVectorYNegYComponentXName = "CalVectorYNegYComponent";

        public const string TargetNameXName = "TargetName";
        public const string TargetAdjustCheckedXName = "TargetAdjustChecked";
        public const string TargetRAXName = "TargetRA";
        public const string TargetDecXName = "TargetDec";
        public const string TargetPAXName = "TargetPA";
        public const string SequenceStartTimeXName = "SequenceStartTime";
        public const string SequenceEndTimeXName = "SequenceEndTime";
        public const string SequenceDawnTimeXName = "SequenceDawnTime";


        public const string ImageExposureTimeXName = "ImageExposureTime";
        public const string LoopsXName = "Loops";
        public const string LRGBRatioXName = "LRGBRatio";
        public const string DelayXName = "Delay";
        public const string MakeFlatsCheckedXName = "MakeFlats";

        public const string AutoFocusCheckedXName = "AutoFocusChecked";
        public const string AutoGuideCheckedXName = "AutoGuideChecked";
        public const string RotatorCheckedXName = "RotatorChecked";
        public const string RecalibrateAfterFlipCheckedXName = "RecalibrateAfterFlipChecked";
        public const string DitherCheckedXName = "DitherChecked";
        public const string GuiderCalibrateCheckedXName = "CalibrateChecked";
        public const string ResyncCheckedXName = "ResyncChecked";
        public const string ClearFilterXName = "ClearFilter";
        public const string OverheadXName = "ImageOverheadTime";
        public const string CameraTemperatureSetXName = "CameraTemperatureSet";
        public const string AtFocusPickedXName = "AtFocusPicked";
        public const string SmallSolarSystemBodyXName = "SSSBody";
        public const string DeltaRARateXName = "DeltaRARate";
        public const string DeltaDecRateXName = "DeltaDecRate";

        public const string PlateSolveExposureTimeXName = "PlateSolveExposureTime";
        public const string RotatorDirectionXName = "RotatorDirection";

        public const string FilterSetXName = "FilterSet";
        #endregion

        //Local data -- saves on some repeated file accesses to get path info, etc
        private string hDirectoryPath;
        private Axess hTargetPlanX;
        public string DefaultPlanPath { get; set; } = null;

        public string TargetPlanPath { get; set; } = null;

        #region Instantiation

        public TargetPlan(string targetName)
        {
            /* Creates a target plan object for accessing the class
             Several outcomes can occur depending upon the targetName.
               If the targetName is empty then the target is assumed to be "Default"
                    and this instantiation is set up for the "Default" file
               else if the targetName is Default then the target is assumed to be "Default"
                    and this instantiation is set up for the "Default" file
               otherwise, 
                    if the targetName has a file, then get an Axess instance for it.
                    otherwise check tSX for the targetName
                        if found, then a new target plan file is created from cloning the
                            default target pland merged with the TSX data
                        otherwise (no plan, no target, no nothing) do nothing
             */
            SessionControl openSession = new SessionControl();

            //Store the Humason directory path so we don't have to keep looking it up when using this instance
            hDirectoryPath = openSession.HumasonDirectoryPath;
            //If the target name is empty for some reason, just set a path to the default file
            if ((targetName == "") || (targetName == null) || (targetName == "Default"))
            {
                targetName = "Default";
                //Set up a folder path for the presumed target -- we still don't know if anything is there
                TargetPlanPath = hDirectoryPath + "\\" + targetName.Replace('/', '_') + "." + HumasonTargetPlanFilename;
                DefaultPlanPath = TargetPlanPath;
                if (File.Exists(TargetPlanPath)) { hTargetPlanX = new Axess(TargetPlanPath); }
                else
                {
                    hTargetPlanX = CreateDefaultTargetPlan();
                }
            }
            else  //Got a target name, look for a file
            {
                TargetPlanPath = hDirectoryPath + "\\" + targetName.Replace('/', '_') + "." + HumasonTargetPlanFilename;
                //if the plan file for this target already exists.  
                //   Just set up XML access for it.     
                if (File.Exists(TargetPlanPath)) { hTargetPlanX = new Axess(TargetPlanPath); }
                else
                //The plan file doesn't exist, look for a TSX match
                //  If the target can be found, Create the file, Populate with the target position info, and expand with default plan
                {
                    hTargetPlanX = SpawnTargetPlanFromTSX(targetName);
                }
                //no tsx data either, just return with a null target file path
            }
        }

        public Axess SpawnTargetPlanFromTSX(string targetName)
        {
            //perform search on substring of targetName preceding any "-" in order to work with mosaic target names
            SessionControl openSession = new SessionControl();
            TSXLink.Target tgto = TSXLink.StarChart.FindTarget((targetName.Split('-'))[0]);
            if (tgto != null)
            {
                Axess defaultTargetPlanX = new Axess(openSession.DefaultTargetPlanPath);
                hTargetPlanX = new Axess(defaultTargetPlanX, TargetPlanPath);
                hTargetPlanX.SetItem(TargetNameXName, tgto.Name);
                hTargetPlanX.SetItem(TargetRAXName, tgto.RA.ToString());
                hTargetPlanX.SetItem(TargetDecXName, tgto.Dec.ToString());
                return hTargetPlanX;
            }
            else
            {
                return null;
            }
        }

        public Axess SpawnTargetPlan(string targetName)
        {
            //This command will save a copy of the current tPlan
            //under the name targetname.configuration.xml
            string spawnTargetPlanPath = hDirectoryPath + "\\" + targetName.Replace('/', '_') + "." + HumasonTargetPlanFilename;
            Axess sourceTargetPlanX = new Axess(TargetPlanPath);
            hTargetPlanX = new Axess(sourceTargetPlanX, spawnTargetPlanPath);
            return hTargetPlanX;
        }

        public void FlushOutFromDefaultPlan()
        {
            //This command will save a copy of the current tPlan
            //under the name targetname.configuration.xml
            SessionControl openSession = new SessionControl();

            XElement tpPlanX = XElement.Load(TargetPlanPath);
            XElement defaultPlanX = XElement.Load(openSession.DefaultTargetPlanPath);
            foreach (XElement tpX in defaultPlanX.Elements())
            {
                if (!(tpPlanX.Elements().Contains(tpX)))
                { tpPlanX.Add(tpX); }
            }
            tpPlanX.Save(TargetPlanPath);
        }

        public void SavePlanAsDefaultPlan()
        {
            //This command will save a copy of the current target plan file
            SessionControl openSession = new SessionControl();
            //as the default plan file
            XElement tpPlanX = XElement.Load(TargetPlanPath);
            //Delete the name, ra, dec, and adjust entries, if this isn't the default plan itself
            XElement nameX = tpPlanX.Element(TargetNameXName);
            if (nameX != null)
            {
                nameX.Remove();
            }

            XElement raX = tpPlanX.Element(TargetRAXName);
            if (raX != null)
            {
                raX.Remove();
            }

            XElement decX = tpPlanX.Element(TargetDecXName);
            if (decX != null)
            {
                decX.Remove();
            }

            XElement adjX = tpPlanX.Element(TargetAdjustCheckedXName);
            if (adjX != null)
            {
                adjX.Remove();
            }

            tpPlanX.Save(openSession.DefaultTargetPlanPath);
            return;
        }

        public void DeleteTargetPlan(string targetName)
        {
            //Removes the configuration file with the filename targetname
            string tpFilePath = hDirectoryPath + "\\" + targetName.Replace('/', '_') + "." + HumasonTargetPlanFilename;
            System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this file?", "Confirm Deletion", System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (dr == System.Windows.Forms.DialogResult.OK)
            { File.Delete(tpFilePath); }
            return;
        }

        public bool IsSparsePlan()
        {
            //Returns true if plan contains 4 or fewer elements
            XElement tpPlanX = XElement.Load(TargetPlanPath);
            if (tpPlanX.Elements().Count() < 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Axess CreateDefaultTargetPlan()
        {
            Axess defaultTP = new Axess(TargetPlanPath, HumasonTargetPlanXCName);

            defaultTP.SetItem(TargetNameXName, "Default");
            defaultTP.SetItem(TargetRAXName, 0);
            defaultTP.SetItem(TargetDecXName, 0);
            defaultTP.SetItem(TargetPAXName, 0);
            defaultTP.SetItem(TargetAdjustCheckedXName, false);

            defaultTP.SetItem(FocusExposureXName, 5);
            defaultTP.SetItem(FocusFilterXName, 3);
            defaultTP.SetItem(AtFocusCheckedXName, false);

            defaultTP.SetItem(GuideExposureTimeXName, 5);
            defaultTP.SetItem(MinGuideExposureTimeXName, 0.5);
            defaultTP.SetItem(MaxGuideExposureTimeXName, 10);
            defaultTP.SetItem(GuideCycleTimeXName, 0);
            defaultTP.SetItem(GuideStarADUXName, 1000);
            defaultTP.SetItem(AOCheckedXName, false);
            defaultTP.SetItem(GuiderFocuserCheckedXName, false);
            defaultTP.SetItem(XAxisMoveTimeXName, 30);
            defaultTP.SetItem(YAxisMoveTimeXName, 30);
            defaultTP.SetItem(GuideStarXXName, 0);
            defaultTP.SetItem(GuideStarYXName, 0);
            defaultTP.SetItem(GuiderBinningXName, 1);
            defaultTP.SetItem(GuiderSubframeEnabledXName, false);

            defaultTP.SetItem(CalVectorXPosXComponentXName, 0);
            defaultTP.SetItem(CalVectorXPosYComponentXName, 0);
            defaultTP.SetItem(CalVectorYPosXComponentXName, 0);
            defaultTP.SetItem(CalVectorYPosYComponentXName, 0);
            defaultTP.SetItem(CalVectorXNegXComponentXName, 0);
            defaultTP.SetItem(CalVectorXNegYComponentXName, 0);
            defaultTP.SetItem(CalVectorYNegXComponentXName, 0);
            defaultTP.SetItem(CalVectorYNegYComponentXName, 0);

            defaultTP.SetItem(TargetNameXName, "Default");
            defaultTP.SetItem(TargetAdjustCheckedXName, false);
            defaultTP.SetItem(SequenceStartTimeXName, 0);
            defaultTP.SetItem(SequenceEndTimeXName, 0);
            defaultTP.SetItem(ImageExposureTimeXName, 10);
            defaultTP.SetItem(LoopsXName, 4);
            defaultTP.SetItem(LRGBRatioXName, 4);
            defaultTP.SetItem(DelayXName, 0);
            defaultTP.SetItem(MakeFlatsCheckedXName, false);
            defaultTP.SetItem(SmallSolarSystemBodyXName, false);

            defaultTP.SetItem(AutoFocusCheckedXName, false);
            defaultTP.SetItem(AutoGuideCheckedXName, false);
            defaultTP.SetItem(RotatorCheckedXName, false);
            defaultTP.SetItem(RecalibrateAfterFlipCheckedXName, false);
            defaultTP.SetItem(DitherCheckedXName, false);
            defaultTP.SetItem(GuiderCalibrateCheckedXName, false);
            defaultTP.SetItem(ResyncCheckedXName, false);
            defaultTP.SetItem(ClearFilterXName, 3);
            defaultTP.SetItem(OverheadXName, 0);
            defaultTP.SetItem(CameraTemperatureSetXName, 0);
            defaultTP.SetItem(AtFocusPickedXName, 1);

            defaultTP.SetItem(PlateSolveExposureTimeXName, 10);
            defaultTP.SetItem(RotatorDirectionXName, -1);

            //defaultTP.SetItem( FilterSetXName,0);
            return defaultTP;
        }

        #endregion

        #region Target Properties

        public bool HasValue(string fieldXName)
        {
            return hTargetPlanX.CheckItem(fieldXName);
        }

        public double FocusExposure
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(FocusExposureXName));
            set => hTargetPlanX.ReplaceItem(FocusExposureXName, value.ToString());
        }

        public int FocusFilter
        {
            get => Convert.ToInt32(hTargetPlanX.GetItem(FocusFilterXName));
            set => hTargetPlanX.ReplaceItem(FocusFilterXName, value.ToString());
        }

        public double GuideExposure
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(GuideExposureTimeXName));
            set => hTargetPlanX.ReplaceItem(GuideExposureTimeXName, value.ToString());
        }

        public double MinimumGuiderExposure
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(MinGuideExposureTimeXName));
            set => hTargetPlanX.ReplaceItem(MinGuideExposureTimeXName, value.ToString());
        }

        public double MaximumGuiderExposure
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(MaxGuideExposureTimeXName));
            set => hTargetPlanX.ReplaceItem(MaxGuideExposureTimeXName, value.ToString());
        }

        public double GuiderCycleTime
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(GuideCycleTimeXName));
            set => hTargetPlanX.ReplaceItem(GuideCycleTimeXName, value.ToString());
        }

        public int GuideStarADU
        {
            get => Convert.ToInt32(hTargetPlanX.GetItem(GuideStarADUXName));
            set => hTargetPlanX.ReplaceItem(GuideStarADUXName, value.ToString());
        }

        public bool AOEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(AOCheckedXName));
            set => hTargetPlanX.ReplaceItem(AOCheckedXName, value.ToString());
        }

        public bool GuiderSubframeEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(GuiderSubframeEnabledXName));
            set => hTargetPlanX.ReplaceItem(GuiderSubframeEnabledXName, value.ToString());
        }

        public bool SmallSolarSystemBodyEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(SmallSolarSystemBodyXName));
            set => hTargetPlanX.ReplaceItem(SmallSolarSystemBodyXName, value.ToString());
        }

        public double DeltaRARate
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(DeltaRARateXName));
            set => hTargetPlanX.ReplaceItem(DeltaRARateXName, value.ToString());
        }

        public double DeltaDecRate
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(DeltaDecRateXName));
            set => hTargetPlanX.ReplaceItem(DeltaDecRateXName, value.ToString());
        }

        public bool GuiderFocuserEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(GuiderFocuserCheckedXName));
            set => hTargetPlanX.ReplaceItem(GuiderFocuserCheckedXName, value.ToString());
        }

        public double XAxisMoveTime
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(XAxisMoveTimeXName));
            set => hTargetPlanX.ReplaceItem(XAxisMoveTimeXName, value.ToString());
        }

        public double YAxisMoveTime
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(YAxisMoveTimeXName));
            set => hTargetPlanX.ReplaceItem(YAxisMoveTimeXName, value.ToString());
        }

        public double GuideStarX
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(GuideStarXXName));
            set => hTargetPlanX.ReplaceItem(GuideStarXXName, value.ToString());
        }

        public double GuideStarY
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(GuideStarYXName));
            set => hTargetPlanX.ReplaceItem(GuideStarYXName, value.ToString());
        }

        public int GuiderBinning
        {
            get => Convert.ToInt32(hTargetPlanX.GetItem(GuiderBinningXName));
            set => hTargetPlanX.ReplaceItem(GuiderBinningXName, value);
        }

        public double CalVectorXPosXComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorXPosXComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorXPosXComponentXName, value.ToString());
        }

        public double CalVectorXPosYComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorXPosYComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorXPosYComponentXName, value.ToString());
        }

        public double CalVectorYPosXComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorYPosXComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorXPosYComponentXName, value.ToString());
        }

        public double CalVectorYPosYComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorYPosYComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorYPosYComponentXName, value.ToString());
        }

        public double CalVectorXNegXComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorXNegXComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorXNegXComponentXName, value.ToString());
        }

        public double CalVectorXNegYComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorXNegYComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorXNegYComponentXName, value.ToString());
        }

        public double CalVectorYNegXComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorYNegXComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorXNegYComponentXName, value.ToString());
        }

        public double CalVectorYNegYComponent
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CalVectorYNegYComponentXName));
            set => hTargetPlanX.ReplaceItem(CalVectorYPosYComponentXName, value.ToString());
        }

        public string TargetName
        {
            get
            {
                if (hTargetPlanX.GetItem(TargetNameXName) == null)
                {
                    return "";
                }
                else
                {
                    return (hTargetPlanX.GetItem(TargetNameXName));
                }
            }
            set => hTargetPlanX.ReplaceItem(TargetNameXName, value);
        }

        public bool TargetAdjustEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(TargetAdjustCheckedXName));
            set => hTargetPlanX.ReplaceItem(TargetAdjustCheckedXName, value.ToString());
        }

        public double TargetRA
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(TargetRAXName));
            set => hTargetPlanX.ReplaceItem(TargetRAXName, value.ToString());
        }

        public double TargetDec
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(TargetDecXName));
            set => hTargetPlanX.ReplaceItem(TargetDecXName, value.ToString());
        }

        public double TargetPA
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(TargetPAXName));
            set => hTargetPlanX.ReplaceItem(TargetPAXName, value.ToString());
        }

        public DateTime SequenceStartTime
        {
            //Get the start time, if it is null, then set the start time to Now()
            get
            {
                string startTimeString = hTargetPlanX.GetItem(SequenceStartTimeXName);
                try { Convert.ToDateTime(startTimeString); }
                catch (Exception ex) { startTimeString = null; }
                if ((startTimeString == null) || (Convert.ToDateTime(startTimeString) < DateTime.Now))
                {
                    hTargetPlanX.ReplaceItem(SequenceStartTimeXName, DateTime.Now.ToString());
                    return DateTime.Now;
                }
                else
                { return Convert.ToDateTime(startTimeString); }
            }
            set => hTargetPlanX.ReplaceItem(SequenceStartTimeXName, value.ToString());
        }

        public DateTime SequenceEndTime
        {
            get => Convert.ToDateTime(hTargetPlanX.GetItem(SequenceEndTimeXName));
            set => hTargetPlanX.ReplaceItem(SequenceEndTimeXName, value.ToString());
        }

        public DateTime SequenceDawnTime
        {
            get => Convert.ToDateTime(hTargetPlanX.GetItem(SequenceDawnTimeXName));
            set => hTargetPlanX.ReplaceItem(SequenceDawnTimeXName, value.ToString());
        }

        public double ImageExposureTime
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(ImageExposureTimeXName));
            set => hTargetPlanX.ReplaceItem(ImageExposureTimeXName, value.ToString());
        }

        public int Loops
        {
            get => Convert.ToInt32(hTargetPlanX.GetItem(LoopsXName));
            set => hTargetPlanX.ReplaceItem(LoopsXName, value.ToString());
        }

        public int LRGBRatio
        {
            get => Convert.ToInt32(hTargetPlanX.GetItem(LRGBRatioXName));
            set => hTargetPlanX.ReplaceItem(LRGBRatioXName, value.ToString());
        }

        public double Delay
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(DelayXName));
            set => hTargetPlanX.ReplaceItem(DelayXName, value.ToString());
        }

        public bool MakeFlatsEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(MakeFlatsCheckedXName));
            set => hTargetPlanX.ReplaceItem(MakeFlatsCheckedXName, value.ToString());
        }

        public bool AutoFocusEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(AutoFocusCheckedXName));
            set => hTargetPlanX.ReplaceItem(AutoFocusCheckedXName, value.ToString());
        }

        public bool AutoGuideEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(AutoGuideCheckedXName));
            set => hTargetPlanX.ReplaceItem(AutoGuideCheckedXName, value.ToString());
        }

        public bool RotatorEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(RotatorCheckedXName));
            set => hTargetPlanX.ReplaceItem(RotatorCheckedXName, value.ToString());
        }

        public bool RecalibrateAfterFlipEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(RecalibrateAfterFlipCheckedXName));
            set => hTargetPlanX.ReplaceItem(RecalibrateAfterFlipCheckedXName, value.ToString());
        }

        public bool DitherEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(DitherCheckedXName));
            set => hTargetPlanX.ReplaceItem(DitherCheckedXName, value.ToString());
        }

        public bool GuiderCalibrateEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(GuiderCalibrateCheckedXName));
            set => hTargetPlanX.ReplaceItem(GuiderCalibrateCheckedXName, value.ToString());
        }

        public bool ResyncEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(ResyncCheckedXName));
            set => hTargetPlanX.ReplaceItem(ResyncCheckedXName, value.ToString());
        }

        public int ClearFilter
        {
            get => Convert.ToInt32(hTargetPlanX.GetItem(ClearFilterXName));
            set => hTargetPlanX.ReplaceItem(ClearFilterXName, value.ToString());
        }

        public double Overhead
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(OverheadXName));
            set => hTargetPlanX.ReplaceItem(OverheadXName, value.ToString());
        }

        public double CameraTemperatureSet
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(CameraTemperatureSetXName));
            set => hTargetPlanX.ReplaceItem(CameraTemperatureSetXName, value.ToString());
        }

        public bool AtFocusEnabled
        {
            get => Convert.ToBoolean(hTargetPlanX.GetItem(AtFocusCheckedXName));
            set => hTargetPlanX.ReplaceItem(AtFocusCheckedXName, value.ToString());
        }

        public int AtFocusSelect
        {
            get => Convert.ToInt32(hTargetPlanX.GetItem(AtFocusPickedXName));
            set => hTargetPlanX.ReplaceItem(AtFocusPickedXName, value.ToString());
        }

        public double PlateSolveExposureTime
        {
            get => Convert.ToDouble(hTargetPlanX.GetItem(PlateSolveExposureTimeXName));
            set => hTargetPlanX.ReplaceItem(PlateSolveExposureTimeXName, value.ToString());
        }

        #endregion

        #region Filter Methods and Properties

        public List<Filter> FilterWheelList
        {
            get
            {
                //Read and return the set of entries stored in the active target plan file
                // , in ths case, in the form of Filter objects

                XElement sectionX = hTargetPlanX.GetItems(TargetPlan.FilterSetXName);
                if (sectionX == null)
                {
                    return null;
                }
                else
                {
                    //Otherwise, look through the section for the itemname
                    //if found, then return the entry, if not, make a null entry
                    int filterCount = sectionX.Elements().Count();
                    List<Filter> fSet = new List<Filter>();
                    foreach (XElement fx in sectionX.Elements())
                    {
                        fSet.Add(new Filter(fx.Name.ToString(), Convert.ToInt32(fx.Value), 1));
                    }
                    return fSet;
                }
            }
            set
            {
                //Clear the list of filters on the target plan
                XElement sectionX = hTargetPlanX.GetItems(TargetPlan.FilterSetXName);
                foreach (XElement fx in sectionX.Elements())
                {
                    hTargetPlanX.SetItem(FilterSetXName, fx.Name.ToString(), null);
                }
                //Add each of the filter list items back
                foreach (Filter fltr in value)
                {
                    hTargetPlanX.SetItem(FilterSetXName, fltr.Name, fltr.Index.ToString());
                }
            }

        }

        public void SetFilter(Filter fltr, bool swtch)
        {
            //Adds or removes a particular filter from the list of filters in the target file
            if (swtch)
            {
                hTargetPlanX.SetItem(TargetPlan.FilterSetXName, fltr.Name, fltr.Index.ToString());
            }
            else
            {
                hTargetPlanX.SetItem(TargetPlan.FilterSetXName, fltr.Name, null);
            }
            return;
        }

        public bool CheckFilter(Filter fltr)
        {
            //checks to see if a filter (by name) is in list of filters in the target file
            if (hTargetPlanX.GetItem(TargetPlan.FilterSetXName, fltr.Name) != null)
            { return true; }
            else
            { return false; }
        }

        #endregion
    }
}

