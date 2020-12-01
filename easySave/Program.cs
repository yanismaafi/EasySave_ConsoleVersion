using System;

namespace easySave
{
    class Program
    {
        static void Main(string[] args)
        {

            Menu console = new Menu();
            console.BeepMelody();

            Console.Title = "Easy Save";
       
            console.MenuConsole();
        }
    }
}
