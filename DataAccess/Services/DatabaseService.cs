using DataModel.Model;
using SQLite;

namespace DataAccess.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _databaseConnection;

        public DatabaseService()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "expensesapps.db");
            _databaseConnection = new SQLiteAsyncConnection(databasePath);
        }

        public async Task InitializeDatabaseAsync()
        {
            // Create tables if they do not exist
            try
            {
                await _databaseConnection.CreateTableAsync<User>();
                await _databaseConnection.CreateTableAsync<Category>();
                await _databaseConnection.CreateTableAsync<Transaction>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<int> InsertAsync<T>(T entity) where T : new()
        {
            return _databaseConnection.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : new()
        {
            return _databaseConnection.UpdateAsync(entity);
        }

        public Task<int> DeleteAsync<T>(T entity) where T : new()
        {
            return _databaseConnection.DeleteAsync(entity);
        }

        public Task<List<T>> QueryAsync<T>(string query, params object[] args) where T : new()
        {
            return _databaseConnection.QueryAsync<T>(query, args);
        }

        public Task<List<T>> GetAllAsync<T>() where T : new()
        {
            return _databaseConnection.Table<T>().ToListAsync();
        }
    }

}