using GreenEye.DataAccess.Domain;
using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class GoodsReceiptDAO
    {
        private BookStoreContext _database = new BookStoreContext();


        public List<Book> getBook(int id)
        {
            List<Book> ListBook = new List<Book>();
           var data =  _database.GoodsReceipts.Where(i => i.GoodsReceiptId == id).Join(_database.GoodsReceipt_Books, d => d.GoodsReceiptId, b => b.GoodsReceiptId,
                (d, b) => new 
                {
                    Amount = b.Number,
                    BookId = b.BookId
                });

            var name = data.Join(_database.Books, d => d.BookId, b => b.BookId, (d, b) => new             {
                BookId = d.BookId,
                Name = b.Name,
                typeid = b.BookTypeId,
                Author = b.Author,
                Amount = d.Amount
            });

            foreach (var book in name.Join(_database.BookTypes, b => b.typeid, t => t.BookTypeId, (b, t) => new             {
                BookId = b.BookId,
                Name = b.Name,
                Type = t.Name,
                Author = b.Author,
                Amount = b.Amount


            }))
            {
                ListBook.Add(new Book()
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    Publisher = book.Amount.ToString(),
                    Author = book.Author,
                    Sales = book.Amount

                }) ;
            }

            return ListBook;



            
        }

        public void deleteItem(int id)
        {
            _database.GoodsReceipt_Books.RemoveRange(_database.GoodsReceipt_Books.Where(x => x.GoodsReceiptId == id));
            _database.SaveChanges();

            _database.GoodsReceipts.Remove(_database.GoodsReceipts.SingleOrDefault(x => x.GoodsReceiptId == id));
            _database.SaveChanges();

        }

        public DateTime getDate(int id)
        {
            return _database.GoodsReceipts.SingleOrDefault(x => x.GoodsReceiptId == id).Date;
        }
        public List<GoodsReceiptModel> getAll()
        {
            var data = _database.GoodsReceipts.Join(_database.Employees, g => g.EmployeeId, e => e.EmployeeId, (g, e) => new
            {
                Id = g.GoodsReceiptId,
                Date = g.Date,
                EmployeeName = e.Name,

            });

            var receipt = _database.GoodsReceipt_Books.GroupBy(x => x.GoodsReceiptId).Select(g => new
            {
                goodsId = g.Key,
                Amount = g.Sum(w => w.Number)

            }) ;
                
            var amount = data.Join(receipt, d => d.Id, g => g.goodsId, (d, g) => new
            {
                Id = d.Id,
                Date = d.Date,
                EmployeeName = d.EmployeeName,
                Amount = g.Amount
            });

           
            return amount.Select(x => new GoodsReceiptModel
            {
                Id = x.Id,
                Date = x.Date,
                EmployeeName = x.EmployeeName,
                Amount = x.Amount,

            }).ToList();

        


        }
    }
}
