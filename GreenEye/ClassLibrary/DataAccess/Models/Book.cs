using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DatePublished { get; set; }
        public decimal PriceImport { get;set; }
        public decimal Price { get; set; }
        public int Stroke { get; set; }
        public int Sale { get;set;}

        // Navigation
        public List<GoodsReceiptManagement> GoodsReceiptManagements { get; set; }   
    }
}
