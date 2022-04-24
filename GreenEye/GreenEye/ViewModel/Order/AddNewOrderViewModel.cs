using GreenEye.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Order
{
    public class AddNewOrderViewModel : BaseViewModel
    {
        public AddNewOrderViewModel(NavigateStore navigateStore)
        {
            NavigateStore = navigateStore;
        }

        public AddNewOrderViewModel(NavigateStore navigateStore, DataAccess.Domain.Order selectedOrder)
        {
            NavigateStore = navigateStore;
            SelectedOrder = selectedOrder;
        }

        public NavigateStore NavigateStore { get; }
        public DataAccess.Domain.Order SelectedOrder { get; }
    }
}
