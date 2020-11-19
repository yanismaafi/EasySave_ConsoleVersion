
using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;




namespace easySave
{
    class CopyFile
    {

        public void Copy(string sourcePath, string destPath, string Jobname)  
        {
            string fileName;
            string LogPath = "C:\\Users\\ASUS\\Desktop\\JOB\\LogFile.json";
            int nbrFile = 0;


            string[] filePaths = Directory.GetFiles(sourcePath);     // List of all paths files existing into source

            using (StreamWriter LogFile = System.IO.File.CreateText(LogPath))   // Creating our File Log
            {
                foreach (string file in filePaths)                  // Loop on files path
                {
                        nbrFile++;

                        fileName = Path.GetFileName(file);                               
                        string destFile = Path.Combine(destPath, fileName);          // Extract all informations about files (size, name...)
                        long length = new FileInfo(file).Length;
                        DateTime lastAccess = File.GetLastAccessTime(file);

                        DataLog datalog = new DataLog(nbrFile,fileName,Jobname, lastAccess, length);   // Convert the File's information to Json
                        string JsonDatalog = JsonConvert.SerializeObject(datalog);
                     
                        LogFile.WriteLine(JsonDatalog);                                                // Put the Json Format infomration into Log file
                        LogFile.WriteLine("\n-----------------------------------------\n");
                }      
            }

            FileSystem.CopyDirectory(sourcePath, destPath, UIOption.AllDialogs);           // Copy all source files to destination
        }



        public bool checkExistence(string path)   // Verify if file exists or not
        {
            if (File.Exists(path))
            {
                return true;
            }

            return false;
        }


    }
}
