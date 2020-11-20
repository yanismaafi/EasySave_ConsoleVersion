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
            string path = "C:\\Users\\ASUS\\Desktop\\JOB\\job's_Details.json";

            using (StreamWriter jsonFile = System.IO.File.CreateText(path))  
            {
                jsonFile.WriteLine(information);
            }

        }




    }
}
