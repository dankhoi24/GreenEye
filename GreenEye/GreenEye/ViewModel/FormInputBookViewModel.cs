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
using System.Windows;

namespace GreenEye.ViewModel
{
    public class FormInputBookViewModel:BaseViewModel
    {

        private BaseViewModel _viewmodel { get; set; }
        public ObservableCollection<Book> Suggest { get; set; }
        public ObservableCollection<Book> AllBooks { get; set; }
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

                    if (Products.Contains(SelectedSuggest))
                    {
                        
                        MessageBox.Show("This item has been selected !!!");
                        
                    }
                    else
                    {

                        Products.Add(SelectedSuggest);
                    }

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
                getSuggest();
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
            AllBooks = new ObservableCollection<Book>(_productDAO.getAll());
            
          
        }


        // method 
        public void  getSuggest()
        {

             Suggest = new ObservableCollection<Book>();
            foreach(var str in AllBooks)
            {
                               Debug.WriteLine("User " + AllBooks);
                if (str.Name.ToLower().Contains(Searching.ToLower()))
                {
                    Suggest.Add(str);

                }


            }

        }

    }
}
