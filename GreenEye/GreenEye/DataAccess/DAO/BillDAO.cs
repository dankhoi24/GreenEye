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
            throw new NotImplementedException();
        }

        internal void deleteOne(Bill navSelectedBill)
        {
            throw new NotImplementedException();
        }
    }
}
