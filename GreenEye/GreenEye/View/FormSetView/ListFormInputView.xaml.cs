using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GreenEye.View.FormSetView
{
    /// <summary>
    /// Interaction logic for ListFormInputView.xaml
    /// </summary>
    public partial class ListFormInputView : UserControl
    {
        public ListFormInputView()
        {
            InitializeComponent();
        }

        private void onContextMenuOpend(object sender, RoutedEventArgs e)
        {
            (sender as ContextMenu).DataContext = ListGoodsReceiptWindow.DataContext;
        }
    }
}
