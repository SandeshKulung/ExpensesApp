using SQLite;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Model
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }
        public int CategoryId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        public string? Type { get; set; }
    }
}
