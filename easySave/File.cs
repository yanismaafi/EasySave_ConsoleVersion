using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace easySave
{
    class File
    {

        public string getFileContent(string path)
        {
            string fileText = System.IO.File.ReadAllText(path);

            return fileText;
        }

 

        public bool findTaskName(string fileText, string taskName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileText);

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


        public string GetSpecificPath(string fileText, string taskName, string destinationPath)
        {
            string[] lines = System.IO.File.ReadAllLines(fileText);

            foreach (string line in lines)
            {
                var data = (JObject)JsonConvert.DeserializeObject(line);

                string name = data["name"].Value<string>();

                if (string.Compare(name, taskName) == 0)
                {
                    string path = data["destination"].Value<string>();

                    return path;
                }

            }

            return "nope";
        }

        internal static DateTime GetLastAccessTime(string file)
        {
            throw new NotImplementedException();
        }

        public bool checkExistence(string path)   // Verify if file exists or not
        {
            if (System.IO.File.Exists(path))
            {
                return true;
            }

            return false;
        }


    }
}
