using MahCoadz.Data;
using MahCoadz.Data.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahCoadz.Data.FunnySongNS
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FunnySong 
    {
        public static string myMetaFlag = "SILS1";
        public static string myMetaStruct = "SillySongs";
        public    int   Id                     { get; set; } 
        public string   Title                  { get; set; } 
        public string   SubTitle               { get; set; } 
        public string   Composer               { get; set; } 
        public string   ComposerPickerID       { get; set; } 
        public string   Lyricist               { get; set; } 
        public string   Arranger               { get; set; } 
        public string   Publisher              { get; set; } 
        public string   CopyrightNumber        { get; set; } 
        public string   UserPermissionIfNotFGS { get; set; } 
        public string   PricePerCopy           { get; set; } 
        public string   PricePerCopyAsOf       { get; set; } 
        public string   Style                  { get; set; } 
        public string   Instrumentation        { get; set; } 
        public string   Level                  { get; set; } 
        public   bool   IsActive               { get; set; }
        public string   ModifiedBy             { get; set; } 
        public string   ModifiedByUserId       { get; set; } 
        public DateTime ModifiedOn             { get; set; } 
        public string   CreatedBy              { get; set; } 
        public string   CreatedById            { get; set; } 
        public DateTime CreatedOn              { get; set; }           
        public string   ImportedOn             { get; set; }
        public int      SomeRandomValue        { get; set; }
        public DateTime APIProvided            { get; set; }           
        
        public FunnySong() { }
        public FunnySong(DataShort1 givenData) 
        {
            //many ways to do this...
            if (!(givenData is null))
            {
                DOAImporter(givenData);
            }
            else
            {
                throw new Exception("Error Creating object [FunnySong]");
            }
        }

        //lotsa ways to do - brute forcing it...
        public FunnySong DOAImporter(DataShort1 givenData) 
        {
            this.Id         = givenData.ID;
            this.Title      = givenData.StringIdx100A;
            this.Composer   = givenData.StringIdx50B;
            this.Publisher  = givenData.StringIdx50C;
            this.Style      = givenData.StringIdx50D;
            this.IsActive   = givenData.IsActive;
            this.CreatedBy  = givenData.MetaCreatedBy;
            this.ModifiedBy = givenData.MetaEditedBy;
            this.CreatedOn  = givenData.MetaCreated;
            this.ModifiedOn = givenData.MetaEdited;
            return this;
        }

        public DataShort1 DOAExporter() 
        {
            DataShort1 rezult    = new DataShort1();
            rezult.MetaFlag      = myMetaFlag;
            rezult.MetaStruct    = myMetaStruct;
            rezult.ID            = this.Id;
            rezult.StringIdx100A = thisDefaultPopulate("",this.Title);
            rezult.StringIdx50B  = thisDefaultPopulate("",this.Composer);
            rezult.StringIdx50C  = thisDefaultPopulate("",this.Publisher);
            rezult.StringIdx50D  = thisDefaultPopulate("",this.Style);
            rezult.IsActive      = this.IsActive;
            rezult.MetaCreatedBy = this.CreatedBy;
            rezult.MetaEditedBy  = this.ModifiedBy;
            rezult.MetaCreated   = this.CreatedOn;
            rezult.MetaEdited    = this.ModifiedOn;
            return rezult;
        }

        private string thisDefaultPopulate(string givenDefault, string givenDataObject)
        {
            return (givenDataObject is null) ? givenDefault : givenDataObject;
        }

        public static List<FunnySong> FunnySongs(List<DataShort1> givenData)
        {
            List<FunnySong> rezult = new List<FunnySong>();
            foreach (DataShort1 thisRecord in givenData)
            {
                rezult.Add(new FunnySong(thisRecord));
            }
            return rezult;
        }
    }
}
