using GreenEye.DataAccess.DAO;
using GreenEye.DataAccess.Domain;
using GreenEye.Store;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Discount
{
    public class AddNewDiscountViewModel : BaseViewModel
    {
        public NavigateStore NavigateStore { get; set; }
        public Promotion Promotion { get; set; }
        public string Name { get => Promotion.Name;
            set
            {
                Promotion.Name = value;
            }
        }
        public decimal MinPrice { get => Promotion.MinPrice; set
            {
                Promotion.MinPrice = value;
            }
            }
        public int PercentDiscount { get => Promotion.PercentDiscount; set
            {
                Promotion.PercentDiscount = value;
            }
            }
        public int MaxDiscount { get => Promotion.MaxDiscount; set
            {
                Promotion.MaxDiscount = value;
            }
        }
        public DateTime StartDate { get => Promotion.StartDate; set
            {
                Promotion.StartDate = value;
            }
            }
        public DateTime EndDate
        {
            get => Promotion.EndDate;
            set
            {
                Promotion.EndDate = value;
            }
        }
        public RelayCommand NavigateSubmitCommand { get; set; }
        public RelayCommand NavigateCancelCommand { get; set; }
        public AddNewDiscountViewModel(NavigateStore navigateStore)
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            NavigateStore = navigateStore;
            Promotion = new Promotion();

            NavigateSubmitCommand = new RelayCommand(SubmitAdd, null);
            NavigateCancelCommand = new RelayCommand(Cancel, null);
        }

        private void Cancel(object obj)
        {
            NavigateStore.CurrentViewModel = new DiscountManagementViewModel(NavigateStore);
        }

        private void SubmitAdd(object obj)
        {
            PromotionDAO promotionDAO = new PromotionDAO();

            promotionDAO.insertOne(Promotion);

            NavigateStore.CurrentViewModel = new DiscountManagementViewModel(NavigateStore);
        }

        public AddNewDiscountViewModel(NavigateStore navigateStore, Promotion selectedPromotion)
        {
            NavigateStore = navigateStore;
            Promotion = selectedPromotion;

            NavigateSubmitCommand = new RelayCommand(SubmitEdit, null);
            NavigateCancelCommand = new RelayCommand(Cancel, null);
        }

        private void SubmitEdit(object obj)
        {
            PromotionDAO promotionDAO = new PromotionDAO();

            promotionDAO.updateOne(Promotion);

            NavigateStore.CurrentViewModel = new DiscountManagementViewModel(NavigateStore);
        }
    }
}
