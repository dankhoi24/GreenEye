using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
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
    public class ProductListViewModel: BaseViewModel
    {

        private ProductDAO _prodcutDAO = new ProductDAO();

        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Book> PrepersentPage { get; set; }
        public ObservableCollection<string> Category { get; set; }

        public ObservableCollection<string> SearchSuggest { get; set; }
        public ObservableCollection<string> AllName { get; set; }

        public Book SelectedProduct { get; set; }


        private BaseViewModel _viewmodel { get; set; }

        private string _categorySelected;

        public string CategorySelected
        {
            get
            {
                return _categorySelected;
            }
            set
            {
                _categorySelected = value;
                onPropertyChanged(nameof(CategorySelected));
                setCategory();

            }
        }


        

        public string _itemSelected;
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
                if (!string.IsNullOrEmpty(_itemSelected))
                {
                    Searching = _itemSelected;
                }
                SearchVisibility = "Hidden";
            }
        }
        public string SearchVisibility { get; set; }


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
                getSearchSuggest();

                SearchVisibility = "Visible";
            }

        }

        public string test { get; set; } = "hello";
        
        
        //Command
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand NextCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand LastCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

         int totalItem = 0;
        int itemPerPage = 3;
        public int TotalPage { get; set; } = 0;
        public string CurrentPage { get; set; } = "1";


        private BookTypeDAO _bookTypeDAO = new BookTypeDAO();

        // Constructor


        public ProductListViewModel(BaseViewModel viewModel)
        {
            Debug.WriteLine("START LISTING BOOK");
            Books = new ObservableCollection<Book>(_prodcutDAO.getAll());
            initPaging();
            PreviousCommand = new RelayCommand(previousCommand, null);
            NextCommand = new RelayCommand(nextCommand, null);
            FirstCommand = new RelayCommand(firstCommand, null);
            LastCommand = new RelayCommand(lastCommand, null);
            SearchCommand = new RelayCommand(seachCommand, null);
            AddCommand = new RelayCommand(addCommand, null);
            EditCommand = new RelayCommand(editCommand,null);

            Category = new ObservableCollection<string>(_bookTypeDAO.getAll());
            AllName = new ObservableCollection<string>(_prodcutDAO.getName());

            SearchVisibility = "Hidden";

            _viewmodel = viewModel as NavigateViewModel;

 
        }


        //method
          private void editCommand(object x)
        {


            (_viewmodel as NavigateViewModel).goToEditProduct(this);
        }

        public void goToProduct(object x) {
            (_viewmodel as NavigateViewModel).goToProduct(x);
        }

     
        private void addCommand(object x)
        {
            (_viewmodel as NavigateViewModel).goToAddProduct(x);
        }

        private void setCategory()
        {

              if (string.IsNullOrEmpty(Searching))
            {
                Books =  new ObservableCollection<Book>(_prodcutDAO.getAll());
            }
            else
            {

                Books = new ObservableCollection<Book>(_prodcutDAO.getContain(Searching));
            }
            List<string> name = _prodcutDAO.getCategory(CategorySelected);
            ObservableCollection<Book> result = new ObservableCollection<Book>();

            foreach(var book in Books)
            {
                if (name.Contains(book.Name))
                {
                    result.Add(book);
                }
            }

            Books = result;
            initPaging();
        }

        private void seachCommand(object x)
        {
            if (string.IsNullOrEmpty(Searching))
            {
                Books =  new ObservableCollection<Book>(_prodcutDAO.getAll());
            }
            else
            {

                Books = new ObservableCollection<Book>(_prodcutDAO.getContain(Searching));
            }
            initPaging();

        }


          private void getSearchSuggest()
        {

            SearchSuggest = new ObservableCollection<string>();
            foreach(var str in AllName)
            {
                Debug.WriteLine("User " + AllName);
                if (str.ToLower().Contains(Searching.ToLower()))
                {
                    SearchSuggest.Add(str);

                }

            }
            if (SearchSuggest.Count() == 0)
            {
                SearchSuggest.Add("No result");
            }

                
        }

         private void firstCommand(object x)
        {

            int temp = Int32.Parse(CurrentPage);
            if (temp == 1 || TotalPage==0)
            {
                return;
            }
            CurrentPage = "1";
            PrepersentPage = new ObservableCollection<Book>(Books
                             .Skip((Int32.Parse(CurrentPage) - 1) * itemPerPage)
                             .Take(itemPerPage));

        }

        private void lastCommand(object x)
        {

            int temp = Int32.Parse(CurrentPage);
            if (temp == 1 || TotalPage==0)
            {
                return;
            }
            Debug.WriteLine(TotalPage);
            Debug.WriteLine("LAST");
            CurrentPage = TotalPage.ToString();
            PrepersentPage = new ObservableCollection<Book>(Books
                             .Skip((Int32.Parse(CurrentPage) - 1) * itemPerPage)
                             .Take(itemPerPage));

        }






         private void previousCommand(object x)
        {

            int temp = Int32.Parse(CurrentPage);
            if (temp == 1 || TotalPage==0)
            {
                return;
            }
            CurrentPage = (temp - 1).ToString();
            PrepersentPage = new ObservableCollection<Book>(Books
                             .Skip((Int32.Parse(CurrentPage) - 1) * itemPerPage)
                             .Take(itemPerPage));

        }


         private void nextCommand(object x)
        {
            

            int temp = Int32.Parse(CurrentPage);
            if (temp == TotalPage || TotalPage==0)
            {
                return;
            }
            CurrentPage = (temp + 1).ToString();
            PrepersentPage = new ObservableCollection<Book>(Books
                             .Skip((Int32.Parse(CurrentPage) - 1) * itemPerPage)
                             .Take(itemPerPage));
        }





         public void initPaging()
        {

            if (Books == null)
            {
                return;
            }
            totalItem = Books.Count();
            TotalPage = totalItem / itemPerPage + (totalItem % itemPerPage == 0 ? 0 : 1);


            CurrentPage = "1";

            PrepersentPage = new ObservableCollection<Book>(Books
            .Skip((Int32.Parse(CurrentPage) - 1) * itemPerPage)
            .Take(itemPerPage));

            foreach(var x in Books)
            {
                Debug.WriteLine(x.Img);
            }





            Debug.WriteLine(TotalPage);
            Debug.WriteLine(totalItem);
        }

    }
}
