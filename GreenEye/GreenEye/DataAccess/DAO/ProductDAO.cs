using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
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
    }
}
