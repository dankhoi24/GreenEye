using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public decimal MinPrice { get; set; }
        public int PercentDiscount { get; set; }
        public int MaxDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Navigation

        //Foreign Key
        public virtual List<Order> Orders { get; set; }
    }
}
