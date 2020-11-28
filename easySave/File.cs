﻿using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace easySave
{
    class File
    {
        public static int nbrFile;
        public string taskInformationFile = "C:\\EasySave\\Task's_Details.json";   // Task's Details File path
        public string LogPath = "C:\\EasySave\\LogFile.json";     // Log File path




        public DataLog extractInformationFile(string file)
        {
            nbrFile++;

            string fileName = Path.GetFileName(file);          // Extract all informations about file (size, name, extention ...)
            string extention = Path.GetExtension(file);
            long length = new System.IO.FileInfo(file).Length;
            DateTime lastAccess = System.IO.File.GetLastAccessTime(file);

            DataLog InformationFile = new DataLog(nbrFile, fileName, lastAccess, length, extention);

            return InformationFile;
        }




        public bool checkExistence(string path)   // Verify if file exists or not
        {
            if (System.IO.File.Exists(path))
            {
                return true;
            }

            return false;
        }

        
  


        public string getSpecificTask(string taskName)
        {
            string[] informationTask = System.IO.File.ReadAllLines(taskInformationFile);

            foreach(string line in informationTask)
            {
                var data = (JObject)JsonConvert.DeserializeObject(line);
                string name = data["name"].Value<string>();

                if (string.Compare(taskName, name) == 0)
                {
                    return line;
                }
            }

            return null;
        }


   

        public bool checkExistenceDirectory(string sourceDirectory, string destination)          //check if directory exist into destination directory
        {
            string[] directories = System.IO.Directory.GetDirectories(destination);

            DirectoryInfo dirInfo = new DirectoryInfo(sourceDirectory);
            string dirname = dirInfo.Name;

            foreach (string directory in directories)
            {
                DirectoryInfo dirinfo = new DirectoryInfo(directory);
                string name = dirinfo.Name;

                if ( string.Compare(name, dirname) == 0 )
                {
                    return true;
                }
            }
            return false;
        }


        public bool findTaskName(string taskName)     // Check if a Task Name entred by user exist in taskInformationFile
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

           return null;
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

            return null;
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
