using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;



namespace DataModel.Model
{
    public class ExpenseDbContext:DbContext
    {
        public ExpenseDbContext(DbContextOptions options): base(options)
        { } 

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }



    }
}
