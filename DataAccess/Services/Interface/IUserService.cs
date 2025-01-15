using DataModel.Model;

namespace DataAccess.Services.Interface
{
    public interface IUserService
    {
        bool Login(User user);
    }
}
