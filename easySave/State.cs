using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace easySave
{
    class State
    {

        public string taskName;
        public string source;
        public string destination;
        public bool state;
        public int progress;
        public int FilesToCopy;
        public long Files_size;
        public DateTime Time;

        public State (string taskName, DateTime Time, string source, string destination, int FilesToCopy, long Files_size, int progress, bool state )
        {
            this.taskName = taskName;
            this.Time = Time;
            this.source = source;
            this.destination = destination;
            this.FilesToCopy = FilesToCopy;
            this.Files_size = Files_size;
            this.progress = progress;
            this.state = state;
        }


        public void writeOnStateFile( State information )
        {
            string StatePath = ConfigurationManager.AppSettings.Get("State");    // State File path

            string JsonData = JsonConvert.SerializeObject(information);  // Convert the state information to Json Format

            if (System.IO.File.Exists(StatePath) == false)   
            {

                using (StreamWriter StateFile = System.IO.File.CreateText(StatePath))   // if State file doesn't exist, create the state file
                {
                    StateFile.WriteLine(JsonData);                               // Put the Json Format infomration into State file      
                }
            }else
            {
                // else if the StateFile exist , Opening our File and write on it.

                using (StreamWriter StateFile = System.IO.File.AppendText(StatePath))
                {
                    StateFile.WriteLine(JsonData);                                 // Put the Json Format infomration into State file           
                }
            }
        }

    }
}
