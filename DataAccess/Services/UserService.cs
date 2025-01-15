using DataAccess.Services.Interface;
using DataModel.Abstraction;
using DataModel.Model;

namespace DataAccess.Services
{
    public class UserService : UserBase, IUserService
    {
        private List<User> _users;

        public const string SeedUsername = "admin";
        public const string SeedPassword = "password";
        public const string SeedCurrency_Type = "";

        public UserService()
        {
            _users = LoadUsers();

            if (_users.Any())
            {
                _users.Add(new User { Username = SeedUsername, Password = SeedPassword});
                SaveUsers(_users);
            }
        }
        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
            return _users.Any(u => u.Username == user.Username && u.Password == user.Password);
        }
    }
}
