using CQRS.WebChat.Worker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.WebChat.WorkerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string storageConnection = ConfigurationManager.ConnectionStrings["storageConnection"].ConnectionString;
            CommandsPerformer commandsPerformer = new CommandsPerformer(storageConnection);

            Console.WriteLine("Type \"quit\" to stop working\n");
            commandsPerformer.StartWorking();

            string userInput = null;
            while (true)
            {
                userInput = Console.ReadLine();
                if (userInput == "quit")
                {
                    break;
                }
                else if (commandsPerformer.IsWorking)
                {
                    commandsPerformer.StopWorking();
                }
                else
                {
                    commandsPerformer.StartWorking();
                }
            }

            if (commandsPerformer.IsWorking)
            {
                commandsPerformer.StopWorking();
            }
        }
    }
}
