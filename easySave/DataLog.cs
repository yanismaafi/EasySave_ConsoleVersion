using System;
using System.Collections.Generic;
using System.Text;

namespace easySave
{
    class DataLog
    {
        public int FileNumber;
        public string FileName;
        public long size;
        public DateTime LastAccess;
        public string extention;

        public DataLog(int FileNumber, string FileName, DateTime LastAccess, long size, string extention)  // Data Log constructor 
        {
            this.FileNumber = FileNumber;
            this.FileName = FileName;
            this.LastAccess = LastAccess;
            this.size = size;
            this.extention = extention;

        }


    }
}
