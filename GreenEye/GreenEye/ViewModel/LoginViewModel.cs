using GreenEye.DataAccess;
using GreenEye.DataAccess.DAO;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
                    var user = employeeDAO.getUser(Username);
                    if (!string.IsNullOrEmpty(user))
                    {
                        Password = decode(user.Split(' ')[0], user.Split(' ')[1]);
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
            string userPass = employeeDAO.getUser(Username);
            if (!string.IsNullOrEmpty(userPass)){

                string readPass = decode(userPass.Split(' ')[0], userPass.Split(' ')[1]);
                Debug.WriteLine("Compare " + readPass+" "+Password);
                Debug.WriteLine(Password);
               
                if(readPass == Password)
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
            Debug.WriteLine("NONO");





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
