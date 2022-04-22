using GreenEye.ViewModel.Command;
using GreenEye.ViewModel.CustomerViewModel;
using GreenEye.ViewModel.Discount;
using GreenEye.ViewModel.Employee;
using GreenEye.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class NavigateViewModel: BaseViewModel
    {
        public RelayCommand OrderNavigateCommand { get; set; }
        public RelayCommand CustomerNavigateCommand { get; set; }
        public RelayCommand EmployeeNavigateCommand { get; set; }
        public RelayCommand DiscountNavigateCommand { get; set; }
        public BaseViewModel CurrentViewModel { get; set; }

        public NavigateViewModel()
        {
            CurrentViewModel = new DashboardViewModel(CurrentViewModel);

            OrderNavigateCommand = new RelayCommand(OrderNavigate, null);
            CustomerNavigateCommand = new RelayCommand(CustomerNavigate, null);
            EmployeeNavigateCommand = new RelayCommand(EmployeeNavigate, null);
            DiscountNavigateCommand = new RelayCommand(DiscountNavigate, null);

        }

        private void DiscountNavigate(object obj)
        {
            CurrentViewModel = new DiscountManagementViewModel(CurrentViewModel);
        }

        private void EmployeeNavigate(object obj)
        {
            CurrentViewModel = new EmployeeManagementViewModel(CurrentViewModel);
        }

        private void CustomerNavigate(object obj)
        {
            CurrentViewModel = new CustomerMangagementViewModel(CurrentViewModel);
        }

        private void OrderNavigate(object obj)
        {
            CurrentViewModel=new OredrManagementViewModel(CurrentViewModel);
        }
    }
}
