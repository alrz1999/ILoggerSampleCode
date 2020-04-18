using ILoggerSampleCode.Models;
using ILoggerSampleCode.UserController;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILoggerSampleCode.CommandHandlers
{
    public class CommandHandler
    {
        public ILogger Logger { get; set; }
        public UserRepository UserRepository { get; set; }

        public CommandHandler(ILogger logger, UserRepository userRepository)
        {
            Logger = logger;
            UserRepository = userRepository;
        }

        internal void Handle(string command)
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(Handle)))
            {
                switch (command)
                {
                    case "sign up":
                        SignUp();
                        break;
                    case "sign in":
                        SignIn();
                        break;
                    case "sell":
                        Sell();
                        break;
                    case "sign out":
                        SignOut();
                        break;
                    default:
                        break;
                }
            }
        }

        private void SignOut()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(SignOut)))
            {
                var id = GetUserId();
                if (UserRepository.IsRegistered(id))
                {
                    var user = UserRepository.GetUser(id);
                    if (user.IsSignedIn)
                    {
                        user.IsSignedIn = false;
                        Logger.LogInformation("user signed out");
                    }
                    else
                    {
                        Logger.LogWarning("a user was signed out but asked to sign out again");
                    }
                }
                else
                {
                    Logger.LogWarning("not found user with {Id} to sign out", id);
                }
            }
        }

        private void Sell()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(Sell)))
            {
                var id = GetUserId();
                if (UserRepository.IsRegistered(id))
                {
                    var user = UserRepository.GetUser(id);
                    if (user.IsSignedIn)
                    {
                        user.Money += 100;
                        Logger.LogInformation("a sell operation was done");
                        Logger.LogTrace("user with id {Id} sold something for 100$", user.Id);
                    }
                    else
                    {
                        Logger.LogWarning("a user tried to sell but wasn't signed in");
                    }
                }
                else
                {
                    Logger.LogWarning("not found user with specified id to sell");
                }
            }
        }

        private void SignIn()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(SignIn)))
            {
                var id = GetUserId();
                if (UserRepository.IsRegistered(id))
                {
                    var user = UserRepository.GetUser(id);
                    if (user.IsSignedIn)
                    {
                        Logger.LogWarning("a user was signed in but try to sign in again");
                    }
                    else
                    {
                        user.IsSignedIn = true;
                        Logger.LogInformation("a user signed in");
                        Logger.LogTrace("user with {Id}", user.Id);
                    }
                }
                else
                {
                    Logger.LogWarning("not found user with specific id to sign in");
                }
            }
        }

        private void SignUp()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(SignUp)))
            {
                Console.WriteLine("enter your name");
                var name = Console.ReadLine();
                Console.WriteLine("enter your age");
                var age = int.Parse(Console.ReadLine());
                var user = new User(name, age);
                UserRepository.RegisterUser(user);
                Console.WriteLine($"your id : {user.Id}");
                Logger.LogInformation("a user siged up");
                Logger.LogTrace("a user with id {Id} signed up",user.Id);
            }
        }

        private int GetUserId()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(GetUserId)))
            {
                Console.WriteLine("Enter your id");
                int id;
                try
                {
                    id = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "invalid input format");
                    throw;
                }

                return id;
            }
        }
    }
}
