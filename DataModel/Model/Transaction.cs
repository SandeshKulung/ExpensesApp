using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Model
{
    public  class Transaction
    {
        [Key]
        public int TransactionId {  get; set; }

        public int Category_Id { get; set; } 

        public Category Category { get; set; }

        //Accessing the categories Id from category.cs

        public int Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;


        //Not Mandatory Field

        [Column(TypeName = "nvarchar(100)")]
        public string ? Note { get; set; }



    }
}
