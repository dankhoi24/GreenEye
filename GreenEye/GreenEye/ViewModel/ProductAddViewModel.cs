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
    public class ProductAddViewModel:BaseViewModel
    {
    

        public string BeforeImport { get; set; }
        public string AfterImport { get; set; }


        public ObservableCollection<string> BookTypes { get; set; }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand ViewProductCommand { get; set; }


        public RelayCommand EditCommand { get; set; }




        public string Filename{ get; set; }
        private string _filename { get; set; }

        private BaseViewModel _viewmodel { get; set; }

        public RelayCommand ImportCommand { get; set; }
        public string ItemSelected { get; set; }


        private BookTypeDAO _bookTypeDAO = new BookTypeDAO();
        //Constructor
        public ProductAddViewModel(BaseViewModel viewModel)
        {
            BeforeImport = "Visible";
            AfterImport = "Hidden";
            SubmitCommand = new RelayCommand(submitCommand, null);
            CancelCommand = new RelayCommand(cancelCommand, null);
            ImportCommand = new RelayCommand(importCommand, null);
            ViewProductCommand = new RelayCommand(viewProductCommand, null);
            EditCommand = new RelayCommand(editCommand,null);
            Filename = "../../img/store/aka.png";

            _viewmodel = viewModel as NavigateViewModel;
            BookTypes = new ObservableCollection<string>(_bookTypeDAO.getAll());
        }

        //method

        private void editCommand(object x)
        {
            Debug.WriteLine("EDIT");

            MessageBox.Show("jyfiuf");    

        }

        public void viewProductCommand(object x)
        {
            Debug.WriteLine("VIEWWW");
        }
      
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookPublisher { get; set; }
        public DateTime BookDate { get; set; }
        public decimal BookInputPrice { get; set; }
        public decimal BookOutputPrice { get; set; }
        public int BookAmount { get; set; }
        


        private void cancelCommand(object x)
        {
            (_viewmodel as NavigateViewModel).goToProduct(x);
        }
        private void submitCommand(object x)
        {
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
                Img = @"\img\store\"+ _filename
            };

            BookStoreContext db = new BookStoreContext();
            db.Books.Add(temp);
            db.SaveChanges();


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
                _filename =  Path.GetFileName(openFile.FileName);

               
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




        
        
    }
}
