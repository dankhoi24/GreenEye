using GreenEye.DataAccess;
using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GreenEye.ViewModel
{
    public class FormInputBookViewModel : BaseViewModel
    {


        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddProduct { get; set; }
        public RelayCommand DeleteCommand { get; set; }


        private BaseViewModel _viewmodel { get; set; }
        public ObservableCollection<Book> Suggest { get; set; }
        public ObservableCollection<Book> AllBooks { get; set; }
        public ObservableCollection<Book> Products { get; set; }

        public Book _selectedSuggest;



        public DateTime Date { get; set; }
        public Book SelectedSuggest
        {
            get
            {
                return _selectedSuggest;
            }
            set
            {
                _selectedSuggest = value;
                onPropertyChanged(nameof(SelectedSuggest));
                if (SelectedSuggest != null)
                {
                    SelectedSuggest.Publisher = "1";

                    if (Products.Contains(SelectedSuggest))
                    {

                        MessageBox.Show("This item has been selected !!!");

                    }
                    else
                    {

                        Products.Add(SelectedSuggest);
                    }

                }

                Visibility = "Hidden";


                foreach (var x in Products)
                {
                    Debug.WriteLine(x.Name + " " + x.Publisher);
                }
            }

        }
        public string Visibility { get; set; }



        private string _searching;
        public string Searching
        {
            get
            {
                return _searching;
            }
            set
            {
                _searching = value;
                onPropertyChanged(nameof(Searching));
                getSuggest();
                Visibility = "Visible";

            }
        }

        ProductDAO _productDAO = new ProductDAO();



        private void deleteCommand(object x)
        {
            Products.Remove(x as Book);
        }
        private void cancelCommand(object x)
        {

            (_viewmodel as NavigateViewModel).goTListFrmProduct();
        }

        public void initSuggest()
        {
            
            AllBooks = new ObservableCollection<Book>(_productDAO.getAll());
        }

        public FormInputBookViewModel(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            Suggest = new ObservableCollection<Book>(_productDAO.getAll());
            initSuggest();
            Visibility = "Hidden";

            Products = new ObservableCollection<Book>();
            Date = DateTime.Now;
            SubmitCommand = new RelayCommand(submitCommand, null);
            AddProduct = new RelayCommand(addProduct, null);
            CancelCommand = new RelayCommand(cancelCommand, null);
            DeleteCommand = new RelayCommand(deleteCommand, null);


                     
        }


        // method 

        private void addProduct(object x)
        {
            (_viewmodel as NavigateViewModel).saveProductState(this);
            (_viewmodel as NavigateViewModel).goToAddProduct(_viewmodel);
        }

        private List<GoodsReceipt_Book> getListBook()
        {
            List<GoodsReceipt_Book> result = new List<GoodsReceipt_Book>();

            foreach(var book in Products)
            {
                GoodsReceipt_Book temp = new GoodsReceipt_Book()
                {
                    BookId = book.BookId,
                    Number = Int32.Parse( book.Publisher)
                };
                result.Add(temp);
            }
            return result;
        }
    

        private void submitCommand(object x)
        {

            if (Products.Count() == 0)
            {
                MessageBox.Show("Please choose product");
                return;
            }
            BookStoreContext db = new BookStoreContext();
            db.GoodsReceipts.Add(new GoodsReceipt()
            {
                EmployeeId = (_viewmodel as NavigateViewModel).UserId,
                Date = this.Date,
                GoodsReceipt_Books = getListBook()
            }) ;
            db.SaveChanges();

            (_viewmodel as NavigateViewModel).deleteProductState();
            MessageBox.Show("Add Succeeded");

            (_viewmodel as NavigateViewModel).goTListFrmProduct();


        }

       
        public void  getSuggest()
        {
            if (string.IsNullOrEmpty(Searching))
            {
                Suggest = AllBooks;
            }

             Suggest = new ObservableCollection<Book>();
            foreach(var str in AllBooks)
            {
                               Debug.WriteLine("User " + AllBooks);
                if (str.Name.ToLower().Contains(Searching.ToLower()))
                {
                    Suggest.Add(str);

                }


            }

        }

    }
}
