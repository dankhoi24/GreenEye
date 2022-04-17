using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        
        //Navigation
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }

        public int PromotionId { get; set; }
        //Foreign key
        public virtual Customer Customer { get; set; }
        public virtual Refund Refund { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Promotion Promotion { get; set; }
        public virtual List<Order_Book> Order_Books { get; set; }
    }
}
