using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal ExportPrice { get; set; }
        public int Stroke { get; set; }
        public int Sales { get; set; }

        //Navigate
        public int BookTypeId { get; set; }
        
        //Foreign key
        public virtual List<GoodsReceipt_Book> GoodsReceipt_Books { get; set; }
        public virtual BookType BookType { get; set;}

        public virtual List<Order_Book> Order_Books { get; set; }
        
        
        

    }
}
