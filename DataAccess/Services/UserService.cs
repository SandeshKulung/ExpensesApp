
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

            if (!_users.Any())
            {
                _users.Add(new User { UserId=1, Username = SeedUsername, Password = SeedPassword});
                SaveUsers(_users);
            }
        }
        public bool Login(User user)
        {
            if (_users.Any(x=>x.Username==user.Username && x.Password==user.Password))
            {
                return true;
            }
            return false;
        }
    }
}
