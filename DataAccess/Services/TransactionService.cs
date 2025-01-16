using DataAccess.Services.Interface;
using DataModel.Model;
using SQLite;

namespace DataAccess.Services
{
    public class TransactionService : ITransaction
    {
        private SQLiteAsyncConnection _dbConnection;
        public TransactionService()
        {
            SetUpDb();
        }
        private async void SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<Transaction>();
            }
        }
        public async Task<int> Add(Transaction mod)
        {
            return await _dbConnection.InsertAsync(mod);
        }

        public async Task<List<Transaction>> GetAll()
        {
            return await _dbConnection.Table<Transaction>().ToListAsync();
        }

        public async Task<int> Delete(Transaction mod)
        {
            return await _dbConnection.DeleteAsync(mod);
        }

        public async Task<int> Update(Transaction mod)
        {
            return await _dbConnection.UpdateAsync(mod);
        }
    }
}
