using GreenEye.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Discount
{
    using DataAccess.Domain;
    using GreenEye.DataAccess.DAO;
    using GreenEye.ViewModel.Command;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    public class DiscountManagementViewModel : BaseViewModel
    {
        //Command context menu
        public RelayCommand DeletePromotionCommand { get; set; }
        public RelayCommand NavigateEditPromotionCommand { get; set; }
        public Promotion SelectedPromotion { get; set; }
        //Phân trang thông số
        public int totalItem { get; set; } = 0;
        public int itemPerPage { get; set; } = 4;
        public int totalPage { get; set; } = 0;
        public int CurrentPage { get; set; }
        public ObservableCollection<Promotion> PromotionPageList { get; set; }
        // Phaan trang command
        public RelayCommand NextCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand LastCommand { get; set; }
        //-------------------------
        public BaseViewModel CurrentViewModel { get; set; }
        public RelayCommand AddPromotionNavigateCommand { get; set; }
        public ObservableCollection<Promotion> PromotionList { get; set; }

        public NavigateStore NavigateStore { get; set; }

        public void initPaging()
        {
            totalItem = PromotionList.Count();

            totalPage = totalItem / itemPerPage + (totalItem % itemPerPage == 0 ? 0 : 1);
            CurrentPage = 1;
            PromotionPageList = new ObservableCollection<Promotion>(PromotionList
            .Skip((CurrentPage - 1) * itemPerPage)
            .Take(itemPerPage));
        }

        public DiscountManagementViewModel(NavigateStore navigateStore)
        {
            this.NavigateStore = navigateStore;
            PromotionDAO PromotionDAO = new PromotionDAO();

            PromotionList = PromotionDAO.getAll();

            initPaging();

            AddPromotionNavigateCommand = new RelayCommand(AddPromotionNavigate, null);
            NextCommand = new RelayCommand(goNext, null);
            PreviousCommand = new RelayCommand(goPrev, null);
            FirstCommand = new RelayCommand(goFirst, null);
            LastCommand = new RelayCommand(goLast, null);

            DeletePromotionCommand = new RelayCommand(deletePromotion, null);
            NavigateEditPromotionCommand = new RelayCommand(navigateEditPromotion, null);
        }

        private void navigateEditPromotion(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewDiscountViewModel(NavigateStore, SelectedPromotion);
        }

        private void deletePromotion(object obj)
        {
            PromotionDAO PromotionDAO = new PromotionDAO();
            PromotionDAO.deleteOne(SelectedPromotion);
            PromotionList.Remove(SelectedPromotion);
            if (PromotionPageList.Contains(SelectedPromotion))
                PromotionPageList.Remove(SelectedPromotion);


        }

        private void goLast(object obj)
        {
            Debug.WriteLine("-------------------------------Last-----------------------------------------");
            CurrentPage = totalPage;
            PromotionPageList = new ObservableCollection<Promotion>(PromotionList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đé trang đầu
        private void goFirst(object obj)
        {
            Debug.WriteLine("-------------------------------First-----------------------------------------");
            Debug.WriteLine(totalPage);
            Debug.WriteLine(totalItem);
            Debug.WriteLine(CurrentPage);

            CurrentPage = 1;
            PromotionPageList = new ObservableCollection<Promotion>(PromotionList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
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
            PromotionPageList = new ObservableCollection<Promotion>(PromotionList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

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
            PromotionPageList = new ObservableCollection<Promotion>(PromotionList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Next-----------------------------------------");
        }

        private void AddPromotionNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewDiscountViewModel(NavigateStore);
        }
    }
}
