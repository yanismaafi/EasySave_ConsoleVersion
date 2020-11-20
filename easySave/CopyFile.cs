
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


        public void Copy(string sourcePath, string destPath, string taskname)  
        {
            string fileName;
            string LogPath = "C:\\Users\\ASUS\\Desktop\\Task\\LogFile.json";     // Log File path
            

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
            

            for (int i = 0; i <= timer.ElapsedMilliseconds; i++)
            {
                Console.Write($"\r PROGRESS : {i} %  ");       // Progress Bar
                Thread.Sleep(2);
            }


        }

    }
}
