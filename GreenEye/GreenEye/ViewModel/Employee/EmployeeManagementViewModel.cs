using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Employee
{
    public class EmployeeManagementViewModel : BaseViewModel
    {
        public EmployeeManagementViewModel(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;
        }

        public BaseViewModel CurrentViewModel { get; set; }
    }
}
