using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class GoodsReceipt_Book
    {
        public int GoodsReceipt_BookId { get; set; }
        public int Number { get; set; }

        //Navigation
        public int BookId { get; set; }
        public int GoodsReceiptId { get; set; }

        //Foreign Key
        public Book Book { get; set; }
        public GoodsReceipt GoodsReceipt { get; set; }
    }
}
