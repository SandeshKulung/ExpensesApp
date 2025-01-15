using System.Text.Json;
using DataModel.Model;

namespace DataModel.Abstraction
{
    public abstract class UserBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "users.json");

        protected List<User> LoadUsers()
        {
            if (!File.Exists(FilePath)) return new List<User>();

            var json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        // Method to save user data to the users.json file
        protected void SaveUsers(List<User> users)
        {
            // Serialize the list of User objects into a JSON string
            var json = JsonSerializer.Serialize(users);

            // Write the JSON string to the users.json file
            File.WriteAllText(FilePath, json);  
        }
    }
}
