using ILoggerSampleCode.Models;
using System;
using System.Collections.Generic;

namespace ILoggerSampleCode.UserController
{
    public class UserRepository
    {
        private Dictionary<string, User> RegisteredUsers { get; set; } = new Dictionary<string, User>();

        public bool IsRegistered(string username) => RegisteredUsers.ContainsKey(username);
        public void RegisterUser(User user) => RegisteredUsers.Add(user.Username, user);
        public User GetUser(string username, string password)
        {
            if (IsRegistered(username))
            {
                var user = RegisteredUsers[username];
                if (user.Password == password)
                {
                    return user;
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
        public User GetUser(string username, double key)
        {
            if (IsRegistered(username))
            {
                var user = RegisteredUsers[username];
                if (user.Key == key)
                {
                    return user;
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
