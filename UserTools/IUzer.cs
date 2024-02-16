using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahCoadz.Uzer
{
    public interface IUzer 
    {
        int    id             { get; set; } // internal PK
        string uzerId         { get; set; } // external key UNIQUE!
        string hashedPassword { get; set; }
        string hashseed       { get; set; }
        string firstName      { get; set; }
        string lastName       { get; set; }
        string name           { get; set; }
        bool   enabled        { get; set; }
        string accountType    { get; set; } // for directory browsing requests from non authenticated Front ends Go to cod in Util.Tool to understand use
        string accountMeta1   { get; set; } //  "  "
        string dirkeys        { get; set; } // for human accounts to state the various directory keys they posess..
        string postkeys       { get; set; } // for human accounts to state the various poststructure keys they possess..
        //note: I dislike the denormalized environment this is... should really be refactored..
    }

    public enum AccountTypes
    {
        Human,
        DirectoryInfo
    }
}
