using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenEye.DataAccess.Domain
{
    public class DebitBook
    {

        public int DebitBookId { get; set; }
        public decimal BeginDebit { get; set; }
        public decimal CurrentDebit { get; set; }
        public DateTime Date { get; set; }
        
        // Navigation
        public int CustomerId { get; set; }
        //Foreign key

        [Required]
        public virtual Customer Customer { get; set; }
    }
}
