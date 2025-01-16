using DataAccess.Services.Interface;
using DataModel.Model;
using SQLite;

namespace DataAccess.Services
{

    public class UserAccess: IUserAccess
    {
        private SQLiteAsyncConnection _dbConnection;
        public UserAccess()
        {
            SetUpDb();
        }

        private async void SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<User>();
            }
        }
        public async Task<int> AddUser(User user)
        {
            return await _dbConnection.InsertAsync(user);
        }

        public async Task<int> DeleteUser(User user)
        {
            return await _dbConnection.DeleteAsync(user);
        }
        public async Task<int> UpdateUser(User user)
        {
            return await _dbConnection.UpdateAsync(user);
        }
        public async Task<List<User>> GetAllUser()
        {
            return await _dbConnection.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserByID(int UserId)
        {
            var student = await _dbConnection.QueryAsync<User>($"Select * From {nameof(User)} where UserId={UserId} ");
            return student.FirstOrDefault();
        }
    }
}
