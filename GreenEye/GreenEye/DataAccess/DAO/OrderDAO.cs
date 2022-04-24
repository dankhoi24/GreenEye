﻿using GreenEye.DataAccess.Domain;
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
            throw new NotImplementedException();
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
    }
}
