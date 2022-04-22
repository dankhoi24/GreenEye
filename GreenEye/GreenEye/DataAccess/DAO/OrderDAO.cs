using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class OrderDAO
    {
        BookStoreContext Database = new BookStoreContext();

        public int getCount()
        {
            return Database.Orders.Count();
        }
    }
}
