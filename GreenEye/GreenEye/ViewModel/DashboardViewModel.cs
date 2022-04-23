using GreenEye.DataAccess.DAO;
using GreenEye.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class DashboardViewModel: BaseViewModel
    {
        public int TotalProduct { get; set; }
        public BaseViewModel CurrentViewModel { get; }
        public NavigateStore NavigateStore { get; }

        private ProductDAO _productDAO = new ProductDAO();
        public DashboardViewModel()
        {
            TotalProduct = _productDAO.getCount();
        }

        public DashboardViewModel(BaseViewModel currentViewModel)
        {
            CurrentViewModel = currentViewModel;
        }

        public DashboardViewModel(NavigateStore navigateStore)
        {
            NavigateStore = navigateStore;
        }
    }
}
