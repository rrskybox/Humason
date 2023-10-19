using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;

namespace Humason
{
    internal static class FlatPresets
    {

        const string FlatPresetsFilename = "FlatPresets.xml";

        const string FlatManConfiguration = "FlatManConfiguration";
        const string FlatSetupX = "FlatPreset";
        const string FlatFilterX = "Filter";
        const string FlatExposureX = "Exposure";
        const string FlatBrightnessX = "Brightness";

        public static double? GetExposure(int filterID)
        {
            XElement fPX = null;
            XElement flatPresetListX = LoadFlatPresets();
            if (flatPresetListX != null)
                fPX = flatPresetListX.Elements(FlatSetupX).FirstOrDefault(f => Convert.ToInt16(f.Element(FlatFilterX).Value) == filterID);
            if (fPX != null)
                return Convert.ToDouble(fPX.Element(FlatExposureX).Value);
            return null;
        }

        public static int? GetBrightness(int filterID)
        {
            XElement fPX = null;
            XElement flatPresetListX = LoadFlatPresets();
            if (flatPresetListX != null)
                fPX = flatPresetListX.Elements(FlatSetupX).FirstOrDefault(f => Convert.ToInt16(f.Element(FlatFilterX).Value) == filterID);
            if (fPX != null)
                return Convert.ToInt16(fPX.Element(FlatBrightnessX).Value);
            return null;
        }

        public static void SetPreset(int filterID, double exposure, double brightness)
        {
            XElement flatPresetX = new XElement(FlatSetupX, new XElement[]
                {
                    new XElement (FlatFilterX, filterID.ToString()),
                    new XElement (FlatExposureX, exposure.ToString()),
                    new XElement(FlatBrightnessX, brightness.ToString()) }
                );
            XElement flatPresetListX = LoadFlatPresets();
            if (flatPresetListX != null)
            {
                XElement fPX = flatPresetListX.Elements(FlatSetupX).FirstOrDefault(f => Convert.ToInt16(f.Element(FlatFilterX).Value) == filterID);
                if (fPX != null)
                    fPX.ReplaceWith(flatPresetX);
                else
                    flatPresetListX.Add(flatPresetX);
            }
            else
                flatPresetListX = new XElement(FlatManConfiguration, flatPresetX);
            SaveFlatPresets(flatPresetListX);
            return;
        }

        private static XElement LoadFlatPresets()
        {
            XElement presets = null;
            SessionControl openSession = new SessionControl();
            string fpDir = openSession.HumasonDirectoryPath;
            //Create the flats request xml file, if it doesn't exist
            string fpPath = fpDir + "\\" + FlatPresetsFilename;
            if (File.Exists(fpPath))
                presets = XElement.Load(fpPath);
            return presets;
        }

        private static void SaveFlatPresets(XElement fListX)
        {
            SessionControl openSession = new SessionControl();
            string fpDir = openSession.HumasonDirectoryPath;
            //Create the flats request xml file, if it doesn't exist
            string fpPath = fpDir + "\\" + FlatPresetsFilename;
            fListX.Save(fpPath);
            return;
        }
    }
}
