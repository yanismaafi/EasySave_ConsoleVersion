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
        public string belongsJob;

        public DataLog(int FileNumber, string FileName, string belongsJob, DateTime LastAccess, long size)  //
        {
            this.FileNumber = FileNumber;
            this.FileName = FileName;
            this.belongsJob = belongsJob;
            this.LastAccess = LastAccess;
            this.size = size;

        }


    }
}
