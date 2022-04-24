using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class InventoryDAO
    {
          BookStoreContext Database = new BookStoreContext();

        public List<ReportInventory> getDate(DateTime date)
        {
            var data = Database.Inventories.Where(x => (( x.Date.Month == date.Month) &&( x.Date.Year == date.Year)));
            var price = data.Join(Database.Books, d => d.BookId, b => b.BookId, (d, b) => new { Name = b.Name, amount = d.Amount, b.ExportPrice });
            var resultEnd = price.GroupBy(x => x.Name).Select(y => new { Name = y.Key, endPrice = y.Sum(w => w.amount * w.ExportPrice) });

            DateTime previoudate = date.AddDays(-35);
            var previousdate =  Database.Inventories.Where(x => (( x.Date.Month == previoudate.Month) &&( x.Date.Year == previoudate.Year)));
            var previousprice = previousdate.Join(Database.Books, d => d.BookId, b => b.BookId, (d, b) => new { Name = b.Name, amount = d.Amount, b.ExportPrice });
            var resultStart = previousprice.GroupBy(x => x.Name).Select(y => new { Name = y.Key, startPrice = y.Sum(w => w.amount * w.ExportPrice) });

            return resultEnd.Join(resultStart, e => e.Name, s => s.Name,
                (e, s) => new ReportInventory { Name = e.Name, Init = s.startPrice, Incurred = e.endPrice - s.startPrice, Final = e.endPrice }).ToList();


            




        }

    }
}
