using MahCoadz.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppConfig
{
    public enum GlobalAppVars
    {
        globalGenericPostEntities
    }
    public static class Setting
    {
#if DEBUG
        //this is purely to make IDE debugging running easier

        public static string ThisAppKey                 = ConfigDefault("ThisAppKey",                        ""); // this (could be) so critical we can't risk a default ending up in prod - config it everywhere, everytime!
        public static string JWTsigningSecret           = ConfigDefault("JWTsigningSecret",                  "QmlnTG9uZ1NlY3JldEZvclRlc3RpbmdhbmRBbGx0aGF0SGFwcHlKYXp6T0tITU1NTQ");

        public static string JWTapiSite                 = ConfigDefault("JWTapiSite",                        "http://localhost:50191");
        public static string JWTclientSite              = ConfigDefault("JWTclientSite",                     "http://localhost:3000");
        public static double JWTMinutesToLive           = ConfigDefault("JWTMinutesToLive",                  3);
        public static string CORSorigin                 = ConfigDefault("CORSorigin",                        "http://localhost:3000");
        public static bool   EnableSwagger              = ConfigDefault("EnableSwagger",                     true);
        public static string LandingPageTitleVerbage    = ConfigDefault("ViewBagHomeControllerTitleVerbage", "nbAPI1-net47(d)");
        public static string LandingPageOneLineVerbiage = ConfigDefault("LandingPageOneLineVerbiage",        "This is an API.  Please refer to documentation to use.");
        //public static string DirInfoBase                = ConfigDefault("DirInfoBase",                       null); //this should fail if not configured - no way hozay can you laze into my server file structure!
        //public static string APIRootDir                 = ConfigDefault("APIRootDir",                        "API"); // note: this is not used at this time...
        public static string ConfiguredUzers            = ConfigDefault("SpecialMockUzers",                  "");
        public static string AppKeyHeaderValue          = ConfigDefault("AppKeyHeaderValue",                 "ClientUUID");
        public static int    LogVerbosity               = ConfigDefault("LogVerbosity",                      3); // should be turned down after debugged..
        //public static bool   OverRideDirKlientKey       = ConfigDefault("OverRideDirKlientKey",              false); // this is purely for public facing dir using apps - USE WITH CAUTION!!!
        public static bool   LogFailQuietly             = ConfigDefault("LogFailQuietly",                    false);

        public static List<GenericPostEntity> AppGenericEntities = JsonAPITool.CompilePostEntities(ConfigDefault("AppGenericEntities", "")); // "MUZIK_SCORE;OZkezfAf+WayTgxgyBFHLfAncho;updateorcreate,delete|FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4;updateorcreate,delete|"));

        //this should NOT be defined (or used) in prod
        public static bool   DevByPassJWTtoken         = ConfigDefault("DevByPassJWTtoken",                  false); // this only works for debug - will blow up logins...

#else
        // prod should ALWAYS fail out of the box if no configuring on the server takes place - Mr T says: PROD requires a higher level of attention FOOL! 
        public static string ThisAppKey                 = ConfigDefault("ThisAppKey",                        ""); // this (could be) so critical we can't risk a default ending up in prod - config it everywhere, everytime!
        public static string JWTsigningSecret           = ConfigDefault("JWTsigningSecret",                  "");
        public static string JWTapiSite                 = ConfigDefault("JWTapiSite",                        "");
        public static string JWTclientSite              = ConfigDefault("JWTclientSite",                     "");
        public static double JWTMinutesToLive           = ConfigDefault("JWTMinutesToLive",                   1);
        public static string CORSorigin                 = ConfigDefault("CORSorigin",                        "");
        public static bool   EnableSwagger              = ConfigDefault("EnableSwagger",                      false);
        public static string LandingPageTitleVerbage    = ConfigDefault("ViewBagHomeControllerTitleVerbage", "nbAPI1-net47");
        public static string LandingPageOneLineVerbiage = ConfigDefault("LandingPageOneLineVerbiage",        "This is an API.  Please refer to documentation to use.");
        public static string DirInfoBase                = ConfigDefault("DirInfoBase",                       null); //this should fail if not configured - no way hozay can you laze into my server file structure!
        public static string APIRootDir                 = ConfigDefault("APIRootDir",                        "API");
        public static string SpecialMockUzers           = ConfigDefault("SpecialMockUzers",                  "");
        public static string ConfiguredUzers            = ConfigDefault("SpecialMockUzers", "");
        //blic static string genericPostStructs         = ConfigDefault("genericPostStructure",              ""); // anything specified will start from a provided MetaFlag value and then work out from there
        //blic static string genericPostStructsDefault  = ConfigDefault("genericPostStructsDefault",         "");
        public static string AppKeyHeaderValue          = ConfigDefault("AppKeyHeaderValue",                 "ClientUUID");
        public static int    LogVerbosity               = ConfigDefault("LogVerbosity",                        3); // should be turned down after debugged..
        public static bool   OverRideDirKlientKey       = ConfigDefault("OverRideDirKlientKey",              false); // this is purely for public facing dir using apps - USE WITH CAUTION!!!
        public static bool   LogFailQuietly             = ConfigDefault("LogFailQuietly",                    false); 

        public static List<GenericPostEntity> AppGenericEntities = JsonAPITool.CompilePostEntities(ConfigDefault("AppGenericEntities", ""));  // ie <add key="AppGenericEntities" value="FunnyMusic;x7oD9xvCmIIi79hF1hYfdwhEaR4;updateorcreate,delete|" />

#endif
        private static string ConfigDefault(string givenAppSetting, string defaultValueIfError)
        {
            string givenSettingTextValue = ConfigurationManager.AppSettings[givenAppSetting];
            string result = defaultValueIfError;
            if ( givenSettingTextValue != "" && !(givenSettingTextValue is null) ) { result = givenSettingTextValue; }
            if (givenSettingTextValue == "INTENTIONALLYEMPTY") { result = ""; }
            return result;
        }

        private static double ConfigDefault(string givenAppSetting, double defaultValueIfError)
        {
            string givenSettingTextValue = ConfigurationManager.AppSettings[givenAppSetting];
            double result = defaultValueIfError;
            if ( !(givenSettingTextValue == "") && !(givenSettingTextValue is null) ) // "" parse to 0 - don't want 0 unintentionally!
            {
                double.TryParse(givenSettingTextValue, out result);
            }            
            return result;
        }

        private static int ConfigDefault(string givenAppSetting, int defaultValueIfError)
        {
            string givenSettingTextValue = ConfigurationManager.AppSettings[givenAppSetting];
            int result = defaultValueIfError;
            if (!(givenSettingTextValue == "") && !(givenSettingTextValue is null)) // "" parse to 0 - don't want 0 unintentionally!
            {
                int.TryParse(givenSettingTextValue, out result);
            }
            return result;
        }


        private static bool ConfigDefault(string givenAppSetting, bool defaultValueIfError)
        {
            string givenSettingTextValue = ConfigurationManager.AppSettings[givenAppSetting];
            bool result = defaultValueIfError;
            if (!(givenSettingTextValue == "") && !(givenSettingTextValue is null)) 
            {
                bool.TryParse(givenSettingTextValue, out result);
            }
            return result;
        }

    }


}

