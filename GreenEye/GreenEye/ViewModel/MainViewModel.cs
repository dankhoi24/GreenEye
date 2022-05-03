using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; set; }

        public MainViewModel()
        {
<<<<<<< HEAD
            //CurrentViewModel = new LoginViewModel(this);
            CurrentViewModel = new NavigateViewModel("ndkhoi");
=======
            CurrentViewModel = new LoginViewModel(this);
>>>>>>> NDKhoi
        }

        public void goToNavigate(string username)
        {
            CurrentViewModel = new NavigateViewModel(username);


        }
    }
}
