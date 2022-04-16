using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class GoodsReceipt
    {
          public int Id { get; set; }
          public DateTime DateImport { get; set; }

        //Navigation
          public List<GoodsReceiptManagement> GoodsReceiptManagements { get; set; }

    }
}
