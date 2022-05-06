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

            //CurrentViewModel = new LoginViewModel(this);
           //CurrentViewModel = new NavigateViewModel("VDPhuc");

            CurrentViewModel = new LoginViewModel(this);

        }

        public void goToNavigate(string username)
        {
            CurrentViewModel = new NavigateViewModel(this,username);


        }

        public void goToLogin()
        {
            CurrentViewModel = new LoginViewModel(this);

        }
    }
}
