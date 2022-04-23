using GreenEye.DataAccess;
using GreenEye.DataAccess.Domain;
using GreenEye.ViewModel.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class ProductAddViewModel:BaseViewModel
    {
    

        public string BeforeImport { get; set; }
        public string AfterImport { get; set; }



        public string Filename{ get; set; }

        private BaseViewModel _viewmodel { get; set; }




        public RelayCommand ImportCommand { get; set; }
        //Constructor
        public ProductAddViewModel(BaseViewModel viewModel)
        {
            BeforeImport = "Visible";
            AfterImport = "Hidden";
            ImportCommand = new RelayCommand(importCommand, null);
            Filename = "../../img/store/aka.png";

            _viewmodel = viewModel as NavigateViewModel;
        }

        //method
      
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public string BookPublisher { get; set; }
        public DateTime BookDate { get; set; }
        public string BookInputPrice { get; set; }
        public string BookOutputPrice { get; set; }
        public int BookAmount { get; set; }
        
        private void submitCommand()
        {
            Book temp = new Book()
            {
                Name = BookName,
                Author = BookAuthor,
                Publisher = BookPublisher,
                Date = BookDate,
                ImportPrice = decimal.Parse( BookInputPrice),
                ExportPrice = decimal.Parse( BookOutputPrice),
                Stroke = BookAmount,
            };


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




        
        
    }
}
