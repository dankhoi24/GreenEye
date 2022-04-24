using GreenEye.ViewModel.Command;
using System;
﻿using GreenEye.Store;
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


        public RelayCommand DashboardCommand { get; set; }
        public RelayCommand ProductCommand { get; set; }
        public RelayCommand ReportCommand{ get; set; }

        public NavigateStore NavigateStore { get; set; }
        public BaseViewModel CurrentViewModel => NavigateStore.CurrentViewModel;
        

        public NavigateViewModel()
        {
            NavigateStore = new NavigateStore();
            NavigateStore.CurrentViewModel = new DashboardViewModel(NavigateStore);

            NavigateStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            OrderNavigateCommand = new RelayCommand(OrderNavigate, null);
            CustomerNavigateCommand = new RelayCommand(CustomerNavigate, null);
            EmployeeNavigateCommand = new RelayCommand(EmployeeNavigate, null);
            DiscountNavigateCommand = new RelayCommand(DiscountNavigate, null);

        }

        private void OnCurrentViewModelChanged()
        {
            onPropertyChanged(nameof(CurrentViewModel));
        }

        private void DiscountNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new DiscountManagementViewModel(NavigateStore);
        }

        private void EmployeeNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new EmployeeManagementViewModel(NavigateStore);
        }

        private void CustomerNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new CustomerMangagementViewModel(NavigateStore);
        }

        private void OrderNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new OredrManagementViewModel(NavigateStore);
        }
    }
}
