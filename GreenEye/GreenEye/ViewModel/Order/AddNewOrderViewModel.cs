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
            //-------init book--------
            BookList = bookDAO.getAll();
            
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


            NavigateStore = navigateStore;
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
