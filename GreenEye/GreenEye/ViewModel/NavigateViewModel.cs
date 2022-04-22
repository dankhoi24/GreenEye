using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class NavigateViewModel: BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; set; }

        public NavigateViewModel()
        {
            CurrentViewModel = new ProductAddViewModel();
        }
    }
}
