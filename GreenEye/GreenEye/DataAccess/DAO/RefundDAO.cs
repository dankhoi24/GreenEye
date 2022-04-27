using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    internal class RefundDAO
    {
        private BookStoreContext Database = new BookStoreContext();

        internal void deleteByOrder(int orderId)
        {
            List<Refund> refunds = Database.Refunds.Where(r => r.RefundId == orderId).ToList();  

            foreach (Refund refund in refunds)
                Database.Refunds.Remove(refund);
            Database.SaveChanges();
        }
    }
}
