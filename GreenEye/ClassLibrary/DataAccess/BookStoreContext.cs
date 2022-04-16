using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataAccess
{
    internal class BookStoreContext: DbContext
    {
       
        public DbSet<Book> Books { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<GoodsReceiptManagement> GoodsReceiptManagements { get; set; }

        public BookStoreContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<BookStoreContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
