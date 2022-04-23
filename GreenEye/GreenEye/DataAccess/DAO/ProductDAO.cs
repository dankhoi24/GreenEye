using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class ProductDAO
    {
        BookStoreContext Database = new BookStoreContext();

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
