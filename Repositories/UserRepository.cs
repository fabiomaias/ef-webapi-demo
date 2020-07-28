using System.Collections.Generic;
using System.Linq;
using efwebapi.Models;

namespace efwebapi.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password) {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "fabiomaias", Password = "123456", Role = "manager" });
            users.Add(new User { Id = 2, Username = "fulano", Password = "123456", Role = "employee" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}