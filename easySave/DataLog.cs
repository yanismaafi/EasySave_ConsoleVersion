using System;
using System.Text;

namespace easySave
{
    class DataLog
    {
        public string TaskName; 
        public string Name;     
        public long size;
        public DateTime LastAccess;
        public string extention;




        /* DataLog for the File Case */

        public DataLog (string TaskName, string Name, DateTime LastAccess, long size, string extention)  // Data Log constructor 
        {
            this.TaskName = TaskName;
            this.Name = Name;
            this.LastAccess = LastAccess;
            this.size = size;
            this.extention = extention;
        }



        /* DataLog for the Directory Case */

        public DataLog (string TaskName, string Name, DateTime LastAccess, long size)
        {
            this.TaskName = TaskName;
            this.Name = Name;
            this.LastAccess = LastAccess;
            this.size = size;
        }


    }
}
