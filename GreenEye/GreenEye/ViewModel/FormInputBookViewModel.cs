using GreenEye.DataAccess;
using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class FormInputBookViewModel:BaseViewModel
    {

        private BaseViewModel _viewmodel { get; set; }
        public ObservableCollection<Book> Suggest { get; set; }
        public ObservableCollection<Book> Products { get; set; }

        public Book _selectedSuggest;
        public Book SelectedSuggest
        {
            get
            {
                return _selectedSuggest; 
            }
            set
            {
                _selectedSuggest = value;
                onPropertyChanged(nameof(SelectedSuggest));
                if (SelectedSuggest != null)
                {
                    SelectedSuggest.Publisher = "1";

                    Products.Add(SelectedSuggest);
                }

                Visibility = "Hidden";


                foreach(var x in Products)
                {
                    Debug.WriteLine(x.Name + " " + x.Publisher);
                }
            }

        }
        public string Visibility { get; set; }



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
                Visibility = "Visible";

            }
        }

        ProductDAO _productDAO = new ProductDAO();

        public FormInputBookViewModel(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            Suggest = new ObservableCollection<Book>(_productDAO.getAll());
            Visibility = "Hidden";

            Products = new ObservableCollection<Book>();
                 
            
          
        }

    }
}
