using GreenEye.DataAccess.DAO;
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


        private ProductDAO _productDAO = new ProductDAO();
        public DashboardViewModel()
        {
            TotalProduct = _productDAO.getCount();
        }
    }
}
