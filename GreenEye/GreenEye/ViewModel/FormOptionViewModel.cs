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
        public RelayCommand BillCommand { get; set; }

        public FormOptionViewModel(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            InventoryCommand = new RelayCommand(goToInventory, null);

            BillCommand = new RelayCommand(goToBill, null);

        }

        private void goToInventory(object x)
        {
            (_viewmodel as NavigateViewModel).goToInventoryReport(x);
        }
        private void goToBill(object x)
        {
            (_viewmodel as NavigateViewModel).goToBillReport(x);
        }


        
    }
}
