using ILoggerSampleCode.CommandHandlers;
using ILoggerSampleCode.UserController;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ILoggerSampleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = "";
            var userRepository = new UserRepository();
            var commandHandler = new CommandHandler(userRepository);
            var commandId = 1;
            while (command != "close")
            {
                PrintMenu();

                command = Console.ReadLine();
                commandHandler.Handle(command);

                commandId++;
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("1.sign up");
            Console.WriteLine("2.sign in");
            Console.WriteLine("3.sign out");
            Console.WriteLine("4.sell");
            Console.WriteLine("--------------");
        }
    }
}
