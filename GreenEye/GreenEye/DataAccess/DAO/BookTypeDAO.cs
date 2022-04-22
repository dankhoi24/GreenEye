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

    }
}
