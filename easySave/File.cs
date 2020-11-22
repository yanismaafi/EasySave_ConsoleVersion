using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace easySave
{
    class File
    {

        public string taskInformationFile = "C:\\Users\\ASUS\\Desktop\\Task\\Task's_Details.json";   // Task's Details File path
        public string LogPath = "C:\\Users\\ASUS\\Desktop\\Task\\LogFile.json";     // Log File path


        public string getFileContent(string path)
        {
            string fileText = System.IO.File.ReadAllText(path);

            return fileText;
        }



        public bool isDifferential (string DetailFilePath)
        {
            string[] linesDetailFile = System.IO.File.ReadAllLines(DetailFilePath);

            string type;

            foreach (string line in linesDetailFile)
            {
                var data = (JObject)JsonConvert.DeserializeObject(line);

                type = data["type"].Value<string>();

                if(type == "Differential")
                {
                    return true;
                }
            }

            return false;
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

            return "Error";
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

            return "Error";
        }




        public bool findChangedFile(string sourceFile, string destinationPath)    
        {

            string[] DestfilePaths = Directory.GetFiles(destinationPath);         // List of all paths files existing into destination

            string sourceFileName;  long SourceFilelength;
            string destFileName;    long DestFilelength;


                sourceFileName = Path.GetFileName(sourceFile);               
                SourceFilelength = new System.IO.FileInfo(sourceFile).Length;

                foreach (string destfile in DestfilePaths)
                {
                     destFileName = Path.GetFileName(destfile);

                     if( string.Compare(sourceFileName, destFileName) == 0 )
                     {
                        DestFilelength = new System.IO.FileInfo(destfile).Length;

                         if(SourceFilelength != DestFilelength)
                         {
                             return true;
                         }
                     }
                }

            return false;
        }

    }
}
