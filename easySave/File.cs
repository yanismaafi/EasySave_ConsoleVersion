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



        public bool isDifferential(string FilePath)
        {
            string[] linesDetailFile = System.IO.File.ReadAllLines(taskInformationFile);

            string path;
            string type;

            foreach (string line in linesDetailFile)
            {
                var data = (JObject)JsonConvert.DeserializeObject(line);

                path = data["source"].Value<string>();
                type = data["type"].Value<string>();

                if ( string.Compare(path, FilePath) == 0 && type == "Differential")
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


        public bool ExistenceIntoDestination(string fileName ,string destinationPath)    // Verify if file exists into destination
        {
            string file = System.IO.Path.Combine(destinationPath, fileName);

            if (System.IO.File.Exists(file))
            {
                return true;
            }

            return false;
        }


        public bool taskNameExistence(string taskName)           // Check if a Task Name entred by user exist in taskInformationFile
        {
            if( checkExistence(taskInformationFile) == true )
            {
                string[] informationTask = System.IO.File.ReadAllLines(taskInformationFile);

                foreach(string line in informationTask)
                {
                    var data = (JObject)JsonConvert.DeserializeObject(line);
                    string name = data["name"].Value<string>();

                    if( string.Compare(taskName, name) ==0 )
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool verifyLength (string fileName, long fileLength ,string destinationPath)    // 
        {
            string[] destinationFiles = System.IO.Directory.GetFiles(destinationPath);

            foreach (string file in destinationFiles)
            {
                string FName = Path.GetFileName(file);

                if (string.Compare(fileName, FName) == 0)
                {
                    long length = new System.IO.FileInfo(file).Length;

                    if (fileLength == length)
                    {
                        return true;
                    }
                }
            }
          return false;
        }


        public bool findTaskName(string taskName)
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



        public string getSourcePath(string taskName)
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



        public void ShowTasksName()           // List all saved Tasks
        {

            if (checkExistence(taskInformationFile) == false)   // verify if taskInformationFile was created or not
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n No task has been saved");
                Console.ForegroundColor = ConsoleColor.White;
            }else
            {
                string[] lines = System.IO.File.ReadAllLines(taskInformationFile);

                foreach (string line in lines)
                {
                    var data = (JObject)JsonConvert.DeserializeObject(line);

                    string TaskName = data["name"].Value<string>();                     // Get the name of the task

                    Console.WriteLine("- Task : " + TaskName );
                }

                Console.WriteLine("\n--------------------------------------\n");
            }

        }



        public void ShowAllTasks()           // List all saved Tasks
        {

            if (checkExistence(taskInformationFile) == false)   // verify if taskInformationFile was created or not
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n No task has been saved");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                string[] lines = System.IO.File.ReadAllLines(taskInformationFile);     

                foreach (string line in lines)
                {
                    var data = (JObject)JsonConvert.DeserializeObject(line);

                    string TaskName = data["name"].Value<string>();
                    string source = data["source"].Value<string>();
                    string destination = data["destination"].Value<string>();
                    string type = data["type"].Value<string>();
                    string date = data["date"].Value<string>();

                    Console.WriteLine("\n-------------------------------------------------------------------------\n");
                    Console.WriteLine("Name : " + TaskName + "\nSource : " + source + "\nDestination : " + destination + "\nType : " + type + "\nCreated at : " + date);
                }

                Console.WriteLine("\n-------------------------------------------------------------------------\n");

            }

        }




    }
}
