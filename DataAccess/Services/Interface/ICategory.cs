using DataModel.Model;

namespace DataAccess.Services.Interface
{
    public interface ICategory
    {
        Task<int> Add(Category mod);
        Task<List<Category>> GetAll();
        Task<int> Delete(Category mod);
        Task<int> Update(Category mod);
    }
}
