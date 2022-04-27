using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.Model
{

    public class GoodsReceiptModel
    
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public int Amount { get; set; }
    }
}
