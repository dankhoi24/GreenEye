using GreenEye.Store;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEye.DataAccess.Domain;

namespace GreenEye.ViewModel.Employee
{
    using GreenEye.DataAccess.DAO;
    using GreenEye.DataAccess.Domain;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    public class EmployeeManagementViewModel : BaseViewModel
    {
        //Command context menu
        public RelayCommand DeleteEmployeeCommand { get; set; }
        public RelayCommand NavigateEditEmployeeCommand { get; set; }
        public Employee SelectedEmployee { get; set; }
        //Phân trang thông số
        public int totalItem { get; set; } = 0;
        public int itemPerPage { get; set; } = 4;
        public int totalPage { get; set; } = 0;
        public int CurrentPage { get; set; }
        public ObservableCollection<Employee> EmployeePageList { get; set; }
        // Phaan trang command
        public RelayCommand NextCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand LastCommand { get; set; }
        //-------------------------
        public BaseViewModel CurrentViewModel { get; set; }
        public RelayCommand AddEmployeeNavigateCommand { get; set; }
        public ObservableCollection<Employee> EmployeeList { get; set; }

        public NavigateStore NavigateStore { get; set; }

        public void initPaging()
        {
            totalItem = EmployeeList.Count();

            totalPage = totalItem / itemPerPage + (totalItem % itemPerPage == 0 ? 0 : 1);
            CurrentPage = 1;
            EmployeePageList = new ObservableCollection<Employee>(EmployeeList
            .Skip((CurrentPage - 1) * itemPerPage)
            .Take(itemPerPage));
        }
        public EmployeeManagementViewModel(NavigateStore navigateStore)
        {
            this.NavigateStore = navigateStore;
            EmployeeDAO employeeDAO = new EmployeeDAO();

            EmployeeList = employeeDAO.getAll();

            initPaging();

            AddEmployeeNavigateCommand = new RelayCommand(AddEmployeeNavigate, null);
            NextCommand = new RelayCommand(goNext, null);
            PreviousCommand = new RelayCommand(goPrev, null);
            FirstCommand = new RelayCommand(goFirst, null);
            LastCommand = new RelayCommand(goLast, null);

            DeleteEmployeeCommand = new RelayCommand(deleteEmployee, null);
            NavigateEditEmployeeCommand = new RelayCommand(navigateEditEmployee, null);
        }

        private void navigateEditEmployee(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewEmployeeViewModel(NavigateStore, SelectedEmployee);
        }

        private void deleteEmployee(object obj)
        {
            EmployeeDAO employeeDAO = new EmployeeDAO();
            employeeDAO.deleteOne(SelectedEmployee);
            EmployeeList.Remove(SelectedEmployee);
            if (EmployeePageList.Contains(SelectedEmployee))
                EmployeePageList.Remove(SelectedEmployee);


        }

        private void goLast(object obj)
        {
            Debug.WriteLine("-------------------------------Last-----------------------------------------");
            CurrentPage = totalPage;
            EmployeePageList = new ObservableCollection<Employee>(EmployeeList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đé trang đầu
        private void goFirst(object obj)
        {
            Debug.WriteLine("-------------------------------First-----------------------------------------");
            Debug.WriteLine(totalPage);
            Debug.WriteLine(totalItem);
            Debug.WriteLine(CurrentPage);

            CurrentPage = 1;
            EmployeePageList = new ObservableCollection<Employee>(EmployeeList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
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
            EmployeePageList = new ObservableCollection<Employee>(EmployeeList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

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
            EmployeePageList = new ObservableCollection<Employee>(EmployeeList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Next-----------------------------------------");
        }

        private void AddEmployeeNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewEmployeeViewModel(NavigateStore);
        }


    }
}
