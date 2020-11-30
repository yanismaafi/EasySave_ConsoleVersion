using System;
using System.Collections.Generic;
using System.Text;

namespace easySave
{
    class Task
    {

       public string name;
       public string source;
       public string destination;
       public string type;
       public DateTime created_at;

        public Task (string name, string source, string destination, string type , DateTime Now)
        {
            this.name = name;
            this.source = source;
            this.destination = destination;
            this.type = type;
            this.created_at = Now;
        }
    }


}
