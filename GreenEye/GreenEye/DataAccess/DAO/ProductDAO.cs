using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;

using System.Diagnostics;

using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class ProductDAO
    {
        BookStoreContext Database = new BookStoreContext();

        public ProductDAO()
        {


        }


        public List<string> getCategory(string Type)
        {
            var type = Database.Books.Join(Database.BookTypes, b => b.BookTypeId, bt => bt.BookTypeId, (b, bt) => new { Book = b.Name, type = bt.Name });
            return type.Where(x => x.type == Type).Select(y => y.Book).ToList();
        }
        public List<Book> getContain(string text)
        {
            return Database.Books.Where(x => x.Name.Contains(text)).ToList();
        }

        public List<Book> getAll()
        {
            return Database.Books.Select(x => x).ToList();
        }

        public List<string> getName()
        {
            return Database.Books.Select(x => x.Name).ToList();
        }


        public int getCount()
        {
            return Database.Books.Count();
        }




        public List<Book> getBookYear()
        {

            List<Book> listBook = new List<Book>();

            var date = Database.Order_Books.Join(Database.Orders, ob => ob.OrderId, o => o.OrderId, (ob, o) => new { OBid = ob.Order_BookId,bookid= ob.BookId, amount =ob.Amount,date =  o.Date });

            var amount = date.Join(Database.Books, d => d.bookid, b => b.BookId, (d, b) => new { bookname = b.Name, amount = d.amount, date = d.date });


             var result = amount.Where(info => info.date.Year == DateTime.Today.Year);


                    

            List<Book> books = new List<Book>();

            Debug.WriteLine("!!!!!!!!");

            foreach (var day in result.GroupBy(info => info.bookname)
                            .Select(group => new {
                                Name = group.Key,
                                Amount = group.Sum(w => w.amount)
                            }))
            {
                
                     Console.WriteLine("{0} {1} {2}", day.Name, day.Amount,"hello");
                listBook.Add(new Book()
                {
                    Name = day.Name,
                    Sales = day.Amount
                    
                });
            }

            return listBook;

           


        }

        public List<Book> getBookMonth()
        {

            List<Book> listBook = new List<Book>();

            var date = Database.Order_Books.Join(Database.Orders, ob => ob.OrderId, o => o.OrderId, (ob, o) => new { OBid = ob.Order_BookId,bookid= ob.BookId, amount =ob.Amount,date =  o.Date });

            var amount = date.Join(Database.Books, d => d.bookid, b => b.BookId, (d, b) => new { bookname = b.Name, amount = d.amount, date = d.date });


             var result = amount.Where(info => info.date.Month == DateTime.Today.Month);

                    

            List<Book> books = new List<Book>();

            Debug.WriteLine("!!!!!!!!");

            foreach (var day in result.GroupBy(info => info.bookname)
                            .Select(group => new {
                                Name = group.Key,
                                Amount = group.Sum(w => w.amount)
                            }))
            {
                
                     Console.WriteLine("{0} {1} {2}", day.Name, day.Amount,"hello");
                listBook.Add(new Book()
                {
                    Name = day.Name,
                    Sales = day.Amount
                    
                });
            }

            return listBook;

           


        }












        public List<Book> getBookDay()
        {

            List<Book> listBook = new List<Book>();

            var date = Database.Order_Books.Join(Database.Orders, ob => ob.OrderId, o => o.OrderId, (ob, o) => new { OBid = ob.Order_BookId,bookid= ob.BookId, amount =ob.Amount,date =  o.Date });

            var amount = date.Join(Database.Books, d => d.bookid, b => b.BookId, (d, b) => new { bookname = b.Name, amount = d.amount, date = d.date });


             var result = amount.Where(info => info.date == DateTime.Today);

                    

            List<Book> books = new List<Book>();

            Debug.WriteLine("!!!!!!!!");

            foreach (var day in result.GroupBy(info => info.bookname)
                            .Select(group => new {
                                Name = group.Key,
                                Amount = group.Sum(w => w.amount)
                            }))
            {
                
                     Console.WriteLine("{0} {1} {2}", day.Name, day.Amount,"hello");
                listBook.Add(new Book()
                {
                    Name = day.Name,
                    Sales = day.Amount
                    
                });
            }

            return listBook;

           


        }



        public List<BookType> getGroupBrand()
        {

            List<BookType> result = new List<BookType>();


            foreach(var line in Database.BookTypes.GroupBy(info => info.Name)
                        .Select(group => new  { 
                             Metric = group.Key, 
                             Count = group.Count() 
                        })
                        .OrderBy(x => x.Metric))
                {
                    result.Add(new BookType()
                    {
                        Name = line.Metric,
                        BookTypeId = line.Count
                    }) ;
                     Console.WriteLine("{0} {1}", line.Metric, line.Count);
                }







          
            return result;
        }

        public List<Book> getBook(int count)
        {
            return Database.Books.Select(x => x).OrderByDescending(y => y.Sales).ToList();
        }

        public Book getOneByID (int Bookid)
        {
            return Database.Books.Find(Bookid);
        }

        internal ObservableCollection<Book> getAll()
        {
            List<Book> books = Database.Books.ToList();

            return new ObservableCollection<Book>(books);
        }
    }
}
