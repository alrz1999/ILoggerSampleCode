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
        public string Name { get; set; }
        public int Age { get; set; }
        public long Money { get; set; }
        public bool IsSignedIn { get; set; } = false;

        public User(string name, int age)
        {
            Id = SetUserId();
            Name = name;
            Age = age;
            Money = 1000;
        }

        private int SetUserId()
        {
            IdCounter+=1;
            return IdCounter;
        }
    }
}
