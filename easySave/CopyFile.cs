
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
        public string LogPath = "C:\\Users\\ASUS\\Desktop\\Task\\LogFile.json";       // Log File path
        public string taskInformationFile = "C:\\Users\\ASUS\\Desktop\\Task\\Task's_Details.json";   // Log Task's details File





        public void Copy(string sourcePath, string destPath)  
        { 

            var timer = new Stopwatch();       // Calculate copy time 
            timer.Start();

            FileSystem.CopyDirectory(sourcePath, destPath, UIOption.AllDialogs);           // Copy all source files to destination
            LogFile(sourcePath, destPath);

            timer.Stop();
            

            for (int i = 0; i <= ( timer.ElapsedMilliseconds * 0.001 ); i++)
            {
                Console.Write($"\r PROGRESS : {i} %  ");       // Progress Bar
                Thread.Sleep(5);
            }

        }



        
        public void CopyAllTask()
        {
            File File = new File();

            if( File.checkExistence(taskInformationFile) )
            {
                string[] lines = System.IO.File.ReadAllLines(taskInformationFile);

                foreach (string line in lines )
                {
                    var data = (JObject)JsonConvert.DeserializeObject(line);

                    string source = data["source"].Value<string>();
                    string destination = data["destination"].Value<string>();


                    if ( File.isDifferential(source) == true)
                    {
                            string[] Files = System.IO.Directory.GetFiles(source);

                            foreach(string file in Files)
                            {
                                string fileName = Path.GetFileName(file);
                                Console.WriteLine(fileName);
                                long length = new System.IO.FileInfo(file).Length;

                                string dest = Path.Combine(destination, fileName);

                                    if (File.ExistenceIntoDestination(fileName, destination) == true)
                                    {
                                        if (File.verifyLength(fileName, length, destination) != true)
                                        {
                                            System.IO.File.Copy(file, dest, true);
                                        }
                                    }else
                                    {
                                        System.IO.File.Copy(file, dest, true);
                                    }

                                 LogFile(file, dest);
                            }
                    }else
                    {
                        Copy(source, destination);    
                    }
                }
            }else
            {
                Console.WriteLine("No task saved");
            }

        }




        public void LogFile(string sourcePath, string destinationPath)
        {
            nbrFile++;

            string fileName = Path.GetFileName(sourcePath);          // Extract all informations about sourceFile (size, name, extention ...)
            string extention = Path.GetExtension(sourcePath);
            long length = new System.IO.FileInfo(sourcePath).Length;
            DateTime lastAccess = System.IO.File.GetLastAccessTime(sourcePath);

            DataLog datalog = new DataLog(nbrFile, fileName, lastAccess, length, extention);   // Convert the File information to Json
            string JsonDatalog = JsonConvert.SerializeObject(datalog);


            File file = new File();

            if (file.checkExistence(LogPath) == false)           //Check if the Log file Exists
            {
                    // Creating our File Log

                    using (StreamWriter LogFile = System.IO.File.CreateText(LogPath))
                    {
                        LogFile.WriteLine(JsonDatalog);                                                // Put the Json Format infomration into Log file      
                    }
            }else
            {
                    // else if the File log exist , Opening our File Log

                    using (StreamWriter LogFile = System.IO.File.AppendText(LogPath))
                    {
                        LogFile.WriteLine(JsonDatalog);                                                // Put the Json Format infomration into Log file           
                    }
            }

        }




    }
}
