using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.DataAccess.Domain
{
    public class Bill
    {
        public int BillId { get; set; }
        public decimal Price { get; set; }

        //Navigation
        public int CustomerId { get; set; }

        //Foreign Key
        public virtual Customer Customer { get; set; }
    }
}
