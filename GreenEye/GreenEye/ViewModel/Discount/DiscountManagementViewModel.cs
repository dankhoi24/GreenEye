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

        public BaseViewModel CurrentViewModel { get; set; }
    }
}
