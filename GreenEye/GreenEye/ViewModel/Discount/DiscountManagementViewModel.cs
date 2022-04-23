using GreenEye.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Discount
{
    public class DiscountManagementViewModel : BaseViewModel
    {
        public DiscountManagementViewModel(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;
        }

        public DiscountManagementViewModel(NavigateStore navigateStore)
        {
        }

        public BaseViewModel CurrentViewModel { get; set; }
    }
}
