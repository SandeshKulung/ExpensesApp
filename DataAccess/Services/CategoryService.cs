using DataAccess.Services.Interface;
using DataModel.Model;
using SQLite;

namespace DataAccess.Services
{
    public class CategoryService : ICategory
    {
        private SQLiteAsyncConnection _dbConnection;
        public CategoryService()
        {
            SetUpDb();
        }
        private async void SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<Category>();
            }
        }
        public async Task<int> Add(Category mod)
        {
            return await _dbConnection.InsertAsync(mod);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _dbConnection.Table<Category>().ToListAsync();
        }

        public async Task<int> Delete(Category mod)
        {
            return await _dbConnection.DeleteAsync(mod);
        }

        public async Task<int> Update(Category mod)
        {
            return await _dbConnection.UpdateAsync(mod);
        }
    }
}
