using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace easySave
{
    class File
    {

        public string taskInformationFile = "C:\\Users\\ASUS\\Desktop\\Task\\Task's_Details.json";


        public string getFileContent(string path)
        {
            string fileText = System.IO.File.ReadAllText(path);

            return fileText;
        }


        public bool checkExistence(string path)   // Verify if file exists or not
        {
            if (System.IO.File.Exists(path))
            {
                return true;
            }

            return false;
        }



        public bool findTaskName(string fileText, string taskName)
        {
            string[] lines = System.IO.File.ReadAllLines(taskInformationFile);

              foreach (string line in lines)
              {
                  var data = (JObject)JsonConvert.DeserializeObject(line);

                  string name = data["name"].Value<string>();

                  if (string.Compare(name, taskName) == 0)
                  {
                      return true;
                  }
              }

              return false;
        }



        public string getSourcePath(string taskInformtionFile, string taskName)
        {
            string[] lines = System.IO.File.ReadAllLines(taskInformationFile);

            foreach (string line in lines)
            {
                var data = (JObject)JsonConvert.DeserializeObject(line);

                string name = data["name"].Value<string>();

                if (string.Compare(name, taskName) == 0)
                {
                    string sourcePath = data["source"].Value<string>();

                    return sourcePath;
                }

            }

            return "nope";
        }


        public string getDestinationPath(string taskInformationFile, string taskName)
        {
            string[] lines = System.IO.File.ReadAllLines(taskInformationFile);

            foreach (string line in lines)
            {
                var data = (JObject)JsonConvert.DeserializeObject(line);

                string name = data["name"].Value<string>();

                if (string.Compare(name, taskName) == 0)
                {
                    string destinationPath = data["destination"].Value<string>();

                    return destinationPath;
                }

            }

            return "nope";
        }






    }
}
