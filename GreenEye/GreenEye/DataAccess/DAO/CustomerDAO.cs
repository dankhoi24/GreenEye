using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    class CustomerDAO
    {
         BookStoreContext Database = new BookStoreContext();

        public int getCount()
        {
            return Database.Customers.Count();
        }

        public List<Customer> getCustomer(int count)
        {
            return Database.Customers.Select(x => x).OrderByDescending(t => t.Coin).Take(count).ToList();


        }


        public ObservableCollection<Customer> getAll()
        {
            var user = Database.Customers.ToList();

            return new ObservableCollection<Customer>(user);
           
        }

        public void insertOne (Customer customer)
        {
            Database.Customers.Add(customer);
            Database.SaveChanges();
        }

        public void updateOne(Customer customer)
        {
            var entity = Database.Customers.Find(customer.CustomerId);
            if (entity == null)
            {
                return;
            }

            Database.Entry(entity).CurrentValues.SetValues(customer);
            Database.SaveChanges();
        }

        internal void deleteOne(Customer customer)
        {
            customer = Database.Customers.Find(customer.CustomerId);

            var debitBooks = Database.DebitBooks.Where(x => x.CustomerId == customer.CustomerId).ToList();
            Database.DebitBooks.RemoveRange(debitBooks);

            var bills = Database.Bills.Where(x => x.CustomerId == customer.CustomerId).ToList();
            Database.Bills.RemoveRange(bills);

            var orders = Database.Orders.Where(x => x.CustomerId == customer.CustomerId).ToList();

            foreach (Order order in orders)
            {
                var refunds = Database.Refunds.Where(x => x.OrderId == order.OrderId).ToList();
                Database.Refunds.RemoveRange(refunds);
            }

            Database.Orders.RemoveRange(orders);

            Database.Customers.Remove(customer);
            Database.SaveChanges();
        }
    }
}
