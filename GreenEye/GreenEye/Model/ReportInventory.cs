using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.Model
{
    public class ReportInventory
    {
        public int BookId { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public decimal Init { get; set; }
        public decimal Incurred { get; set; }
        public decimal Final { get; set; }

    }
}
