using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using GreenEye.Store;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.CustomerViewModel
{
    public class CustomerMangagementViewModel : BaseViewModel
    {
        //Command context menu
        public RelayCommand DeleteCustomerCommand { get; set; }
        public RelayCommand NavigateEditCustomerCommand { get; set; }
        public Customer SelectedCustomer { get; set; }
        //Phân trang thông số
        public int totalItem { get; set; } = 0;
        public int itemPerPage { get; set; } = 4;
        public int totalPage { get; set; } = 0;
        public int CurrentPage { get; set; }
        public ObservableCollection<Customer> CustomerPageList { get; set; }
        // Phaan trang command
        public RelayCommand NextCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand LastCommand { get; set; }
        //-------------------------
        public BaseViewModel CurrentViewModel { get; set; }
        public RelayCommand AddCustomerNavigateCommand { get; set; }
        public ObservableCollection<Customer> CustomerList { get; set; }

        public NavigateStore NavigateStore { get; set; }
        public CustomerMangagementViewModel(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;

            CustomerDAO customerDAO = new CustomerDAO();

            CustomerList = customerDAO.getAll();

            AddCustomerNavigateCommand = new RelayCommand(AddCustomerNavigate, null);
        }

        public void initPaging()
        {
            totalItem = CustomerList.Count();

            totalPage = totalItem / itemPerPage + (totalItem % itemPerPage == 0 ? 0 : 1);
            CurrentPage = 1;
            CustomerPageList = new ObservableCollection<Customer>(CustomerList
            .Skip((CurrentPage - 1) * itemPerPage)
            .Take(itemPerPage));
        }

        public CustomerMangagementViewModel(NavigateStore navigateStore)
        {
            this.NavigateStore = navigateStore;
            CustomerDAO customerDAO = new CustomerDAO();

            CustomerList = customerDAO.getAll();

            initPaging();   

            AddCustomerNavigateCommand = new RelayCommand(AddCustomerNavigate, null);
            NextCommand = new RelayCommand(goNext, null);
            PreviousCommand = new RelayCommand(goPrev, null);
            FirstCommand = new RelayCommand(goFirst, null);
            LastCommand = new RelayCommand(goLast, null);

            DeleteCustomerCommand = new RelayCommand(deleteCustomer, null);
            NavigateEditCustomerCommand = new RelayCommand(navigateEditCustomer, null);
        }

        private void navigateEditCustomer(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewCustomerViewModel(NavigateStore, SelectedCustomer);
        }

        private void deleteCustomer(object obj)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            customerDAO.deleteOne(SelectedCustomer);
            CustomerList.Remove(SelectedCustomer);
            if (CustomerPageList.Contains(SelectedCustomer))
                CustomerPageList.Remove(SelectedCustomer);
           
           
        }

        private void goLast(object obj)
        {
            Debug.WriteLine("-------------------------------Last-----------------------------------------");
            CurrentPage = totalPage;
            CustomerPageList = new ObservableCollection<Customer>(CustomerList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đé trang đầu
        private void goFirst(object obj)
        {
            Debug.WriteLine("-------------------------------First-----------------------------------------");
            Debug.WriteLine(totalPage);
            Debug.WriteLine(totalItem);
            Debug.WriteLine(CurrentPage);

            CurrentPage = 1;
            CustomerPageList = new ObservableCollection<Customer>(CustomerList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đến trang trước
        private void goPrev(object obj)
        {
            Debug.WriteLine("-------------------------------Prev-----------------------------------------");

            int temp = CurrentPage;
            if (temp == 1 || totalPage == 0)
            {
                return;
            }
            CurrentPage = (temp - 1);
            CustomerPageList = new ObservableCollection<Customer>(CustomerList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Prev-----------------------------------------");
        }
        //Đi đến trang sau
        private void goNext(object obj)
        {
            Debug.WriteLine("-------------------------------Next-----------------------------------------");

            int temp = CurrentPage;
            if (temp == totalPage || totalPage == 0)
            {
                return;
            }
            CurrentPage = (temp + 1);
            CustomerPageList = new ObservableCollection<Customer>(CustomerList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Next-----------------------------------------");
        }

        private void AddCustomerNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewCustomerViewModel(NavigateStore);
        }

       
    }
}
