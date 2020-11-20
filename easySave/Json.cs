using System;
using System.IO;
using Newtonsoft.Json;




namespace easySave
{
    class Json
    {
        

        public string ConvertToJson (string name, string source, string destination, string type)   //Convert data to Json format
        {

            Work newWork = new Work(name, source, destination, type);
           
            string jsonResult = JsonConvert.SerializeObject(newWork);

            Console.WriteLine($"\n The Json Format result : \n" + jsonResult);

            return jsonResult;
        }


        


        public void CreateFileJson (string information)    // Create json File and write json content (information's about the job)
        {
            string path = "C:\\Users\\ASUS\\Desktop\\Task\\Task's_Details.json";

            File file = new File();

            if (file.checkExistence(path) != true)                // if file doesn't exist create a new file and write information
            {
                using (StreamWriter jsonFile =  System.IO.File.CreateText(path))
                {
                    jsonFile.WriteLine(information);
                }

            }
            else                                                 // else (file exist) append information
            {
                using (StreamWriter jsonFile = System.IO.File.AppendText(path))
                {
                    jsonFile.WriteLine(information);
                }

            }

        }




    }
}
