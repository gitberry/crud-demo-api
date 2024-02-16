using MahCoadz.Uzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahCoadz.Util
{
    public class Tuul 
    {
        public static List<string> SplitMeGood(string givenString)
        {
            Char[] myDelim = new Char[] { ':' };
            var Rezult1 = givenString.Split(myDelim, StringSplitOptions.RemoveEmptyEntries).ToList();
            //this version of .net doesn't have trim option... so:
            var Rezult = new List<string>();
            foreach (string thisItem in Rezult1)
            {
                if (thisItem.Trim().Length > 0) Rezult.Add(thisItem);
            }
            return Rezult;
        }
        public static List<string> SplitMeGood(string givenString, char Splitter)
        {
            Char[] myDelim = new Char[] { Splitter };
            var Rezult1 = givenString.Split(myDelim, StringSplitOptions.RemoveEmptyEntries).ToList();
            //this version of .net doesn't have trim option... so:
            var Rezult = new List<string>();
            foreach (string thisItem in Rezult1)
            {
                if (thisItem.Trim().Length > 0) Rezult.Add(thisItem);
            }
            return Rezult;
        }        
    }

    public class SimpleFolderInfo
    {
        public string  FullPathAndName { get; set; }
        public string Name { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedDT { get; set; }
        public DateTime EditedDT { get; set; }
    }
}
