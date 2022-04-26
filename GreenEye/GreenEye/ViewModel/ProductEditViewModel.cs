using GreenEye.DataAccess;
using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using GreenEye.ViewModel.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GreenEye.ViewModel
{
    public class ProductEditViewModel: BaseViewModel
    {
        private BaseViewModel _viewmodel { get; set; }


        public string BeforeImport { get; set; }
        public string AfterImport { get; set; }


        //Commnad
        public RelayCommand CloseImageCommand { get; set; }
        public RelayCommand ImportCommand { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }


        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookPublisher { get; set; }
        public DateTime BookDate { get; set; }
        public decimal BookInputPrice { get; set; }
        public decimal BookOutputPrice { get; set; }
        public int BookAmount { get; set; }

        public string Filename { get; set; }

        public BookTypeDAO _bookTypeDAO = new BookTypeDAO();
        public ObservableCollection<string> BookTypes { get; set; }
        public string ItemSelected { get; set; }


        private Book Book { get; set; }

        public ProductEditViewModel(BaseViewModel viewmodel)
        {

            _viewmodel = viewmodel;

            BeforeImport = "Hidden";
            AfterImport = "Visible";

            BookTypes = new ObservableCollection<string>(_bookTypeDAO.getAll());


            CloseImageCommand = new RelayCommand(closeImageCommand, null);
            ImportCommand = new RelayCommand(importCommand, null);
            SubmitCommand = new RelayCommand(submitCommand, null);
            CancelCommand = new RelayCommand(cancelCommand, null);




            string name = (_viewmodel as ProductListViewModel).SelectedProduct.Name;
            Debug.WriteLine("BOOK INSTANCE " + name);
            Book = (_viewmodel as ProductListViewModel).SelectedProduct;
            Debug.WriteLine(@"..\.." + Book.Img);

            initBook();

        }


        //Method

        private void cancelCommand(object x)
        {
            (_viewmodel as ProductListViewModel).goToProduct(x);
        }


          private void submitCommand(object x)
        {

            if(string.IsNullOrEmpty(BookName) || string.IsNullOrEmpty(BookAuthor) || string.IsNullOrEmpty(BookPublisher) || string.IsNullOrEmpty(ItemSelected))
            {
                
                MessageBox.Show("Invalid input!!!");
                return;
            }

             Debug.WriteLine(BookInputPrice + " | " + BookOutputPrice + " | " + BookAmount);
            if(BookInputPrice == -1 || BookOutputPrice == -1 || BookAmount == -1)
            {
                MessageBox.Show("Invalid input!!!");
                return;
            }

            int id = _bookTypeDAO.getId(ItemSelected);
           
            Book temp = new Book()
            {
                Name = BookName,
                Author = BookAuthor,
                Publisher = BookPublisher,
                Date = BookDate,
                ImportPrice = BookInputPrice,
                ExportPrice = BookOutputPrice,
                Stroke = BookAmount,
                BookTypeId = id,
                Img = string.IsNullOrEmpty(Filename)? @"\img\store\meow.png":  Filename
            };

            BookStoreContext db = new BookStoreContext();
            db.Books.Add(temp);
            db.SaveChanges();

            MessageBox.Show("Add new book succeeded");

            if(_viewmodel == null)
            {
                Debug.WriteLine("NULL");

            }
            else
            {
                Debug.WriteLine("KOKO");
            }
            (_viewmodel as ProductListViewModel).goToProduct(x);


        }
         private void importCommand(object parameter)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = "Excel files (*.xlsx)|*.xlsx|All files(*.*)|*.*";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            


            if(openFile.ShowDialog() == true)
            {

                movingImg(openFile.FileName);
                BeforeImport = "Hidden";
                AfterImport = "Visible";
                 Debug.WriteLine("((((((((((((((((");
                Debug.WriteLine(Filename);

                Filename ="../../img/store/"+ Path.GetFileName(openFile.FileName);

               
            }

            
          
        }

          private void movingImg(string filename)
        {
                string destinationFolder = $@"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\GreenEye\img\store";
                string fileName = Path.GetFileName(filename);
                string fileToMove = filename;
                string moveTo = destinationFolder+ @"\" + fileName;
            Debug.WriteLine("============");
                Debug.WriteLine(fileToMove);
                Debug.WriteLine(moveTo);

                File.Move(fileToMove, moveTo);
        }




         private void closeImageCommand(object x)
        {
            BeforeImport = "Visible";
            AfterImport = "Hidden";
            Filename = "";

        }


        private void initBook()
        {
            BookName = Book.Name;
            BookAuthor = Book.Author;
            BookAmount = Book.Stroke;
            BookPublisher = Book.Publisher;
            BookInputPrice = Book.ImportPrice;
            BookOutputPrice = Book.ExportPrice;
            BookDate = Book.Date;
            Filename = @"../../"+Book.Img;
            ItemSelected = _bookTypeDAO.getName(Book.BookTypeId);

        }

        


    }
}
