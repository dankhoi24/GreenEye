using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class BillDAO
    {
        private BookStoreContext Database = new BookStoreContext();

        internal ObservableCollection<Bill> getAll()
        {
            var data = Database.Bills.Select(x => x).ToList();
            return new ObservableCollection<Bill>(data);
        }

        public DateTime getDate(int id)
        {
            return Database.Bills.SingleOrDefault(x => x.BillId == id).Date;
                
        }



        public decimal getMoney(int id)
        {
            return Database.Bills.SingleOrDefault(x => x.BillId == id).Price;
                
        }


        
        internal void deleteOne(Bill navSelectedBill)
        {
            throw new NotImplementedException();
        }
    }
}
