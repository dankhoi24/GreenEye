using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GreenEye.ViewModel;
using System.Windows;
using GreenEye.DataAccess.DAO;
using GreenEye.Model;

namespace GreenEye.DataAccess.Domain
{
    public class Book:BaseViewModel
    {

        [Key]
        public int BookId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Publisher { get; set; }

        [StringLength(200)]
        public string Author { get; set; }

        public DateTime Date { get; set; }


        [StringLength(200)]
        public string Img { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal ExportPrice { get; set; }
        public int Stroke { get; set; }
        public int Sales { get; set; }

        // Binding amount in order att

        [NotMapped]
        public string AmountReceipt { get; set; }

        [NotMapped]
        public int _amountInOrder { get; set; }
        [NotMapped]
        public int AmountInOrder
        {
            get => _amountInOrder; set
            {

                SettingModel settingmodel = new SettingModel();
                settingmodel.readData();


                ProductDAO productDAO = new ProductDAO();
                if (value <= productDAO.getStock(BookId)- Int32.Parse (settingmodel.MinStoreSell))
                {
                    _amountInOrder = value;
                    onPropertyChanged(nameof(AmountInOrder));
                }
                else
                {
                    MessageBox.Show("Remaining product is not enough");
                }
            }
        }
        //Navigate
        public int BookTypeId { get; set; }

        //Foreign key
        [InverseProperty(nameof(GoodsReceipt_Book.Book))]
        public virtual List<GoodsReceipt_Book> GoodsReceipt_Books { get; set; }
        public virtual BookType BookType { get; set;}

        [InverseProperty(nameof(Order_Book.Book))]
        public virtual List<Order_Book> Order_Books { get; set; }

        [InverseProperty(nameof(Inventory.Book))]
        public virtual List<Inventory> Inventories { get; set; }
        
        
        

    }
}
