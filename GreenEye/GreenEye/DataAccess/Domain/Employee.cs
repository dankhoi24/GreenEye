using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Role { get; set; }
        public decimal Salary { get; set; }

        // Login
        public string Username { get; set; }
        public string Password { get; set; }
        public string Entropy { get; set; }
        public bool Remember { get; set; }

        //Navigation

        //Foreign key
        [ForeignKey(nameof(EmployeeId))]
        public virtual List<Order> Orders { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public virtual List<GoodsReceipt> GoodsReceipts { get; set; }
    }
}
