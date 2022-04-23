using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;

using System.Diagnostics;

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
