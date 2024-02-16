using MahCoadz.Data.DAL;
using MahCoadz.Uzer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahCoadz.Data
{
    public class MahDataMgr : DbContext
    {
        private nbApiEntities theDbContext;
        public MahDataMgr()
        { 
            theDbContext = new nbApiEntities();
        }

        public DataShort1 RetrieveById(int givenID, string givenMetaFlag, string givenMetaStruct)
        {
            var Rezult = (from z in theDbContext.DataShort1 where z.MetaFlag == givenMetaFlag && z.MetaStruct == givenMetaStruct && z.ID == givenID select z).ToList();
            if (Rezult.Count == 1) return Rezult[0];
            return null;
        }

        public List<DataShort1> RetrieveAll(string givenMetaFlag, string givenMetaStruct)
        {
            var Rezult = (from z in theDbContext.DataShort1 where z.IsActive && z.MetaFlag == givenMetaFlag && z.MetaStruct == givenMetaStruct select z).ToList();
            return Rezult;
        }

        public List<DataShort1> RetrieveXrecords(string givenMetaFlag, string givenMetaStruct, int givenTopX, bool RandomQuery)
        {
            //idea for next iteration? extend to have error codes - return errors instead of throwing system errors...(or build objects out of catches upstream..)
            List<DataShort1> rezult = null;
            if (RandomQuery)
            {
                rezult = (from z in theDbContext.DataShort1 where z.IsActive && z.MetaFlag == givenMetaFlag && z.MetaStruct == givenMetaStruct select z).OrderBy(yy => Guid.NewGuid()).Take(givenTopX).ToList();
            }
            else
            {
                rezult = (from z in theDbContext.DataShort1 where z.IsActive && z.MetaFlag == givenMetaFlag && z.MetaStruct == givenMetaStruct select z).Take(givenTopX).ToList();                
            }
            return rezult;
        }

        public bool AddOrUpdateOne(DataShort1 givenRecord, IUzer givenUzer, DateTime? stompStamp = null) 
        {
            givenRecord.MetaEdited = (stompStamp is null) ? DateTime.UtcNow : (DateTime)stompStamp;
            givenRecord.MetaEditedBy = givenUzer.uzerId;
            theDbContext.DataShort1.Add(givenRecord);
            if (givenRecord.ID == 0) { theDbContext.Entry(givenRecord).State = EntityState.Added; }
            else { theDbContext.Entry(givenRecord).State = EntityState.Modified; }            
            theDbContext.SaveChanges();
            return true;
        }

        public DataShort1 DeleteById(int id, string givenTag, string givenStruct, IUzer givenUzer, bool Undelete = false )
        {
            //retrive by tag and struct - if exists - then we "delete" it...
            try
            {
                DataShort1 thisRecord = RetrieveById(id, givenTag, givenStruct);
                if (!(thisRecord is null))
                {
                    if (thisRecord.IsActive != Undelete)
                    {
                        thisRecord.IsActive = Undelete;
                        if (Undelete) { thisRecord.IsActive = true; }
                        AddOrUpdateOne(thisRecord, givenUzer);
                    }
                }
                return thisRecord;
            }
            catch
            {
                return null;
            }
        }

        public DataShort1 CreateByTagStruct(string givenTag, string givenStruct, IUzer givenUzer)
        {
            try
            {
                DataShort1 thisRecord = new DataShort1(); 
                if (!(thisRecord is null))
                {
                    thisRecord.MetaFlag = givenTag;
                    thisRecord.MetaStruct = givenStruct;
                    thisRecord.MetaCreated = DateTime.UtcNow;
                    thisRecord.MetaCreatedBy = "123"; // givenUzer.uzerId;
                    AddOrUpdateOne(thisRecord, givenUzer, thisRecord.MetaCreated);
                }
                return thisRecord;
            }
            catch (Exception ex)
            {
                var mes = ex.Message;
                return null;
            }
        }
    }
}
