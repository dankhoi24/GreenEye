using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using GreenEye.Store;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GreenEye.ViewModel
{
    public class FormBillManagementViewModel:BaseViewModel
    {
        //Command context menu
        public RelayCommand DeleteBillCommand { get; set; }
        public RelayCommand NavigateEditBillCommand { get; set; }
        public Bill SelectedBill { get; set; }
        //Phân trang thông số
        public int totalItem { get; set; } = 0;
        public int itemPerPage { get; set; } = 8;
        public int totalPage { get; set; } = 0;
        public int CurrentPage { get; set; }
        public ObservableCollection<Bill> BillPageList { get; set; }
        // Phaan trang command
        public RelayCommand NextCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand LastCommand { get; set; }
        //-------------------------
        public RelayCommand AddBillNavigateCommand { get; set; }
        public ObservableCollection<Bill> BillList { get; set; }
        public NavigateStore NavigateStore { get; set; }


        public void initPaging()
        {
            totalItem = BillList.Count();

            totalPage = totalItem / itemPerPage + (totalItem % itemPerPage == 0 ? 0 : 1);
            CurrentPage = 1;
            BillPageList = new ObservableCollection<Bill>(BillList
            .Skip((CurrentPage - 1) * itemPerPage)
            .Take(itemPerPage));
        }
        public FormBillManagementViewModel(NavigateStore navigateStore)
        {
            this.NavigateStore = navigateStore;
            BillDAO BillDAO = new BillDAO();

            BillList = BillDAO.getAll();

            initPaging();

            //AddBillNavigateCommand = new RelayCommand(AddBillNavigate, null);
            NextCommand = new RelayCommand(goNext, null);
            PreviousCommand = new RelayCommand(goPrev, null);
            FirstCommand = new RelayCommand(goFirst, null);
            LastCommand = new RelayCommand(goLast, null);

            DeleteBillCommand = new RelayCommand(deleteBill, null);
            //NavigateEditBillCommand = new RelayCommand(navigateEditBill, null);

        }

      /*  private void NavNavigateEditBill(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewBillViewModel(NavigateStore, NavSelectedBill);
        }*/

        private void deleteBill(object obj)
        {
            BillDAO BillDAO = new BillDAO();
            DebitBookDAO debitBookDAO = new DebitBookDAO();

            DebitBook debitBook = debitBookDAO.getCurrentDebitBook(SelectedBill.CustomerId);
            debitBookDAO.increaseCurrentDebit(SelectedBill.Customer, debitBook);

            BillDAO.deleteOne(SelectedBill);
            BillList.Remove(SelectedBill);


            if (BillPageList.Contains(SelectedBill))
                BillPageList.Remove(SelectedBill);
        }

      /*  private void AddBillNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewBillViewModel(NavigateStore);
        }

        private void navigateEditBill(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewBillViewModel(NavigateStore, SelectedBill);
        }
*/
        private void goLast(object obj)
        {
            Debug.WriteLine("-------------------------------Last-----------------------------------------");
            CurrentPage = totalPage;
            BillPageList = new ObservableCollection<Bill>(BillList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đé trang đầu
        private void goFirst(object obj)
        {
            Debug.WriteLine("-------------------------------First-----------------------------------------");
            Debug.WriteLine(totalPage);
            Debug.WriteLine(totalItem);
            Debug.WriteLine(CurrentPage);

            CurrentPage = 1;
            BillPageList = new ObservableCollection<Bill>(BillList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đến trang trước
        private void goPrev(object obj)
        {
            Debug.WriteLine("-------------------------------Prev-----------------------------------------");

            int temp = CurrentPage;
            if (temp == 1 || totalPage == 0)
            {
                return;
            }
            CurrentPage = (temp - 1);
            BillPageList = new ObservableCollection<Bill>(BillList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Prev-----------------------------------------");
        }
        //Đi đến trang sau
        private void goNext(object obj)
        {
            Debug.WriteLine("-------------------------------Next-----------------------------------------");

            int temp = CurrentPage;
            if (temp == totalPage || totalPage == 0)
            {
                return;
            }
            CurrentPage = (temp + 1);
            BillPageList = new ObservableCollection<Bill>(BillList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Next-----------------------------------------");
        }

    }
}
