using GreenEye.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GreenEye.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {
        private string _minGoodsReceipt;
        private string _minStoreImport;
        private string _maxdebt;
        private string _minStoreSell;
        private bool _maxDebtBool;


        private SettingModel _settingModel { get; set; } = new SettingModel();

        public SettingViewModel()
        {
            _settingModel.readData();
            MinGoodsReceipt = _settingModel.MinGoodsReceipt;
            MinStoreImport = _settingModel.MinStoreImport;
            Maxdebt = _settingModel.Maxdebt;
            MinStoreSell = _settingModel.MinStoreSell;
            MaxDebtBool = _settingModel.MaxDebtBool;
        
        }


        private bool isNumber(string data)
        {
            if (int.TryParse(data, out int value) && !string.IsNullOrEmpty(data)) 
            {

                return true;
            }

            return false;

        }




        public string MinGoodsReceipt
        {
            get
            {
                return _minGoodsReceipt;
            }
            set
            {
                if (!isNumber(value))
                {
                    MessageBox.Show("Invalid Value");
                    return;
                }
                _minGoodsReceipt = value;
                onPropertyChanged(nameof(MinGoodsReceipt));
                _settingModel.writeData(MinGoodsReceipt, MinStoreImport, Maxdebt, MinStoreSell, MaxDebtBool);

            }
        }
        public string MinStoreImport
        {
            get
            {
                return _minStoreImport;
            }
            set
            {if (!isNumber(value))
                {
                    MessageBox.Show("Invalid Value");
                    return;
                }

                _minStoreImport = value;
                onPropertyChanged(nameof(MinStoreImport));
                _settingModel.writeData(MinGoodsReceipt, MinStoreImport, Maxdebt, MinStoreSell, MaxDebtBool);
            }
        }
        public string Maxdebt
        {
            get
            {
                return _maxdebt;
            }
            set
            {if (!isNumber(value))
                {
                    MessageBox.Show("Invalid Value");
                    return;
                }

                _maxdebt = value;

                onPropertyChanged(nameof(Maxdebt));
                _settingModel.writeData(MinGoodsReceipt, MinStoreImport, Maxdebt, MinStoreSell, MaxDebtBool);
            }
        }
        public string MinStoreSell
        {
            get
            {
                return _minStoreSell;
            }
            set
            {if (!isNumber(value))
                {
                    MessageBox.Show("Invalid Value");
                    return;
                }

                _minStoreSell = value;
                onPropertyChanged(nameof(MinStoreSell));
                _settingModel.writeData(MinGoodsReceipt, MinStoreImport, Maxdebt, MinStoreSell, MaxDebtBool);
            }
        }
        public bool MaxDebtBool
        {
            get
            {
                return _maxDebtBool;
            }
            set
            {
                _maxDebtBool = value;

                onPropertyChanged(nameof(MaxDebtBool));
                _settingModel.writeData(MinGoodsReceipt, MinStoreImport, Maxdebt, MinStoreSell, MaxDebtBool);
            }
        }


    }
}
