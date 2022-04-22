using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Order
{
    public class OredrManagementViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; }
        public OredrManagementViewModel(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;
        }

        
    }
}
