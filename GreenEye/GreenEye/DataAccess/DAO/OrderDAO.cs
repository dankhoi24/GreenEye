using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class OrderDAO
    {
        BookStoreContext Database = new BookStoreContext();

        internal void deleteOne(Order selectedOrder)
        {
            Order order = Database.Orders.Find(selectedOrder.OrderId);
            Database.Orders.Remove(order);
            Database.SaveChanges();
        }

        public ObservableCollection<Order> getAll()
        {
            var orderWithCustomerAndProductAndPromotion = Database.Orders.ToList();
            return new ObservableCollection<Order>(orderWithCustomerAndProductAndPromotion);
        }
        public int getCount()
        {
            return Database.Orders.Count();
        }

        internal Order insertOne(Order order)
        {
            Order r=Database.Orders.Add(order);
            Database.SaveChanges();
            return r;
        }

        internal Order findOne(int orderId)
        {
            return Database.Orders.Find(orderId);
        }

        internal void update(Order order)
        {
            var _oredr = Database.Orders.Find(order.OrderId);

            Database.Entry(_oredr).CurrentValues.SetValues(order);

            Database.SaveChanges();
        }

        internal ObservableCollection<Order> getAllBetwenDates(DateTime startSearchDate, DateTime endSearchDate)
        {
           List<Order> orders = Database.Orders.Where(x => x.Date>=startSearchDate && x.Date<=endSearchDate).ToList();

            return new ObservableCollection<Order>(orders);
        }
    }
}
