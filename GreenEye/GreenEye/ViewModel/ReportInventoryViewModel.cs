using GreenEye.DataAccess;
using GreenEye.DataAccess.DAO;
using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class ReportInventoryViewModel: BaseViewModel
    {
        private DateTime _date;
        InventoryDAO _inventoryDAO = new InventoryDAO();
        public ObservableCollection<ReportInventory> Reports { get; set; }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                onPropertyChanged(nameof(Date));
                Debug.WriteLine(Date.AddDays(-35));
                Debug.WriteLine("000000000000000000");
            }
        }

        public ReportInventoryViewModel()
        {
            Date = DateTime.Now;
            Reports = new ObservableCollection<ReportInventory>(_inventoryDAO.getDate(DateTime.Now));
           


        }


       

    }
}
