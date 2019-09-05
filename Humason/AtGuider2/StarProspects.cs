using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TheSkyXLib;

namespace AtGuider2
{
    public class StarProspects
    {

        const string DBQFilePath =
            "\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\AtFocus2.dbq";

        private List<DBQStar> starProspectList = new List<DBQStar>();

        public StarProspects(double searchAreaDeg)
        {
            //Creates a list of star prospects from the associated observing list

            //Set StarChart FOV to something reasonable for this search
            sky6StarChart tsxsc = new sky6StarChart
            {
                FieldOfView = searchAreaDeg
            };

            //Determine if search database file exists, if not, create it
            //if (!IsDBQInstalled())
            //{ InstallDBQ(); }

            //Load the path, open and run the selected search database query
            sky6DataWizard tsxdw = new sky6DataWizard();
            tsxdw.Path = GetDBQPath();
            tsxdw.Open();
            sky6ObjectInformation tsxoi = tsxdw.RunQuery;
            //Create a star list array for population of data

            //Fill in data arrays (for speed purposes)
            for (int i = 0; i < tsxoi.Count; i++)
            {
                tsxoi.Index = i;

                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
                string starName = (tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
                double starRA = (tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
                double starDec = (tsxoi.ObjInfoPropOut);
                tsxoi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_MAG);
                double starMag = (tsxoi.ObjInfoPropOut);
                DBQStar newStar = new DBQStar
                {
                    StarName = starName,
                    StarRA = starRA,
                    StarDec = starDec,
                    StarMag = starMag
                };
                starProspectList.Add(newStar);
            }
            return;
        }

        public string StarName(int starIdx) => starProspectList[starIdx].StarName;
        public double StarRA(int starIdx) => starProspectList[starIdx].StarRA;
        public double StarDec(int starIdx) => starProspectList[starIdx].StarDec;
        public double StarMag(int starIdx) => starProspectList[starIdx].StarMag;

        //Get the number of prospects in the list
        public int Count { get { return starProspectList.Count; } }

        //Get the starDBQ for the nth prospect
        public DBQStar Star(int starIdx)
        { return starProspectList[starIdx]; }

        //Checks to see if search database file is already installed or not
        public bool IsDBQInstalled() => File.Exists(GetDBQPath());

        //Installs the guider DBQ in the proper SB folder
        public void InstallDBQ()
        {
            ////Collect the file contents to be written
            Assembly dgassembly = Assembly.GetExecutingAssembly();
            Stream dgstream = dgassembly.GetManifestResourceStream("AtGuider2.AtGuider2.dbq");
            Byte[] dgbytes = new Byte[dgstream.Length];
            FileStream dbqgfile = File.Create(GetDBQPath());
            int dgreadout = dgstream.Read(dgbytes, 0, (int)dgstream.Length);
            dbqgfile.Close();
            //write to destination file
            File.WriteAllBytes(GetDBQPath(), dgbytes);
            dgstream.Close();
            return;
        }

        //Returns a path to the observing list query for the dbqType
        public string GetDBQPath() => "C:\\Users\\" + System.Environment.UserName + DBQFilePath;

    }

    public class DBQStar
    {
        //Basically a structure for encapsulating a star database entry
        public DBQStar()
        {
            //Nothing really to do here upon instantiation
        }
        public string StarName { get; set; }
        public double StarRA { get; set; }
        public double StarDec { get; set; }
        public double StarMag { get; set; }
    }

}
