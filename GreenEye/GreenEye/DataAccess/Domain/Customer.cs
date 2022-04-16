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
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public int Coin { get; set; }

        //Navigate

        //Foreign key
        public virtual DebitBook DebitBook { get; set; }
        public virtual List<Bill> Bills{ get;set; }

        public virtual List<Order> Orders { get; set; }

    }
}
