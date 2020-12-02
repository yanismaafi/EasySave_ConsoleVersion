
using System;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace easySave
{
    class CopyFile
    {


        public string taskInformationFile = ConfigurationManager.AppSettings.Get("taskInformationFile");   // Task's Details File path
        public string LogPath = ConfigurationManager.AppSettings.Get("LogPath");    // Log File path



        public void completeCopy(string sourcePath, string destination, string taskName)
        {

            if (System.IO.Directory.Exists(sourcePath) == true)   // First check if source Path is Directory
            {
                if (System.IO.Directory.GetFiles(sourcePath).Length > 0)  // Check if the source Path contain files
                {
                    string[] sourceFiles = System.IO.Directory.GetFiles(sourcePath);    // Get all source Files
                    int nbrFiles = System.IO.Directory.GetFiles(sourcePath).Length;     // Get nbr of Files
                   
                    Directory dir = new Directory();
                    long sizeFiles = dir.getDirectorySize(sourcePath);                  // Get size of files
                    bool state = false;

                    int Progression = 100 * (1 - (nbrFiles / System.IO.Directory.GetFiles(sourcePath).Length)); //Calculate the progression
                    State stateInfo = new State(taskName, DateTime.Now, sourcePath, destination, nbrFiles, sizeFiles, Progression, state);
                    stateInfo.writeOnStateFile(stateInfo);

                    foreach (string file in sourceFiles)   // Loop on source Files (file by file)
                    {
                        File F = new File();
                        DataLog InformationFile = F.extractInformationFile(file, taskName);   //Extract file's information

                        string desFileName = System.IO.Path.GetFileName(file);
                        string destFile = System.IO.Path.Combine(destination, desFileName);


                        var timer = new Stopwatch();       // Calculate Copy Time 
                        timer.Start();

                        System.IO.File.Copy(file, destFile, true);   // Do copy 
                        state = true;

                        timer.Stop();

                        nbrFiles--;
                        sizeFiles = sizeFiles - (new System.IO.FileInfo(file).Length);



                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("File copied : " + file);
                        Thread.Sleep((int)timer.ElapsedMilliseconds);
                        Console.ForegroundColor = ConsoleColor.Black;

                        writeOnLogFile(InformationFile);   //Write on the Log file the informations

                        
                    }

                     Progression = 100 * (1 - (nbrFiles / System.IO.Directory.GetFiles(sourcePath).Length)); //Calculate the progression
                     stateInfo = new State(taskName, DateTime.Now, sourcePath, destination, nbrFiles, sizeFiles, Progression, state);
                     stateInfo.writeOnStateFile(stateInfo);

                }


                if (System.IO.Directory.GetDirectories(sourcePath).Length > 0)  // Check if source path contain Sub Directory
                {
                    string[] sourceDirectories = System.IO.Directory.GetDirectories(sourcePath);  //Get all source's Directories ( case if we have a sub directory)

                    foreach (string directory in sourceDirectories)
                    {
                        Directory dir = new Directory();
                        DataLog InformationDirectory = dir.extractInformationDirectory(directory, taskName);


                        DirectoryInfo dirInfo = new DirectoryInfo(directory);
                        string dirName = dirInfo.Name;   // Get name of directory

                        string destDirectory = System.IO.Path.Combine(destination, dirName);
                        System.IO.Directory.CreateDirectory(destDirectory);   // Create the directory 

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Directory copied :" + directory);
                        Console.ForegroundColor = ConsoleColor.Black;

                        writeOnLogFile(InformationDirectory);   // Write on the LogFile the Directory informations


                        completeCopy(directory, destDirectory, taskName);      // We call the method for recursivity sub sub ... directory and their files 
                    }

                }

            }
        }





        public void CopyAllTask()
        {
            File File = new File();

            if( File.checkExistence(taskInformationFile) )                  // Check  if the task's Information File exist
            {
                string[] lines = System.IO.File.ReadAllLines(taskInformationFile);               // Get task's Information File Content

                foreach (string line in lines )                                               // Loop line by line
                { 
                    var data = (JObject)JsonConvert.DeserializeObject(line);                  // Deserialize line 

                    string taskName = data["name"].Value<string>();
                    string source = data["source"].Value<string>();
                    string destination = data["destination"].Value<string>();                  // Get the value of source, destination and type
                    string type = data["type"].Value<string>();


                    if ( type == "Differential" )                                       // Check the type of save
                    {
                        differentialCopy(source, destination, taskName);
                    }else
                    {
                        completeCopy(source, destination, taskName);       // Do the Complete Copy
                    }
                }
            }else
            {
                Console.WriteLine("No task saved");
            }

        }
 
        


       public void differentialCopy(string sourcePath , string destinationPath, string taskName)
        {
            if( System.IO.Directory.Exists(sourcePath) == true )  // Check if source path is a Directory
            {
                if ( System.IO.Directory.GetFiles(sourcePath).Length > 0 )   // Check if source path contain files
                {
                    if ( System.IO.Directory.GetFiles(destinationPath).Length > 0)  // Check if destination path contain files
                    {
                        string[] sourcefiles = System.IO.Directory.GetFiles(sourcePath);    // Get all source files
                        string[] destinationfiles = System.IO.Directory.GetFiles(destinationPath); // Get all destination files

                        foreach (string sfile in sourcefiles)   
                        {
                            string sfileName = Path.GetFileName(sfile);     // Extract name and source file size 
                            long slength = new System.IO.FileInfo(sfile).Length;

                            bool find = false;

                            foreach (string dfile in destinationfiles)
                            {
                                string dfileName = Path.GetFileName(dfile);    // Extract name and destination file size
                                long dlength = new System.IO.FileInfo(dfile).Length;

                                if (string.Compare(sfileName, dfile) == 0 )    // Comparing
                                {
                                    find = true;       // We find one source file into destination

                                    if(slength != dlength)   // compare by sizing 
                                    {
                                        System.IO.File.Copy(sfile, dfile, true);   // Do the copy with true parametre for overwrite

                                        File F = new File();
                                        DataLog information = F.extractInformationFile(sfile, taskName);
                                        writeOnLogFile(information);             // Write on LogFile
                                    }
                                }
                            }

                            if(find == false)  // Case if we didn't find source file into destination we need to copy it 
                            {
                                string srcname = System.IO.Path.GetFileName(sfile);
                                string destination = System.IO.Path.Combine(destinationPath, srcname);

                                System.IO.File.Copy(sfile, destination, true);   

                                File F = new File();
                                DataLog information = F.extractInformationFile(sfile, taskName);   // Do the copy with true parametre for overwrite
                                writeOnLogFile(information);   // Write on LogFile
                            }

                        }
                    }
                    else       // Case when destination path doesn't contain files, we copy all source files to destination
                    {
                        string[] sourcefiles = System.IO.Directory.GetFiles(sourcePath);

                        foreach (string file in sourcefiles)
                        {
                            string srcname = System.IO.Path.GetFileName(file);
                            string destination = System.IO.Path.Combine(destinationPath, srcname);
                            System.IO.File.Copy(file, destination, true);

                            File F = new File();
                            DataLog information = F.extractInformationFile(file, taskName);   // Do the copy with true parametre for overwrite
                            writeOnLogFile(information);   // Write on LogFile
                        }
                    }
                }


                if ( System.IO.Directory.GetDirectories(sourcePath).Length > 0 )  // Check if the source Path contain sub directory 
                {
                    string[] sourceDirectories = System.IO.Directory.GetDirectories(sourcePath);

                    foreach(string srcdirectory in sourceDirectories)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(srcdirectory);
                        string dirName = dirInfo.Name;
                        string destinationDir = System.IO.Path.Combine(destinationPath, dirName);

                        differentialCopy(srcdirectory, destinationDir, taskName);  // Do the recursivity for files and sub directory 
                    }
                }
            }
        }




        public void specificCopy(string taskName)
        {
            File file = new File();

            if(System.IO.File.Exists(taskInformationFile) == true)
            {

                string task = file.getSpecificTask(taskName);

                var data = (JObject)JsonConvert.DeserializeObject(task);

                string type = data["type"].Value<string>();
                string source = data["source"].Value<string>();
                string destination = data["destination"].Value<string>();

                if (type == "Complete")
                {
                    completeCopy(source, destination, taskName);
                
                }else if (type == "Differential")
                {
                    differentialCopy(source, destination, taskName);
                }
            }
        }




        public void completeLog (string sourcePath,string taskName)  
        {

            if(System.IO.Directory.Exists(sourcePath) == true)   // First check if source Path is Directory
            {
                if (System.IO.Directory.GetFiles(sourcePath).Length > 0)  // Check if the source Path contain files
                {
                    string[] sourceFiles = System.IO.Directory.GetFiles(sourcePath);    // Get all source Files

                    foreach (string file in sourceFiles)   // Loop on source Files (file by file)
                    {
                        File F = new File();
                        DataLog InformationFile = F.extractInformationFile(file,taskName);   //Extract file's information

                        writeOnLogFile(InformationFile);   //Write on the Log file the informations
                    }
                }


                if (System.IO.Directory.GetDirectories(sourcePath).Length > 0)  // Check if source path contain Sub Directory
                {
                    string[] sourceDirectories = System.IO.Directory.GetDirectories(sourcePath);  //Get all source's Directories ( case if we have a sub directory)

                    foreach (string directory in sourceDirectories)
                    {
                        Directory dir = new Directory();

                        DataLog InformationDirectory = dir.extractInformationDirectory(directory, taskName);
                        writeOnLogFile(InformationDirectory);   // Write on the LogFile the Directory informations

                        completeLog(directory, taskName);      // We call the method for recursivity sub sub ... directory and their files 
                    }

                }

            }
        }





        public void writeOnLogFile(DataLog information)
        {
            File file = new File();
            string JsonDatalog = JsonConvert.SerializeObject(information); // Convert the File's information to Json


            if (file.checkExistence(LogPath) == false)           // Check if the Log file Exists
            {
                // Creating our File Log

                using (StreamWriter LogFile = System.IO.File.CreateText(LogPath))
                {
                    LogFile.WriteLine(JsonDatalog);                               // Put the Json Format infomration into Log file      
                }
            }
            else
            {
                // else if the File log exist , Opening our File Log and write on it.

                using (StreamWriter LogFile = System.IO.File.AppendText(LogPath))
                {
                    LogFile.WriteLine(JsonDatalog);                                 // Put the Json Format infomration into Log file           
                }
            }
        }







    }
}
