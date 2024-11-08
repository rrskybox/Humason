﻿using System;
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
        private FlatMan fMan;

        public static string HumasonFlatStackFilename = "FlatStack.xml";
        public static string HumasonFlatsXCName = "HumasonFlats";

        public FlatManager()
        {
            //Create the folder path for the base folder.
            SessionControl openSession = new SessionControl();
            nhDir = openSession.HumasonDirectoryPath;
            //Create the flats request xml file, if it doesn't exist
            fReq = new Axess(nhDir + "\\" + HumasonFlatStackFilename, HumasonFlatsXCName);
            if (openSession.IsFlatManEnabled)
                fMan = new FlatMan();
        }

        /// <summary>
        /// FMSetUp
        /// Prepares imaging for flats.  Closes dome, points telescope at MyFlat
        /// </summary>
        public bool FlatManStage()
        {
            //Console routine to set up the scope to use the FlatMan
            //  to be called from CCDAP or other apps prior to running flats
            //
            //Routine will find the "My Flat Field" location via TSX, after
            //  it has been installed in the SDB (see instructions)
            //Then the slit will be closed, if open and dome homed and disconnected
            //The mount will then be sent to that position and tracking turned off

            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();

            lg.LogIt("Establishing TSX interfaces: star chart, mount, dome.");
            //If the dome is enabled,  Home it and disconnect
            if (openSession.HasDome)
            {
                //Complete any dome commands, including homing and closing the dome, if needed
                //The mount will be parked and disconnected during this operation
                //Dome will be decoupled from mount
                if (openSession.HasDome)
                {
                    lg.LogIt("Homing and Closing Dome");
                    lg.LogIt("Connecting Dome");
                    TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Dome);
                    //No idea how we could be here, but lg.LogIt the error and quit
                    //Clear any operation that might be underway for whatever bogus reason
                    lg.LogIt("Aborting any active dome command... again");
                    TSXLink.Dome.AbortDomeOperation();
                    //Wait for five seconds for everything to clear (Maxdome is a bit slow)
                    lg.LogIt("Waiting for dome operations to abort, if any");
                    System.Threading.Thread.Sleep(5000);
                    //Close Dome (if open) -- Close dome will home the dome before closing
                    TSXLink.Dome.CloseSlit();
                    //Uncouple the dome from the mount
                    lg.LogIt("Disconnecting Dome");
                    TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Dome);
                }
            }
            //Connect to telescope, 
            // if manual set up, then park the mount (to keep it from tracking someplace bad)
            //  and ask the user for permission to continue, abort if user cancels, otherwise continue.
            lg.LogIt("Connecting mount");
            TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Mount);
            if (openSession.IsFlatManManualSetupEnabled)
            {
                lg.LogIt("Parking mount to wait for manual flatman set up");
                TSXLink.Mount.Park();
                DialogResult dr = MessageBox.Show("Manual FlatMan set up selected:  Continue?", "Manual FlatMan Preparation", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel) return false;
            }
            //Unpark (if parked), look up My Flat Field, slew to it, turn off tracking
            TSXLink.Mount.UnPark();
            lg.LogIt("Looking up flat panel position");
            TSXLink.Target ffTarget = TSXLink.StarChart.FindTarget("MyFlatField");
            if (ffTarget == null)
            {
                lg.LogIt("Could not find My Flat Field");
                return false;
            }
            double altitude = ffTarget.Altitude;
            double azimuth = ffTarget.Azimuth;
            lg.LogIt("Slewing to flat panel position");
            try
            {
                TSXLink.Mount.SlewRADec(ffTarget.RA, ffTarget.Dec, ffTarget.Name);
            }
            catch (Exception ex)
            {
                //If manual set up and slew error, then log and just keep going, otherwise terminate the flats session
                if (openSession.IsFlatManManualSetupEnabled)
                {
                    MessageBox.Show("Slew to Flats Box failure. " + ex.Message + " \r\n Manual setup so continuing flats session.");
                }
                else
                {
                    MessageBox.Show("Slew to Flats Box failure. " + ex.Message + " \r\n Turn off slew limits.  Ending flats session.");
                    return false;
                }
            }
            lg.LogIt("Turning off tracking");
            TSXLink.Mount.TurnTrackingOff();
            //Disconnect from mount
            lg.LogIt("Disconnecting Mount");
            TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Mount);
            lg.LogIt("FlatMan positioning complete");

            //All done -- garbage collect and exit
            return true;
                    }


        public void TakeFlats()
        {
            //Pulls flat requests from the flat stack (file) and services accordingly
            LogEvent lg = new LogEvent();
            SessionControl openSession = new SessionControl();
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
            if (openSession.IsFlatManEnabled)
            {
                //Stage the mount to the flatman
                lg.LogIt("Staging FlatMan");
                bool stgResult = FlatManStage();
                if (!stgResult)
                {
                    lg.LogIt("FlatMan Staging Failed -- aborting flats");
                    return;
                }
                //If Manual Setup is selected, then pause for user to position the FlatMan for flats
                //  Disconnect imaging devices before attaching panel, then reconnect afterwards
                //    this keeps the SBIG driver from freaking out when the guider USB is hot swapped
                //    with the FlatMan USB (Build 182+)
                if (openSession.IsFlatManManualSetupEnabled)
                {
                    lg.LogIt("Pausing to attach FlatMan panel");
                    lg.LogIt("Disconnecting imaging devices");
                    TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Camera);
                    TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Guider);
                    TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Focuser);
                    TSXLink.Connection.DisconnectDevice(TSXLink.Connection.Devices.Rotator);
                    MessageBox.Show("Attach the FlatMan, then press OK");
                    lg.LogIt("Connecting imaging devices");
                    TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Camera);
                    TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Guider);
                    TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Focuser);
                    TSXLink.Connection.ConnectDevice(TSXLink.Connection.Devices.Rotator);
                }
                //Turn on Flatman panel, if it hasn't been done already
                lg.LogIt("Lighting up FlatMan panel");
                fMan.Light = true;
            }
            else //Dusk or dawn flats
            {
                //Unpark mount, if parked, which it often is to do dusk flats
                TSXLink.Mount.UnPark();
                //point telescope essentially up
                lg.LogIt("Pointing telescope just west of zenith");
                //wait for dome to catch up with mount
                if (openSession.HasDome)
                    while (!TSXLink.Dome.IsGotoAzmComplete)
                        System.Threading.Thread.Sleep(1000);
                //slew to meridian
                TSXLink.Mount.SlewAzAlt(200.0, (60), "Flat Spot");
                //Turn tracking off
                TSXLink.Mount.TurnTrackingOff();
            }

            //Alright, all ready to go.  
            //Loop on the flat entries in the flat stack file, if any
            while (HaveFlatsToDo())
            {
                switch (openSession.FlatLightSource)
                {
                    case (LightSource.lsNone):
                        {
                            break;
                        }
                    case (LightSource.lsFlatMan):
                        {
                            if (openSession.IsFlatManEnabled)
                            {
                                // **********************  Use Flatman
                                //Rotate to PA, if there is a rotator is enabled
                                Flat iFlat = GetLeastRotatedFlat();
                                if (openSession.IsRotationEnabled)
                                {
                                    Rotator.RotateToRotatorPA(iFlat.RotationPA);
                                }
                                Imaging nhi = new Imaging();
                                nhi.DoFlatManFlats(iFlat.TargetName, iFlat.RotationPA, iFlat.SideOfPier, iFlat.FlatFilter, fMan);
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
            if (openSession.IsFlatManEnabled)
            {
                //Turn off flatman functions
                lg.LogIt("Terminating FlatMan");
                //Turn on Flatman panel, if it hasn't been done already
                lg.LogIt("Turning off FlatMan panel");
                fMan.Light = false;
                //If Manual Setup is selected, then pause for user to position the FlatMan for flats
                if (openSession.IsFlatManManualSetupEnabled)
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
            SessionControl openSession = new SessionControl();
            double nowRotAngle = 0;
            if (openSession.IsRotationEnabled)
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
