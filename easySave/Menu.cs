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
                string job_type = string.Empty; ;

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

                    Console.WriteLine("\n 1- Create a job \n");
                    Console.WriteLine("\n 2- Execute a specific job \n");
                    Console.WriteLine("\n 3- Execute all jobs \n");
                    Console.WriteLine("\n 4- Exit \n");

                    options = Convert.ToInt32(Console.ReadLine());


                    switch (options)
                    {
                        case 1:

                            Console.WriteLine("\n Create a new job ! \n");
                            Console.Write("\n give us a name for your job : ");
                            job_name = Console.ReadLine();

                            Console.Write("\n give us the source path of the directory you wanna copy :  ");
                            job_sourcePath = Console.ReadLine();

                            Console.Write("\n give us the destination path :  ");
                            job_targetPath = Console.ReadLine();

                            Console.WriteLine("\n give the type of the job <1>:complete <2>:differential : ");
                           
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
                                    Console.WriteLine("\n Error invalid job type ! \n ");
                                }

                            } while (typeOfJob != 1 || typeOfJob != 2);

                             Console.WriteLine("\n Job saved ! \n");
                             Console.WriteLine($"\n name : {job_name} \n \n source Path : {job_sourcePath}  \n \n destination path : {job_targetPath} \n \n job type : {job_type} \n ");

                             Json convert = new Json();       //Convert information's job to json format 
                             string informationsJob = convert.ConvertToJson(job_name, job_sourcePath, job_targetPath, job_type);
                             convert.CreateFileJson(informationsJob);  // Put json infromation into file 
                           
                             

                        break;

                        case 2:
                            Console.WriteLine("\n Execute a specific job ! \n ");
                            break;

                        case 3:

                            CopyFile Files = new CopyFile();
                            Files.Copy(job_sourcePath, job_targetPath, job_name);

                          /*  Console.WriteLine("\n Execute all jobs !\n ");
                            CopyFile Files = new CopyFile();

                            if(Files.checkExistence(job_targetPath) == false)
                            {
                             
                                 Files.Copy(job_sourcePath, job_targetPath,job_name);
                           
                            } else { Console.WriteLine("Le fichier existe deja"); }

                        */

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
