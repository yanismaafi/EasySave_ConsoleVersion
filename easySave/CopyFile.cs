
using System;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace easySave
{
    class CopyFile
    {

        public static int nbrFile;
        public string LogPath = "C:\\Users\\ASUS\\Desktop\\Task\\LogFile.json";     // Log File path
        public string taskInformationFile = "C:\\Users\\ASUS\\Desktop\\Task\\Task's_Details.json";





        public void Copy(string sourcePath, string destPath, string taskname)  
        {
            string fileName;

            string[] filePaths = Directory.GetFiles(sourcePath);                 // List of all paths files existing into source

            File FILE = new File();

            if (FILE.checkExistence(LogPath) != true)
            {
                      // Creating our File Log

                    using (StreamWriter LogFile = System.IO.File.CreateText(LogPath))   
                    {
                        foreach (string file in filePaths)                  // Loop on files path
                        {
                            nbrFile++;

                            fileName = Path.GetFileName(file);
                            string destFile = Path.Combine(destPath, fileName);          // Extract all informations about files (size, name, extention ...)
                            string extention = Path.GetExtension(file);
                            long length = new System.IO.FileInfo(file).Length;
                            DateTime lastAccess = System.IO.File.GetLastAccessTime(file);

                            DataLog datalog = new DataLog(nbrFile, fileName, taskname, lastAccess, length, extention);   // Convert the File information to Json
                            string JsonDatalog = JsonConvert.SerializeObject(datalog);

                            LogFile.WriteLine(JsonDatalog);                                                // Put the Json Format infomration into Log file      
                        }
                    }
          
            }else
            {
                   // else if the File log exist , Opening our File Log

                using ( StreamWriter LogFile = System.IO.File.AppendText(LogPath))
                {

                    foreach (string file in filePaths)                  // Loop on files path
                    {
                        nbrFile++;

                        fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(destPath, fileName);          // Extract all informations about files (size, name...)
                        string extention = Path.GetExtension(file);
                        long length = new System.IO.FileInfo(file).Length;
                        DateTime lastAccess = System.IO.File.GetLastAccessTime(file);

                        DataLog datalog = new DataLog(nbrFile, fileName, taskname, lastAccess, length, extention);   // Convert the File information to Json
                        string JsonDatalog = JsonConvert.SerializeObject(datalog);

                        LogFile.WriteLine(JsonDatalog);                                                // Put the Json Format infomration into Log file      
                    }

                }
            }


            var timer = new Stopwatch();       // Calculate copy time 
            timer.Start();

            FileSystem.CopyDirectory(sourcePath, destPath, UIOption.AllDialogs);           // Copy all source files to destination

            timer.Stop();
            

            for (int i = 0; i <= ( timer.ElapsedMilliseconds * 0.001 ); i++)
            {
                Console.Write($"\r PROGRESS : {i} %  ");       // Progress Bar
                Thread.Sleep(5);
            }

        }




        public void copyChangedFile(string sourcePath, string destinationPath)
        {
            string[] SourcefilesPath = Directory.GetFiles(sourcePath);                // List of all paths files existing into source

            File file = new File();

            foreach (string sourceFile in SourcefilesPath)
            {
                string SourcefileName = Path.GetFileName(sourceFile);

                if (file.findChangedFile(sourceFile, destinationPath) == true)
                {
                    string destination = System.IO.Path.Combine(destinationPath, SourcefileName);

                    var timer = new Stopwatch();       // Calculate copy time 
                    timer.Start();

                    System.IO.File.Copy(sourceFile, destination, true);    //Do the copy

                    timer.Stop();

                    for (int i = 0; i <= (timer.ElapsedMilliseconds * 0.001 ); i++)
                    {
                        Console.Write($"\r PROGRESS : {i} %  ");       // Progress Bar
                        Thread.Sleep(5);
                    }

                    if (file.checkExistence(LogPath) != true)
                    {
                        // Creating our File Log

                        using (StreamWriter LogFile = System.IO.File.CreateText(LogPath))
                        {

                            nbrFile++;

                            string fileName = Path.GetFileName(sourceFile);
                            string destFile = Path.Combine(destinationPath, fileName);          // Extract all informations about files (size, name, extention ...)
                            string extention = Path.GetExtension(sourceFile);
                            long length = new System.IO.FileInfo(sourceFile).Length;
                            DateTime lastAccess = System.IO.File.GetLastAccessTime(sourceFile);

                            DataLog datalog = new DataLog(nbrFile, fileName, "task", lastAccess, length, extention);   // Convert the File information to Json
                            string JsonDatalog = JsonConvert.SerializeObject(datalog);

                            LogFile.WriteLine(JsonDatalog);

                        }

                    }else
                    {
                        // else if the File log exist , Opening our File Log

                        using (StreamWriter LogFile = System.IO.File.AppendText(LogPath))
                        {
                                nbrFile++;

                                string fileName = Path.GetFileName(sourceFile);
                                string destFile = Path.Combine(destinationPath, fileName);          // Extract all informations about files (size, name, extention ...)
                                string extention = Path.GetExtension(sourceFile);
                                long length = new System.IO.FileInfo(sourceFile).Length;
                                DateTime lastAccess = System.IO.File.GetLastAccessTime(sourceFile);

                                DataLog datalog = new DataLog(nbrFile, fileName, "task", lastAccess, length, extention);   // Convert the File information to Json
                                string JsonDatalog = JsonConvert.SerializeObject(datalog);

                                LogFile.WriteLine(JsonDatalog);                                                // Put the Json Format infomration into Log file       

                        }
                    }
                }
            }
        }




        public void CopyAllTasks()
        {
            string[] linesTaskInnformation = System.IO.File.ReadAllLines(taskInformationFile);
            string type;
            string task_sourcePath;
            string task_targetPath;
            string taskname;

            foreach (string line in linesTaskInnformation)
            {
                var data = (JObject)JsonConvert.DeserializeObject(line);

                type = data["type"].Value<string>();
                task_sourcePath = data["source"].Value<string>();
                task_targetPath = data["destination"].Value<string>();
                taskname = data["name"].Value<string>();

                CopyFile File = new CopyFile();

                if (type == "Differential")
                {
                    var timer = new Stopwatch();       // Calculate copy time 
                    timer.Start();

                    File.copyChangedFile(task_sourcePath, task_targetPath);     // Do the copy

                    timer.Stop();

                    for (int i = 0; i <= (timer.ElapsedMilliseconds * 0.001); i++)
                    {
                        Console.Write($"\r PROGRESS : {i} %  ");       // Progress Bar
                        Thread.Sleep(5);
                    }

                }
                else
                {
                    var timer = new Stopwatch();       // Calculate copy time 
                    timer.Start();

                    Copy(task_sourcePath, task_targetPath, taskname);    //Do the copy

                    timer.Stop();

                    for (int i = 0; i <= (timer.ElapsedMilliseconds * 0.001) ; i++)
                    {
                        Console.Write($"\r PROGRESS : {i} %  ");       // Progress Bar
                        Thread.Sleep(5);
                    }
                }
            }
        }




    }
}
