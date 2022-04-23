using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class CustomerDAO
    {
        private BookStoreContext Database = new BookStoreContext();

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
    }
}
