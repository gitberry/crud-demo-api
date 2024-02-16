using MahCoadz.Data;
using MahCoadz.Data.DAL;
using MahCoadz.Data.FunnySongNS;
using MahCoadz.Uzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MahCoadz.Json
{
    public static class MahDataEntities
    {
        public static string[] AllowedEntities()
        {
            //note: this method COULD allow inadvertent specification of non-unique values - unlike an enum...
            string[] rezult = new string[]
            {
               $"{MahCoadz.Data.FunnySongNS.FunnySong.myMetaFlag}_{MahCoadz.Data.FunnySongNS.FunnySong.myMetaStruct}",
            };
        return rezult;
        }
        public static bool Allowed(string givenEntity)
        {
            return AllowedEntities().FirstOrDefault(ge => ge == givenEntity) == givenEntity;
        }

    }

    public enum AllowedGenericEntityPostType
    {        
        //create, 
        //update,
        updateorcreate, //simplified too much???
        delete
    }

    public static class genericPostHashes
    {
        //Change this for each deployment.. thus config's can't be compared
        private static string HashSalt = "eyIhIjrzLCJOK2V4IjoiIYCZmFullofSteakLGp3Qmp56LQ2";  //tests are based on this value:eyIhIjrzLCJOK2V4IjoiIYCZmFullofSteakLGp3Qmp56LQ2
        private static string EntityPrefixIt(string givenName) { return $"GenericEntities.{givenName}"; }
        private static string TypePrefixIt(string givenName) { return $"GenericEntityPostType.{givenName}"; }
        public static List<Tuple<string, string>> Hawsh
        {
            get
            {
                var rezult = new List<Tuple<string, string>>();
                foreach (string thisEntity in MahCoadz.Json.MahDataEntities.AllowedEntities()) 
                {
                    rezult.Add(new Tuple<string, string>(EntityPrefixIt(thisEntity), MahCoadz.Kryptow.KryptowTool.HashPassword(thisEntity, HashSalt)));
                }
                foreach (AllowedGenericEntityPostType thisPostType in Enum.GetValues(typeof(AllowedGenericEntityPostType)))
                {
                    rezult.Add(new Tuple<string, string>(TypePrefixIt(thisPostType.ToString()), MahCoadz.Kryptow.KryptowTool.HashPassword(thisPostType.ToString(), HashSalt)));
                }
                return rezult;
            }
        }
        public static bool ValidateEntity(string EntityName, string EntityHawsh)
        {
            return !(Hawsh.FirstOrDefault(hh => hh.Item1 == EntityPrefixIt(EntityName) && hh.Item2 == EntityHawsh) is null);
        }
        public static bool ValidatePostType(string PostTypeName, string EntityHawsh)
        {
            // hashses not used at this time for PostTypes...
            return !(Hawsh.FirstOrDefault(hh => hh.Item1 == TypePrefixIt(PostTypeName)) is null);
        }

    }

    public static class JsonAPITool
    {
        // After building this - I find myself thinking that JToken in c# could do this better??? Might be even more flexible??
        public static genericPostInfo processGenericPostInfo(genericPostInfo givenGenericPostInfo, List<GenericPostEntity> givenGenericEntities, IUzer givenUzer)
        {
            genericPostInfo rezult = givenGenericPostInfo;
            rezult.responseCode = genericPostResponses.ProcessStarted;
            // validate the Entity Type
            // security gate #1
            GenericPostEntity thisPostEntity = givenGenericEntities.Find(ge => ge.EntityTag == rezult.EntityTag); 
            List<GenericPostEntity> givenEntityPermissions = CompilePostEntities(givenUzer.postkeys);
            GenericPostEntity thisPostPermission = givenEntityPermissions.Find(ep => ep.EntityTag == thisPostEntity.EntityTag);
            if ( thisPostEntity is null )
            {
                rezult.responseCode = genericPostResponses.UnknownGenericPostEntityType;
            }
            else if ( thisPostPermission is null)
            {
                rezult.responseCode = genericPostResponses.AuthorizationDenied;
            }
            else
            {
                // happy path - now we find match a post activity/type
                // validate the Post Activity/Type
                // security gate #2
                if ( thisPostEntity.PostTypes.Find( pt => pt == rezult.PostType) is null)
                {
                    rezult.responseCode = genericPostResponses.UnknownPostType;
                }
                else 
                {
                    try
                    {
                        // happy path - let's turn it into a proper object - if we can!
                        string serializedGivenJson = MahCoadz.Kryptow.KryptowTool.faultTolerantBase64Decode(rezult.PostDataJSON); //a vision that may die because JS is just a little tooo wonky... and well Encoding EVERYTHING is not necessary...
                        if (string.IsNullOrEmpty(serializedGivenJson)) { serializedGivenJson = rezult.PostDataJSON; } //a more typical path...
                        // security gate #3
                        if (MahCoadz.Json.MahDataEntities.Allowed(rezult.EntityTag) ) // rezult.EntityTag == AllowedGenericEntities.FunnyMusic.ToString())
                        {
                            // special code for each action                           
                            if (givenGenericPostInfo.PostType == AllowedGenericEntityPostType.updateorcreate.ToString())
                            {
                                //FunnySong thisObject = JsonSerializer.Deserialize<FunnySong>(serializedGivenJson);
                                DataShort1 thisRecord = SpecialDeserialize(serializedGivenJson, givenGenericPostInfo); // thisObject.DOAExporter();
                                                                                                                       // better workaround?
                                if (thisRecord.DtA == DateTime.MinValue) { thisRecord.DtA = null; }
                                if (thisRecord.DtB == DateTime.MinValue) { thisRecord.DtB = null; }
                                if (thisRecord.DtC == DateTime.MinValue) { thisRecord.DtC = null; }
                                if (thisRecord.DtD == DateTime.MinValue) { thisRecord.DtD = null; }
                                if (thisRecord.DtE == DateTime.MinValue) { thisRecord.DtE = null; }
                                new MahDataMgr().AddOrUpdateOne(thisRecord, givenUzer); // do we return a record number?                                
                                rezult.responseCode = genericPostResponses.UpdatedCompleted;
                            }
                            else if (givenGenericPostInfo.PostType == AllowedGenericEntityPostType.delete.ToString()) //todo: this is duplicated in a Get structure - I think this one should go and make it a lot simpler and only the U in CRUD
                            {
                                //FunnySong thisObject = JsonSerializer.Deserialize<FunnySong>(serializedGivenJson);
                                DataShort1 thisRecord = SpecialDeserialize(serializedGivenJson, givenGenericPostInfo);
                                new MahDataMgr().DeleteById(thisRecord.ID, thisRecord.MetaFlag, thisRecord.MetaStruct, givenUzer);
                                rezult.responseCode = genericPostResponses.DeleteComplete;
                            }
                        }
                    }
                    catch( Exception ex)
                    {
                        rezult.responseCode = genericPostResponses.UnknownError;
                        rezult.responseString = ex.Message;
                    }
                }
            }
            return rezult;
        }

        private static DataShort1 SpecialDeserialize(string serializedGivenJson, genericPostInfo givenGenericPostInfo)
        {
            DataShort1 rezult = null;
            if (givenGenericPostInfo.EntityTag == "SILS1_SillySongs" ) 
            {
                FunnySong thisObject = JsonSerializer.Deserialize<FunnySong>(serializedGivenJson);
                rezult = thisObject.DOAExporter();
            }
            // This was designed to have a number of if... statements to  match the data models for JSON designed... Now I want to do it in JToken instead...

            return rezult;
        }

        public static List<GenericPostEntity> CompilePostEntities(string givenConfigString) 
        {
            List<GenericPostEntity> rezult = new List<GenericPostEntity>();
            if (givenConfigString is null) { return rezult; }
            var thesePostEntitys = MahCoadz.Util.Tuul.SplitMeGood(givenConfigString, '|');
            foreach (string thisPostEntity in thesePostEntitys)
            {
                var thisPostEntityValues = MahCoadz.Util.Tuul.SplitMeGood(thisPostEntity, ';');
                if (!genericPostHashes.ValidateEntity(thisPostEntityValues[0], thisPostEntityValues[1]))
                {
                    //default to noisy fail - since config should be perfect out of the box...
                    throw new Exception("Bad Data Entity configuration values..");
                }
                var thesePostTypes = MahCoadz.Util.Tuul.SplitMeGood(thisPostEntityValues[2], ',').ToList();
                foreach (string thisPostType in thesePostTypes)
                {
                    if (!genericPostHashes.ValidatePostType(thisPostType, "")) // the hashes not used for this at this time...
                    {
                        //default to noisy fail - since config should be perfect out of the box...
                        throw new Exception("Bad PostType configuration values..");
                    }
                }
                // this is happy path - we put this one in..
                rezult.Add(item: new GenericPostEntity() { EntityTag = thisPostEntityValues[0], PostTypes = thesePostTypes });
            }

            return rezult;
        }
    }

    public class GenericPostEntity
    {
        public string EntityTag { get; set; }
        public List<string> PostTypes { get; set; }  // just more logic to give flexibility in the future?
    }
      
    public enum genericPostResponses
    {
        ProcessStarted,
        UpdatedCompleted,
        UpdateFailed,
        UnknownGenericPostEntityType,
        UnknownPostType,
        UnknownError,
        DeleteComplete,
        AuthorizationDenied
    }

    public class genericPostInfo
    {
        public string EntityTag { get; set; }
        public string PostType { get; set; }
        public string PostDataJSON { get; set; }
        public string responseString { get; set; }
        public string UserToken { get; set; }
        public genericPostResponses responseCode { get; set; }
        public string responseCodeString { get { return responseCode.ToString(); } }
    }
    
    //public class KlientKeeRing
    //{
    //    public string Kee { get; set; }
    //    public string KeeSigned { get; set; }
    //    public string KeeRingStatus { get; set; }
    //    public const string KlientKeeRingOKStatus = "SIGNATURE_VERIFIED";
    //    public const string KlientKeeRingNewStatus = "SIGNATURE_CREATED";
    //}
}
