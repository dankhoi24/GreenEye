using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        public int Coin { get; set; }

        //Navigate

        //Foreign key
        public virtual DebitBook DebitBook { get; set; }
        public virtual List<Bill> Bills{ get;set; }

        public virtual List<Order> Orders { get; set; }

    }
}
