using GreenEye.DataAccess;
using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using GreenEye.Model;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GreenEye.ViewModel
{
    public class FormBillViewModel: BaseViewModel
    {

        private BaseViewModel _viewmodel { get; set; }
        private CustomerDAO _customerDAO = new CustomerDAO();
        private BillDAO _billDAO = new BillDAO();
        private DebitBookDAO _debitBookDAO = new DebitBookDAO();

        public ObservableCollection<Customer> Suggest { get; set; }
        
        public ObservableCollection<Customer> AllCustomer { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }
        public DateTime Date { get; set; }
        public Decimal Money { get; set; }
        public RelayCommand MouseDownCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand AddCustomer { get; set; }

        



        public string Visibility { get; set; } = "Hidden";
        public Customer _selectedSuggest;


        public Customer SelectedSuggest
        {
            get
            {
                return _selectedSuggest;
            }
            set
            {
                Debug.WriteLine("SELECTED");
                _selectedSuggest = value;
                onPropertyChanged(nameof(SelectedSuggest));
                if (SelectedSuggest != null)
                {
                    //SelectedSuggest.Publisher = "1";

                    if (Customers.Count()==1)
                    {

                        MessageBox.Show("Item has been selected !!!");

                    }
                    else
                    {

                        Customers.Add(SelectedSuggest);
                    }

                }

               Visibility = "Hidden";


            }

        }


         private string _searching;
        public string Searching
        {
            get
            {
                return _searching;
            }
            set
            {
                _searching = value;
                onPropertyChanged(nameof(Searching));
                getSuggest();
                Visibility = "Visible";

            }
        }




        public FormBillViewModel(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            MouseDownCommand = new RelayCommand(mouseDownCommand, null);
            CloseCommand = new RelayCommand(closeCommand, null);
            SubmitCommand = new RelayCommand(submitCommand, null);
            AddCustomer = new RelayCommand(addCustomer, null);
            Customers = new ObservableCollection<Customer>();
            Date = DateTime.Now;

            initSuggest();
        }


        public FormBillViewModel(BaseViewModel viewModel, int IdBill)  // Edit constructor
        {

            _viewmodel = viewModel;
            MouseDownCommand = new RelayCommand(mouseDownCommand, null);
            CloseCommand = new RelayCommand(closeCommand, null);
            SubmitCommand = new RelayCommand(submitCommand, null);
            AddCustomer = new RelayCommand(addCustomer, null);

            // get from given bill
            Customers = new ObservableCollection<Customer>();
            Customers.Add(_customerDAO.findOne(IdBill));

            Date = _billDAO.getDate(IdBill);
            Money = _billDAO.getMoney(IdBill);

            initSuggest();
        }

        private void addCustomer(object x)
        {

            (_viewmodel as NavigateViewModel).saveBillState(this);
            (_viewmodel as NavigateViewModel).goToAddCustomer();
        }

        private void  getSuggest()
        {
            if (string.IsNullOrEmpty(Searching))
            {
                Suggest = AllCustomer;
            }

             Suggest = new ObservableCollection<Customer>();
            foreach(var str in AllCustomer)
            {
                               Debug.WriteLine("User " + AllCustomer);
                if (str.Name.ToLower().Contains(Searching.ToLower()))
                {
                    Suggest.Add(str);

                }


            }

        }

        private void closeCommand(object x)
        {
            Debug.WriteLine("CLOSE");
            Customers.Clear();
        }


        public void initSuggest()
        {

            AllCustomer = _customerDAO.getAll();
        }

        private void mouseDownCommand(object x)
        {
            Visibility = "Hidden";
        }


         private void submitCommand(object x)
        {

            if (Customers.Count() == 0)
            {
                MessageBox.Show("Please choose product");
                return;
            }


            DebitBook currentDebit = _debitBookDAO.getCurrentDebitBook(Customers[0].CustomerId);


            SettingModel settingModel = new SettingModel();

            settingModel.readData();
            


            if(currentDebit.CurrentDebit < this.Money )
            {
                if(settingModel.MaxDebtBool)
                {

                    MessageBox.Show("Money is greater than debt");
                    return;
                }
                else
                {

                    MessageBox.Show("Change " + (Money - currentDebit.CurrentDebit));
                    currentDebit.CurrentDebit = 0;
                }

            }
            else
            {


                currentDebit.CurrentDebit -= this.Money;
            }



            _debitBookDAO.updateOrInsertDebit(currentDebit);





            BookStoreContext db = new BookStoreContext();
            db.Bills.Add(new Bill()
            {
                Date = this.Date,
                Price = this.Money,
                CustomerId = Customers[0].CustomerId,
                
            }) ;
            db.SaveChanges();

           (_viewmodel as NavigateViewModel).deleteBillState();
            MessageBox.Show("Add Succeeded");

            (_viewmodel as NavigateViewModel).goToForm(_viewmodel);


        }


        

    }
}
