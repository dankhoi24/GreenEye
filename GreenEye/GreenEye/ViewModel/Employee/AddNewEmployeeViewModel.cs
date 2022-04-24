using GreenEye.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Employee
{
    using DataAccess.Domain;
    using GreenEye.DataAccess.DAO;
    using GreenEye.ViewModel.Command;

    public class AddNewEmployeeViewModel : BaseViewModel
    {
        public Employee Employee { get; set; }
        public int EmployeeId { get => Employee.EmployeeId; set
            {
                Employee.EmployeeId = value;
            }
            }
       
        public string Name { get => Employee.Name; set { 
                Employee.Name = value;
            } }
       
        public string Phone { get =>Employee.Phone; set
            {
                Employee.Phone = value;
            }
        }
       
        public string Address { get => Employee.Address; set
            {
                Employee.Address = value;
            } }
    
        public string Role { get => Employee.Role; set
            {
                Employee.Role = value;
            }
            }
        public decimal Salary { get => Employee.Salary; set
            {
                Employee.Salary= value;
            }
        }
        public NavigateStore NavigateStore { get; set; }
        public RelayCommand NavigateSubmitCommand { get; set; }
        public RelayCommand NavigateCancelCommand { get; set; }

        public AddNewEmployeeViewModel(NavigateStore navigateStore)
        {
            NavigateStore = navigateStore;
            Employee = new Employee();
            NavigateSubmitCommand = new RelayCommand(NavigateSubmitAdd, null);
            NavigateCancelCommand = new RelayCommand(NavigateCacel, null);
        }

        private void NavigateCacel(object obj)
        {
            NavigateStore.CurrentViewModel = new EmployeeManagementViewModel(NavigateStore);
        }

        private void NavigateSubmitAdd(object obj)
        {
            EmployeeDAO employeeDAO = new EmployeeDAO();
            employeeDAO.insertOne(Employee);
            NavigateStore.CurrentViewModel = new EmployeeManagementViewModel(NavigateStore);
        }

        public AddNewEmployeeViewModel(NavigateStore navigateStore, Employee selectedEmployee)
        {
            NavigateStore = navigateStore;
            Employee = selectedEmployee;
            NavigateSubmitCommand = new RelayCommand(NavigateEditAdd, null);
            NavigateCancelCommand = new RelayCommand(NavigateCacel, null);
        }

        private void NavigateEditAdd(object obj)
        {
            EmployeeDAO employeeDAO = new EmployeeDAO();
            employeeDAO.updateOne(Employee);
            NavigateStore.CurrentViewModel = new EmployeeManagementViewModel(NavigateStore);
        }
    }
}
