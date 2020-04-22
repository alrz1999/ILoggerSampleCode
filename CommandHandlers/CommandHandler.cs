using ILoggerSampleCode.Models;
using ILoggerSampleCode.UserController;
using System;
using System.Threading;

namespace ILoggerSampleCode.CommandHandlers
{
    public class CommandHandler
    {
        public UserRepository UserRepository { get; set; }

        public CommandHandler(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        internal void Handle(string command)
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

        private void SignOut()
        {
            var username = GetUsername();
            var key = GetKey();
            if (UserRepository.IsRegistered(username))
            {
                var user = UserRepository.GetUser(username, key);
                user.Key = null;
            }
            else
            {
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
                    throw;
                }
                user.Money += 100;
            }
            else
            {
            }

        }

        private void SignIn()
        {
            var username = GetUsername();
            var password = GetPassword();
            var user = UserRepository.GetUser(username, password);
            user.Key = new Random().Next();
            Console.WriteLine($"use this key for command {user.Key}");
        }

        private void SignUp()
        {
            string username = GetUsername();
            if (UserRepository.IsRegistered(username))
            {
                throw new Exception();
            }
            string password = GetPassword();
            var user = new User(username, password);
            UserRepository.RegisterUser(user);
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
    }
}
