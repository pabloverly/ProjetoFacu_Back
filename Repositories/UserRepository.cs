using ApiTools.Model;

namespace ApiTools.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "admin", Password = "admin", Role = "adm" });
            users.Add(new User { Id = 2, Username = "robin", Password = "robin", Role = "user" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}