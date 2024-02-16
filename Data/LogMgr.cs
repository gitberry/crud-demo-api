using MahCoadz.Data.LOG;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahCoadz.Data
{
    // I deliberately chose to make the logging context separate 
    // pros - allows me to store the db on a separate db if I wish...
    // cons - adds complexity that I may regret (ie can't (easily) run SQL that involves the log AND other data)
    // 
    public class LogMgr : DbContext
    {
        private nbApi1Logger theLoggerContext;
        public LogMgr()
        {
            theLoggerContext = new nbApi1Logger();
        }

        public void Loggit(string givenInfo, string givenClientID = "", string givenIP = "", string givenURL = "",  string givenUserInfo = "")
        {
            var toLog = new nbAPI1_Log()
            {
                ClientID = givenClientID,
                IP = givenIP,
                URL = givenURL,
                Info = givenInfo,
                UserInfo = givenUserInfo,
                DateTimeUTC = DateTime.UtcNow
            };
            // design question: do we want a log failure to bjork the app?? or silent fail??
            // data hoarder q: 
            AddOrUpdateOne(toLog);
        }
        public bool AddOrUpdateOne(nbAPI1_Log givenRecord)
        {
            theLoggerContext.nbAPI1_Log.Add(givenRecord);
            if (givenRecord.ID == 0) { theLoggerContext.Entry(givenRecord).State = EntityState.Added; }
            else { theLoggerContext.Entry(givenRecord).State = EntityState.Modified; }
            theLoggerContext.SaveChanges();
            return true;
        }

        //public string read_check()
        //{
        //    string rezult = "start";
        //    //rezult = from theL
        //    var Rezult = (from z in theLoggerContext.nbAPI1_Log orderby z.ID descending select z).FirstOrDefault().Info.ToList();
        //    return rezult;
        //}
    }
}
