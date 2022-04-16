using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class BookStoreContext: DbContext
    {
        public BookStoreContext(DbContextOptions options): base(options)
        {
            


        }

        public DbSet<Book> Books { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<GoodsReceiptManagement> GoodsReceiptManagements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=GreenEye;Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }

    } 
}
