using GreenEye.ViewModel.Command;
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
using GreenEye.DataAccess.DAO;

namespace GreenEye.ViewModel
{
    public class NavigateViewModel: BaseViewModel
    {


        private InventoryDAO _inventoryDAO = new InventoryDAO(); 

        public string Name { get; set; }
        public int UserId { get; set; }
        private BaseViewModel _formReceiptState { get; set; } = null;
        private BaseViewModel _formBillState { get; set; } = null;

        public RelayCommand OrderNavigateCommand { get; set; }
        public RelayCommand CustomerNavigateCommand { get; set; }
        public RelayCommand EmployeeNavigateCommand { get; set; }
        public RelayCommand DiscountNavigateCommand { get; set; }

        public NavigateStore NavigateStore { get; set; }
        public BaseViewModel CurrentViewModel => NavigateStore.CurrentViewModel;

        public BaseViewModel Mycurrent;
        


        public RelayCommand DashboardCommand { get; set; }
        public RelayCommand ProductCommand { get; set; }
        public RelayCommand ReportCommand{ get; set; }
        public RelayCommand FormCommand { get; set; }
        public RelayCommand SettingCommand { get; set; }

        public NavigateViewModel(string username)
        {
            _inventoryDAO.init();

            EmployeeDAO employeeDAO = new EmployeeDAO();
            Name = employeeDAO.getName(username);
            UserId = employeeDAO.getId(username);

            Mycurrent = new DashboardViewModel();
            DashboardCommand = new RelayCommand(goToDashBoard, null);
            ProductCommand = new RelayCommand(goToProduct, null);
            ReportCommand = new RelayCommand(goToReport, null);


            FormCommand = new RelayCommand(goToForm, null);
            NavigateStore = new NavigateStore();
            NavigateStore.CurrentViewModel = new DashboardViewModel(NavigateStore);

            NavigateStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            OrderNavigateCommand = new RelayCommand(OrderNavigate, null);
            CustomerNavigateCommand = new RelayCommand(CustomerNavigate, null);
            EmployeeNavigateCommand = new RelayCommand(EmployeeNavigate, null);
            DiscountNavigateCommand = new RelayCommand(DiscountNavigate, null);
            SettingCommand = new RelayCommand(settingCommand, null);

            }
    




        public void goToEditBill(BaseViewModel viewModel, int id)
        {
            NavigateStore.CurrentViewModel = new FormBillViewModel(viewModel, id); // edit constructor
        }
        public void saveBillState(BaseViewModel viewModel)
        {
            _formBillState = viewModel;
        }

        public void deleteBillState()
        {
            _formBillState = null;
        }

        public void gotoBillState()
        {
            (_formBillState as FormBillViewModel).initSuggest();
            NavigateStore.CurrentViewModel = _formBillState as FormBillViewModel;
        }
        public bool isExistBillState()
        {
            return _formBillState != null;
        }

        public void goToFormBill()
        {
            NavigateStore.CurrentViewModel = new FormBillViewModel(this);
        }

        public void goToListBill()
        {
            NavigateStore.CurrentViewModel = new FormBillManagementViewModel(this);
        }
        public void goToEditForm(int id)
        {
            NavigateStore.CurrentViewModel = new EditFormViewModel(this, id);
        }
        public void goTListFrmProduct()
        {
            NavigateStore.CurrentViewModel = new ListFormInputViewModel(this);
        }
        public void deleteProductState()
        {
            _formReceiptState = null;
        }
        public void goToFormProductState()
        {
            (_formReceiptState as FormInputBookViewModel).initSuggest();
            NavigateStore.CurrentViewModel = _formReceiptState as FormInputBookViewModel;

        }

        public void saveProductState(BaseViewModel viewmodel)
        {

            _formReceiptState = viewmodel;
        }

        public bool isExistFormState()
        {
            return _formReceiptState != null;
        }
        public void goToEditProduct(object x)
        {
            NavigateStore.CurrentViewModel = new ProductEditViewModel(x as BaseViewModel);

        }

        public void settingCommand(object x)
        {
            NavigateStore.CurrentViewModel = new SettingViewModel();

        }
            public void goToInputForm(object x)
        {
            NavigateStore.CurrentViewModel = new FormInputBookViewModel(this);
        }
            public void goToForm(object x)
        {

                NavigateStore.CurrentViewModel =  new FormViewmodel(this);
        }
            public void goToDashBoard(object x)
            {
                NavigateStore.CurrentViewModel = new DashboardViewModel();
            }


            public void goToProduct(object x)
            {
                 NavigateStore.CurrentViewModel = new ProductListViewModel(this);
            }

             public void goToAddProduct(object x)
            {
                NavigateStore.CurrentViewModel = new ProductAddViewModel(this);
            }

            public void goToReport(object x)
            {
                NavigateStore.CurrentViewModel =  new FormOptionViewModel(this);

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

            public void goToAddCustomer()
        {
            NavigateStore.CurrentViewModel = new AddNewCustomerViewModel(this);
        }

            private void OrderNavigate(object obj)
            {
                NavigateStore.CurrentViewModel = new OredrManagementViewModel(NavigateStore);
            }
            public void goToInventoryReport(object x)
            {
                 NavigateStore.CurrentViewModel = new ReportInventoryViewModel();
        }
         public void goToBillReport(object x)
            {
                 NavigateStore.CurrentViewModel = new ReportBillVIewModel();
        }


    }
}
