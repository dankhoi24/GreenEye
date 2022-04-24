using GreenEye.DataAccess.DAO;
<<<<<<< HEAD
using GreenEye.DataAccess.Domain;
using GreenEye.ViewModel.Command;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
=======
using GreenEye.Store;
>>>>>>> VDPhuc
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class DashboardViewModel: BaseViewModel
    {
        public int TotalProduct { get; set; }
<<<<<<< HEAD
        public int TotalOrder { get; set; }
        public int TotalCustomer { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Book> Products { get; set; }
        public SeriesCollection ChartData { get; set; } = new LiveCharts.SeriesCollection();


=======
        public BaseViewModel CurrentViewModel { get; }
        public NavigateStore NavigateStore { get; }
>>>>>>> VDPhuc

        private ProductDAO _productDAO = new ProductDAO();
        private OrderDAO _orderDAO = new OrderDAO();
        private CustomerDAO _customerDAO = new CustomerDAO();
        private EmployeeDAO _employeeDAO = new EmployeeDAO();
        


        // Command
        public RelayCommand DayCommand { get; set; }
        public RelayCommand MonthCommand { get; set; }
        public RelayCommand YearCommand { get; set; }



        // constructor
        public DashboardViewModel()
        {
            TotalProduct = _productDAO.getCount();
            TotalOrder = _orderDAO.getCount();
            TotalCustomer = _customerDAO.getCount();


            DayCommand = new RelayCommand(dayCommand, null);
            MonthCommand = new RelayCommand(monthCommand, null);
            YearCommand = new RelayCommand(yearCommand, null);

            Customers = new ObservableCollection<Customer>(_customerDAO.getCustomer(3));
            Employees = new ObservableCollection<Employee>(_employeeDAO.getAll());
            Products = new ObservableCollection<Book>(_productDAO.getBook(10));

            _productDAO.getBookDay();


            



            initChart();
        }



        //method
        

        public void dayCommand(object x)
        {
            Products = new ObservableCollection<Book>(_productDAO.getBookDay());

        }
         public void monthCommand(object x)
        {
            Products = new ObservableCollection<Book>(_productDAO.getBookMonth());

        }

         public void yearCommand(object x)
        {
            Products = new ObservableCollection<Book>(_productDAO.getBookYear());

        }



        public void initChart()
        {
            List<BookType> BookTypes = _productDAO.getGroupBrand();
            foreach(var booktype in BookTypes)
            {
                ChartData.Add(new PieSeries
                {
                    Title = booktype.Name,
                    Values =  new ChartValues<ObservableValue>{new ObservableValue(booktype.BookTypeId) },
                    DataLabels =true,


                }) ;


            }
           
        }

        public DashboardViewModel(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;
        }

        public DashboardViewModel(NavigateStore navigateStore)
        {
            NavigateStore = navigateStore;
        }
    }
}
