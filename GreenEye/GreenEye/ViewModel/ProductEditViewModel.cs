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

            BookStoreContext db = new BookStoreContext();
            var bookUpdate = db.Books.SingleOrDefault(b => b.BookId == Book.BookId);



            bookUpdate.Name = BookName;
            bookUpdate.Author = BookAuthor;
            bookUpdate.Publisher = BookPublisher;
            bookUpdate.Date = BookDate;
            bookUpdate.ImportPrice = BookInputPrice;
            bookUpdate.ExportPrice = BookOutputPrice;
            bookUpdate.Stroke = BookAmount;
            bookUpdate.BookTypeId = id;
            bookUpdate.Img = string.IsNullOrEmpty(Filename) ? @"meow.png" : Filename;

            db.SaveChanges();

            MessageBox.Show("Edited book succeeded");

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

                Filename = Path.GetFileName(openFile.FileName);
                Debug.WriteLine(openFile.FileName);

                //movingImg(Filename);
            }

            

            
          
        }

          private void movingImg(string filePath)
        {

            string filename =  Path.GetFileName(filePath);


            string currentPath = Environment.CurrentDirectory;

            string rootDestination = Directory.GetParent(currentPath).Parent.FullName + @"\img\store";
            string binDestination = currentPath + @"\img\store";

            Debug.WriteLine(filename);
            Debug.WriteLine(rootDestination);
            Debug.WriteLine(binDestination);

            if (!File.Exists(binDestination + @"\" + filename))
            {

                File.Copy(filePath, binDestination + @"\"+filename);
            }



            if (Directory.Exists(rootDestination))
            {

                if(!File.Exists(rootDestination + @"\" + filename))
                {

                    File.Copy(filePath, rootDestination   + @"\"+filename);
                }
            }
            else
            {
                Debug.WriteLine("NONONO");
            }

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
            Filename = Book.Img;
            ItemSelected = _bookTypeDAO.getName(Book.BookTypeId);

        }

        


    }
}
