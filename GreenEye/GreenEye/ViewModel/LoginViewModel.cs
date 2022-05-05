using GreenEye.DataAccess;
using GreenEye.DataAccess.DAO;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {


        private BaseViewModel _viewModel { get; set; }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                onPropertyChanged(nameof(Username));
                getSearchSuggest();
                VisibilitySuggest = "Visible";
            }
        }

        public string Password { get; set; }

        public bool IsRemember { get; set; }

        public string VisibilityWarining { get; set; }

        public string VisibilitySuggest { get; set; }

        private string _itemSelected;
        public string ItemSelected
        {
            get
            {
                return _itemSelected;
            }
            set
            {
                _itemSelected = value;

                onPropertyChanged(nameof(ItemSelected));
                if (_itemSelected != null)
                {
                    Username = _itemSelected;
                    DataAccess.Domain.Employee employee = employeeDAO.getByUsername(Username);
                    if (employee != null)
                    {
                        Password = employee.Phone;
                    }
                }

                VisibilitySuggest = "Hidden";

            }
        }

        public ObservableCollection<string> AllUserName { get; set; }
        public ObservableCollection<string> SuggestUsername { get; set; }


        private EmployeeDAO employeeDAO = new EmployeeDAO();
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand MouseDownCommand { get; set; }

        public LoginViewModel(BaseViewModel viewmodel)
        {
            LoginCommand = new RelayCommand(loginCommand,null);
            MouseDownCommand = new RelayCommand(mouseDownCommand, null);
            VisibilityWarining = "Hidden";
            VisibilitySuggest = "Hidden";
            AllUserName  = employeeDAO.getSuggestUsername();

            _viewModel = viewmodel as MainViewModel;
                



        }



        //method


        private void getSearchSuggest()
        {

            SuggestUsername = new ObservableCollection<string>();
            foreach(var str in AllUserName)
            {
                Debug.WriteLine("User " + Username);
                if (str.ToLower().Contains(Username.ToLower()))
                {
                    SuggestUsername.Add(str);

                }

            }

                
        }

        private void mouseDownCommand(object x)
        {
            VisibilitySuggest = "Hidden";
        }

        private void loginCommand(object x)
        {
            VisibilitySuggest = "Hidden";
            DataAccess.Domain.Employee employee = employeeDAO.getByUsername(Username);
            if (employee!=null){
               
                if(BCrypt.Net.BCrypt.Verify(Password, employee.Password))
                {
                    // Success
                    Debug.WriteLine("OKOK");


                    if(IsRemember == true)
                    {
                        employeeDAO.updateUserRemember(Username);
                    }

                    (_viewModel as MainViewModel).goToNavigate(Username);


                    return;
                }
            }

            VisibilityWarining = "Visible";
         

        }

    }
}
