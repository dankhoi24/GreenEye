﻿using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;

using System.Diagnostics;

using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GreenEye.DataAccess.DAO
{
    public class ProductDAO
    {
        BookStoreContext Database = new BookStoreContext();

        public ProductDAO()
        {


        }

        public void DeleteProduct(Book book)
        {
            Database.Entry(book).State = EntityState.Deleted;
            Database.SaveChanges();
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

            return listBook.OrderByDescending(y => y.Sales).Take(10).ToList();


           


        }

        internal void increaseStock(int bookId, int amount)
        {
            Book initBook = Database.Books.Find(bookId);
            Book book = Database.Books.Find(bookId);

            book.Stroke = book.Stroke + amount;

            Database.Entry(initBook).CurrentValues.SetValues(book);
            Database.SaveChanges();
        }

        internal int getStock(int bookId)
        {
            return Database.Books.Find(bookId).Stroke;
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

            return listBook.OrderByDescending(y => y.Sales).Take(10).ToList();


           


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

            return listBook.OrderByDescending(y => y.Sales).Take(10).ToList();


           


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

        internal void decreaseStock(int bookId, int amountInOrder)
        {
            Book initBook = Database.Books.Find(bookId);
            Book book = Database.Books.Find(bookId);

            book.Stroke = book.Stroke - amountInOrder;

            Database.Entry(initBook).CurrentValues.SetValues(book);
            Database.SaveChanges();
        }

        public List<Book> getBook(int count)
        {
            return Database.Books.Select(x => x).OrderByDescending(y => y.Sales).Take(count).ToList();
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

        internal ObservableCollection<Book> getAllByOrderID(int orderId)
        {
            var books = Database.Order_Books.Where(x => x.OrderId == orderId)
                .Join(
                Database.Books
                , ob => ob.BookId,
                b => b.BookId,
                (ob, b) => new
                {
                    BookId = b.BookId,
                    Name = b.Name,
                    Publisher = b.Publisher,
                    Author = b.Author,
                    Date = b.Date,
                    Img = b.Img,
                    ImportPrice = b.ImportPrice,
                    ExportPrice = b.ExportPrice,
                    Stroke = b.Stroke,
                    Sales = b.Sales,
                    BookTypeId = b.BookTypeId,
                    OrderId = ob.OrderId,
                    AmountInOrder = ob.Amount
                })
                /*.Join(Database.Orders,
                oob => oob.OrderId,
                o => o.OrderId,
                (oob, o) => new
                {
                    BookId = oob.BookId,
                    Name = oob.Name,
                    Publisher = oob.Publisher,
                    Author = oob.Author,
                    Date = oob.Date,
                    Img = oob.Img,
                    ImportPrice = oob.ImportPrice,
                    ExportPrice = oob.ExportPrice,
                    Stroke = oob.Stroke,
                    Sales = oob.Sales,
                    BookTypeId = oob.BookTypeId,
                    AmountInOrder = oob.AmountInOrder
                })*/.ToList();

            var results = new ObservableCollection<Book>();

            foreach (var oob in books)
            {
                results.Add(new Book ()
                {
                    BookId = oob.BookId,
                    Name = oob.Name,
                    Publisher = oob.Publisher,
                    Author = oob.Author,
                    Date = oob.Date,
                    Img = oob.Img,
                    ImportPrice = oob.ImportPrice,
                    ExportPrice = oob.ExportPrice,
                    Stroke = oob.Stroke,
                    Sales = oob.Sales,
                    BookTypeId = oob.BookTypeId,
                    AmountInOrder = oob.AmountInOrder
                });
            }

            return results;
        }
    }
}
