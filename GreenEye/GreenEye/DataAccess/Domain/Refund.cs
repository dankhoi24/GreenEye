using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GreenEye.DataAccess.Domain
{
    public class Refund
    {
        public int RefundId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }


        //Navigation
        public int OrderId { get; set; }
        //Foreign key
        [Required]
        public virtual Order Order { get; set; }
    }
}
