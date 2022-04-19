using GreenEye.DataAccess;
using GreenEye.DataAccess.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Data.Entity;
using System.Diagnostics;

namespace GreenEye
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookStoreContext DatabaseContext = new BookStoreContext();
        public MainWindow()
        {
            InitializeComponent();
            if (DatabaseContext.BookTypes.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/BookType.json");
                var data = JsonConvert.DeserializeObject<List<BookType>>(file);
                DatabaseContext.BookTypes.AddRange(data);
                DatabaseContext.SaveChanges();
            }
                    
            if (DatabaseContext.Employees.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/Employee.json");
                var data = JsonConvert.DeserializeObject<List<Employee>>(file);
                DatabaseContext.Employees.AddRange(data);
                DatabaseContext.SaveChanges();
            }
            
            if (DatabaseContext.Customers.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/Customer.json");
                var data = JsonConvert.DeserializeObject<List<Customer>>(file);
                DatabaseContext.Customers.AddRange(data);
                DatabaseContext.SaveChanges();
            }


            if (DatabaseContext.Promotions.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/Promotion.json");
                var data = JsonConvert.DeserializeObject<List<Promotion>>(file);
                DatabaseContext.Promotions.AddRange(data);
                DatabaseContext.SaveChanges();
            }


             if (DatabaseContext.Orders.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/Order.json");
                var data = JsonConvert.DeserializeObject<List<Order>>(file);
                DatabaseContext.Orders.AddRange(data);
                DatabaseContext.SaveChanges();
            }



               if (DatabaseContext.Books.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/Book.json");
                var data = JsonConvert.DeserializeObject<List<Book>>(file);
                DatabaseContext.Books.AddRange(data);
                DatabaseContext.SaveChanges();
            }

                if (DatabaseContext.Order_Books.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/Order_Book.json");
                var data = JsonConvert.DeserializeObject<List<Order_Book>>(file);
                DatabaseContext.Order_Books.AddRange(data);
                DatabaseContext.SaveChanges();
            }

                    if (DatabaseContext.GoodsReceipts.Count() == 0)
            {
                string file = File.ReadAllText("ImportData/GoodsReceipt.json");
                var data = JsonConvert.DeserializeObject<List<GoodsReceipt>>(file);
                DatabaseContext.GoodsReceipts.AddRange(data);
                DatabaseContext.SaveChanges();
            }

            var x = DatabaseContext.Books.AsEnumerable().Select(
                (sth) =>
                {
                    return sth.Name;
                }
                );

            foreach(var y in x)
            {
                Debug.WriteLine(y);
            }





        }
    }
}
