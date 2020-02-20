using Planetarium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Humason
{
    public class FlatManager
    {
        public enum LightSource
        {
            lsNone,
            lsFlatMan,
            lsDawn,
            lsDusk
        }

        public static string fmFlatSetName = "FM-FlatSet";
        public static string fmFlatsRepetitionsName = "FM-FlatsRepetitions";
        public static string fmFlatFlipCheckedName = "FM-FlatFlipChecked";
        public static string fmFlatSetRequiredName = "FM_FlatRequired";
        public static string fmFlatSetRequiredTargetName = "FM-FlatTarget";
        public static string fmFlatSetRequiredSideOfPierName = "FM-FlatSideOfPier";
        public static string fmFlatSetRequiredRotationPAName = "FM-RotationPA";
        public static string fmFlatSetRequiredFilterNameName = "FM-FlatFilterName";
        public static string fmFlatSetRequiredFilterIndexName = "FM-FlatFilterIndex";
        public static string fmFlatSetRequiredRepetitionsName = "FM-FlatRepetitions";

        private readonly string nhDir;
        private Axess fReq;  //Flats Request file

        public static string HumasonFlatStackFilename = "FlatStack.xml";
        public static string HumasonFlatsXCName = "HumasonFlats";

        public FlatManager()
        {
            //Create the folder path for the base folder.
            nhDir = FormHumason.openSession.HumasonDirectoryPath;
            //Create the flats request xml file, if it doesn't exist
            fReq = new Axess(nhDir + "\\" + HumasonFlatStackFilename, HumasonFlatsXCName);
        }

        public void TakeFlats()
        {
            //Pulls flat requests from the flat stack (file) and services accordingly
            LogEvent lg = FormHumason.lg;
            lg.LogIt("Checking for flat requests");

            //Opt out if no flats to look at
            if (!HaveFlatsToDo())
            {
                lg.LogIt("No flats to do");
                return;
            }
            else
            {
                lg.LogIt("Have flats do: Starting Flats");
            }

            //Fire off flatman if enabled, otherwise point the telescope up for dawn or dusk flats
            // Sort flats by filter and source (i.e. twightlight or dawn)
            if (FormHumason.openSession.IsFlatManEnabled)
            {
                //Stage the mount to the flatman
                FlatMan flmn = new FlatMan();
                lg.LogIt("Staging FlatMan");
                bool stgResult = flmn.FlatManStage();
                if (!stgResult)
                {
                    lg.LogIt("FlatMan Staging Failed -- aborting flats");
                    return;
                }
                //If Manual Setup is selected, then pause for user to position the FlatMan for flats
                if (FormHumason.openSession.IsFlatManManualSetupEnabled)
                {
                    lg.LogIt("Pausing to attach FlatMan panel");
                    MessageBox.Show("Attach the FlatMan, then press OK");
                }
                //Turn on Flatman panel, if it hasn't been done already
                lg.LogIt("Lighting up FlatMan panel");
                flmn.Light = true;
            }
            else //Dusk or dawn flats
            {
                //Unpark mount, if parked, which it often is to do dusk flats
                TSXLink.Mount.UnPark();
                //point telescope essentially up
                lg.LogIt("Pointing telescope just west of zenith");
                TSXLink.Mount.SlewAzAlt(200.0, (60), "Flat Spot");
                //Turn tracking off
                TSXLink.Mount.TurnTrackingOff();
            }

            //Alright, all ready to go.  
            //Loop on the flat entries in the flat stack file, if any
            while (HaveFlatsToDo())
            {
                switch (FormHumason.openSession.FlatLightSource)
                {
                    case (LightSource.lsNone):
                        {
                            break;
                        }
                    case (LightSource.lsFlatMan):
                        {

                            if (FormHumason.openSession.IsFlatManEnabled)
                            {
                                // **********************  Use Flatman
                                //Rotate to PA, if there is a rotator is enabled
                                Flat iFlat = GetLeastRotatedFlat();
                                if (FormHumason.openSession.IsRotationEnabled)
                                {
                                    Rotator.RotateToRotatorPA(iFlat.RotationPA);
                                }
                                Imaging nhi = new Imaging();
                                nhi.DoFlatManFlats(iFlat.TargetName, iFlat.RotationPA, iFlat.SideOfPier, iFlat.FlatFilter);
                                RemoveFlat(iFlat);  //remove flat from flat stack file
                            }
                            break;
                        }
                    case (LightSource.lsDusk):
                        {
                            //  ********************  Use Dusk
                            Flat iFlat = GetLowestIndexFlat();
                            Imaging nhi = new Imaging();
                            nhi.DoTwilightFlats(iFlat, true);
                            RemoveFlat(iFlat);  //remove flat from flat stack file
                            break;
                        }
                    case (LightSource.lsDawn):
                        {
                            //  ********************  Use Dawn
                            Flat iFlat = GetHighestIndexFlat();
                            Imaging nhi = new Imaging();
                            nhi.DoTwilightFlats(iFlat, false);
                            RemoveFlat(iFlat);  //remove flat from flat stack file
                            break;
                        }
                    default: break;
                }
            }
            //If FlatMan was used, then shut it down
            if (FormHumason.openSession.IsFlatManEnabled)
            {
                //Turn off flatman functions
                FlatMan flmn = new FlatMan();
                lg.LogIt("Terminating FlatMan");
                //Turn on Flatman panel, if it hasn't been done already
                lg.LogIt("Turning off FlatMan panel");
                flmn.Light = false;
                //If Manual Setup is selected, then pause for user to position the FlatMan for flats
                if (FormHumason.openSession.IsFlatManManualSetupEnabled)
                {
                    lg.LogIt("Pausing to detach FlatMan panel");
                    MessageBox.Show("Detach the FlatMan, then press OK");
                }
            }

            //Turn tracking on
            TSXLink.Mount.TurnTrackingOn();
            //Park the mount
            TSXLink.Mount.Park();
            return;
        }

        public void FlatSetClearAll()
        {
            //Removes all flat set requests from the flats XML file
            List<Flat> iflats = GetFlats();
            if (iflats != null)
            {
                foreach (Flat fl in iflats)
                {
                    RemoveFlat(fl);
                }
            }
            return;
        }

        public void AddFlat(Flat flt)
        {
            //Add an entry to the flats file, if it isn't there already

            //Create Xelement for flat entry
            XElement fltXreq = new XElement(fmFlatSetRequiredName);
            XElement tNameX = new XElement(fmFlatSetRequiredTargetName, flt.TargetName);
            XElement sopX = new XElement(fmFlatSetRequiredSideOfPierName, flt.SideOfPier);
            XElement rPAX = new XElement(fmFlatSetRequiredRotationPAName, flt.RotationPA.ToString());
            XElement filName = new XElement(fmFlatSetRequiredFilterNameName, flt.FlatFilter.Name.ToString());
            XElement filIdx = new XElement(fmFlatSetRequiredFilterIndexName, flt.FlatFilter.Index.ToString());
            XElement filRep = new XElement(fmFlatSetRequiredRepetitionsName, flt.FlatFilter.Repeat.ToString());
            fltXreq.Add(tNameX);
            fltXreq.Add(sopX);
            fltXreq.Add(rPAX);
            fltXreq.Add(filName);
            fltXreq.Add(filIdx);
            fltXreq.Add(filRep);

            //Now lets add it, if it isn't already there
            string tpFilePath = nhDir + "\\" + HumasonFlatStackFilename;
            XElement tpPlanX = XElement.Load(tpFilePath);
            XElement sectionX = tpPlanX.Element(fmFlatSetName);
            //Check for flats section, if doesn't exist, then create section
            if (sectionX == null)
            {
                XElement fltSetX = new XElement(fmFlatSetName);
                fltSetX.Add(fltXreq);
                tpPlanX.Add(fltSetX);
                //Save the xml back to the file and return
                tpPlanX.Save(tpFilePath);
                return;
            }
            else
            {
                //Otherwise, check for a duplication, if one then return, if not add the element
                foreach (XElement flatX in sectionX.Elements(fmFlatSetRequiredName))
                {
                    if (CompareFlat(flatX, flt))
                    { return; }
                }
                sectionX.Add(fltXreq);
                //Save the xml back to the file and return
                tpPlanX.Save(tpFilePath);
                return;
            }
        }

        public void RemoveFlat(Flat flt)
        {
            //Removes a flat from the flat entries based on Side of Pier and Rotator PA
            //Find entry, if any
            string tpFilePath = nhDir + "\\" + HumasonFlatStackFilename;
            XElement tpPlanX = XElement.Load(tpFilePath);
            XElement sectionX = tpPlanX.Element(fmFlatSetName);
            //Check for flats section, if doesn't exist, then return null
            if (sectionX == null)
            {
                return;
            }
            //Otherwise, load the flat definition entries, if any
            Flat[] flatOut = new Flat[sectionX.Elements(fmFlatSetRequiredName).Count()];
            int fi = 0;
            foreach (XElement flatX in sectionX.Elements(fmFlatSetRequiredName))
            {
                if (CompareFlat(flatX, flt))
                {
                    flatX.Remove();
                    tpPlanX.Save(tpFilePath);
                    return;
                }
                fi++;
            }
            return;
        }

        public bool HaveFlatsToDo()
        {
            //Checks to see if there are any entries in the flats configuration file
            List<Flat> iflats = GetFlats();
            if (iflats != null && iflats.Count != 0)
            { return true; }
            else
            { return false; }
        }

        private Flat GetLeastRotatedFlat()
        {
            //Get the current rotation position, if any
            double nowRotAngle = 0;
            if (FormHumason.openSession.IsRotationEnabled)
            {
                nowRotAngle = Rotator.RealRotatorPA;
            }

            //Get the list of flat requests
            List<Flat> flatSet = GetFlats();
            //Create a return Flat and set to first of the flat set, return null if none
            if (flatSet.Count == 0)
            { return null; }
            Flat flatOut = flatSet[0];
            foreach (Flat flt in flatSet)
            {
                if ((Math.Abs(flatOut.RotationPA - nowRotAngle)) > (Math.Abs(flt.RotationPA - nowRotAngle)))
                { flatOut = flt; }
            }
            return flatOut;
        }

        private Flat GetLowestIndexFlat()
        {
            //find the flat request with the lowest filter index
            //Get the list of flat requests
            List<Flat> flatSet = GetFlats();
            //Create a return Flat and set to first of the flat set, return null if none
            if (!HaveFlatsToDo())
            { return null; }
            int leastIndex = 12;  //just pick the maximum number of filters that I can think of
            Flat flatOut = flatSet[0];
            foreach (Flat flt in flatSet)
            {
                if (flt.FlatFilter.Index < leastIndex)
                {
                    flatOut = flt;
                    leastIndex = flt.FlatFilter.Index;
                }
            }
            return flatOut;
        }

        private Flat GetHighestIndexFlat()
        {
            //find the flat request with the highest filter index
            //Get the list of flat requests
            List<Flat> flatSet = GetFlats();
            //Create a return Flat and set to first of the flat set, return null if none
            if (!HaveFlatsToDo())
            { return null; }
            int mostIndex = 0;  //just pick the minimum filter index
            Flat flatOut = flatSet[0];
            foreach (Flat flt in flatSet)
            {
                if (flt.FlatFilter.Index > mostIndex)
                {
                    flatOut = flt;
                    mostIndex = flt.FlatFilter.Index;
                }
            }
            return flatOut;
        }

        private List<Flat> GetFlats()
        {
            string fsFilePath = nhDir + "\\" + HumasonFlatStackFilename;
            XElement tpPlanX = XElement.Load(fsFilePath);
            XElement sectionX = tpPlanX.Element(fmFlatSetName);
            //Check for flats section, if doesn't exist, then return null
            if (sectionX == null)
            {
                return null;
            }
            //Otherwise, load the flat definition entries, if any
            List<Flat> flatOut = new List<Flat>();
            foreach (XElement flatX in sectionX.Elements(fmFlatSetRequiredName))
            {
                Flat flt = new Flat(flatX);
                flatOut.Add(flt);
            }
            return flatOut;
        }

        private bool CompareFlat(Flat flat1, Flat flat2)
        {
            if ((flat1.TargetName == flat2.TargetName) &&
                    (flat1.SideOfPier == flat2.SideOfPier) &&
                    (flat1.RotationPA == flat2.RotationPA) &&
                    (flat1.FlatFilter.Index == flat2.FlatFilter.Index) &&
                    (flat1.FlatFilter.Repeat == flat2.FlatFilter.Repeat))
            { return true; }
            else
            { return false; }
        }

        private bool CompareFlat(XElement flat1X, Flat flat2)
        {
            //Convert the element to a flat class, then compare
            Flat flat1 = new Flat(flat1X);
            if (CompareFlat(flat1, flat2))
            { return true; }
            else
            { return false; }
        }

        private int FlatsRepetitions
        {
            get { return Convert.ToInt32(fReq.GetItem(fmFlatsRepetitionsName)); }
            set { fReq.ReplaceItem(fmFlatsRepetitionsName, value); }
        }

        private bool IsFlatFlipEnabled
        {
            get { return Convert.ToBoolean(fReq.GetItem(fmFlatFlipCheckedName)); }
            set { fReq.ReplaceItem(fmFlatFlipCheckedName, value); }
        }

        private bool IsFlatSetRequired
        {
            get { return Convert.ToBoolean(fReq.GetItem(fmFlatSetRequiredName)); }
            set { fReq.ReplaceItem(fmFlatSetRequiredName, value); }
        }

        private string FlatSetTargetName
        {
            get { return (fReq.GetItem(fmFlatSetRequiredTargetName)); }
            set { fReq.ReplaceItem(fmFlatSetRequiredTargetName, value); }
        }

        private string FlatSetFilterName
        {
            get { return (fReq.GetItem(fmFlatSetRequiredFilterNameName)); }
            set { fReq.ReplaceItem(fmFlatSetRequiredFilterNameName, value); }
        }

        private int FlatSetSideOfPier
        {
            get { return Convert.ToInt32(fReq.GetItem(fmFlatSetRequiredSideOfPierName)); }
            set { fReq.ReplaceItem(fmFlatSetRequiredSideOfPierName, value); }
        }

        private double FlatSetrotationPA
        {
            get { return Convert.ToDouble(fReq.GetItem(fmFlatSetRequiredRotationPAName)); }
            set { fReq.ReplaceItem(fmFlatSetRequiredRotationPAName, value); }
        }

        private int FlatSetFilterIndex
        {
            get { return Convert.ToInt32(fReq.GetItem(fmFlatSetRequiredFilterIndexName)); }
            set { fReq.ReplaceItem(fmFlatSetRequiredFilterIndexName, value); }
        }

    }

    public class Flat
    {
        //public fields
        public string TargetName;
        public string SideOfPier;
        public bool RotateRequired;
        public double RotationPA;
        public Filter FlatFilter;

        public Flat(string targetName, string SideOfPierName, double positionAngle, Filter flatFilter, bool rotateRequired)
        {
            TargetName = targetName;
            SideOfPier = SideOfPierName;
            RotateRequired = rotateRequired;
            RotationPA = positionAngle;
            FlatFilter = flatFilter;
            return;
        }

        public Flat(XElement flatX)
        {
            TargetName = flatX.Element(FlatManager.fmFlatSetRequiredTargetName).Value;
            SideOfPier = flatX.Element(FlatManager.fmFlatSetRequiredSideOfPierName).Value;
            RotationPA = Convert.ToDouble(flatX.Element(FlatManager.fmFlatSetRequiredRotationPAName).Value);
            int FlatReps = Convert.ToInt32(flatX.Element(FlatManager.fmFlatSetRequiredRepetitionsName).Value);
            FlatFilter = new Filter((flatX.Element(FlatManager.fmFlatSetRequiredFilterNameName).Value),
                                     Convert.ToInt32(flatX.Element(FlatManager.fmFlatSetRequiredFilterIndexName).Value),
                                     FlatReps);
        }
    }
}
