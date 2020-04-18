using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILoggerSampleCode
{
    class UserRepository
    {
        private Dictionary<int, User> RegisteredUsers { get; set; } = new Dictionary<int, User>();

        public bool IsRegistered(int id) => RegisteredUsers.ContainsKey(id);
        public void RegisterUser(User user) => RegisteredUsers.Add(user.Id, user);
        public User GetUser(int id) => RegisteredUsers[id];
    }
}
