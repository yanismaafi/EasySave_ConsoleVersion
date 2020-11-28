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

         public string taskInformationFile = "C:\\EasySave\\Task's_Details.json";    // Log Task's details File




        public void MenuConsole()
        {
                Console.ForegroundColor = ConsoleColor.White;

                string options;
                string task_name;
                string task_sourcePath;
                string task_targetPath;
                string task_type = string.Empty;
                int typeOftask;

            do {
                    easySaveLogo();
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("\n 1- Create a task \n");
                    Console.WriteLine("\n 2- Execute a specific Task \n");
                    Console.WriteLine("\n 3- Execute all Tasks \n");
                    Console.WriteLine("\n 4- Show my Saved Tasks \n");
                    Console.WriteLine("\n 5- Exit \n");

                    options = Console.ReadLine();
                   
                    switch (options)
                    {

                        case "1":

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\n\n ------------------------------------ Create a new task ----------------------------------------  \n");
                            Console.Write("\n Enter your task name : ");
                           
                            task_name = Console.ReadLine();
                           
                            Console.Write("\n Enter the source path of the directory you want to copy :  ");
                            task_sourcePath = Console.ReadLine();

                            while( System.IO.Directory.Exists(task_sourcePath) != true )
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

                                }else if (typeOftask == 2)
                                {
                                    task_type = "Differential";
                                    break;

                                }else
                                {
                                    Console.WriteLine("\n Error invalid task type ! \n ");
                                }

                            } while (typeOftask != 1 || typeOftask != 2);

                           

                             Json convert = new Json();        //Convert information's task to json format 
                             
                             string informationstask = convert.ConvertToJson(task_name, task_sourcePath, task_targetPath, task_type, DateTime.Now);
                             convert.CreateFileJson(informationstask);  // Put json infromation into File   

                             savedMsg();
                        break;


                        case "2":

                            Console.WriteLine("\n----------------------------------------------- Execute a specific Task ----------------------------------------------\n ");

                            if (System.IO.File.Exists(taskInformationFile) == true)
                            {
                                    File task = new File();
                                    Console.WriteLine("\nList of saved Task : \n ");
                                    task.ShowTasksName();

                                    Console.Write("\nName of the task you want to execute : ");
                                    task_name = Console.ReadLine();

                                    while (string.IsNullOrEmpty(task_name))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("\nThis field are required, please enter a Task name ... ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("\nEnter name of the task you want to execute : ");
                                        task_name = Console.ReadLine();
                                    }

                                    while (task.findTaskName(task_name) == false)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("\nThe task : " + task_name + " doesn't exist ... \n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("\nPlease, enter an existing Task name : ");
                                        task_name = Console.ReadLine();
                                    }

                                    CopyFile file = new CopyFile();
                                    file.specificCopy(task_name);

                                    successMsg();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\n There is no saved task \n");
                                Thread.Sleep(2000);
                            }

                            MenuConsole();
                        break;

                        case "3":
                                CopyFile f = new CopyFile();
                                f.CopyAllTask();

                                successMsg();

                        break;


                        case "4":

                                File FILE = new File();
                                FILE.ShowAllTasks();

                                Console.Write("\n- Press 'M' key for startup Menu : ");
                                string x = Console.ReadLine();
                                if (x == "m" || x == "M")
                                {
                                    MenuConsole();
                                }
                        break;


                        case "5":
                                    goodByeMsg();
                                    Environment.Exit(0);
                        break;


                        default:

                                 Console.ForegroundColor = ConsoleColor.Red;
                                 Console.WriteLine(" \n Invalid Input, please choose between 1 and 4 \n ");
                                 Console.ForegroundColor = ConsoleColor.White;
                                 Thread.Sleep(2000);

                        break;
                    }

            } while (options != "4");
            
        }






        public void easySaveLogo()
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

        }





        public void successMsg()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t\t ███████╗██╗   ██╗ ██████╗ ██████╗███████╗███████╗███████╗██╗   ██╗██╗ ");
            Console.WriteLine("\t\t\t ██╔════╝██║   ██║██╔════╝██╔════╝██╔════╝██╔════╝██╔════╝██║   ██║██║ ");
            Console.WriteLine("\t\t\t ███████╗██║   ██║██║     ██║     █████╗  ███████╗█████╗  ██║   ██║██║ ");
            Console.WriteLine("\t\t\t ╚════██║██║   ██║██║     ██║     ██╔══╝  ╚════██║██╔══╝  ██║   ██║██║ ");
            Console.WriteLine("\t\t\t ███████║╚██████╔╝╚██████╗╚██████╗███████╗███████║██║     ╚██████╔╝███████╗ ");
            Console.WriteLine("\t\t\t ╚══════╝ ╚═════╝  ╚═════╝ ╚═════╝╚══════╝╚══════╝╚═╝      ╚═════╝ ╚══════╝ ");
            Console.WriteLine("\n\n");

            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
        }



        public void savedMsg()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t  ████████╗ █████╗ ███████╗██╗  ██╗    ███████╗ █████╗ ██╗   ██╗███████╗██████╗ ");
            Console.WriteLine("\t\t  ╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝    ██╔════╝██╔══██╗██║   ██║██╔════╝██╔══██╗ ");
            Console.WriteLine("\t\t     ██║   ███████║███████╗█████╔╝     ███████╗███████║██║   ██║█████╗  ██║  ██║ ");
            Console.WriteLine("\t\t     ██║   ██╔══██║╚════██║██╔═██╗     ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝  ██║  ██║ ");
            Console.WriteLine("\t\t     ██║   ██║  ██║███████║██║  ██╗    ███████║██║  ██║ ╚████╔╝ ███████╗██████╔╝ ");
            Console.WriteLine("\t\t     ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝    ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝╚═════╝  ");
            Console.WriteLine("\n\n");

            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
        }



        public void goodByeMsg()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t\t  ██████╗  ██████╗  ██████╗ ██████╗      ██████╗ ██╗   ██╗███████╗    ██╗");
            Console.WriteLine("\t\t\t  ██╔════╝ ██╔═══██╗██╔═══██╗██╔══██╗    ██╔══██╗╚██╗ ██╔╝██╔════╝    ██║");
            Console.WriteLine("\t\t\t  ██║  ███╗██║   ██║██║   ██║██║  ██║    ██████╔╝ ╚████╔╝ █████╗      ██║");
            Console.WriteLine("\t\t\t  ██║   ██║██║   ██║██║   ██║██║  ██║    ██╔══██╗  ╚██╔╝  ██╔══╝      ╚═╝");
            Console.WriteLine("\t\t\t  ██████╔╝╚██████╔╝╚██████╔╝██████╔╝     ██████╔╝   ██║   ███████╗    ██╗");
            Console.WriteLine("\t\t\t  ╚═════╝  ╚═════╝  ╚═════╝ ╚═════╝      ╚═════╝    ╚═╝   ╚══════╝    ╚═╝");

            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.White;
        }


    }
}
