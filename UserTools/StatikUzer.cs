using MahCoadz.Kryptow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahCoadz.Uzer;

namespace MahCoadz.Uzer
{
    public class StatikUzer: IUzer
    {
        public int id { get; set; }
        public string uzerId { get; set; }
        public string hashedPassword { get; set; }
        public string hashseed { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public string accountType { get; set; }
        public string accountMeta1 { get; set; }
        public string dirkeys { get; set; }
        public string postkeys { get; set; }

        public StatikUzer() { }
        public StatikUzer(int givenid, string givenU, string givenP, string givenS)
        {
            id = givenid;
            uzerId = givenU;
            hashedPassword = KryptowTool.HashPassword(givenP, givenS);
            hashseed = givenS;
            firstName = uzerId;
            lastName = uzerId;
            name = givenU + " " + givenP;
        }
    }

    public static class UzerTools
    {
#if DEBUG
        // only for debug, testing, (and maybe demo) purposes
        private static string SomeDebugDefaultUzers = ""; // a|c;Bob|booya;Betty|wont;billy|bob;bear|ash;happy|test;sad|test;arthurdent|test;zaphod|test";
#else
        private static string SomeDebugDefaultUzers = "";
#endif

        public static List<IUzer> GimmeStaticUzers(bool TestMode = false,  string SpecialMockUzers = "")  
        {
            List<IUzer> rezult = new List<IUzer>();
            var tmplist = SomeDebugDefaultUzers.Split(';');
            foreach (var uzer in tmplist)
            {
                var u = uzer.Split('|');
                if (u.Length > 1)
                {
                    var theNewUzer = new StatikUzer(rezult.Count + 1, u[0], u[1], KryptowTool.GenRandomSeed());
                    rezult.Add(theNewUzer);
                }                
            }
            if (TestMode)
            {
                // special users for testing
            }

            // a major kludge to allow static users to be defined IN THE CONFIGURATION!! <ominous organ music>
            if (SpecialMockUzers.Length > 0 )
            {
                List<string> Special1 = SplitMeeGood(SpecialMockUzers,'■');
                foreach (string ThisMeta in Special1)
                {
                    List<string> Special2 = SplitMeeGood(ThisMeta, '≈');
                    if (Special2.Count > 1)
                    {
                        var thisAcctType = Uzer.AccountTypes.DirectoryInfo.ToString(); //a default
                        if (Special2.Count >= 3) { thisAcctType = ConfirmAccountType(Special2[2]); }
                        if (thisAcctType == Uzer.AccountTypes.DirectoryInfo.ToString())
                        {
                            rezult.Add(new StatikUzer() {
                                uzerId = Special2[0], // A random hard-to-guess-string 
                                accountType = Uzer.AccountTypes.DirectoryInfo.ToString(),
                                enabled = true,
                                accountMeta1 = Special2[1] 
                            });
                        }
                        else
                        {
                            // a password MUST be provided or we bjork - the onus is on the configurer...
                            var thisPassword = "";
                            if (Special2.Count >= 3) { thisPassword = Special2[1]; } else { throw new Exception("User Password configuration error!"); }
                            var theNewUzer = new StatikUzer(rezult.Count + 1, Special2[0].ToLower(), thisPassword, KryptowTool.GenRandomSeed());
                            if (Special2.Count >= 4) { theNewUzer.postkeys = Special2[3]; }
                            rezult.Add(theNewUzer);
                        }
                    }
                }               
            }
            return rezult;

        }
        private static string ConfirmAccountType(string givenAccountTypeText)
        {
            string rezult = "";
            foreach ( Uzer.AccountTypes thisType in Enum.GetValues(typeof(Uzer.AccountTypes)))
            {
                if (thisType.ToString().ToLower() == givenAccountTypeText.ToLower())
                {
                    rezult = thisType.ToString(); 
                    break;
                }
            }
            if (rezult == "") { throw new Exception("User Configuration Eror"); }
            return rezult;
        }

        public static List<string> SplitMeeGood(string givenString, char Splitter)
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
}
