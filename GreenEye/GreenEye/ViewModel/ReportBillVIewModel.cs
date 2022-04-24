using GreenEye.DataAccess.DAO;
using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class ReportBillVIewModel: BaseViewModel
    {

        DebitBookDAO _inventoryDAO = new DebitBookDAO();


        private DateTime _date;
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

                Reports = new ObservableCollection<ReportBill>(_inventoryDAO.getDate(Date));
            }
        }
        public ObservableCollection<ReportBill> Reports { get; set; }

        public ReportBillVIewModel()
        {

            Date = DateTime.Now;
            Reports = new ObservableCollection<ReportBill>(_inventoryDAO.getDate(Date));
        }
    }
}
