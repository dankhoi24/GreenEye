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
    using System.Diagnostics;
    using System.Security.Cryptography;

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

        private string getUsername()
        {

            List<string> name = Employee.Name.Split(' ').ToList();
            string username = "";

            for (int i = 0 ; i < name.Count() ;i ++)
            {
                if (i == name.Count() - 1)
                {
                    username += name[i];
                }
                else
                {
                    username += name[i][0];
                }
            }

            return username;

        }

        private void NavigateSubmitAdd(object obj)
        {
            EmployeeDAO employeeDAO = new EmployeeDAO();
            Employee.Username = getUsername();
            string pass = encode(Employee.Phone);
            Employee.Password =pass.Split(' ')[0];
            Employee.Entropy = pass.Split(' ')[1];
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






         // encode and decode password
        private string encode(string password)
        {

             var passwordInBytes = Encoding.UTF8.GetBytes(password);

            var entropy = new byte[20];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            var entropyBase64 = Convert.ToBase64String(entropy);








            var cypherText = ProtectedData.Protect(passwordInBytes, entropy,
                DataProtectionScope.CurrentUser);

            var cypherTextBase64 = Convert.ToBase64String(cypherText);

            Debug.WriteLine(cypherTextBase64);
            Debug.WriteLine(entropyBase64);
            Debug.WriteLine("=========================");

            return cypherTextBase64 + " " + entropyBase64;




        }

        private string decode(string cypherTextBase64, string entropyBase64)
        {
             var cypherTextInBytes = Convert.FromBase64String(cypherTextBase64);

            var entropyTextInBytes = Convert.FromBase64String(entropyBase64);

            var passwordInBytesR = ProtectedData.Unprotect(cypherTextInBytes,
                entropyTextInBytes, DataProtectionScope.CurrentUser);

            var result = Encoding.UTF8.GetString(passwordInBytesR);
            Debug.WriteLine(result);
            return result;

        }

    }
}
