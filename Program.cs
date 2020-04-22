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
            var logger = GetLogger();
            var commandHandler = new CommandHandler(logger, userRepository);
            var commandId = 1;
            while (command != "close")
            {
                PrintMenu();
                using (logger.BeginScope("{ClassName}-{MethodName}-{CommandId}", nameof(Program), nameof(Main), commandId.ToString()))
                {
                    command = Console.ReadLine();
                    logger.LogInformation("a command received");
                    commandHandler.Handle(command);

                }
                commandId++;
            }
        }

        private static void PrintMenu()
        {
            Thread.Sleep(10);
            Console.WriteLine("--------------");
            Console.WriteLine("1.sign up");
            Console.WriteLine("2.sign in");
            Console.WriteLine("3.sign out");
            Console.WriteLine("4.sell");
            Console.WriteLine("--------------");
        }

        private static ILogger GetLogger()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
                                        builder
                                        .AddConsole(option => option.IncludeScopes=true)
                                        .AddDebug()
                                        .AddFilter<DebugLoggerProvider>("CommandHandler", LogLevel.Trace)                                  
                                        );
            return loggerFactory.CreateLogger("CommandHandler");
        }
    }
}
