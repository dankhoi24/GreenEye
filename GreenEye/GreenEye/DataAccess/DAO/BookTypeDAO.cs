using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.DAO
{
    public class BookTypeDAO
    {
        private BookStoreContext Database = new BookStoreContext();

        public List<string> getAll()
        {
            return Database.BookTypes.Select(x => x.Name).ToList();
        }


        public int getId(string name)
        {

            return Database.BookTypes.SingleOrDefault(x => x.Name == name).BookTypeId;

        }

        public string getName(int id)
        {
            return Database.BookTypes.SingleOrDefault(x => x.BookTypeId == id).Name;
        }

        public void insertType(string type)
        {
            Database.BookTypes.Add(new BookType()
            {
                Name = type,
            }) ;
            Database.SaveChanges();

        }
    }
}
