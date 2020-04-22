using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILoggerSampleCode.Models
{
    public class User
    {
        private static int IdCounter { get; set; } = 0;
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public long Money { get; set; }
        public long? Key { get; set; }

        public User(string name, string password)
        {
            Id = SetUserId();
            Username = name;
            Password = password;
            Money = 1000;
        }

        private int SetUserId()
        {
            IdCounter+=1;
            return IdCounter;
        }
    }
}
