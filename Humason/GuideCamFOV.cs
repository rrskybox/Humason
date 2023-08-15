using System;

namespace Humason
{
    public class GuideCamFOV
    {
        //Instantiation picks up the guider FOV size, center, offset and PA
        //Open "my equipment.txt" file in TSX.  Translate to XML version
        //  for ease of parsing contents

        //Guide camera FOVI name could be "Autoguider" but maybe not
        public const string GuiderName = "Autoguider";
        //Guide camera FOVI should be second element (zero based) -- this may be a must, don't know yet
        public const int GuiderElementNumber = 1;

        //Gonna pick up an instance of the FOV data
        private FOVX gFOV;

        public GuideCamFOV()
        {
            //Open and populate the FOV database from the TSX my equipment.txt file
            gFOV = new FOVX();
            //Get the FOV name found for the first active entry (should only be one)
            Name = gFOV.GetActiveFOVHeaderEntry(FOVX.Description1FieldXName);
            //Get the second FOV element name for this entry -- should be the guide camera
            string aGuiderName = gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.ElementDescriptionFieldXName);
            //Populate the position fields for the FOV
            PA = Convert.ToDouble(gFOV.GetActiveFOVHeaderEntry(FOVX.PositionAngleFieldXName));
            CenterX = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.CenterOffsetXFieldXName));
            CenterY = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.CenterOffsetYFieldXName));
            PixelSizeX = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.PixelsXFieldXName));
            PixelSizeY = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.PixelsYFieldXName));
            ArcMinSizeX = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.SizeXFieldXName));
            ArcMinSizeY = Convert.ToDouble(gFOV.GetActiveFOVElementEntry(GuiderElementNumber, FOVX.SizeYFieldXName));
        }

        //Create automatic properties to hold FOV data in the class instance
        public string Name { get; set; }
        public double PA { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double PixelSizeX { get; set; }
        public double PixelSizeY { get; set; }
        public double ArcMinSizeX { get; set; }
        public double ArcMinSizeY { get; set; }
    }
}

