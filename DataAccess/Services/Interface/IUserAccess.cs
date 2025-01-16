using DataModel.Model;

namespace DataAccess.Services.Interface
{
    public interface IUserAccess
    {
        Task<int> AddUser(User user);
        Task<int> DeleteUser(User user);
        Task<int> UpdateUser(User user);
        Task<List<User>> GetAllUser();
        Task<User> GetUserByID(int UserId);
    }
}
