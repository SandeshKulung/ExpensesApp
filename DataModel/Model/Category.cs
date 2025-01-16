using SQLite;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Model
{
    public class Category
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } = "Expense";
    }



}
