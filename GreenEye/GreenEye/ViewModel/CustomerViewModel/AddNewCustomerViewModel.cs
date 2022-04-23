using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using GreenEye.Store;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GreenEye.ViewModel.CustomerViewModel
{
    public class AddNewCustomerViewModel : BaseViewModel
    {
        public Customer customer { get; set; }
        public string Name { get
            {
                return customer.Name;
            }
            set 
            { 
                customer.Name = value;
            } 
        }
        public string Phone 
        {
            get
            {
                return customer.Phone;
            }
            set
            {
                customer.Phone = value;
            }
        }
        public string Email
        {
            get
            {
                return customer?.Email;
            }
            set
            {
                customer.Email = value;
            }
        }
        public string Address
        {
            get
            {
                return customer?.Address;
            }
            set
            {
                customer.Address = value;
            }
        }
        public NavigateStore NavigateStore { get; set; }
       public RelayCommand NavigateSubmitCommand { get; set; }
       public RelayCommand NavigateCancelCommand { get; set; }

        public AddNewCustomerViewModel(NavigateStore navigateStore)
        {
            customer = new Customer();
            NavigateStore = navigateStore;
            NavigateSubmitCommand = new RelayCommand(NavigateSubmit, null);
            NavigateCancelCommand = new RelayCommand(NavigateCacel, null);
        }
        private void NavigateSubmit(object obj)
        {
           CustomerDAO customerDAO = new CustomerDAO();
           customerDAO.insertOne(customer);
           NavigateStore.CurrentViewModel = new CustomerMangagementViewModel(NavigateStore);
        }
        private void NavigateCacel(object obj)
        {
            NavigateStore.CurrentViewModel = new CustomerMangagementViewModel(NavigateStore);
        }
    }
}
