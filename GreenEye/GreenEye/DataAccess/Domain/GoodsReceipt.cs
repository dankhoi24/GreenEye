using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class GoodsReceipt
    {
        public int GoodsReceiptId { get; set; }
        public DateTime Date { get; set; }


        //Navigattion
        [Required]
        public int EmployeeId { get; set; }
        //Foreign Key
        public virtual List<GoodsReceipt_Book> GoodsReceipt_Books { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
