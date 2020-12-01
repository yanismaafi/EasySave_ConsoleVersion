using System;
using System.IO;
using System.Threading;
using System.Configuration;

namespace easySave
{
    class Menu
    { 

         public string taskInformationFile = ConfigurationManager.AppSettings.Get("taskInformationFile");   // Task's Details File path   // Log Task's details File
         public string EasySave = ConfigurationManager.AppSettings.Get("EasySave");  // EasySave Directory 



        public void MenuConsole()
        {

                    string options;
                    string task_name;
                    string task_sourcePath;
                    string task_targetPath;
                    string task_type = string.Empty;
                    int typeOftask;

                    EasySaveExistence();

  
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Clear();

                    easySaveLogo();
                    

                    Console.WriteLine("\n 1- Create a task \n");
                    Console.WriteLine("\n 2- Execute a specific Task \n");
                    Console.WriteLine("\n 3- Execute all Tasks \n");
                    Console.WriteLine("\n 4- Show my Saved Tasks \n");
                    Console.WriteLine("\n 5- Exit \n");

                    options = Console.ReadLine();

                   
                    switch (options)
                    {

                        case "1":   // Case Create a Task

                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("\n\n ---------------------------------------------- Create a new task ---------------------------------------------  \n");
                            Console.Write("\n Enter your task name : ");
                           
                            task_name = Console.ReadLine();
                           
                            Console.Write("\n Enter the source path of the directory you want to copy :  ");
                            task_sourcePath = Console.ReadLine();

                            while( System.IO.Directory.Exists(task_sourcePath) != true )
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("\n Invalid source path, please try again  \n");
                                Console.ForegroundColor = ConsoleColor.Black;

                                Console.Write("\n Enter the source path of the directory :  ");
                                task_sourcePath = Console.ReadLine();
                            }

                            Console.Write("\n Enter the destination path of the directory :  ");
                            task_targetPath = Console.ReadLine();

                            while (System.IO.Directory.Exists(task_targetPath) != true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("\n Invalid destination path, please try again ! \n");
                                Console.ResetColor();
                                Console.Write("\n Enter the destination path of the directory you want to copy :  ");
                                task_targetPath = Console.ReadLine();
                            }

                            Console.WriteLine("\n Enter the type of Backup : \n\n 1- Complete \n\n 2- Differential \n");
                           
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

                             MenuConsole();
                        break;


                        case "2":   // Case Execute a specific Task

                            if ( System.IO.File.Exists(taskInformationFile) && new FileInfo(taskInformationFile).Length != 0 )
                            {

                                    Console.WriteLine("\n----------------------------------------------- Execute a specific Task ----------------------------------------------\n ");

                                    File task = new File();
                                    Console.WriteLine("\nList of saved Task : \n ");
                                    task.ShowTasksName();

                                    Console.Write("\nName of the task you want to execute : ");
                                    task_name = Console.ReadLine();

                                    while (string.IsNullOrEmpty(task_name))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write("\nThis field are required, please enter a Task name ... ");
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\nEnter name of the task you want to execute : ");
                                        task_name = Console.ReadLine();
                                    }

                                    while (task.findTaskName(task_name) == false)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.Write("\nThe task : " + task_name + " doesn't exist ... \n");
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("\nPlease, enter an existing Task name : ");
                                        task_name = Console.ReadLine();
                                    }

                                    CopyFile file = new CopyFile();
                                    file.specificCopy(task_name);

                                    successMsg();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("\n There is no saved task \n");
                                Thread.Sleep(2000);
                            }

                            MenuConsole();
                        break;

                        case "3":   //Cas Execute all Task
                                
                                if ( System.IO.File.Exists(taskInformationFile) && new FileInfo(taskInformationFile).Length != 0 )
                                {
                                    CopyFile f = new CopyFile();
                                    f.CopyAllTask();
                                    successMsg();

                                }else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n There is no saved task");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Thread.Sleep(2000);
                                }

                                MenuConsole();
                        break;


                        case "4":       // Case Show my saved Tasks

                                File FILE = new File();
                                FILE.ShowAllTasks();

                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("\n- Press 'M' key for startup Menu : ");
                                string x = Console.ReadLine();
                                if (x == "m" || x == "M")
                                {
                                    MenuConsole();
                                }
                        break;


                        case "5":
                                    goodByeMsg();
                                    BeepMelody();
                                    Environment.Exit(0);
                        break;


                        default:

                                 Console.ForegroundColor = ConsoleColor.DarkRed;
                                 Console.WriteLine(" \n Invalid Input, please choose between 1 and 5 \n ");
                                 Console.ForegroundColor = ConsoleColor.Black;
                                 Thread.Sleep(2000);
                                 MenuConsole();
                        break;
                    }

            
            
        }






        public void easySaveLogo()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\n\n\n");

            Console.WriteLine("\t\t\t  ███████╗ █████╗ ███████╗██╗   ██╗    ███████╗ █████╗ ██╗   ██╗███████╗ ");
            Console.WriteLine("\t\t\t  ██╔════╝██╔══██╗██╔════╝╚██╗ ██╔╝    ██╔════╝██╔══██╗██║   ██║██╔════╝");
            Console.WriteLine("\t\t\t  █████╗  ███████║███████╗ ╚████╔╝     ███████╗███████║██║   ██║█████╗ ");
            Console.WriteLine("\t\t\t  ██╔══╝  ██╔══██║╚════██║  ╚██╔╝      ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝  ");
            Console.WriteLine("\t\t\t  ███████╗██║  ██║███████║   ██║       ███████║██║  ██║ ╚████╔╝ ███████╗");
            Console.WriteLine("\t\t\t  ╚══════╝╚═╝  ╚═╝╚══════╝   ╚═╝       ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝");

            Console.WriteLine("\n\t\t\t\t\t\t Welcome to EasySave !  \n");

            Console.ForegroundColor = ConsoleColor.Black;

        }



        public void successMsg()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t\t ███████╗██╗   ██╗ ██████╗ ██████╗███████╗███████╗███████╗██╗   ██╗██╗ ");
            Console.WriteLine("\t\t\t ██╔════╝██║   ██║██╔════╝██╔════╝██╔════╝██╔════╝██╔════╝██║   ██║██║ ");
            Console.WriteLine("\t\t\t ███████╗██║   ██║██║     ██║     █████╗  ███████╗█████╗  ██║   ██║██║ ");
            Console.WriteLine("\t\t\t ╚════██║██║   ██║██║     ██║     ██╔══╝  ╚════██║██╔══╝  ██║   ██║██║ ");
            Console.WriteLine("\t\t\t ███████║╚██████╔╝╚██████╗╚██████╗███████╗███████║██║     ╚██████╔╝███████╗ ");
            Console.WriteLine("\t\t\t ╚══════╝ ╚═════╝  ╚═════╝ ╚═════╝╚══════╝╚══════╝╚═╝      ╚═════╝ ╚══════╝ ");
            Console.WriteLine("\n\n");

            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Black;
        }



        public void savedMsg()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t  ████████╗ █████╗ ███████╗██╗  ██╗    ███████╗ █████╗ ██╗   ██╗███████╗██████╗ ");
            Console.WriteLine("\t\t  ╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝    ██╔════╝██╔══██╗██║   ██║██╔════╝██╔══██╗ ");
            Console.WriteLine("\t\t     ██║   ███████║███████╗█████╔╝     ███████╗███████║██║   ██║█████╗  ██║  ██║ ");
            Console.WriteLine("\t\t     ██║   ██╔══██║╚════██║██╔═██╗     ╚════██║██╔══██║╚██╗ ██╔╝██╔══╝  ██║  ██║ ");
            Console.WriteLine("\t\t     ██║   ██║  ██║███████║██║  ██╗    ███████║██║  ██║ ╚████╔╝ ███████╗██████╔╝ ");
            Console.WriteLine("\t\t     ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝    ╚══════╝╚═╝  ╚═╝  ╚═══╝  ╚══════╝╚═════╝  ");
            Console.WriteLine("\n\n");

            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Black;
        }



        public void goodByeMsg()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t\t  ██████╗  ██████╗  ██████╗ ██████╗      ██████╗ ██╗   ██╗███████╗    ██╗");
            Console.WriteLine("\t\t\t  ██╔════╝ ██╔═══██╗██╔═══██╗██╔══██╗    ██╔══██╗╚██╗ ██╔╝██╔════╝    ██║");
            Console.WriteLine("\t\t\t  ██║  ███╗██║   ██║██║   ██║██║  ██║    ██████╔╝ ╚████╔╝ █████╗      ██║");
            Console.WriteLine("\t\t\t  ██║   ██║██║   ██║██║   ██║██║  ██║    ██╔══██╗  ╚██╔╝  ██╔══╝      ╚═╝");
            Console.WriteLine("\t\t\t  ██████╔╝╚██████╔╝╚██████╔╝██████╔╝     ██████╔╝   ██║   ███████╗    ██╗");
            Console.WriteLine("\t\t\t  ╚═════╝  ╚═════╝  ╚═════╝ ╚═════╝      ╚═════╝    ╚═╝   ╚══════╝    ╚═╝");

            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Black;
        }
        


        public void BeepMelody()
        {
            for (int j = 1; j < 5; j++)
            {
                Console.Beep(500 * j, 200);
            }
            Console.Beep(1500, 200);
            Console.Beep(1500, 200);
        }

        public void EasySaveExistence()
        {
            if (System.IO.Directory.Exists(EasySave) == false)   // Check if EasySave directory exist 
            {
                try
                {
                    System.IO.Directory.CreateDirectory(EasySave);  // create it if not 

                }
                catch (Exception e)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n" + e);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }




    }
}
