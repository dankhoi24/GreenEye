using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess
{
    public class BookStoreContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<GoodsReceipt_Book> GoodsReceipt_Books { get; set; }

        public DbSet<BookType> BookTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<DebitBook> DebitBooks { get; set; }

        public DbSet<Refund> Refunds { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Order_Book> Order_Books { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Inventory> Inventories { get; set; }


        public BookStoreContext(): base("name = ConnectionString")
        {
            Database.SetInitializer<BookStoreContext>(new DropCreateDatabaseIfModelChanges<BookStoreContext>());
        }

    }
}
