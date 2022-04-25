using GreenEye.DataAccess.Domain;
using GreenEye.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Order
{
    using DataAccess.Domain;
    using GreenEye.DataAccess.DAO;
    using GreenEye.ViewModel.Command;

    public class AddNewOrderViewModel : BaseViewModel
    {
        //---------------Init component---------------
        public NavigateStore NavigateStore { get; }
        public Order Order { get; }

        //-----------Command------------------
        public RelayCommand DeleteBookInOrderBookCommand { get; set; }
        public RelayCommand CalcSubtotalCommand { get; set; }
        public RelayCommand NavigateCancelCommand { get; set; }
        public RelayCommand NavigateSubmitCommand { get; set; }


        //-------Customer--------------
        public Customer SelectedSearchCustomer { get; set; }
        public ObservableCollection<Customer> CustomerSearchList { get; set; }
        public ObservableCollection<Customer> CustomerList { get; set; }
        private string _customerSearchBox;
        public string CustomerSearchBox { get => _customerSearchBox; set
            {
                _customerSearchBox = value;
                onPropertyChanged(nameof(CustomerSearchBox));
                searchCustomer();

            }
        }

        //---------------Book--------------
        private decimal _subtotal;
        public decimal Subtotal { get {
                decimal ss = 0;
                foreach (Book book in BookInOrderList)
                {
                    ss += book.AmountInOrder * book.ExportPrice;
                }
                return ss;
            }
            set
            {
                _subtotal = value;
                onPropertyChanged(nameof(Subtotal));
            }
        }
        private decimal _total;
        public decimal Total { get { return Subtotal-Discount; } set {_total = value; onPropertyChanged(nameof(Total)); } }

        private decimal _discount = 0;
        public decimal Discount { get { return _discount; } set { _discount = value; onPropertyChanged(nameof(Discount)); } }

        private Book _selectedSearchBook;
        public Book SelectedSearchBook { get => _selectedSearchBook; set
            {
                if (value != null) {
                    _selectedSearchBook = value;
                    onPropertyChanged(nameof(SelectedSearchBook));
                    BookInOrderList.Add(_selectedSearchBook);
                }
            }
            }
        public ObservableCollection<Book> BookSearchList { get; set; }
        public ObservableCollection<Book> BookList { get; set; }
        public ObservableCollection<Book> BookInOrderList { get; set; }
        private string _bookSearchBox;
        public string BookSearchBox { get => _bookSearchBox; set
            {
                _bookSearchBox = value;
                onPropertyChanged(nameof(BookSearchBox));

                searchBook();
            }
            }

        ProductDAO bookDAO = new ProductDAO();
        OrderDAO orderDAO = new OrderDAO();
        CustomerDAO customerDAO = new CustomerDAO();
        Order_BookDAO order_BookDAO = new Order_BookDAO();

        private void searchCustomer()
        {
            CustomerSearchList = new ObservableCollection<Customer>();
            foreach (var customer in CustomerList)
            {
                if (customer.Name.ToLower().Contains(CustomerSearchBox.ToLower()))
                {
                    CustomerSearchList.Add(customer);
                }
            }
        }

        private void searchBook()
        {
            BookSearchList = new ObservableCollection<Book>();
            foreach (var book in BookList)
            {
                if (book.Name.ToLower().Contains(BookSearchBox.ToLower()))
                {
                    BookSearchList.Add(book);
                }
            }
        }

        public AddNewOrderViewModel(NavigateStore navigateStore)
        {

            //---------init order-----------
            Order = new Order(); 

            //-------init book--------
            BookList = bookDAO.getAll();

            foreach (var book in BookList)
            {
                book.AmountInOrder = 0;
            }
            
            BookSearchList = new ObservableCollection<Book>();

            BookInOrderList=new ObservableCollection<Book>();

            SelectedSearchBook = null;

            CustomerList = customerDAO.getAll();

            //-------init customer--------

            CustomerSearchList = new ObservableCollection<Customer>();

            SelectedSearchCustomer = new Customer();

            BookList = bookDAO.getAll();

            //------------init Command---------------
            DeleteBookInOrderBookCommand = new RelayCommand(deleteBookInOrderBook, null);
            CalcSubtotalCommand = new RelayCommand(calcSubtotal, null);
            NavigateCancelCommand = new RelayCommand(Cancel, null);
            NavigateSubmitCommand = new RelayCommand(SubmitAdd, null);


            NavigateStore = navigateStore;
        }

        private void SubmitAdd(object obj)
        {
            Order.CustomerId=SelectedSearchCustomer.CustomerId;
            this.Order.EmployeeId = 1;
            this.Order.PromotionId = 1;
            Order _order= orderDAO.insertOne(Order);

            foreach (Book book in BookList)
            {
                bookDAO.decreaseStock(book.BookId, book.AmountInOrder);
                Order_Book ob = new Order_Book() { OrderId= _order.OrderId, BookId= book.BookId, Amount=book.AmountInOrder };
                order_BookDAO.insertOne(ob);
            }

            NavigateStore.CurrentViewModel = new OredrManagementViewModel(NavigateStore);
        }

        private void Cancel(object obj)
        {
            NavigateStore.CurrentViewModel = new OredrManagementViewModel(NavigateStore);
        }

        private void calcSubtotal(object obj)
        {
            decimal ss = 0;
            foreach (Book book in BookInOrderList)
            {
                ss += book.AmountInOrder * book.ExportPrice;
            }
            Subtotal = ss;
        }

        private void deleteBookInOrderBook(object obj)
        {
            BookInOrderList.Remove((Book)obj);
        }

        public AddNewOrderViewModel(NavigateStore navigateStore, Order SendedOrder)
        {
            NavigateStore = navigateStore;
            Order = SendedOrder;
        }

       
    }
}
