using GreenEye.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel
{
    public class FormViewmodel: BaseViewModel
    {

        public RelayCommand GoodsCommand { get; set; }
        private BaseViewModel _viewmodel { get; set; }

        public FormViewmodel(BaseViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            GoodsCommand = new RelayCommand(goodsCommand, null);


        }

        void goodsCommand(object x)
        {
            (_viewmodel as NavigateViewModel).goTListFrmProduct();
        }

    }
}
