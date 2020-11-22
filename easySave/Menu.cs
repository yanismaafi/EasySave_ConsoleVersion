using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading;

namespace easySave
{
    class Menu
    {

        public void MenuConsole()
        {
                Console.ForegroundColor = ConsoleColor.White;

                int options = 0;
                int typeOftask = 0;

                string task_name = string.Empty;
                string task_sourcePath = string.Empty; ;
                string task_targetPath = string.Empty; ;
                string task_type = string.Empty;

                string taskInformationFile = "C:\\Users\\ASUS\\Desktop\\Task\\Task's_Details.json";


            do
                {
             
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\n\n\n");

                   
                    Console.WriteLine("\t\t\t  ███████╗ █████╗ ███████╗██╗   ██╗    ███████╗ █████╗ ██╗   ██╗███████╗ ");
                    Console.WriteLine("\t\t\t  ██╔════╝██╔══██╗██╔════╝╚██╗ ██╔╝    ██╔════╝██╔══██╗██║   ██║██╔════╝");
                    Console.WriteLine("\t\t\t  █████╗  ███████║███████╗ ╚████╔╝     ███████╗███████║██║   ██║█████╗ ");
                    Console.WriteLine("\t\t\t  ██╔══╝  ██╔══██║╚════██║  ╚██╔╝      ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝  ");
                    Console.WriteLine("\t\t\t  ███████╗██║  ██║███████║   ██║       ███████║██║  ██║ ╚████╔╝ ███████╗");
                    Console.WriteLine("\t\t\t  ╚══════╝╚═╝  ╚═╝╚══════╝   ╚═╝       ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝");

                    Console.WriteLine("\n\t\t\t\t\t\t Welcome to EasySave !  \n");

                    Console.ForegroundColor = ConsoleColor.White;


                    Console.WriteLine("\n 1- Create a task \n");
                    Console.WriteLine("\n 2- Execute a specific Task \n");
                    Console.WriteLine("\n 3- Execute all Tasks \n");
                    Console.WriteLine("\n 4- Exit \n");

                     options = Convert.ToInt32(Console.ReadLine());
                   
                    

                    switch (options)
                    {
                        case 1:

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\n\n ------------------------------------ Create a new task ----------------------------------------  \n");
                            Console.Write("\n Enter your task name : ");
                           
                            task_name = Console.ReadLine();
                           
                            Console.Write("\n Enter the source path of the directory you want to copy :  ");
                            task_sourcePath = Console.ReadLine();

                            while(System.IO.Directory.Exists(task_sourcePath) != true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\n Invalid source path, please try again  ");
                                Console.ResetColor();

                                Console.Write("\n Enter the source path of the directory :  ");
                                task_sourcePath = Console.ReadLine();
                            }

                            Console.Write("\n Enter the destination path of the directory :  ");
                            task_targetPath = Console.ReadLine();

                            while (System.IO.Directory.Exists(task_targetPath) != true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\n Invalid destination path, please try again ! ");
                                Console.ResetColor();
                                Console.Write("\n Enter the destination path of the directory you want to copy :  ");
                                task_targetPath = Console.ReadLine();
                            }

                            Console.WriteLine("\n Enter the type of the task \n <1>: Complete. \n <2>: Differential : ");
                           
                            do
                            {
                                typeOftask = Convert.ToInt32(Console.ReadLine());

                                if (typeOftask == 1)
                                {
                                    task_type = "Complete";
                                    break;
                                }
                                else if (typeOftask == 2)
                                {
                                    task_type = "Differential";
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\n Error invalid task type ! \n ");
                                }

                            } while (typeOftask != 1 || typeOftask != 2);

                            

                             Console.ForegroundColor = ConsoleColor.Green;
                             Console.WriteLine("\n\n");
                             Console.WriteLine("  ████████╗ █████╗ ███████╗██╗  ██╗    ███████╗ █████╗ ██╗   ██╗███████╗██████╗ ");
                             Console.WriteLine("  ╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝    ██╔════╝██╔══██╗██║   ██║██╔════╝██╔══██╗");
                             Console.WriteLine("     ██║   ███████║███████╗█████╔╝     ███████╗███████║██║   ██║█████╗  ██║  ██║");
                             Console.WriteLine("     ██║   ██╔══██║╚════██║██╔═██╗     ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝  ██║  ██║");
                             Console.WriteLine("     ██║   ██║  ██║███████║██║  ██╗    ███████║██║  ██║ ╚████╔╝ ███████╗██████╔╝");
                             Console.WriteLine("     ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝    ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝╚═════╝ ");
                             Console.WriteLine("\n\n");

                             Thread.Sleep(1000);
                             Console.ForegroundColor = ConsoleColor.White;

                             Console.WriteLine($"\n\t name : {task_name} \n \n source Path : {task_sourcePath}  \n \n destination path : {task_targetPath} \n \n task type : {task_type} \n ");

                             Json convert = new Json();        //Convert information's task to json format 
                             string informationstask = convert.ConvertToJson(task_name, task_sourcePath, task_targetPath, task_type);
                             convert.CreateFileJson(informationstask);  // Put json infromation into file    
                            
                        break;

                        case 2:

                            Console.WriteLine("\t\n Execute a specific Task ! \n ");

                            Console.WriteLine("\t\n Name of the task tou want to execute :");
                            task_name = Console.ReadLine();

                            File file = new File();

                            string textFile = file.getFileContent(taskInformationFile);
                          

                            if ( file.findTaskName(textFile, task_name) == true )         // If task name exists into task's informations file 
                            {
                                string source = file.getSourcePath(taskInformationFile, task_name);
                                string destination = file.getDestinationPath(taskInformationFile, task_name);
                                CopyFile copyTask = new CopyFile();

                                  if(file.isDifferential(taskInformationFile) == true)
                                  {
                                     copyTask.copyChangedFile(source, destination);
                                  
                                  }else
                                  {
                                      copyTask.Copy(source, destination, task_name);
                                  }

                        }
                        else {  Console.WriteLine("\t\n Task doesn't exist. \n"); }

                          
                        break;

                        case 3:

                            CopyFile files = new CopyFile();
                            files.CopyAllTasks();

                        break;

                        case 4:

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n\n");
                                Console.WriteLine("  ██████╗  ██████╗  ██████╗ ██████╗      ██████╗ ██╗   ██╗███████╗    ██╗");
                                Console.WriteLine("  ██╔════╝ ██╔═══██╗██╔═══██╗██╔══██╗    ██╔══██╗╚██╗ ██╔╝██╔════╝    ██║");
                                Console.WriteLine("  ██║  ███╗██║   ██║██║   ██║██║  ██║    ██████╔╝ ╚████╔╝ █████╗      ██║");
                                Console.WriteLine("  ██║   ██║██║   ██║██║   ██║██║  ██║    ██╔══██╗  ╚██╔╝  ██╔══╝      ╚═╝");
                                Console.WriteLine("  ██████╔╝╚██████╔╝╚██████╔╝██████╔╝     ██████╔╝   ██║   ███████╗    ██╗");
                                Console.WriteLine("  ╚═════╝  ╚═════╝  ╚═════╝ ╚═════╝      ╚═════╝    ╚═╝   ╚══════╝    ╚═╝");
                            
                                Thread.Sleep(2000);
                                Console.ForegroundColor = ConsoleColor.White;
                                Environment.Exit(0);

                        break;

                        default:

                                 Console.ForegroundColor = ConsoleColor.Red;
                                 Console.WriteLine(" \n Invalid Input, please choose between 1 and 4 \n ");
                                 Console.ForegroundColor = ConsoleColor.White;
                                 Thread.Sleep(1000);

                        break;
                    }

                } while (options != 4);
            
        }


    }
}
