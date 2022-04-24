using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GreenEye.DataAccess.Domain
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        //Naviagate
        public int BookId { get; set; }
        //Foreign Ket
        public virtual Book Book { get; set; }
        
    }
}
