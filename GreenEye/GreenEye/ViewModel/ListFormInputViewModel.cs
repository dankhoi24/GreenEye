using GreenEye.DataAccess.DAO;
using GreenEye.Model;
using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{

    public class ListFormInputViewModel:BaseViewModel

    {

        private BaseViewModel _viewmodel { get; set;}
        public ObservableCollection<GoodsReceiptModel> GoodsReceiptList { get; set; }
        public RelayCommand AddGoods { get; set; }
        public RelayCommand DeleteContext { get; set; }
        public RelayCommand EditContext { get; set; }


        public GoodsReceiptModel SelectedItem { get; set; }

        public DateTime Date { get; set; }

        private GoodsReceiptDAO _goodsReceiptDAO = new GoodsReceiptDAO();

        public ListFormInputViewModel(BaseViewModel viewModel)
        {

            _viewmodel = viewModel;
                 
            GoodsReceiptList = new ObservableCollection<GoodsReceiptModel>(_goodsReceiptDAO.getAll());

            AddGoods = new RelayCommand(addGoods, null);
            DeleteContext = new RelayCommand(deleteContext, null);
            EditContext = new RelayCommand(editContext, null);

        }

        private void addGoods(object x)
        {
            (_viewmodel as NavigateViewModel).goToInputForm(_viewmodel);
        }
        private void deleteContext(object x)
        {
            _goodsReceiptDAO.deleteItem(SelectedItem.Id);
            GoodsReceiptList = new ObservableCollection<GoodsReceiptModel>(_goodsReceiptDAO.getAll());

        }

        private void editContext(object x)
        {
            (_viewmodel as NavigateViewModel).goToEditForm(SelectedItem.Id);
        }

    }
}
