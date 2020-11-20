﻿
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

        public void Copy(string sourcePath, string destPath, string Jobname)  
        {
            string fileName;
            string LogPath = "C:\\Users\\ASUS\\Desktop\\JOB\\LogFile.json";
            int nbrFile = 0;


            string[] filePaths = Directory.GetFiles(sourcePath);                 // List of all paths files existing into source

            using (StreamWriter LogFile = System.IO.File.CreateText(LogPath))   // Creating our File Log
            {
                foreach (string file in filePaths)                  // Loop on files path
                {
                        nbrFile++;

                        fileName = Path.GetFileName(file);                               
                        string destFile = Path.Combine(destPath, fileName);          // Extract all informations about files (size, name...)
                        long length = new FileInfo(file).Length;
                        DateTime lastAccess = File.GetLastAccessTime(file);

                        DataLog datalog = new DataLog(nbrFile,fileName,Jobname, lastAccess, length);   // Convert the File information to Json
                        string JsonDatalog = JsonConvert.SerializeObject(datalog);
                     
                        LogFile.WriteLine(JsonDatalog);                                                // Put the Json Format infomration into Log file
                      
                }      
            }

            var timer = new Stopwatch();       // Calculate copy time 
            timer.Start();

            FileSystem.CopyDirectory(sourcePath, destPath, UIOption.AllDialogs);           // Copy all source files to destination

            timer.Stop();
            

            for (int i = 0; i <= timer.ElapsedMilliseconds; i++)
            {
                Console.Write($"\n PROGRESS : {i} %   ");       // Progress Bar
                Thread.Sleep(2);
            }


        }

    }
}
