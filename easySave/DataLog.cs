using System;
using System.Text;

namespace easySave
{
    class DataLog
    {
        public int FileNumber;      public int directoryNumber;
        public string Name;     
        public long size;
        public DateTime LastAccess;
        public string extention;




        /* DataLog for the File Case */

        public DataLog(int FileNumber, string Name, DateTime LastAccess, long size, string extention)  // Data Log constructor 
        {
            this.FileNumber = FileNumber;
            this.Name = Name;
            this.LastAccess = LastAccess;
            this.size = size;
            this.extention = extention;
        }



        /* DataLog for the Directory Case */

        public DataLog(int directoryNumber, string Name, DateTime LastAccess, long size)
        {
            this.directoryNumber = directoryNumber;
            this.Name = Name;
            this.LastAccess = LastAccess;
            this.size = size;
        }


    }
}
