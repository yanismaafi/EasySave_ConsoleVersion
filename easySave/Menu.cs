using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;



namespace easySave
{
    class Menu
    {

        public void MenuConsole()
        {

                int options = 0;
                int typeOfJob = 0;

                string job_name = string.Empty;
                string job_sourcePath = string.Empty; ;
                string job_targetPath = string.Empty; ;
                string job_type = string.Empty;

                string taskInformationFile = "C:\\Users\\ASUS\\Desktop\\Task\\Task's_Details.json";

            do
                {
                
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("\t\t\t _______  _______  _______  __   __    _______  _______  __   __  _______");
                    Console.WriteLine("\t\t\t|       ||   _   ||       ||  | |  |  |       ||   _   ||  | |  ||       |");
                    Console.WriteLine("\t\t\t|    ___||  |_|  ||  _____||  |_|  |  |  _____||  |_|  ||  |_|  ||    ___|");
                    Console.WriteLine("\t\t\t|   |___ |       || |_____ |       |  | |_____ |       ||       ||   |___");
                    Console.WriteLine("\t\t\t|    ___||       ||_____  ||_     _|  |_____  ||       ||       ||    ___|");
                    Console.WriteLine("\t\t\t|   |___ |   _   | _____| |  |   |     _____| ||   _   | |     | |   |___");
                    Console.WriteLine("\t\t\t|_______||__| |__||_______|  |___|    |_______||__| |__|  |___|  |_______| \n");

                    Console.WriteLine("\n\t\t\t\t\t\t Welcome to EasySave !  \n");

                    Console.ResetColor();

                    Console.WriteLine("\n Please Select an Option : \n");

                    Console.WriteLine("\n 1- Create a task \n");
                    Console.WriteLine("\n 2- Execute a specific Task \n");
                    Console.WriteLine("\n 3- Execute all Tasks \n");
                    Console.WriteLine("\n 4- Exit \n");

                    options = Convert.ToInt32(Console.ReadLine());


                    switch (options)
                    {
                        case 1:

                            Console.WriteLine("\n Create a new task ! \n");
                            Console.Write("\n give us a name for your task : ");
                            job_name = Console.ReadLine();

                            Console.Write("\n give us the source path of the directory you wanna copy :  ");
                            job_sourcePath = Console.ReadLine();

                            Console.Write("\n give us the destination path :  ");
                            job_targetPath = Console.ReadLine();

                            Console.WriteLine("\n give the type of the task \n<1>: Complete. \n<2>: Differential : ");
                           
                            do
                            {
                                typeOfJob = Convert.ToInt32(Console.ReadLine());

                                if (typeOfJob == 1)
                                {
                                    job_type = "complete";
                                    break;
                                }
                                else if (typeOfJob == 2)
                                {
                                    job_type = "differential";
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\n Error invalid task type ! \n ");
                                }

                            } while (typeOfJob != 1 || typeOfJob != 2);

                             Console.WriteLine("\n task saved ! \n");
                             Console.WriteLine($"\n name : {job_name} \n \n source Path : {job_sourcePath}  \n \n destination path : {job_targetPath} \n \n task type : {job_type} \n ");

                             Json convert = new Json();       //Convert information's job to json format 
                             string informationsJob = convert.ConvertToJson(job_name, job_sourcePath, job_targetPath, job_type);
                             convert.CreateFileJson(informationsJob);  // Put json infromation into file 
                           
                             

                        break;

                        case 2:

                            Console.WriteLine("\n Execute a specific Task ! \n ");

                            Console.WriteLine("\n Name of the task tou want to execute :");
                            job_name = Console.ReadLine();

                            File file = new File();

                            string textFile = file.getFileContent(taskInformationFile);
                           // file.findTaskName(textFile, job_name);

                            if ( file.findTaskName(textFile, job_name) == true )         // If task name exists into task's informations file 
                            {
                                string source = file.getSourcePath(taskInformationFile, job_name);
                                string destination = file.getDestinationPath(taskInformationFile, job_name);

                                CopyFile copyTask = new CopyFile();
                                copyTask.Copy(source, destination, job_name);

                            }else {  Console.WriteLine(" \n Task doesn't exist. \n"); }



                        break;

                        case 3:

                            CopyFile Files = new CopyFile();
                            Files.Copy(job_sourcePath, job_targetPath, job_name);


                        break;
                        case 4:

                              Environment.Exit(0);
                              break;

                        default:

                            Console.WriteLine(" \n Invalid Input ! please choose between 1 and 4 \n ");

                            break;
                    }

                } while (options != 4);
            
        }
    }
}
