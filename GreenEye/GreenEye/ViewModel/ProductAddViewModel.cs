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
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime CreateDate { get; set; }
        public Decimal ImportPrice { get; set; }
        public Decimal ExportPrice { get; set; }
        public string UrlImage { get; set; }


        public string BeforeImport { get; set; }
        public string AfterImport { get; set; }

        public RelayCommand ImportCommand { get; set; }
        //Constructor
        public ProductAddViewModel()
        {
            BeforeImport = "Visible";
            AfterImport = "Hidden";
        }

        //method
        public void importCommand()
        {

        }

         private void addFile(object parameter)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = "Excel files (*.xlsx)|*.xlsx|All files(*.*)|*.*";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if(openFile.ShowDialog() == true)
            {
                FileName = Path.GetFileName(openFile.FileName);
                Debug.WriteLine(openFile.FileName);
                _folderName = Path.GetDirectoryName(openFile.FileName);
                Debug.WriteLine(Path.GetDirectoryName(openFile.FileName));
                Debug.WriteLine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName);

                /*
                 * MyShop\MyShop
                 */
                getProductData(openFile.FileName);
            }
          
        }



        
        
    }
}
