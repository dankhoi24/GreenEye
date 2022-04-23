using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class BookType
    {
        public int BookTypeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        //Navigation

        //Foreign Key
        public virtual List<Book> Books { get; set; }


        public BookType(int id, string name)
        {
            BookTypeId = id;
            Name = name;

        }

        public BookType()
        {

        }
    }
}
