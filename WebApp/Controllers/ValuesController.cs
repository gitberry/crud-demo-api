using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Cors;
using System.Text.Json;
using System.Web.Http.Description;
using MahCoadz.JWT;
using MahCoadz.Uzer;
using MahCoadz.Data;
using MahCoadz.Util;
using MahCoadz.Json;
using AppConfig;
using MahCoadz.Data.FunnySongNS;
using MahCoadz.Data.DAL;

namespace WebApp.Controllers
{
    /// <summary>
    /// Values Controller
    ///  Features:
    ///   - JWT for authentication
    ///   - Each data model follows a pattern"
    ///      - a genericPost Tool helps each POCO/model maps back to a single table
    ///      - in the db - generic fields being remapped with standard meta (create*, edit*)
    ///      - automatic logging
    ///      - each model & function has a salted hash that must be configured to enable
    ///        (when used without authentication - the configurator must ONLY allow functions
    ///        that are strictly needed)
    ///        Salt can (and argueably should) be changed for each deployment
    ///         (increases configuration setup misses/mistakes though...)
    ///   - Considerations for multiple instances
    ///        Can be a folder (ie ..whatever../API) instead of just root 
    ///        CORS configurable
    ///   - Considerations for future
    ///      Throttling/Anti DDOS features??
    ///      Dynamic JSON objects instead of POCO??
    ///      
    /// Comments:
    ///   I prefer to use alternate spellings to avoid name clash with framework-names.
    ///     thus avoiding past debugging trauma - plus alternate spellings are WAY easier to search for in code!
    ///    
    /// </summary>
    public class ValuesController : ApiController
    {
                private MahDataMgr localDataMgr = null;
                private LogMgr localLogger = null;
                private List<IUzer> systemUsers = null;

        public ValuesController()
        {
            localDataMgr = new MahDataMgr();
            localLogger = new LogMgr();
            systemUsers = MahCoadz.Uzer.UzerTools.GimmeStaticUzers(false, AppConfig.Setting.ConfiguredUzers); //todo: change to configurable db or config users...
        }
        // this constant allows one to compile this API to be targetted to a root of any given path - OR any subdirectories therein
        // if you don't/can't use virtual directories then pay attention to the following line...
        const string APIRootDir = ""; //"API/";  // use "API/" or the like to hang all these API's off that path..

        // Authentication via JsonWebToken ================================================
        [Route(APIRootDir + "authenikait")]
        [HttpGet]
        public Object authenikait(string username, string password)
        {
            Laug("authentikait", 3);
            if (IsValidJWTToken())
            {
                // this is an odd situation - and obviously the client code (or user) is doing something that we really don't think is valid 
                // (ie they're trying to be authenticated WHILE they're carrying a valid token
                // unless the use case changes - this code will throw them an esoteric error (teapot seems not to be available awww)
                Laug("Tried To Log in WHILE Logged in", 2);
                throw new HttpResponseException(HttpStatusCode.ExpectationFailed);     
            }
            else
            {
                var rezult = MahCoadz.JWT.JwtTool.ValidateAndGenerateToken(
                      username.ToLower()
                    , password
                    , UzerTools.GimmeStaticUzers(false, AppConfig.Setting.ConfiguredUzers)
                    , AppConfig.Setting.JWTsigningSecret
                    , AppConfig.Setting.JWTapiSite
                    , AppConfig.Setting.JWTclientSite
                    , username
                    , DateTime.UtcNow.AddMinutes(AppConfig.Setting.JWTMinutesToLive)
                    );
                if (rezult != null)
                {
                    Laug($"Logged in successfully with [{rezult}]", 2);
                    return rezult;
                }
            }
            Laug($"authenikait: JWT Error authenticating using [{username}|{password}]", 2);
            throw new HttpResponseException(HttpStatusCode.Unauthorized);     //note: null is simple but does not inform client...
        }

        #region FunnySongs Data // =======================================
        [Route(APIRootDir + "funnysongs")]
        [HttpGet]
        public Object[] FunnySongs(int recordRequest)
        {
            try
            {
                Laug("funnysongs", 3);
                object[] dataResult = null;
                if (IsValidJWTToken())
                {
                    var rawrezultSet = localDataMgr.RetrieveXrecords(
                        MahCoadz.Data.FunnySongNS.FunnySong.myMetaFlag,
                        MahCoadz.Data.FunnySongNS.FunnySong.myMetaStruct, recordRequest, true);
                    if (rawrezultSet is null)
                    {
                        Laug($"funnysongs:null return", 2);
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        Laug($"funnysongs:{rawrezultSet.Count} records", 2);
                        dataResult = MahCoadz.Data.FunnySongNS.FunnySong.FunnySongs(rawrezultSet).ToArray();
                        return dataResult;
                    }
                }
                Laug($"funnysongs:Unauthorized", 1);
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                var tmpRezult = new Object[1];
                tmpRezult[0] = ex;
                return tmpRezult;
            }
        }

        [Route(APIRootDir + "funnysong/{id}")]
        [HttpGet]
        public Object FunnySong(int id)
        {
            Laug("funnysong/{id}", 3);
            IUzer thisJWT = ValidJWTToken();
            if (IsValidJWTToken(thisJWT))
            {
                DataShort1 rawrezult = null;
                if (id == 0)
                {
                    // please note - this object is NOT saved to database...
                    rawrezult = new DataShort1();
                }
                else
                {
                    rawrezult = localDataMgr.RetrieveById(
                        id,
                        MahCoadz.Data.FunnySongNS.FunnySong.myMetaFlag,
                        MahCoadz.Data.FunnySongNS.FunnySong.myMetaStruct); 
                }

                if (rawrezult is null)
                {
                    Laug("funnysong/{id}:null", 2);
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                Laug($"funnysong/{id}:{rawrezult.ID}", 2);
                var rezult = new FunnySong(rawrezult);
                if (id == 0)
                {
                    // minimal fields to populate on new...
                    rezult.IsActive = true;
                    rezult.Title = "(new title)";
                    rezult.CreatedBy = thisJWT.uzerId;
                    rezult.CreatedOn = DateTime.UtcNow;
                }
                rezult.APIProvided = DateTime.UtcNow;
                return rezult;
            }
            Laug("funnysong/{id}:unauthorized", 1);
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route(APIRootDir + "funnysongdelete/{id}")]
        [HttpGet]
        public object FunnySongDelete(int id)
        {
            Laug("funnysongdelete/{id}", 3);
            IUzer thisUzer = ValidJWTToken();
            if (IsValidJWTToken(thisUzer))
            {
                var rawrezult = localDataMgr.DeleteById(
                    id,
                    MahCoadz.Data.FunnySongNS.FunnySong.myMetaFlag,
                    MahCoadz.Data.FunnySongNS.FunnySong.myMetaStruct,
                    thisUzer);
                if (rawrezult is null)
                {
                    Laug("funnysongdelete/{id}:fail", 1);
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                Laug($"funnysongdelete/{id}:completed", 3);
                var rezult = new FunnySong(rawrezult);
                rezult.APIProvided = DateTime.UtcNow;
                return rezult;
            }
            Laug("funnysongdelete/{id}:unauthorized", 1);
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [Route(APIRootDir + "funnysongundelete/{id}")]
        [HttpGet]
        public object FunnySongUnDelete(int id)
        {
            Laug("funnysongundelete/{id}", 3);
            IUzer thisUzer = ValidJWTToken();
            if (IsValidJWTToken(thisUzer))
            {
                var rawrezult = localDataMgr.DeleteById(
                    id,
                    MahCoadz.Data.FunnySongNS.FunnySong.myMetaFlag,
                    MahCoadz.Data.FunnySongNS.FunnySong.myMetaStruct,
                    thisUzer, true);
                if (rawrezult is null)
                {
                    Laug("funnysongundelete/{id}:fail", 1);
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                Laug($"funnysongundelete/{id}:completed", 3);
                var rezult = new FunnySong(rawrezult);
                rezult.APIProvided = DateTime.UtcNow;
                return rezult;
            }
            Laug("funnysongundelete/{id}:unauthorized", 1);
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
        #endregion

        #region "Private Utilities Used"
        bool IsValidJWTToken()
        {
            return IsValidJWTToken(ValidJWTToken());
        }

        bool IsValidJWTToken(IUzer givenToken)
        {
#if DEBUG
            if (AppConfig.Setting.DevByPassJWTtoken) { return true; }
#endif
            return (!(givenToken is null));
        }

        IUzer ValidJWTToken()
        {
            IUzer rezult = new MahCoadz.JWT.JwtTool().ValidateToken(
                  Request
                , UzerTools.GimmeStaticUzers(false, AppConfig.Setting.ConfiguredUzers)
                , AppConfig.Setting.JWTsigningSecret
                , AppConfig.Setting.JWTclientSite
                , AppConfig.Setting.JWTapiSite);
            if (!(rezult is null))
            {
                Laug($"Valid Jwt:{extractHeaderValue("Authorization")} User:{rezult.uzerId}", 3);
            }
            else
            {
                Laug($"INVALID Jwt:{extractHeaderValue("Authorization")}", 1);
            }
            return rezult;
        }


        [Route(APIRootDir + "genericpost")]
        [HttpPost]
        public IHttpActionResult genericPost(genericPostInfo PostDataJSON)
        {
            Laug("genericpost", 3);
            //GenericPostEntities in config with "hashes" to act as "throttles" to keep only wanted data types in a particular app...
            IUzer thisUzer = ValidJWTToken();
            if (IsValidJWTToken(thisUzer))
            {
                genericPostInfo rezult = JsonAPITool.processGenericPostInfo(PostDataJSON, AppConfig.Setting.AppGenericEntities, thisUzer);
                if (rezult is null)
                {
                    Laug($"Error with GenericPost:{PostDataJSON}", 1);
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                if (rezult.responseCode == genericPostResponses.AuthorizationDenied)
                {
                    Laug($"genericPost: user not authorized to post {PostDataJSON}", 1);
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
                Laug($"Successful Generic post: {PostDataJSON}", 2);
                return Ok(rezult);
            }
            else
            {
                Laug($"genericPost: invalid JWT {PostDataJSON}", 1);
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        string myAppKey()
        {
            return extractHeaderValue(AppConfig.Setting.AppKeyHeaderValue);            
        }

        string extractHeaderValue(string givenKey)
        {
            var rezult = "";
            if (Request.Headers.Contains(givenKey)) 
            {
                rezult = Request.Headers.GetValues(givenKey).First();
            }
            return rezult;
        }

        string clientIPAddress()
        {
            // oddities about IP addresses and IIS and headers on routers etc..
            string rezult = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (rezult.IsNullOrEmpty())
            {
                rezult = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return rezult;
        }

        void Laug(string ExtraLogInfo = "", int givenVerbosity = 1)
        {
            try
            {
                if (givenVerbosity <= AppConfig.Setting.LogVerbosity)
                {
                    localLogger.Loggit($"[{givenVerbosity}]{ExtraLogInfo}", myAppKey(), clientIPAddress());
                }
            }
            catch (Exception ex)
            {
                if (!AppConfig.Setting.LogFailQuietly)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
