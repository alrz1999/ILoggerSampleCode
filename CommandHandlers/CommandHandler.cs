using ILoggerSampleCode.Models;
using ILoggerSampleCode.UserController;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                var username = GetUsername();
                var key = GetKey();
                if (UserRepository.IsRegistered(username))
                {
                    var user = UserRepository.GetUser(username, key);
                    user.Key = null;
                    Logger.LogInformation("user signed out");
                }
                else
                {
                    Logger.LogWarning("not found user with {Username} to sign out", username);
                }
            }
        }

        private double GetKey()
        {
            double key;
            Thread.Sleep(10);
            Console.WriteLine("enter your key");
            try
            {
                key = long.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                throw;
            }
            return key;
        }

        private void Sell()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(Sell)))
            {
                var username = GetUsername();
                var key = GetKey();
                if (UserRepository.IsRegistered(username))
                {
                    User user;
                    try
                    {
                        user = UserRepository.GetUser(username, key);
                    }
                    catch (Exception)
                    {
                        Logger.LogWarning("a user tried to sell but wasn't signed in");
                        throw;
                    }
                    user.Money += 100;
                    Logger.LogInformation("a sell operation was done");
                    Logger.LogTrace("user with id {Id} sold something for 100$", user.Id);
                }
                else
                {
                    Logger.LogWarning("not found user with specified id to sell {CommandId}");
                }
            }
        }

        private void SignIn()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(SignIn)))
            {
                var username = GetUsername();
                var password = GetPassword();
                var user = UserRepository.GetUser(username, password);
                user.Key = new Random().Next();
                Console.WriteLine($"use this key for command {user.Key}");
            }
        }

        private void SignUp()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(SignUp)))
            {
                string username = GetUsername();
                if (UserRepository.IsRegistered(username))
                {
                    throw new Exception();
                }
                string password = GetPassword();
                var user = new User(username, password);
                UserRepository.RegisterUser(user);
                Thread.Sleep(10);
                Logger.LogInformation("a user siged up");
                Logger.LogTrace("a user with id {Id} signed up", user.Id);
            }
        }

        private string GetPassword()
        {
            Thread.Sleep(10);
            Console.WriteLine("enter your password");
            var password = Console.ReadLine();
            return password;
        }

        private string GetUsername()
        {
            Thread.Sleep(10);
            Console.WriteLine("enter your username");
            var username = Console.ReadLine();
            return username;
        }

        private int GetUserId()
        {
            using (Logger.BeginScope("{ClassName}-{MethodName}", nameof(CommandHandler), nameof(GetUserId)))
            {
                Thread.Sleep(10);
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
