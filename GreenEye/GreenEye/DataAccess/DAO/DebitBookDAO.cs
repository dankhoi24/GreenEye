using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    class DebitBookDAO
    {

         BookStoreContext Database = new BookStoreContext();

        public List<ReportBill> getDate(DateTime date)
        {
            var data = Database.DebitBooks.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year);
            var customer = data.Join(Database.Customers, d => d.CustomerId, c => c.CustomerId, 
                (d, c) => new ReportBill { Name = c.Name, Init=d.BeginDebit, Incurred = d.CurrentDebit, Final =  d.CurrentDebit+ d.BeginDebit});

            return customer.ToList();
        }
    }
}
