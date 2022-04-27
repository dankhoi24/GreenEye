using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.Model
{
    public class BookFormModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public int Amount { get; set; }
    }
}
