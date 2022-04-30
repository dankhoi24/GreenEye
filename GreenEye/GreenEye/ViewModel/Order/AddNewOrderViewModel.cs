
using GreenEye.Store;
using System.Collections.ObjectModel;

namespace GreenEye.ViewModel.Order
{
    using DataAccess.Domain;
    using GreenEye.DataAccess.DAO;
    using GreenEye.ViewModel.Command;
    using System;
    using System.Diagnostics;
    using System.Windows;

    public class AddNewOrderViewModel : BaseViewModel
    {
        //-----------check whether editing or adding------------
        private bool _isEditing = false;
        
        //---------------Init component---------------
        public NavigateStore NavigateStore { get; }
        public Order Order { get; }

        //-----------Command------------------
        public RelayCommand DeleteBookInOrderBookCommand { get; set; }
        public RelayCommand CalcSubtotalCommand { get; set; }
        public RelayCommand NavigateCancelCommand { get; set; }
        public RelayCommand NavigateSubmitCommand { get; set; }


        //-------Customer--------------
        //-----Debit Book-------------
        public DebitBook currentDebitBook { get; set; }
        private Customer _selectedSearchCustomer;
        public Customer SelectedSearchCustomer
        {
            get => _selectedSearchCustomer;
            set
            {
                if (value != null)
                {
                    DebitBookDAO debitBookDAO = new DebitBookDAO();
                   currentDebitBook = debitBookDAO.getCurrentDebitBook(value.CustomerId);

                    if (currentDebitBook.CurrentDebit >= 20000 && !_isEditing)
                    {
                        MessageBox.Show("Your debit is reaching 20000 vnd");
                    }
                    else
                    {
                        _selectedSearchCustomer = value;

                        if (currentDebitBook.DebitBookId == 0)
                            _selectedSearchCustomer.DebitBook.Add(currentDebitBook);

                        onPropertyChanged(nameof(SelectedSearchCustomer));
                    }
                }
            }
        }
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
        public decimal Total { 
            get 
            {
                decimal reducedMoney = ((Discount.PercentDiscount * Subtotal / 100) > Discount.MaxDiscount) ? Discount.MaxDiscount: Discount.PercentDiscount * Subtotal / 100 ;
                return Subtotal-reducedMoney; 
            } 
            set 
            {
                _total = value; 
                onPropertyChanged(nameof(Total)); 
            } 
        }

        //------------DisCount--------------

        private Promotion _discount;
        public Promotion Discount { 
            get 
            {
                PromotionDAO discountDAO = new PromotionDAO();

                _discount = discountDAO.getBestDiscount(Subtotal);
                return _discount; 
            } 
            set 
            { 
                _discount = value; 
                onPropertyChanged(nameof(Discount)); 
            } 
        }

        private Book _selectedSearchBook;
        public Book SelectedSearchBook { get => _selectedSearchBook; set
            {
                if (value != null) 
                {
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
        public string BookSearchBox 
        { 
            get => _bookSearchBox; 
            set
                {
                    Debug.WriteLine(value);
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

                    Debug.WriteLine(book.Name);
                }
            }
        }

        public AddNewOrderViewModel(NavigateStore navigateStore)
        {
            _isEditing = false;

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

           /* BookList = bookDAO.getAll();
            */

            //-------init customer--------

            CustomerSearchList = new ObservableCollection<Customer>();

            SelectedSearchCustomer = null;

            CustomerList = customerDAO.getAll();

            //------------init Command---------------
            DeleteBookInOrderBookCommand = new RelayCommand(deleteBookInOrderBook, null);
            CalcSubtotalCommand = new RelayCommand(calcSubtotal, null);
            NavigateCancelCommand = new RelayCommand(Cancel, null);
            NavigateSubmitCommand = new RelayCommand(SubmitAdd, null);

            NavigateStore = navigateStore;
        }

        public AddNewOrderViewModel(NavigateStore navigateStore, Order SendedOrder)
        {
            _isEditing = true;

            NavigateStore = navigateStore;
            //---------init order-----------
            Order = SendedOrder;

            //-------init book--------
            BookList = bookDAO.getAll();

            foreach (var book in BookList)
            {
                book.AmountInOrder = 0;
            }

            BookSearchList = new ObservableCollection<Book>();

            BookInOrderList = bookDAO.getAllByOrderID(SendedOrder.OrderId);


            //-------init customer--------

            CustomerSearchList = new ObservableCollection<Customer>();

            CustomerDAO customerDAO = new CustomerDAO();

            SelectedSearchCustomer = customerDAO.findOne(SendedOrder.CustomerId);

            CustomerList = customerDAO.getAll();


            //------------init Command---------------
            DeleteBookInOrderBookCommand = new RelayCommand(deleteBookInOrderBook, null);
            CalcSubtotalCommand = new RelayCommand(calcSubtotal, null);
            NavigateCancelCommand = new RelayCommand(Cancel, null);
            NavigateSubmitCommand = new RelayCommand(SubmitEdit, null);
        }

        private void SubmitEdit(object obj)
        {
            //Update old order
           

            Order_BookDAO order_BookDAO = new Order_BookDAO();
            RefundDAO refundDAO = new RefundDAO();
            CustomerDAO customerDAO = new CustomerDAO();
            DebitBookDAO debitBookDAO = new DebitBookDAO();

            Order oldOrder = orderDAO.findOne(Order.OrderId);

            Customer oldCustomer = customerDAO.findOne(oldOrder.CustomerId);

            //debitBookDAO.decreaseCurrentDebit(oldCustomer, oldOrder.Total);

            Order.CustomerId = SelectedSearchCustomer.CustomerId;
            Order.EmployeeId = 1;
            Order.PromotionId = Discount.PromotionId;

            order_BookDAO.deleteByOrder(Order.OrderId);
            refundDAO.deleteByOrder(Order.OrderId);

            orderDAO.update(Order);
           

            //Add current order
          
            foreach (Book book in BookList)
            {
                bookDAO.decreaseStock(book.BookId, book.AmountInOrder);
                Order_Book ob = new Order_Book() { OrderId = Order.OrderId, BookId = book.BookId, Amount = book.AmountInOrder };
                order_BookDAO.insertOne(ob);
            }

            

            currentDebitBook.CurrentDebit += Total;

            debitBookDAO.increaseCurrentDebit(SelectedSearchCustomer, currentDebitBook);
            NavigateStore.CurrentViewModel = new OredrManagementViewModel(NavigateStore);
        }

        private void SubmitAdd(object obj)
        {
            Order.CustomerId=SelectedSearchCustomer.CustomerId;
            this.Order.EmployeeId = 1;
            this.Order.PromotionId = Discount.PromotionId;
            Order _order= orderDAO.insertOne(Order);

            foreach (Book book in BookList)
            {
                bookDAO.decreaseStock(book.BookId, book.AmountInOrder);
                Order_Book ob = new Order_Book() { OrderId= _order.OrderId, BookId= book.BookId, Amount=book.AmountInOrder };
                order_BookDAO.insertOne(ob);
            }

            DebitBookDAO debitBookDAO = new DebitBookDAO();

            currentDebitBook.CurrentDebit += Total;

            debitBookDAO.increaseCurrentDebit(SelectedSearchCustomer, currentDebitBook);
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

      
    }
}
