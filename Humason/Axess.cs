//Class for XML access of all XML files in Humason

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Humason
{
    public class Axess
    {
        //Saved location of XML File for this instance
        private string axessXMLFilePath { get; set; } = null;

        //Creation of an instance to filename, but not creating it if there is none
        public Axess(string nhXMLFilePath)
        {
            //  and saves the file path for this isnstance
            axessXMLFilePath = nhXMLFilePath;
            return;
        }

        //Creation of an instance to a possibly nonexistant file
        //   Creates the file, with the container heading, if it does not exist already
        public Axess(string nhXMLFilePath, string nhContainerX)
        {
            //Creates an instance of Xccess for an XMLFilepath, 
            //  and creates a base XML file with the container, if the file doesn't exist
            //  and saves the file path for this isnstance
            if (!File.Exists(nhXMLFilePath))
            {
                XElement cDefaultX = new XElement(nhContainerX);
                cDefaultX.Save(nhXMLFilePath);
            }
            axessXMLFilePath = nhXMLFilePath;
            return;
        }

        //Spawn an instance by creating or overwriting from another instance
        public Axess(Axess sourceAxess, string newXMLFilePath)
        {
            XElement sourceX = XElement.Load(sourceAxess.axessXMLFilePath);
            sourceX.Save(newXMLFilePath);
            axessXMLFilePath = newXMLFilePath;
        }

        public void MergeXFileContents(Axess sourceAxess, string mergeXMLFilePath)
        {
            //Called  to add the fields of a default file into a sparse target plan
            //Get the contents of the file associated with this class instance
            XElement thisFileX = XElement.Load(axessXMLFilePath);
            //Get the contents of the file to be added in
            XElement mergeIntoX = XElement.Load(mergeXMLFilePath);
            foreach (XElement tpX in thisFileX.Elements())
            {
                if (!(thisFileX.Elements().Contains(tpX)))
                { thisFileX.Add(tpX); }
            }
            thisFileX.Save(axessXMLFilePath);
        }

        public void AddXFileContents(string FileToAppendToPath)
        {
            //Called to add a summary entry to the summary file
            //  where this class instance represents a summary file, and the
            //  ContentFile represents a class instance of a targetPlan

            //Get the contents of the file associated with this class instance
            XElement ThisFileX = XElement.Load(axessXMLFilePath);
            //Get the contents of the file to be added in
            XElement FileToAppendThisToX = XElement.Load(FileToAppendToPath);
            //Build new summary element
            FileToAppendThisToX.Add(ThisFileX);
            FileToAppendThisToX.Save(FileToAppendToPath);
            return;
        }

        public bool CheckItem(string itemName)
        {
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement itemX = spPlanX.Element(itemName);
            if (itemX == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetItem(string itemName)
        {
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement itemX = spPlanX.Element(itemName);
            if (itemX == null)
            {
                spPlanX.Add(new XElement(itemName, null));
                return null;
            }
            else
            { return (itemX.Value); }
        }

        public string GetItem(string itemSection, string itemName)
        {
            itemName = itemName.Replace(" ", "");
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sectionX = spPlanX.Element(itemSection);
            //Check section, if doesn't exist, then return empty string
            if (sectionX == null)
            {
                return null;
            }
            else
            {
                //Otherwise, look through the section for the itemname
                //if found, then return the entry, if not, return empty string
                XElement itemX = sectionX.Element(itemName);
                if (itemX == null)
                { return (null); }
                else
                { return (itemX.Value); }
            }
        }

        public XElement GetItems(string itemSection)
        {
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sectionX = spPlanX.Element(itemSection);
            //Check section, if doesn't exist, then return null
            if (sectionX == null)
            { return null; }
            else
            { return (sectionX); }
        }

        public void SetItems(string itemSection, XElement itemsX)
        {
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sectionX = spPlanX.Element(itemSection);
            //Check section, if doesn't exist, then add whole section
            //  otherwise replace the section
            if (sectionX == null)
            { spPlanX.Add(new XElement(itemSection, itemsX)); }
            else
            { spPlanX.ReplaceWith(itemSection, itemsX); }
        }

        public void SetItem(string itemName, string item)
        {
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sscfgXel = spPlanX.Element(itemName);
            if (sscfgXel == null)
            { spPlanX.Add(new XElement(itemName, item)); }
            else
            { sscfgXel.ReplaceWith(new XElement(itemName, item)); }
            spPlanX.Save(spFilePath);
            return;
        }

        public void SetItem(string itemName, bool itemBool)
        {
            string item = Convert.ToString(itemBool);
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sscfgXel = spPlanX.Element(itemName);
            if (sscfgXel == null)
            { spPlanX.Add(new XElement(itemName, item)); }
            else
            { sscfgXel.ReplaceWith(new XElement(itemName, item)); }
            spPlanX.Save(spFilePath);
            return;
        }

        public void SetItem(string itemName, double itemDouble)
        {
            string item = Convert.ToString(itemDouble);
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sscfgXel = spPlanX.Element(itemName);
            if (sscfgXel == null)
            { spPlanX.Add(new XElement(itemName, item)); }
            else
            { sscfgXel.ReplaceWith(new XElement(itemName, item)); }
            spPlanX.Save(spFilePath);
            return;
        }

        public void SetItem(string itemName, int itemInt)
        {
            string item = Convert.ToString(itemInt);
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sscfgXel = spPlanX.Element(itemName);
            if (sscfgXel == null)
            { spPlanX.Add(new XElement(itemName, item)); }
            else
            { sscfgXel.ReplaceWith(new XElement(itemName, item)); }
            spPlanX.Save(spFilePath);
            return;
        }

        public void SetItem(string sectionName, string itemName, string item)
        {
            //Set entry for level two-deep element
            //If the item is in the file, but the entry is "null" then just delete the entry
            //remove whitespace
            itemName = itemName.Replace(" ", "");
            string spFilePath = axessXMLFilePath;
            XElement spPlanX = XElement.Load(spFilePath);
            XElement sectionX = spPlanX.Element(sectionName);
            if (sectionX == null)
            {
                XElement itemX = new XElement(itemName, item);
                spPlanX.Add(new XElement(sectionName, itemX));
            }
            else
            //check section for entry with itemName, if none then add, otherwise replace
            {
                XElement itemX = sectionX.Element(itemName);
                if (itemX == null)
                {
                    if (item != null)
                    { sectionX.Add(new XElement(itemName, item)); }
                }
                else
                {
                    if (item != null)
                    { itemX.ReplaceWith(new XElement(itemName, item)); }
                    else
                    { itemX.Remove(); }
                }
            }
            spPlanX.Save(spFilePath);
            return;
        }

        public bool InitialItem(string ItemName, bool Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned

            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            { return (Convert.ToBoolean(GetItem(ItemName))); }
        }

        public double InitialItem(string ItemName, double Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned

            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            { return (Convert.ToDouble(GetItem(ItemName))); }
        }

        public int InitialItem(string ItemName, int Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned
            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            { return (Convert.ToInt32(GetItem(ItemName))); }
        }

        public string InitialItem(string ItemName, string Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned

            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, " ");
                return Item;
            }
            else
            { return (Convert.ToString(GetItem(ItemName))); }
        }

        public DateTime InitialItem(string ItemName, DateTime Item)
        {
            //If the xfile doesn't have the member, then the original item is returned,
            // otherwise the element in the xfile is returned
            string itemStr = GetItem(ItemName);
            if ((itemStr == null) || (itemStr == ""))
            {
                SetItem(ItemName, Convert.ToString(Item));
                return Item;
            }
            else
            {
                return (DateTime.Parse(itemStr));
            }
        }

        public void ReplaceItem(string ItemName, bool Item)
        {
            //The item is placed in the xfile -- boolean
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, double Item)
        {
            //The item is placed in the xfile -- double
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, int Item)
        {
            //The item is placed in the xfile -- integer
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, DateTime Item)
        {
            //The item is placed in the xfile -- string
            SetItem(ItemName, Convert.ToString(Item));
            return;
        }

        public void ReplaceItem(string ItemName, string Item)
        {
            //The item is placed in the xfile -- string
            SetItem(ItemName, Item);
            return;
        }

        public XElement MakeList(string itemName, List<string> itemList)
        {
            XElement NewListX = new XElement(itemName);
            foreach (string itemStr in itemList)
            {
                NewListX.Add(new XElement(itemName, itemStr));
            }

            return NewListX;
        }

        public List<string> GetList(XElement itemListX)
        {
            List<string> NewListStr = new List<string>();
            foreach (XElement itemX in itemListX.Elements())
            {
                NewListStr.Add(itemX.Value);
            }

            return NewListStr;
        }
    }
}
