using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class NavigateViewModel: BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; set; }


        public RelayCommand DashboardCommand { get; set; }
        public RelayCommand ProductCommand { get; set; }
        public RelayCommand ReportCommand{ get; set; }

        public NavigateViewModel()
        {
            CurrentViewModel = new DashboardViewModel();
            DashboardCommand = new RelayCommand(goToDashBoard, null);
            ProductCommand = new RelayCommand(goToProduct, null);
            ReportCommand = new RelayCommand(goToReport, null);
        }


        public void goToDashBoard(object x)
        {
            CurrentViewModel = new DashboardViewModel();
        }


        public void goToProduct(object x)
        {
            CurrentViewModel = new ProductListViewModel(this);
        }

         public void goToAddProduct(object x)
        {
            CurrentViewModel = new ProductAddViewModel(this);
        }

        public void goToReport(object x)
        {
            CurrentViewModel = new FormOptionViewModel(this);
        }
        public void goToInventoryReport(object x)
        {
            CurrentViewModel = new ReportInventoryViewModel();
        }

    }
}
