using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
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

    }
}
