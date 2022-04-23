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

        public CustomerMangagementViewModel(NavigateStore navigateStore)
        {
            this.NavigateStore = navigateStore;
            CustomerDAO customerDAO = new CustomerDAO();

            CustomerList = customerDAO.getAll();

            AddCustomerNavigateCommand = new RelayCommand(AddCustomerNavigate, null);
        }

        private void AddCustomerNavigate(object obj)
        {
            Debug.WriteLine("--------------------------------------------------");

            NavigateStore.CurrentViewModel = new AddNewCustomerViewModel(NavigateStore);
        }

       
    }
}
