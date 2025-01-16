using DataModel.Model;

namespace DataAccess.Services.Interface
{
    public interface ITransaction
    {
        Task<int> Add(Transaction mod);
        Task<List<Transaction>> GetAll();
        Task<int> Delete(Transaction mod);
        Task<int> Update(Transaction mod);
    }
}
