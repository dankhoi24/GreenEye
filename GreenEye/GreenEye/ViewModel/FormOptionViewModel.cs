using GreenEye.DataAccess;
using GreenEye.DataAccess.Domain;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class FormOptionViewModel: BaseViewModel
    {

        public BaseViewModel _viewmodel { get; set; }
        public RelayCommand InventoryCommand { get; set; }

        public FormOptionViewModel(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            InventoryCommand = new RelayCommand(goToInventory, null);

            Inventory temp = new Inventory()
            {
                Amount = 20,
                BookId = 1,
                Date = DateTime.Parse("2/28/2022"),
            };
            BookStoreContext x = new BookStoreContext();
            x.Inventories.Add(temp);
            x.SaveChanges();
            

        }

        private void goToInventory(object x)
        {
            (_viewmodel as NavigateViewModel).goToInventoryReport(x);
        }

        
    }
}
