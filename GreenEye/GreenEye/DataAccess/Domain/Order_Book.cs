using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class Order_Book
    {
        public int Order_BookId{get;set;}
        public int Amount { get; set; }
        
        //Navigation
        public int BookId { get; set; }
        public int OrderId { get; set; }
        //Foreign key
        public Book Book { get; set; }
        public Order Order { get; set; }
        
    }
}
