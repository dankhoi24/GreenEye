using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.CustomerViewModel
{
    public class CustomerMangagementViewModel : BaseViewModel
    {
        public ObservableCollection<Customer> CustomerList { get; set; }
        public CustomerMangagementViewModel(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;

            CustomerDAO customerDAO = new CustomerDAO();

            CustomerList = customerDAO.getAll();
        }

        public BaseViewModel CurrentViewModel { get; set; }
    }
}
