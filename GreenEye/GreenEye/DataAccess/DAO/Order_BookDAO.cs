using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class Order_BookDAO
    {
        BookStoreContext Database = new BookStoreContext();

        internal ObservableCollection<Order_Book> getByBookId(int orderId)
        {
           List<Order_Book> obs = Database.Order_Books.Where(x => x.OrderId == orderId).ToList();
            return new ObservableCollection<Order_Book>(obs);
        }
    }
}
