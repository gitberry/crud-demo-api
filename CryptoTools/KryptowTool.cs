using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MahCoadz.Kryptow
{
    public static class KryptowTool
    {
        public static string HashPassword(string password, string salt)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(salt + password))).Replace("=", "");
            }
        }
        // cargo cult coding: https://stackoverflow.com/questions/31908529/randomnumbergenerator-proper-usage
        public static string GenRandomSeed()
        {
            return Convert.ToBase64String(GenerateSaltNewInstance(42));
        }
        private static byte[] GenerateSaltNewInstance(int size)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var salt = new byte[size];
                generator.GetBytes(salt);
                return salt;
            }
        }

        public static string GUIDB64string(Guid givenGuid)
        {
            return Convert.ToBase64String(givenGuid.ToByteArray()).Replace("=", "");
        }

        public static string faultTolerantBase64Decode(string givenString)
        {
            return faultTolerantBase64Decode(givenString, false); //the default is to return empty string if decode fails...
        }

        public static string faultTolerantBase64Decode(string givenString, bool IfFailReturnGivenString)
        {
            string rezult = "";
            // all this shit because sometimes base64 is encoded sloppily but .NET is kinda rigid about the final padding...
            //briefly considered doing this with recursion - would be simple enough - and no - this works OK...
            try
            {
                rezult = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(givenString));
            }
            catch
            {
                try
                {
                    rezult = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(givenString + "="));
                }
                catch
                {
                    try
                    {
                        rezult = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(givenString + "=="));
                    }
                    catch
                    {
                        try
                        {
                            rezult = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(givenString + "==="));
                        }
                        catch
                        {
                            rezult = ""; //default to return empty string
                            if (IfFailReturnGivenString) { rezult = givenString; }
                        }
                    }
                }
            }
            return rezult;
        }
    }
}
