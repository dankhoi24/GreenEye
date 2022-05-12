using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class InventoryDAO
    {
          BookStoreContext Database = new BookStoreContext();



        public void init()
        {
            // init book
            var books = Database.Books.Select(x => new { x.BookId, x.Stroke }).ToList();

            // check date
            DateTime date = DateTime.Parse("01/04/2022");

            for(int i =0; ; i++)
            {
                if (date.Month > DateTime.Now.Month) return;
                Debug.WriteLine(i);

                date = date.AddMonths(i);



                foreach (var entity in books)
                {
                    var inventory = Database.Inventories.SingleOrDefault(x => ((x.BookId == entity.BookId) && (x.Date.Month == date.Month) && (x.Date.Year == date.Year)));
                    if (inventory == null)
                    {
                        // add inventory
                        Database.Inventories.Add(new Domain.Inventory()
                        {
                            BookId = entity.BookId,
                            Amount = entity.Stroke,
                            Date = date,
                        });
                        Database.SaveChanges();
                    }
                    else
                    {
                        // Do nothing
                    }
                }


            }


        }


        public List<ReportInventory> getDate(DateTime date)
        {
            var data = Database.Inventories.Where(x => (( x.Date.Month == date.Month) &&( x.Date.Year == date.Year)));
            var price = data.Join(Database.Books, d => d.BookId, b => b.BookId, (d, b) => new {BookId = b.BookId,Img = b.Img, Name = b.Name, amount = d.Amount});
            var resultEnd = price.GroupBy(x => new {  x.BookId, x.Name, x.Img}).Select(y => new {
                Img = y.Key.Img,
                Name = y.Key.Name, endPrice = y.Sum(w => w.amount) , BookID = y.Key.BookId});



            DateTime previoudate = date.AddMonths(1);

            while (Database.Inventories.Where(x => (( x.Date.Month == previoudate.Month) &&( x.Date.Year == previoudate.Year))) == null)
            {
                previoudate = previoudate.AddMonths(1);
            }



            var previousdate =  Database.Inventories.Where(x => (( x.Date.Month == previoudate.Month) &&( x.Date.Year == previoudate.Year)));
            var previousprice = previousdate.Join(Database.Books, d => d.BookId, b => b.BookId, (d, b) => new { BookId = b.BookId, Name = b.Name, amount = d.Amount});
            var resultStart = previousprice.GroupBy(x => new { x.Name, x.BookId }).Select(y => new { Name = y.Key, startPrice = y.Sum(w => w.amount), BookID = y.Key.BookId });

            return resultEnd.Join(resultStart, e => e.BookID, s => s.BookID,
                (e, s) => new ReportInventory { 
                    BookId = e.BookID,
                    Img = e.Img,
                    Name = e.Name, Init = e.endPrice, Incurred = s.startPrice - e.endPrice, Final = s.startPrice }).ToList();

            ; 
        }

    }
}
