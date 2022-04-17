using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public decimal Price { get; set; }

        //Navigation
        public int CustomerId { get; set; }

        //Foreign Key
        public virtual Customer Customer { get; set; }
    }
}
