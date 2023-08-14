/*
 * Mosaic encapsulates a method for parsing a clipboard of TSX mosaic target entries
 * into a list of structured (Mosaic Target) target data
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Humason
{
    class Mosaic
    {
        public Mosaic()
        { }

        public List<MosaicTarget> ReadTSXMosaic()
        {
            //Reads a clipboard of mosaic entries as placed by TSX
            char[] DelimNewLine = { '\r', '\n' };
            char[] DelimSpace = { ' ' };

            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                //get text from clipboard
                //split on newlines and remove empty entries, if any
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);
                string[] splitmo = clipboardText.Split(DelimNewLine, StringSplitOptions.RemoveEmptyEntries);
                int tgtCount = splitmo.Length;
                //if no entries (for any reason) return nothing
                if (tgtCount == 0)
                {
                    return null;
                }
                //get the current TSX FOVI postion angle
                double iFOVPA = TSXLink.FOVI.GetFOVPA;
                //Create empty target data list
                List<MosaicTarget> mtargets = new List<MosaicTarget>(tgtCount);
                //run through the array of targets, parse them into the list of mosaic entries
                for (int i = 0; i < tgtCount; i++)
                {
                    string[] starget = splitmo[i].Split(DelimSpace, StringSplitOptions.RemoveEmptyEntries);
                    //try to assign contents of clipboard string
                    //  if it screws up then it's probably the wrong data in the wrong format -- return null;
                    try
                    {
                        mtargets.Add(new MosaicTarget(Convert.ToInt32(starget[0]),
                                                                starget[1],
                                                                Convert.ToInt32(starget[2]),
                                                                Convert.ToDouble(starget[3]),
                                                                Convert.ToDouble(starget[4]),
                                                                iFOVPA));
                    }
                    catch (Exception ex)
                    { return null; }
                }
                return mtargets;
            }
            return null;
        }

        //Class structure for target data from a TSX mosaic definition
        public class MosaicTarget
        {
            public int Index { get; set; }
            public string Set { get; set; }
            public int Frame { get; set; }
            public double RA { get; set; }
            public double Dec { get; set; }
            public double PositionAngle { get; set; }

            public MosaicTarget(int index, string set, int frame, double ra, double dec, double iPA)
            {
                this.Index = index;
                this.Set = set;
                this.Frame = frame;
                this.RA = ra;
                this.Dec = dec;
                this.PositionAngle = iPA;
                return;
            }
        }

    }
}
