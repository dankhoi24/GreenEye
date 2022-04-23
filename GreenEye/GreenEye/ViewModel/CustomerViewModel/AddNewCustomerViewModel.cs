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
        public NavigateStore NavigateStore { get; set; }
       public RelayCommand NavigateSubmitCommand { get; set; }
       public RelayCommand NavigateCancelCommand { get; set; }
        


        public AddNewCustomerViewModel(NavigateStore navigateStore)
        {
            NavigateStore = navigateStore;

            NavigateSubmitCommand = new RelayCommand(NavigateSubmit, null);
            NavigateCancelCommand = new RelayCommand(NavigateCacel, null);
        }

        private void NavigateSubmit(object obj)
        {
           NavigateStore.CurrentViewModel = new CustomerMangagementViewModel(NavigateStore);
        }
        private void NavigateCacel(object obj)
        {
            NavigateStore.CurrentViewModel = new CustomerMangagementViewModel(NavigateStore);
        }
    }
}
