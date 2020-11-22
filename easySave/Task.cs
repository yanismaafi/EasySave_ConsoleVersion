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

        public Task(string name, string source, string destination, string type)  //
        {
            this.name = name;
            this.source = source;
            this.destination = destination;
            this.type = type;
        }
    }


}
