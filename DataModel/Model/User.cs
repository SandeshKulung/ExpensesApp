using SQLite;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  

        public string Currency_Type { get; set; }
    }
}
